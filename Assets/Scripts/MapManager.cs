using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public List<SerializableGameObjectList> spotTable;
	public Vector3 ringLocalPosition = new Vector3 (0, 0.1f, 0);
	public Vector3 ringLocalScale = new Vector3 (2, 2, 2);

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

	public GameObject GetButtonObject(int x,int y)
	{
		int bottomOffset = bottomOffsetList[x];
		return spotTable [x] [y - bottomOffset];
	}

    public ButtonCell GetButtonCell(int x, int y)
    {
		return GetButtonObject(x, y).GetComponent<ButtonCell>();
    }

	public void AddRingToCell(Ring ring, ButtonCell cell)
	{
		ring.transform.parent = cell.transform;
		ring.transform.localPosition = ringLocalPosition;
		ring.transform.localScale = ringLocalScale;
		ring.x = cell.x;
		ring.y = cell.y;

		cell.GetComponent<ButtonCell> ().ring = ring;
	}

	public void MoveRingToCell(ButtonCell prev, ButtonCell next)
	{
		Ring ring = prev.ring;
		prev.ring = null;
		prev.buttonState = ButtonState.EmptyState;
		next.ring = ring;
		next.buttonState = ButtonState.RingState;

		ring.transform.parent = next.transform;
		ring.transform.localPosition = ringLocalPosition;
		ring.state = RingState.Idle;
		ring.x = next.x;
		ring.y = next.y;
	}
}
