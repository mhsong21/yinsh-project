using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
	public MapManager parent;

	// Use this for initialization
	void Start () 
	{
		this.parent = transform.parent.gameObject.transform.parent.gameObject.GetComponent<MapManager> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnMouseDown()
	{
		parent.OnChildClick (gameObject);
	}
}
