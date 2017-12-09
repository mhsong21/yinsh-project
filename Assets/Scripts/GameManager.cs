using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager: MonoBehaviour
{
	public MapManager m_MapManager;

	public void Start()
	{
		m_MapManager.m_GameManager = this;
		m_MapManager.ButtonTest ();
		m_MapManager.EnableAllButtons ();
	}

	public void OnClickButton(GameObject obj)
	{
		Debug.Log (obj);
	}
}
