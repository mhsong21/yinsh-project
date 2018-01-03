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
	public GameObject highLightObj;
	public RingState state {
		get { return _state; }
		set
		{
			_state = value;
			if (value == RingState.Idle)
			{
				highLightObj.SetActive(false);
			}
			else
			{
				highLightObj.SetActive(true);
			}
		}
	}
	public bool isIdle { get { return _state == RingState.Idle; } }
	public bool isSelected { get { return _state == RingState.Selected; } }

	// private fields
	private RingState _state = RingState.Idle;
}
