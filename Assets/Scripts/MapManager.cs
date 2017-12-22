using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public List<SerializableGameObjectList> spotTable;
	public Vector3 ringLocalPosition = new Vector3(0, 0.1f, 0);
	public Vector3 ringLocalScale = new Vector3(2f, 2f, 2f);

    private List<int> bottomOffsetList = new List<int>(){1, 0, 0, 0, 0, 1, 1, 2, 3, 4, 6};
    private int[,] delta = new int[,]{ { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 }, { 1, 1 }, { -1, -1 } };

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

	public GameObject GetButtonObject(int x, int y)
	{
        try 
        {
		    int bottomOffset = bottomOffsetList[x];
            var cell = spotTable[x][y - bottomOffset];
            return cell;
        }
        catch
        {
            return null;
        }
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
//		ring.x = cell.X;
//		ring.y = cell.y;

		cell.GetComponent<ButtonCell>().ring = ring;
	}

	public void MoveRingToCell(ButtonCell prev, ButtonCell next)
	{
		Ring ring = prev.ring;
		prev.ring = null;
		prev.state = ButtonState.Empty;
		next.ring = ring;
		next.state = ButtonState.Ring;

		ring.transform.parent = next.transform;
		ring.transform.localPosition = ringLocalPosition;
		ring.state = RingState.Idle;
//		ring.x = next.x;
//		ring.y = next.y;
	}

    public void ActivataePossibleButtons(ButtonCell cell)
    {
        for (int i = 0; i < delta.GetLength(0); i++)
        {
            int dx = delta[i, 0];
            int dy = delta[i, 1];
            bool isStoneAppeared = false;

            for (int x = cell.x + dx, y = cell.y + dy; GetButtonObject(x, y) != null; x += dx, y += dy)
            {
                ButtonCell target = GetButtonCell(x, y);
                if (target.isEmptyState)
                {
                    target.EnableButton();
                    if (isStoneAppeared) { break; }
                }
                else if (target.isStoneState)
                {
                    isStoneAppeared = true;
                }
                else
                {
                    break;
                }
            }
        }
    }

    public void FilpStones(ButtonCell from, ButtonCell to)
    {
        int x = Math.Min(from.x, to.x);
        int y = Math.Min(from.y, to.y);
        int endX = Math.Max(from.x, to.x);
        int endY = Math.Max(from.y, to.y);
        int dx = 0, dy = 0;
        if (from.x == to.x) { dx = 0; dy = 1; }
        if (from.y == to.y) { dx = 1; dy = 0; }
        if (from.z == to.z) { dx = 1; dy = 1; }

        for (; x < endX || y < endY; x += dx, y += dy)
        {
            ButtonCell target = GetButtonCell(x, y);
            if (target.isStoneState)
                target.FlipStone();
        }
    }
}
