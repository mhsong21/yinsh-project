using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public List<SerializableGameObjectList> spotTable;

    public void EnableAllButtons()
    {
        for (int i = 0; i < spotTable.Count; ++i)
        {
            for (int j = 0; j < spotTable[i].rawData.Count; ++j)
            {
                Renderer renderer = spotTable[i][j].GetComponent<Renderer>();
                renderer.material.color = Color.black;
            }
        }
    }

    public void DisableAllButtons()
    {
        for (int i = 0; i < spotTable.Count; ++i)
        {
            for (int j = 0; j < spotTable[i].rawData.Count; ++j)
            {
                Renderer renderer = spotTable[i][j].GetComponent<Renderer>();
                renderer.material.color = Color.gray;
            }
        }
    }

    public void OnChildClick(GameObject obj)
    {
        GameManager.Instance.OnClickButton(obj);
    }
}
