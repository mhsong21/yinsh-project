using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
	SetupState,
	ProcessState
};

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;
	public MapManager mapManager;

	public void Start()
	{
		Instance = this;
		mapManager.EnableAllButtons();
	}

	public void OnClickButton(GameObject obj)
	{
		Debug.Log(obj);
	}
}
