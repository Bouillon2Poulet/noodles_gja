using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class brickGameThemeAudio : MonoBehaviour
{
    
    public AudioClip[] themes;
    public AudioClip intro_sound;
    int randomPick;
    public bool gameStarted;
    bool isOnMenu = true;
    AudioSource audioSource;
    // Start is called before the first frame update
    // Update is called once per frame

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = intro_sound;
        audioSource.Play();
        gameStarted=false;
    }
    void Update()
    {   
        if (gameStarted == true && isOnMenu == true)
        {
            audioSource.clip = themes[Random.Range(0, themes.Length)];
            audioSource.Play();
            isOnMenu = false;
        }
        if (audioSource.isPlaying == false && gameStarted==true){
            audioSource.clip = themes[Random.Range(0, themes.Length)];
            audioSource.Play();
        }
    }
}
