using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatAction_RD_Explode : MonoBehaviour
{
    public ParticleSystem particle;
    public SpriteRenderer touchEffectRenderer;
    public SpriteRenderer whiteHeartRenderer;
    public AudioSource audio;
    public GameObject heart;
    public GameObject distort;
    public float fadeOutSpeed;

    private BPMManager bpmManager;
    private Animator distortAnim;


    private void Awake()
    {
        bpmManager = FindObjectOfType<BPMManager>();
        distortAnim = distort.GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeHitAction();
        }
    }

    public void BounceAction()
    {
        StartCoroutine(BounceAnim());
    }

    public void InvokeHitAction()
    {
        particle.Play();
        StartCoroutine(HitAnim());
        StartCoroutine(HeartAnim());
        audio.Play();
    }

    private IEnumerator HitAnim()
    {
        Color c = touchEffectRenderer.color;
        float value = 0.85f;
        while (value > 0)
        {
            c.a = value;
            touchEffectRenderer.color = c;
            value -= fadeOutSpeed;
            yield return new WaitForSeconds(0.06f);
        }
        c.a = 0;
        touchEffectRenderer.color = c;
    }

    private IEnumerator HeartAnim()
    {
        Color c = whiteHeartRenderer.color;
        double value = 0;
        double totalTime = (bpmManager.beatTime) * 2;
        while (value < 1)
        {
            value += Time.deltaTime / totalTime;
            c.a = Mathf.Lerp(0, 1, (float)value);
            whiteHeartRenderer.color = c;
            yield return null;
        }
        c.a = 0;
        whiteHeartRenderer.color = c;

        // 파동
        distort.SetActive(true);
        distortAnim.SetTrigger("Explode");
        yield return new WaitForSeconds(0.56f);
        distort.SetActive(false);
    }

    private IEnumerator BounceAnim()
    {
        double returnTime = (bpmManager.beatTime) / 4;
        Vector3 originHeartSize = heart.transform.localScale;
        Vector3 heartSize = originHeartSize * 1.08f;
        heart.transform.localScale = heartSize;
        yield return new WaitForSeconds((float)returnTime);
        heart.transform.localScale = originHeartSize;
    }
}
