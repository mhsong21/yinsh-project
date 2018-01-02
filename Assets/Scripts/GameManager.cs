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
	public ObjectPool objectPool;
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

		SetupStatusText();
	}

	public void OnClickButton(ButtonCell cell)
	{
		var x = cell.x;
		var y = cell.y;
		var z = cell.z;
		Debug.Log (x + ", " + y + ", " + z + " button pressed!");

		switch (state)
		{
			case GameState.SetupState:
				if (cell.isRingState || cell.isStoneState)
					return;

				if (lastClicked == cell)
				{
					GameObject obj;
					Player nextPlayer;

					cell.state = ButtonState.Ring;
					if (currentPlayer == player1)
					{
						obj = objectPool.GetWhiteRing();
						nextPlayer = player2;
					}
					else
					{
						obj = objectPool.GetBlackRing();
						nextPlayer = player1;
					}

					obj.SetActive(true);
					Ring ring = obj.GetComponent<Ring>();
					currentPlayer.AddRing(ring);
					mapManager.AddRingToCell(ring, cell);
					currentPlayer = nextPlayer;

					if (player1.ringCount == 5 && player2.ringCount == 5)
					{
						state = GameState.ProcessState;
						lastClicked = null;
					}
				}
				else
				{
					if (lastClicked != null && lastClicked.isSelectedState)
						lastClicked.state = ButtonState.Empty;
				
					cell.state = ButtonState.Selected;
				}
				lastClicked = cell;
				break;
			case GameState.ProcessState:
				if (cell.isStoneState)
					return;

				if (cell.isEmptyState && lastClicked != null && lastClicked.isRingState)
				{
					Player nextPlayer = currentPlayer == player1 ? player2 : player1;

					mapManager.MoveRingToCell(lastClicked, cell);
					currentPlayer = nextPlayer;
					cell.ring.state = RingState.Idle;
					lastClicked = null;
					break;
				}
				else if (cell.isRingState)
				{
					if (lastClicked == cell)
					{
						lastClicked.ring.state = RingState.Idle;
						lastClicked = null;
						break;
					}
					if (lastClicked != null && lastClicked.isRingState && lastClicked.ring.isSelected)
						lastClicked.ring.state = RingState.Idle;

					cell.ring.state = RingState.Selected;
				}
				lastClicked = cell;

				if (cell.isRingState)
					mapManager.ActivatePossibleButtons(cell);
				break;
		}

		SetupStatusText();
	}

	private void SetupStatusText()
	{
		string gameState = state == GameState.SetupState ? "Setup State" : "Progress State";
		string playerState = (currentPlayer == player1) ? "Player1 Turn" : "Player2 Turn";

		statusText.text = gameState + "\n" + playerState;
	}
}
