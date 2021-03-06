using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockStep_SteperController : MonoBehaviour
{
    public List<GameObject> masks = new List<GameObject>();
    public Sprite[] sprites;
    public List<SpriteRenderer> renderers = new List<SpriteRenderer>();
    public List<Camera> cameraList = new List<Camera>();
    private BPMManager bpmManager;
    private int currentCameraIndex = 0;
    private Coroutine coroutine;

    private void Awake()
    {
        bpmManager = FindObjectOfType<BPMManager>();
        foreach (Transform child in transform)
        {
            renderers.Add(child.GetComponent<SpriteRenderer>());
        }
        bpmManager.actionList.Add(EmptyAction);
        bpmManager.actionList.Add(LeftAnimAction);
        bpmManager.actionList.Add(RightAnimAction);
        bpmManager.actionList.Add(BounceAnimAction);
        bpmManager.isReadyToStart = true;
    }

    public void EmptyAction(float cameraSize)
    {
        ChangeCameraSize(cameraSize);
    }

    public void BounceAnimAction(float cameraSize)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        ChangeCameraSize(cameraSize);
        coroutine = StartCoroutine(StartAnim(5, 2, 6));
    }

    public void LeftAnimAction(float cameraSize)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        ChangeCameraSize(cameraSize);
        coroutine = StartCoroutine(StartAnim(1, 0));
    }

    public void RightAnimAction(float cameraSize)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        ChangeCameraSize(cameraSize);
        coroutine = StartCoroutine(StartAnim(3, 4));
    }

    private void ChangeCameraSize(float value)
    {
        if (value != -1)
        {
            for (int i = 0; i < cameraList.Count; i++)
            {
                if (cameraList[i].orthographicSize == value)
                {
                    currentCameraIndex = i;
                    cameraList[i].gameObject.SetActive(true);
                    continue;
                }
                cameraList[i].gameObject.SetActive(false);
            }
        }
    }

    private void ChangeSprites(int spriteNum)
    {
        foreach (var rend in renderers)
        {
            rend.sprite = sprites[spriteNum];
        }
    }

    private IEnumerator StartAnim(int middleSpriteNum, int endSpriteNum, int originNum = 2)
    {
        double totalTime = bpmManager.beatTime;
        double endAnimTime = totalTime * 9 / 10;
        double pastTime = 0d;

        foreach (var mask in masks)
        {
            if (mask.activeSelf)
            {
                mask.SetActive(false);
            }
        }

        ChangeSprites(middleSpriteNum);
        yield return null;


        bool showMask = currentCameraIndex == 4;
        int maskIndex = 0;
        if (showMask)
        {
            maskIndex = endSpriteNum == 0 ? 0 : 1;
            masks[maskIndex].SetActive(true);
        }
        else
        {
            ChangeSprites(endSpriteNum);
        }
        while (endAnimTime > pastTime)
        {
            pastTime += Time.deltaTime;
            yield return null;
        }

        if (showMask)
        {
            masks[maskIndex].SetActive(false);
        }
        else
        {
            ChangeSprites(endSpriteNum);
        }
        yield return null;

        ChangeSprites(originNum);
    }
}
