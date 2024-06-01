using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GIFBackground : MonoBehaviour
{
    public Sprite[] sprites;
    public float frameTime;

    private SpriteRenderer rend;

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        StartCoroutine(ChangeSprites());
    }

    private IEnumerator ChangeSprites()
    {
        int i = 1;
        while (true)
        {
            yield return new WaitForSeconds(frameTime);
            i++;
            if (i >= sprites.Length)
            {
                i = 0;
            }
            rend.sprite = sprites[i];
        }
    }
}
