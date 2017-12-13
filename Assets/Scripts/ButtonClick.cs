using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonClick : MonoBehaviour
{
	public MapManager parent;
	public Animator animator;

	private bool onDown = false;

	public void OnMouseDown()
	{
		// parent.OnChildClick(gameObject);
		animator.SetBool("Pointer Down", true);
		onDown = true;
	}

	public void OnMouseUp()
	{
		if (onDown)
		{
			animator.SetBool("Pointer Down", false);
			onDown = false;
		}
	}
}
