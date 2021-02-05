using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Note"))
        {
            NoteSpawner.noteList.Remove(other.gameObject);
            Destroy(other.gameObject);
        }
    }
}
