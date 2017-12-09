using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


[System.Serializable]
public class ListableClass
{
	public List<GameObject> innerList;

	public ListableClass()
	{
		innerList = new List<GameObject> ();
	}

	public ListableClass(List<GameObject> list)
	{
		innerList = list;
	}
}