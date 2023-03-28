using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealth : MonoBehaviour
{

    playerHealth instance;

    public int maxHealth;
    int health;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null) {
            instance = this;
        }
    }

    // Update is called once per frame
    void start()
    {
        health - maxHealth;
        
    }

    public void TakeDamage() {
        health -= 1;
    }
}
