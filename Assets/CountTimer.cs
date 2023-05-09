using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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
            Application.LoadLevel(levelToLoad);
        }
    }
}
