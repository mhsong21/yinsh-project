using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player: MonoBehaviour
{
	public List<GameObject> rings;
	public int ringCount 
	{
		get 
		{
			return rings.Count;
		}
	}

	public void ActiveRings() 
	{
		
	}

	public void AddRing(Ring ring)
	{
		rings.Add (ring.gameObject);
	}

}
