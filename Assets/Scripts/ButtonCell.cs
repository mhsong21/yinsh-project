using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum ButtonState
{
    Selected,
	Ring,
    Stone,
    Empty
};

public class ButtonCell : MonoBehaviour
{
	public Animator animator;
	public GameObject ringTrans;

	// public accessors
	public ButtonState state { get { return _state; } set { _state = value; } }
	public bool isSelectedState { get { return _state == ButtonState.Selected; } }
	public bool isRingState { get { return _state == ButtonState.Ring; } }
	public bool isStoneState { get { return _state == ButtonState.Stone; } }
	public bool isEmptyState { get { return _state == ButtonState.Empty; } }

	public Ring ring { get { return _ring; } set { _ring = value; } }
	public int x { get { return _x; } set { _x = value; } }
	public int y { get { return _y; } set { _y = value; } }

	// private fields
	private ButtonState _state = ButtonState.Empty;
	private Ring _ring;
	private int _x;
	private int _y;

	private bool onDown = false;

	public void OnMouseDown()
	{
		onDown = true;
	}

	public void OnMouseUp()
	{
		if (onDown)
		{
			GameManager.Instance.OnClickButton(this);
			if (isEmptyState)
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
		ringTrans.SetActive(isSelectedState);
	}
}
