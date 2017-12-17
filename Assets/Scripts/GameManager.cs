using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
	SetupState,
	ProcessState
};

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;
	public NewObjectPullerScript objectPuller;
	public MapManager mapManager;
	public Player player1;
	public Player player2;
	public Text statusText;

	private GameState state = GameState.SetupState;
	private Player currentPlayer;
	private ButtonCell lastClicked;

	public void Start()
	{
		Instance = this;
		mapManager.EnableAllButtons();
		currentPlayer = player1;
		lastClicked = null;

		SetupStatusText ();
	}

	public void OnClickButton(ButtonCell cell)
	{
		var x = cell.x;
		var y = cell.y;
		Debug.Log (x + ", " + y + " button pressed!");

		switch (state)
		{
		case GameState.SetupState:
			if (cell.IsRingState() || cell.IsStoneState())
				return;

			if (lastClicked == cell) 
			{
				GameObject ring;
				Player nextPlayer;

				cell.buttonState = ButtonState.RingState;
				if (currentPlayer == player1) 
				{
					ring = objectPuller.GetWhiteRing ();
					nextPlayer = player2;
				} 
				else 
				{
					ring = objectPuller.GetBlackRing ();
					nextPlayer = player1;
				}

				ring.SetActive (true);
				currentPlayer.AddRing (ring);
				mapManager.AddRingToCell (ring, x, y);
				currentPlayer = nextPlayer;

				if (player1.ringCount == 5 && player2.ringCount == 5) 
					state = GameState.ProcessState;
			} 
			else 
			{
				if (lastClicked != null && lastClicked.IsPressedState())
					lastClicked.buttonState = ButtonState.EmptyState;
				
				cell.buttonState = ButtonState.PressedState;
			}
				
			break;
		case GameState.ProcessState:
			if (lastClicked == cell) 
			{
//				cell.buttonState = ButtonState.RingState;


			} 
			else 
			{
			}
			break;
		}
		lastClicked = cell;

		SetupStatusText ();
	}

	private void SetupStatusText()
	{
		string gameState = state == GameState.SetupState ? "Setup State" : "Progress State";
		string playerState = (currentPlayer == player1) ? "Player1 Turn" : "Player2 Turn";

		statusText.text = gameState + "\n" + playerState;
	}
}
