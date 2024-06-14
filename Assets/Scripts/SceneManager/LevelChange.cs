//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
    public EndlessRunnerManager Manager;
    public AudioSource audioo;
    public AudioClip clip;
    public AudioClip clip2;
    void OnTriggerEnter(Collider other)

    {
        if(Manager.simpSayisi >= 15)
        {
            print(Manager.simpSayisi);
            SceneManager.LoadScene(2);
            audioo.PlayOneShot(clip);
        }
        else
        {
            SceneManager.LoadScene(1);
            audioo.PlayOneShot(clip2);
        }
    }

}

