using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class catStats : MonoBehaviour //rename to CatStats
{
    public LogicScript logic;
    public catSounds cat_effects;
    
    public GameObject arrow;
    public GameObject cat_button;

    public Button eat_button;
    public Button drink_button;
    public Button brush_button;

    
    bool happy = true;

    public int my_id;
    int selected_id;
    public float current_time;

    public int max_need = 20;

    private float br = 0.3996885f;
    private float bg = 1.0f;
    private float bb = 0.3160377f;

    int hunger;
    int thirst;
    int cleanliness;
    int penalty = 0;
    float p_timer = 1; //penalty timer
    float d_timer = 1; //decay timer
    float a_timer = 4; //action timer

    bool eating = false;
    bool drinking = false;
    bool brushing = false;
    bool finished = false;
    bool busy = false;

    int eatCounter;
    int drinkCounter;
    int brushCounter;


    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        cat_effects = GetComponentInChildren<catSounds>();
        eat_button = GameObject.FindGameObjectWithTag("Eat Button").GetComponent<Button>();
        drink_button = GameObject.FindGameObjectWithTag("Drink Button").GetComponent<Button>();
        brush_button = GameObject.FindGameObjectWithTag("Brush Button").GetComponent<Button>();


        hunger = Random.Range(0, max_need);
        thirst = Random.Range(0, max_need);
        cleanliness = Random.Range(0, max_need);
        addCat();

    }

    // Update is called once per frame
    void Update()
    {
        
        if (!finished && logic.isFinished())
        {
            cat_button.SetActive(false);
            finished = true;
            addStats();
            
        }
        
        //if selected, change the button colors
        selected_id = logic.getCatID();

        if (!finished)
        {
            
            d_timer -= Time.deltaTime;
            if (d_timer < 0)
            {
                d_timer = 1;
                if (!eating)
                {
                    hunger--;
                }
                if (!drinking)
                {
                    thirst--;
                }
                if (!brushing)
                {
                    cleanliness--;
                }
            }
        }
        

        if (hunger <= 0 || thirst <= 0 || cleanliness <= 0)
        {
            p_timer -= Time.deltaTime;
            if (p_timer < 0)
            {
                p_timer = 1;
                penalty++;
            }
        }
        updateHappy();

        if (selected_id == my_id)
        {
            arrow.SetActive(true);
            updateButtons();
            if (logic.eatIsClicked())
            {
                logic.eatBool(false);
                if (!busy)
                {
                    eating = true;
                    busy = true;
                    eatCounter++;
                }
                
            }
            if (logic.drinkIsClicked())
            {
                logic.drinkBool(false);
                if (!busy)
                {
                    drinking = true;
                    busy = true;
                    drinkCounter++;
                }
                
            }
            if (logic.brushIsClicked())
            {
                logic.brushBool(false);
                if (!busy)
                {
                    brushing = true;
                    busy = true;
                    brushCounter++;
                }
                
            }

        }
        else
        {
            arrow.SetActive(false);
        }

        if (eating)
        {
            actionTimer("eat");
        }
        if (drinking)
        {
            actionTimer("drink");
        }
        if (brushing)
        {
            actionTimer("brush");
        }


    }


    void actionTimer(string action)
    {
        if (a_timer == 4)
        {
            cat_effects.PlayAnim(action);
        }
        
        if(a_timer > 0)
        {
            a_timer -= Time.deltaTime;
        }
        else
        {
            if (eating)
            {
                hunger = Random.Range(max_need-10,max_need);
            }
            if (drinking)
            {
                thirst = Random.Range(max_need - 10, max_need);
                
            }
            if (brushing)
            {
                cleanliness = Random.Range(max_need - 10, max_need);
            }
            eating = false;
            drinking = false;
            brushing = false;
            busy = false;
            happy = false;
            updateHappy();
            a_timer = 4;
        }
    }

    void updateHappy()
    {
        if ((hunger <= 0 || thirst <= 0 || cleanliness <= 0) && !busy)
        {
            cat_effects.PlayAnim("attention");
            happy = false;
        }
        else if (!happy && (hunger > 0 && thirst > 0 && cleanliness > 0))
        {
            cat_effects.PlayAnim("idle1");
            happy = true;
        }
    }

    void addStats()
    {
        logic.catPenalty(eatCounter, drinkCounter, brushCounter, penalty);
    }
    void addCat()
    {
        logic.addCat();
    }

    void updateButtons()
    {
        eat_button.GetComponent<Image>().color = new Color(br, bg, bb, hunger / (float)max_need);
        drink_button.GetComponent<Image>().color = new Color(br, bg, bb, thirst / (float)max_need);
        brush_button.GetComponent<Image>().color = new Color(br, bg, bb, cleanliness / (float)max_need);
    }
}
