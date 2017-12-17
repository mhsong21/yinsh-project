using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public List<SerializableGameObjectList> spotTable;

    private List<int> bottomOffsetList = new List<int>(){1, 0, 0, 0, 0, 1, 1, 2, 3, 4, 6};

    public void EnableAllButtons()
    {
        for (int i = 0; i < spotTable.Count; ++i)
        {
            for (int j = 0; j < spotTable[i].rawData.Count; ++j)
            {
                spotTable[i][j].GetComponent<ButtonCell>().EnableButton();
            }
        }
    }

    public void DisableAllButtons()
    {
        for (int i = 0; i < spotTable.Count; ++i)
        {
            for (int j = 0; j < spotTable[i].rawData.Count; ++j)
            {
                spotTable[i][j].GetComponent<ButtonCell>().DisableButton();
            }
        }
    }

    public ButtonCell GetButtonCell(int x, int y)
    {
        int bottomOffset = bottomOffsetList[x];
        return spotTable[x][y - bottomOffset].GetComponent<ButtonCell>();
    }

	public void AddRingToCell(GameObject ring, int x, int y)
	{
		int bottomOffset = bottomOffsetList[x];
		var cell = spotTable [x] [y - bottomOffset];
		ring.transform.parent = cell.transform;
		ring.transform.localPosition = new Vector3 (0, 0.1f, 0);
		ring.transform.localScale = new Vector3 (2, 2, 2);
	}
}
