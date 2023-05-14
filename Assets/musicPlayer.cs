using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicPlayer : MonoBehaviour
{
    [Header("Sound Veribles")]
    [SerializeField]
    private AudioClip music;
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private float audioVolume = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        audioSource.clip = music;
        audioSource.volume = audioVolume;
        audioSource.Play();
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
