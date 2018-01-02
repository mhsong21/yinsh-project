using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool: MonoBehaviour
{
	public GameObject[] blackRingPrefabs;
	public GameObject[] whiteRingPrefabs;

	List<GameObject> blackRings;
	List<GameObject> whiteRings;

	void Start()
	{
		blackRings = new List<GameObject> ();
		whiteRings = new List<GameObject> ();
		for (int i = 0; i < 5; i++)
		{
			GameObject obj1 = (GameObject)Instantiate (blackRingPrefabs[i]);
			obj1.SetActive (false);
			blackRings.Add (obj1);

			GameObject obj2 = (GameObject)Instantiate (whiteRingPrefabs[i]);
			obj2.SetActive (false);
			whiteRings.Add (obj2);
		}
	}

	public GameObject GetBlackRing()
	{
		for (int i = 0; i < 5; i++) 
		{
			if (!blackRings [i].activeInHierarchy)
				return blackRings [i];
		}
		return null;
	}

	public GameObject GetWhiteRing()
	{
		for (int i = 0; i < 5; i++) 
		{
			if (!whiteRings [i].activeInHierarchy)
				return whiteRings [i];
		}
		return null;
	}
}


