using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
	public List<SerializableGameObjectList> spotTable;
	public Vector3 ringLocalPosition = new Vector3(0, 0.1f, 0);
	public Vector3 ringLocalScale = new Vector3(2f, 2f, 2f);

	private List<int> bottomOffsetList = new List<int>(){ 1, 0, 0, 0, 0, 1, 1, 2, 3, 4, 6 };
	private int[,] delta = new int[,]{ { 1, 0 }, { 1, 1 }, { 0, 1 }, { -1, 0 } , { -1, -1 }, { 0, -1 } };

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
		if (GetButtonObject(x, y) != null) return GetButtonObject(x, y).GetComponent<ButtonCell>();
		return null;
	}

	public void AddRingToCell(Ring ring, ButtonCell cell)
	{
		ring.transform.parent = cell.transform;
		ring.transform.localPosition = ringLocalPosition;
		ring.transform.localScale = ringLocalScale;

		cell.GetComponent<ButtonCell>().ring = ring;
	}

	public void MoveRingToCell(ButtonCell prev, ButtonCell next, bool isWhite)
	{
		Ring ring = prev.ring;
		prev.ring = null;
		prev.isWhite = isWhite;
		prev.state = ButtonState.Stone;
		next.ring = ring;
		next.state = ButtonState.Ring;

		ring.transform.parent = next.transform;
		ring.transform.localPosition = ringLocalPosition;
		ring.state = RingState.Idle;

		// Flip
		FlipStones(prev, next);
	}

	public void ActivatePossibleButtons(ButtonCell cell)
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
					target.ActivateButton();
					if (isStoneAppeared) break;
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

	public void FlipStones(ButtonCell from, ButtonCell to)
	{
		int x = Math.Min(from.x, to.x);
		int y = Math.Min(from.y, to.y);
		int endX = Math.Max(from.x, to.x);
		int endY = Math.Max(from.y, to.y);

		int dx, dy;
		if (from.x == to.x) { dx = 0; dy = 1; }
		else if (from.y == to.y) { dx = 1; dy = 0; }
		else { dx = 1; dy = 1; }

		// to ignore start & end point
		x += dx; y += dy;

		for (; x != endX || y != endY; x += dx, y += dy)
		{
			Debug.Log(x + " " + y + "is flipped");
			ButtonCell target = GetButtonCell(x, y);
			if (target.isStoneState) target.FlipStone();
		}
	}

	public List<List<ButtonCell>> checkFiveStones()
	{
		List<List<ButtonCell>> fiveStoneList = new List<List<ButtonCell>>();
		for (int p = 0; p < spotTable.Count / 2; p++)
		{
			for (int q = 0; q < spotTable[p].rawData.Count; q++)
			{
				ButtonCell cell = spotTable[p][q].GetComponent<ButtonCell>();
				if (!cell.isStoneState)
					continue;

				for (int i = 0; i < delta.GetLength(0); i++)
				{
					int dx = delta[i, 0];
					int dy = delta[i, 1];
					int count = 0;

					List<ButtonCell> fiveStone = new List<ButtonCell>();
					for (int x = cell.x, y = cell.y; count < 5; x += dx, y += dy)
					{
						ButtonCell target = GetButtonCell(x, y);
						if (target == null || !target.isStoneState)
							break;

						// if same color
						if (cell.isWhite == target.isWhite)
						{
							count++;
							fiveStone.Add(target);
						}
						else
							break;
					}

					if (count == 5)
						fiveStoneList.Add(fiveStone);
				}
			}
		}
		return fiveStoneList;
	}

	public void RemoveRing(ButtonCell cell)
	{
		Ring ring = cell.ring;

		// move to selected position
		ring.gameObject.SetActive(false);
		cell.ring = null;
	}
}
