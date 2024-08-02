using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    FadeInOut fade;
    GameObject startOptions;
    GameObject credits;
    // Start is called before the first frame update
    void Start()
    {
        startOptions = GameObject.FindGameObjectWithTag("Start Options");
        startOptions.SetActive(false);
        credits = GameObject.FindGameObjectWithTag("Credits");
        credits.SetActive(false);

        fade = FindObjectOfType<FadeInOut>();
        fade.FadeOut();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame(int i)
    {
        StartCoroutine(ChangeScene(i));
    }

    public IEnumerator ChangeScene(int scene_index)
    {
        fade.FadeIn();
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(scene_index);
    }

    public void CloseGame()
    {
        Application.Quit(); //Remove for browser build
        //SceneManager.LoadScene("Tutorial");
    }
    public void PickDay(bool open)
    {
        startOptions.SetActive(open);
    }
    public void ToggleCredits(bool open)
    {
        credits.SetActive(open);
    }
}
