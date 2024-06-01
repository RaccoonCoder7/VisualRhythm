using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RDController1_2 : MonoBehaviour
{
    public GameObject[] sticks;
    public GameObject[] bounces;
    [SerializeField]
    public BeatAction_RD_Explode hitAction;

    private BPMManager bpmManager;
    private Coroutine coroutine;

    private void Awake()
    {
        bpmManager = FindObjectOfType<BPMManager>();
        bpmManager.actionList.Add(BounceAnimAction);
        bpmManager.isReadyToStart = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            BounceAnimAction(0);
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            BounceAnimAction(1);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            BounceAnimAction(2);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            BounceAnimAction(3);
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            BounceAnimAction(4);
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            BounceAnimAction(5);
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            BounceAnimAction(6);
        }
    }

    public void BounceAnimAction(float bounceNum)
    {
        int num = (int)bounceNum;
        ChangeSprites(num);
        if (num == bounces.Length - 1)
        {
            hitAction.InvokeHitAction();
            return;
        }

        hitAction.BounceAction();
    }

    private void ChangeSprites(int bounceNum)
    {
        if (bounceNum == -1)
        {
            bounces[bounces.Length - 1].SetActive(false);
            sticks[sticks.Length - 1].SetActive(true);
            return;
        }

        for (int i = 0; i < bounces.Length; i++)
        {
            if (bounceNum == i)
            {
                bounces[i].SetActive(true);
                sticks[i].SetActive(false);
                if (i != 0)
                {
                    bounces[i - 1].SetActive(false);
                    sticks[i - 1].SetActive(true);
                }
            }
        }
    }
}
