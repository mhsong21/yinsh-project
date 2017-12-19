using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RingState
{
	Selected,
	Idle
};

public class Ring : MonoBehaviour 
{
	public int x;
	public int y;
	public RingState state = RingState.Idle;

	public bool IsSelected()
	{
		return state == RingState.Selected;
	}

	public bool IsIdle()
	{
		return state == RingState.Idle;
	}

	public void SetState(RingState newState)
	{
		this.state = newState;
		if (newState == RingState.Idle)
		{
			transform.localScale = new Vector3(2f, 2f, 2f);
		}
		else
		{
			transform.localScale = new Vector3(2.2f, 2.2f, 2.2f);
		}
	} 
}
