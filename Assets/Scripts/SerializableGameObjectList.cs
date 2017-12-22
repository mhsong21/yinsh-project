using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableGameObjectList
{
	public List<GameObject> rawData = new List<GameObject>();

	public GameObject this [int i]
	{
		get
		{
			return rawData[i];
		}
		set
		{
			rawData[i] = value;
		}
	}
}