using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneScript : MonoBehaviour
{
    FadeInOut fade;
    GameObject panel2;
    // Start is called before the first frame update
    void Start()
    {
        fade = FindObjectOfType<FadeInOut>();
        fade.FadeOut();
        panel2 = GameObject.FindGameObjectWithTag("Panel2");
        panel2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ClickContinue()
    {
        panel2.SetActive(true);
    }
    public void StartGame()
    {
        StartCoroutine(ChangeScene(SceneManager.GetActiveScene().buildIndex - 5));
    }
    public IEnumerator ChangeScene(int scene_index)
    {
        fade.FadeIn();
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(scene_index);
    }
}
