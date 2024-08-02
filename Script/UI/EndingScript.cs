using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScript : MonoBehaviour
{
    FadeInOut fade;
    // Start is called before the first frame update
    void Start()
    {
        
        fade = FindObjectOfType<FadeInOut>();
        fade.FadeOut();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ClickMenu()
    {
        StartCoroutine(ChangeScene(0));
    }
    public IEnumerator ChangeScene(int scene_index)
    {
        fade.FadeIn();
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(scene_index);
    }
}
