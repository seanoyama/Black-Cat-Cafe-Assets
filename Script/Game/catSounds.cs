using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catSounds : MonoBehaviour //rename to CatEffects
{
    AudioSource aud;

    private Animator m_animator;
    public LogicScript logic;

    public AudioClip meow;
    public AudioClip eat;
    public AudioClip brush;
    public AudioClip drink;

    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        aud = GetComponent<AudioSource>();
        PlayAnim("idle1");
    }
    public void MeowSound()
    {
        aud.pitch = Random.Range(1.0f,3.0f);
        if (!logic.isFinished())
        {
            aud.PlayOneShot(meow);
        }
    }
    public void EatSound()
    {
        aud.pitch = 1;
        aud.PlayOneShot(eat);
    }

    public void DrinkSound()
    {
        aud.pitch = 2f;//Random.Range(1.0f,1.5f);
        aud.PlayOneShot(drink);
    }

    public void BrushSound()
    {
        aud.pitch = Random.Range(1.5f,2f);
        aud.PlayOneShot(brush);
    }

    public void PlayAnim(string anim)
    {
        m_animator.Play(anim);
    }

}