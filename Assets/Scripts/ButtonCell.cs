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
	public GameObject stone;

	// public accessors
	public ButtonState state { 
		get { return _state; } 
		set 
		{ 
			_state = value;
			switch (value)
			{
				case ButtonState.Ring:
					ringTrans.SetActive(false);
					break;
				case ButtonState.Selected:
					ringTrans.SetActive(true);
					break;
				case ButtonState.Stone:
					stone.SetActive(true);
					break;
				case ButtonState.Empty:
					ringTrans.SetActive(false);
					stone.SetActive(false);
					break;
			}
		} 
	}
	public bool isSelectedState { get { return _state == ButtonState.Selected; } }
	public bool isRingState { get { return _state == ButtonState.Ring; } }
	public bool isStoneState { get { return _state == ButtonState.Stone; } }
	public bool isEmptyState { get { return _state == ButtonState.Empty; } }

	public Ring ring { get { return _ring; } set { _ring = value; } }
	public int x { get { return _x; } set { _x = value; } }
	public int y { get { return _y; } set { _y = value; } }
	public int z { get { return _y - _x + 5; } }

	// private fields
	private ButtonState _state = ButtonState.Empty;
	private Ring _ring;
	[SerializeField] private int _x;
	[SerializeField] private int _y;

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
//			if (isEmptyState)
//			{
//				ringTrans.SetActive(true);
//			}

			onDown = false;
		}
	}

	public void EnableButton()
	{
		Debug.Log(_x + ", " + _y + " is enabled");
	}

	public void DisableButton()
	{

	}

	public void FlipStone()
	{

	}

//	public void OnMouseEnter()
//	{
//		ringTrans.SetActive(true);
//	}
//
//	public void OnMouseExit()
//	{
//		ringTrans.SetActive(false);
//	}

//	public void Update()
//	{
//		ringTrans.SetActive(isSelectedState);
//	}
}
