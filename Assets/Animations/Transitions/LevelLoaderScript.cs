using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoaderScript : MonoBehaviour
{

    public Animator transition;

    public float transitionTime = 1f;
    
    void Update()
    {
        
    }

    public void LoadNextLevel(int LvlIndex) {
        Time.timeScale = 1f;
        StartCoroutine(LoadLevel(LvlIndex));
    }

    IEnumerator LoadLevel(int levelIndex) 
    {

        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }

}
