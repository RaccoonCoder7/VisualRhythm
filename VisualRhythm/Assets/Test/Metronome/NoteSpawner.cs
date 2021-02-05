using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public int bpm = 120;
    public GameObject note;
    public Transform noteSpawnTr;
    public static List<GameObject> noteList = new List<GameObject>();

    private double spawnTime = 0d;

    // Update is called once per frame
    void Update()
    {
        spawnTime += Time.deltaTime;
        double pastTime = 60d / bpm;
        if (spawnTime >= pastTime)
        {
            GameObject noteObj = Instantiate(note, noteSpawnTr.position, Quaternion.identity, transform);
            noteList.Add(noteObj);
            double remainTime = spawnTime - pastTime;
            spawnTime = remainTime;

            // TODO: remainTime만큼 노트위치 수정하기
            // TODO: spawnTime 찍어서 비교하기
        }
    }
}
