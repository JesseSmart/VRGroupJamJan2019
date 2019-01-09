using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrembloContainer : MonoBehaviour
{
    private AudioSource audio;
    public AudioClip thoompSound;

    void Start()
    {
        audio = GetComponent<AudioSource>(); 
    }

    public void AddGremblo(GameObject gremblo)
    {
        audio.Play();
        Object.Destroy(gremblo);
    }
}
