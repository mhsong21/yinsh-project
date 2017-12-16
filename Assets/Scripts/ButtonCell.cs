using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum ButtonState
{
    RingState,
    StoneState,
    EmptyState
};

public class ButtonCell : MonoBehaviour
{
	public MapManager parent;
	public Animator animator;
	public GameObject ringTrans;
	public ButtonState buttonState = ButtonState.EmptyState;
	public int x;
	public int y;

	private bool onDown = false;

	public void Start()
	{
		this.parent = transform.parent.gameObject.transform.parent.gameObject.GetComponent<MapManager> ();
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
		parent.OnChildClick (gameObject);
	}

	public void OnMouseUp()
	{
		if (onDown)
		{
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
}
