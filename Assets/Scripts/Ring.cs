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
	// public accessors
	public RingState state {
		get { return _state; }
		set
		{
			_state = value;
			if (value == RingState.Idle)
			{
				transform.localScale = new Vector3(2f, 2f, 2f);
			}
			else
			{
				transform.localScale = new Vector3(2.2f, 2.2f, 2.2f);
			}
		}
	}
	public bool isIdle { get { return _state == RingState.Idle; } }
	public bool isSelected { get { return _state == RingState.Selected; } }

	// private fields
	private RingState _state = RingState.Idle;
}
