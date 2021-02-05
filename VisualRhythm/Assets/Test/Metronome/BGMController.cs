using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour
{
    private AudioSource audio;
    private bool musicStarted = false;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (musicStarted)
        {
            return;
        }

        if (other.gameObject.tag.Equals("Note"))
        {
            audio.Play();
            musicStarted = true;
        }
    }
}
