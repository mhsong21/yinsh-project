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

	public override bool Equals (object other)
	{
		if (other == null || GetType () != other.GetType ())
			return false;

		Ring obj = other as Ring;
		if (x == obj.x && y == obj.y)
			return true;

		return false;
	}

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (state == RingState.Idle) 
		{
			transform.localScale = new Vector3 (2, 2, 2);
		} 
		else 
		{
			transform.localScale = new Vector3 (2.5f, 2.5f, 2.5f);
		}
	}
}
