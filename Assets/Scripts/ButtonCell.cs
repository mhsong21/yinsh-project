﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum ButtonState
{
    PressedState,
	RingState,
    StoneState,
    EmptyState
};

public class ButtonCell : MonoBehaviour
{
	public Animator animator;
	public GameObject ringTrans;
	public ButtonState buttonState = ButtonState.EmptyState;
	public int x;
	public int y;

	private bool onDown = false;

	public bool IsPressedState()
	{
		return buttonState == ButtonState.PressedState;
	}

	public bool IsRingState()
	{
		return buttonState == ButtonState.RingState;
	}

	public bool IsStoneState()
	{
		return buttonState == ButtonState.StoneState;
	}

	public bool IsEmptyState()
	{
		return buttonState == ButtonState.EmptyState;
	}

	public void OnMouseDown()
	{
		onDown = true;
	}

	public void OnMouseUp()
	{
		if (onDown)
		{
			GameManager.Instance.OnClickButton(this);
			if (IsEmptyState())
			{
				ringTrans.SetActive(true);
			}

			onDown = false;
		}
	}

	public void EnableButton()
	{

	}

	public void DisableButton()
	{
		
	}

	public void OnMouseEnter()
	{
		ringTrans.SetActive(true);
	}

	public void OnMouseExit()
	{
		ringTrans.SetActive(false);
	}

	public void Update()
	{
		ringTrans.SetActive(IsPressedState());
	}
}
