using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonClick : MonoBehaviour
{
	public MapManager parent;
	public Animator animator;

	private bool onEnter = false;
	private bool onDown = false;

	public void OnMouseEnter()
	{
		// parent.OnChildClick(gameObject);
		animator.SetBool("Pointer Down", true);
		onEnter = true;
	}

	public void OnMouseExit()
	{
		if (onEnter)
		{
			animator.SetBool("Pointer Down", false);
			onEnter = false;
		}
	}

	public void OnMouseDown()
	{
		onDown = true;
	}

	public void OnMouseUp()
	{
		if (onDown)
		{
			var renderer = gameObject.GetComponent<MeshRenderer>();
			Color color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
			renderer.materials[0].color = color;
			onDown = false;
		}
	}
}
