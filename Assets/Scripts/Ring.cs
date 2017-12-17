using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour {

	public int x;
	public int y;

	public override bool Equals (object other)
	{
		if (other == null || GetType () != other.GetType ())
			return false;

		if (x == other.x && y == other.y)
			return true;

		return false;
	}

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
