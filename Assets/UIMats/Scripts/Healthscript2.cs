using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.UI;

public class Healthscript2 : MonoBehaviour
{
    [SerializeField]
    private Image[] healthNull; // health images
    [SerializeField]
    private Image[] healthActive; // health images 

    [SerializeField] private int healthCurrent, damageDelaySecsInt; // set in inspector 

 


    void Awake()
    {
       
       
        for (int i = 3; i >= 0; i--) // more efficant for loop
        {
            healthNull[i].enabled = false;
        }
        for (int n = 3; n >= 0; n--)
        {
            healthActive[n].enabled = true;
        }
  
        healthCurrent = healthActive.Length; // set the current health to the max health which is based on how many array elements there are
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == ("EnemyAttack"))
            StartCoroutine(damageDelay(damageDelaySecsInt));
    }


    IEnumerator damageDelay(int delay)
    {
        takeDamage(1);
        yield return new WaitForSeconds(1);
    }

    void takeDamage(int damage)
    {
        healthCurrent = healthCurrent - damage;
        switch (healthCurrent) 
        {
            case 0:
                healthNull[healthNull.Length - 4].enabled = true;
                healthActive[healthActive.Length - 4].enabled = false;
                Death();
                break;

            case 3:
                healthNull[healthNull.Length - 1].enabled = true;
                healthActive[healthActive.Length - 1].enabled = false;
                break;

            case 2:
                healthNull[healthNull.Length - 2].enabled = true;
                healthActive[healthActive.Length - 2].enabled = false;
                break;

            case 1:
                healthNull[healthNull.Length - 3].enabled = true;
                healthActive[healthActive.Length - 3].enabled = false;
                break;

        }
    }

    void Death()
    {
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {
  


    }
}
