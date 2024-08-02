using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    AudioSource aud;

    GameObject endScreen;
    GameObject gradeDisplay;
    GameObject failMessage;
    GameObject transition;

    public Sprite GradeA;
    public Sprite GradeB;
    public Sprite GradeC;
    public Sprite GradeD;

    public int level_day = 1;

    FadeInOut fade;

    public Text Timer;
    public Text Stats;

    public float start_time;
    float time_left;
    bool timer_on = false;
    bool finished = false;
    string time_readable;
    string this_level;

    bool eat_clicked = false;
    bool drink_clicked = false;
    bool brush_clicked = false;

    int total_penalty = 0;
    int total_eat = 0;
    int total_drink = 0;
    int total_brush = 0;
    float score_decimal;

    int total_cats = 0;

    private int cat_id = 0;

    public AudioClip click_sound;

    void Start()
    {
        time_left = start_time;
        Application.targetFrameRate = 24;
        timer_on = true;
        aud = GetComponent<AudioSource>();
        aud.pitch = 2;
        endScreen = GameObject.FindGameObjectWithTag("End Screen");
        gradeDisplay = GameObject.FindGameObjectWithTag("Grade Display");
        failMessage = GameObject.FindGameObjectWithTag("Fail Message");
        
        endScreen.SetActive(false);
        failMessage.SetActive(false);
        

        fade = FindObjectOfType<FadeInOut>();
        fade.FadeOut();

        this_level = SceneManager.GetActiveScene().name;
        

    }
    void Update()
    {
        
        /*if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }*/
        
        if (timer_on)
        {
            if(time_left > 0)
            {
                time_left -= Time.deltaTime;
                updateTimer(time_left);
            }
            else
            {
                finished = true;
                timer_on = false;
            }
        }
        if (finished)
        {

            if(total_cats > 0)
            {
                score_decimal = 1 - (total_penalty / (total_cats * start_time));
                score_decimal = (float)((int)(score_decimal * 1000))/1000.0f;
            }
            
            Stats.text = 
                "Fed " + total_eat + " times\n" +
                "Gave water " + total_drink + " times\n" +
                "Brushed " + total_brush + " times\n" +
                "Cats unhappy for " + total_penalty + " seconds\n" +
                "Score: " + score_decimal*100;

            endScreen.SetActive(true);
            if (score_decimal >= 0.9)
            {
                setGrade(GradeA);
            }
            else if(score_decimal >= 0.8)
            {
                setGrade(GradeB);
            }
            else if (score_decimal >= 0.7)
            {
                setGrade(GradeC);
            }
            else
            {
                setGrade(GradeD);
            }
            if(PlayerPrefs.GetFloat("Score Day" + level_day) < score_decimal)
            {
                PlayerPrefs.SetFloat("Score Day" + level_day, score_decimal);
            }

        }
        else
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if(cat_id > 1)
                {
                    setCatID(cat_id - 1);
                }
                else
                {
                    setCatID(total_cats);
                }
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                if(cat_id < total_cats)
                {
                    setCatID(cat_id + 1);
                }
                else
                {
                    setCatID(1);
                }
            }
        }
    }

    void updateTimer(float current_time)
    {
        current_time += 1;

        float minutes = Mathf.FloorToInt(current_time / 60);
        float seconds = Mathf.FloorToInt(current_time % 60);

        time_readable = minutes + ":" + seconds;
        if(seconds == 0)
        {
            time_readable += "0";
        }
        else if(seconds < 10)
        {
            time_readable = minutes + ":0" + seconds;
        }

        Timer.text = time_readable;

    }
    public void catPenalty(int eat, int drink, int brush, int penalty)
    {
        //total_cats++;
        total_eat += eat;
        total_drink += drink;
        total_brush += brush;
        total_penalty += penalty;        
    }

    public void addCat()
    {
        total_cats++;
    }

    public bool isFinished()
    {
        return finished;
    }

    public void eatBool(bool click)
    {
        if (click)
        {
            aud.PlayOneShot(click_sound);
        }
        eat_clicked = click;
    }
    public void drinkBool(bool click)
    {
        if (click)
        {
            aud.PlayOneShot(click_sound);
        }
        drink_clicked = click;
    }
    public void brushBool(bool click)
    {
        if (click)
        {
            aud.PlayOneShot(click_sound);
        }
        brush_clicked = click;
    }
    public bool eatIsClicked()
    {
        return eat_clicked;
    }
    public bool drinkIsClicked()
    {
        return drink_clicked;
    }
    public bool brushIsClicked()
    {
        return brush_clicked;
    }

    public void setCatID(int id)
    {
        aud.PlayOneShot(click_sound);
        cat_id = id;
        Debug.Log("cat_id is now " + id);
    }
    public int getCatID()
    {
        return cat_id;
    }

    void setGrade(Sprite grade)
    {
        gradeDisplay.GetComponent<Image>().sprite = grade;
    }

    public float getTimeLeft()
    {
        return time_left;
    }

    public void retry()
    {
        StartCoroutine(ChangeScene(SceneManager.GetActiveScene().buildIndex));
    }

    public void nextLevel()
    {
        if(score_decimal >= 0.7)
        {
            if(level_day == 5)
            {
                StartCoroutine(ChangeScene(1));//ending cutscene
            }
            else
            {
                StartCoroutine(ChangeScene(level_day + 7)); //next level cutscene
            }            
        }
        else
        {
            failMessage.SetActive(true);
        }
    }
    public IEnumerator ChangeScene(int scene)
    {
        fade.FadeIn();
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(scene);
    }

    public void hideError()
    {
        Debug.Log("OK clicked");
        failMessage.SetActive(false);
    }
    public void mainMenu()
    {
        StartCoroutine(ChangeScene(0));
    }
}