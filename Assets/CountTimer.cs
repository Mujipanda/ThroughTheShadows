using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CountTimer : MonoBehaviour
{
    //Level Load, Timer, Debug
    public string levelToLoad;
    private float timer = 4f;
   




    //init
    void Start()
    {
        
    }

    
    void Update()
    {
        //Timer Start
        timer -= Time.deltaTime;
        if (timer <= 0) {
            SceneManager.LoadScene(levelToLoad);
        }

        if (Input.GetMouseButtonDown(0)) {
            SceneManager.LoadScene(levelToLoad);
        }
    }
}
