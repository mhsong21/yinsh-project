using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour 
{

	public int x;
	public int y;

	public override bool Equals (object other)
	{
		if (other == null || GetType () != other.GetType ())
			return false;

		Ring obj = other as Ring;
		if (x == obj.x && y == obj.y)
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
