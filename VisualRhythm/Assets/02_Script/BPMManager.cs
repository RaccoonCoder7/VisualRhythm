using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;
using UnityEngine.UI;

[Serializable]
public class BPMManager : MonoBehaviour
{
    public AudioSource audioSource;
    public SpriteRenderer fadeOutPanel;
    public int bpm = 162;
    public float restTime = 0f;
    public List<Action<float>> actionList = new List<Action<float>>();
    [SerializeField]
    public List<BeatAction> beatActionList = new List<BeatAction>();
    public int currentActionCount = 0;
    public double beatTime = 0d;
    public bool isReadyToStart = false;
    private double pastTime = 0d;
    private int currentWaitingBeat;
    private bool rested = false;

    [Serializable]
    public class BeatAction
    {
        public int waitingBeat = 0;
        public int actionNum;
        public int value = -1;
    }

    private void Awake()
    {
        beatTime = 60d / (bpm * 2);
    }

    private void Update()
    {
        if (!isReadyToStart)
        {
            return;
        }

        if (!rested)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
                StartCoroutine(FadeOut(audioSource.clip.length));
            }

            if (pastTime < restTime)
            {
                pastTime += Time.deltaTime;
                return;
            }

            double remainTime = pastTime - restTime;
            pastTime = remainTime;
            rested = true;
        }

        if (beatActionList.Count <= currentActionCount)
        {
            return;
        }

        pastTime += Time.deltaTime;
        if (pastTime >= beatTime)
        {
            if (currentWaitingBeat == beatActionList[currentActionCount].waitingBeat)
            {
                InvokeAction();
            }
            else
            {
                currentWaitingBeat++;
            }
            double remainTime = pastTime - beatTime;
            pastTime = remainTime;
        }
    }

    private void InvokeAction()
    {
        if (beatActionList.Count <= currentActionCount
        || actionList.Count <= beatActionList[currentActionCount].actionNum)
        {
            return;
        }
        actionList[beatActionList[currentActionCount].actionNum]
            .Invoke(beatActionList[currentActionCount].value);
        currentActionCount += 1;
        currentWaitingBeat = 0;
    }

    private IEnumerator FadeOut(float musicLength)
    {
        yield return new WaitForSeconds(musicLength - 5);
        for (float i = 1; i <= 30; i += 1f)
        {
            Color c = fadeOutPanel.color;
            c.a = i / 30;
            fadeOutPanel.color = c;
            yield return new WaitForSeconds(0.1f);
        }
    }

    [ContextMenu("Beat Action List To txt")]
    private void BeatActionListTotxt()
    {
        StringBuilder sb = new StringBuilder();
        int prevNum = -1;
        for (int i = 0; i < beatActionList.Count; i++)
        {
            if (beatActionList[i].actionNum != prevNum)
            {
                prevNum = beatActionList[i].actionNum;
                sb.Append(i).Append(": ").Append(beatActionList[i].waitingBeat)
                .Append(" / ").Append(beatActionList[i].actionNum).Append("\n");
            }
        }

        File.WriteAllText("Assets/BeatActionList.txt", sb.ToString());
        Debug.Log("Create Json File " + Application.dataPath + "/BeatActionList.txt");
        AssetDatabase.Refresh();
    }
}
