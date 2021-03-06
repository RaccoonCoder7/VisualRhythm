using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneCreator : MonoBehaviour
{
    public GameObject prefab;
    public float width;
    public float height;
    public float oddLineDiff;
    public int horizontalCount;
    public int verticalCount;

    [ContextMenu("Clone")]
    public void Clone()
    {
        for (int i = 0; i < verticalCount; i++)
        {
            bool isOdd = i % 2 == 1;
            int tempHorizontalCount = isOdd ? horizontalCount + 1 : horizontalCount;
            for (int j = 0; j < tempHorizontalCount; j++)
            {
                Vector3 pos = transform.position;
                pos.x += width * j;
                pos.y += height * i;
                if (isOdd)
                {
                    pos.x -= oddLineDiff;
                }

                Instantiate(prefab, pos, Quaternion.identity, transform);
            }
        }
    }
}
