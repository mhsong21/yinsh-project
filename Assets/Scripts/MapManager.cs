using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
	public GameManager m_GameManager;
	public List<List<GameObject>> spotTable = new List<List<GameObject>>();

    public void ButtonTest()
    {
        for (int i = 0; i < spotTable.Count; ++i)
        {
            for (int j = 0; j < spotTable[i].Count; ++j)
            {
				Debug.Log(spotTable[i][j].name + "(" + i + ", " + j + ")");
            }
        }
    }

	public void Awake()
	{
		foreach (Transform t in transform) {
			if (t.name == "Yinsh Plane")
				continue;

			List<GameObject> list = new List<GameObject> ();
			foreach (Transform tt in t.transform) {
				list.Add (tt.gameObject);
			}
			spotTable.Add(list);
		}
	}

	public void EnableAllButtons() 
	{
		for (int i = 0; i < spotTable.Count; ++i)
		{
			for (int j = 0; j < spotTable[i].Count; ++j)
			{
				Renderer renderer = spotTable [i] [j].GetComponent<Renderer>();
				renderer.material.color = Color.black;
			}
		}
	}

	public void DisableAllButtons()
	{
		for (int i = 0; i < spotTable.Count; ++i) {
			for (int j = 0; j < spotTable [i].Count; ++j) {
				Renderer renderer = spotTable [i] [j].GetComponent<Renderer> ();
				renderer.material.color = Color.gray;
			}
		}
	}

	public void OnChildClick(GameObject obj)
	{
		m_GameManager.OnClickButton (obj);
	}
}
