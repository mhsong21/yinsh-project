using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public List<List<GameObject>> spotTable = new List<List<GameObject>>();

    public void ButtonTest()
    {
        for (int i = 0; i < spotTable.Count; ++i)
        {
            for (int j = 0; j < spotTable[i].Count; j++)
            {
                Debug.Log(spotTable[i][j].name + "(" + i + ", " + j + ")");
            }
        }
    }
}
