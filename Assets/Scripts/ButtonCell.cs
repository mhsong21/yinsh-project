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
	public GameObject stoneHighlight;
	private readonly string IS_WHITE = "IsWhite";
	private readonly string STONE_ACTIVE = "Stone Active";
	private readonly string STONE_FLIP = "Stone Flip";

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
					animator.SetTrigger(STONE_ACTIVE);
					stone.SetActive(true);
					break;
				case ButtonState.Empty:
					ringTrans.SetActive(false);
					stone.SetActive(false);
					break;
			}
		} 
	}
	public bool isWhite
	{
		get { return _white; }
		set
		{
			_white = value;
			animator.SetBool(IS_WHITE, _white);
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
	[SerializeField] private bool _white = false;
	private Collider m_Collider;
	private bool onDown = false;


	public void Awake()
	{
		m_Collider = GetComponent<Collider>();
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
//			if (isEmptyState)
//			{
//				ringTrans.SetActive(true);
//			}

			onDown = false;
		}
	}

	public void EnableButton()
	{
		m_Collider.enabled = true;
	}

	public void DisableButton()
	{
		m_Collider.enabled = false;
		ringTrans.SetActive(false);
	}

	public void ActivateButton()
	{
		m_Collider.enabled = true;
		ringTrans.SetActive(true);
	}

	public void FlipStone()
	{
		isWhite = !isWhite;
		animator.SetTrigger(STONE_FLIP);
	}

}
