using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform center;
    public RectTransform[] timingRect;

    private List<TimingBox> timingBoxList = new List<TimingBox>();

    public class TimingBox
    {
        public string name;
        public Vector2 position;
    }

    void Start()
    {
        for (int i = 0; i < timingRect.Length; i++)
        {
            TimingBox box = new TimingBox();
            box.name = timingRect[i].name;
            box.position = new Vector2(center.localPosition.x - timingRect[i].rect.width / 2,
                                       center.localPosition.x + timingRect[i].rect.width / 2);
            Debug.Log(box.name + ": " + box.position.x + "/" + box.position.y);
            timingBoxList.Add(box);
        }
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            OnTouchScreen();
        }
#else
        if (Input.touchCount < 1)
        {
            return;
        }

        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            OnTouchScreen();
        }
#endif
    }

    private void OnTouchScreen()
    {
        foreach (var note in NoteSpawner.noteList)
        {
            float notePosX = note.transform.localPosition.x;
            foreach (var box in timingBoxList)
            {
                if (box.position.x <= notePosX && notePosX <= box.position.y)
                {
                    Debug.Log(box.name + ": " + notePosX);
                    NoteSpawner.noteList.Remove(note);
                    Destroy(note);
                    return;
                }
            }
        }
    }
}
