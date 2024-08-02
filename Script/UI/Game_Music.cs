using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource aud;
    LogicScript logic;

    public AudioClip musicIntro;
    public AudioClip musicLoop;
    public AudioClip musicRush;

    public bool musicEnabled = true;

    bool startedRush = false;
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        
        aud = GetComponent<AudioSource>();
        aud.loop = true;
        if (musicEnabled)
        {
            StartCoroutine(playMusic());
        }

    }

    IEnumerator playMusic()
    {
        aud.clip = musicIntro;
        aud.Play();
        yield return new WaitForSeconds(aud.clip.length);
        if (!startedRush)
        {
            aud.clip = musicLoop;
            aud.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(musicEnabled && !startedRush && logic.getTimeLeft() < 64)
        {
            aud.clip = musicRush;
            aud.loop = false;
            StopCoroutine(playMusic());
            aud.Play();
            startedRush = true;
        }
    }
}
