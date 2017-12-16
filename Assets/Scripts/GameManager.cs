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

	private GameState state = GameState.SetupState;

	public void Start()
	{
		Instance = this;
		mapManager.EnableAllButtons();
	}

	public void OnClickButton(GameObject obj)
	{
		var buttonCell = obj.GetComponent<ButtonCell> ();
		var x = buttonCell.x;
		var y = buttonCell.y;
		Debug.Log (x + ", " + y + " button pressed!");
	}
}
