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
	public ButtonState state = ButtonState.Empty;
	public int x { get; set; }
	public int y;

	[HideInInspector] public Ring ring;

	private bool onDown = false;

	public bool IsSelectedState()
	{
		return state == ButtonState.Selected;
	}

	public bool IsRingState()
	{
		return state == ButtonState.Ring;
	}

	public bool IsStoneState()
	{
		return state == ButtonState.Stone;
	}

	public bool IsEmptyState()
	{
		return state == ButtonState.Empty;
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
		ringTrans.SetActive(IsSelectedState());
	}
}
