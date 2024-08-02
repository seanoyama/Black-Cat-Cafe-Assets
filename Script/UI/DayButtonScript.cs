using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int day = 1;
    public GameObject gradeDisplay;
    public MenuScript menu;

    public Sprite GradeA;
    public Sprite GradeB;
    public Sprite GradeC;
    public Sprite GradeD;

    float dayscore;
    float prevdayscore;
    void Start()
    {
        menu = GameObject.FindGameObjectWithTag("Menu").GetComponent<MenuScript>();
        
        dayscore = PlayerPrefs.GetFloat("Score Day" + day);
        if(day-1 == 0)
        {
            prevdayscore = 1;
        }
        else
        {
            prevdayscore = PlayerPrefs.GetFloat("Score Day" + (day - 1));
        }
        if (dayscore >= 0.9f)
        {
            setGrade(GradeA);
        }
        else if (dayscore >= 0.8f)
        {
            setGrade(GradeB);
        }
        else if (dayscore >= 0.7f)
        {
            setGrade(GradeC);
        }
        else if (dayscore < 0.7f && dayscore > 0)
        {
            setGrade(GradeD);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void setGrade(Sprite grade)
    {
        gradeDisplay.GetComponent<Image>().sprite = grade;
    }

    public void startDay()
    {
        if (prevdayscore >= 0.7)
        {
            menu.StartGame(day + 6);
        }
    }

}
