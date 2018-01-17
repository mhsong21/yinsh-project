using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
	SetupState,
	ProcessState,
	PickRingState,
	EndState
};

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;
	public FiveStoneManager fiveStoneManager;
	public ObjectPool objectPool;
	public MapManager mapManager;
	public Player player1;
	public Player player2;
	public Text statusText;

	private GameState state = GameState.SetupState;
	private Player currentPlayer;
	private ButtonCell lastClicked;
	private List<List<ButtonCell>> fiveStoneList;

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
					cell.state = ButtonState.Ring;

					GameObject obj = (currentPlayer == player1) ? objectPool.GetWhiteRing() : objectPool.GetBlackRing();
					obj.SetActive(true);
					Ring ring = obj.GetComponent<Ring>();
					currentPlayer.AddRing(ring);
					mapManager.AddRingToCell(ring, cell);
					currentPlayer = GetNextPlayer();

					if (player1.ringCount == 5 && player2.ringCount == 5)
					{
						state = GameState.ProcessState;
						lastClicked = null;
						break;
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
				if (cell.isStoneState) return;

				if (cell.isEmptyState && lastClicked != null && lastClicked.isRingState)
				{
					mapManager.MoveRingToCell(lastClicked, cell, (currentPlayer == player1));
					currentPlayer = GetNextPlayer();
					cell.ring.state = RingState.Idle;
					lastClicked = null;
					mapManager.DisableAllButtons();

					fiveStoneList = mapManager.checkFiveStones();
					if (fiveStoneList.Count > 0)
					{
						if (currentPlayer.ringCount == 1)
						{
							state = GameState.EndState;
							break;
						}
						// Do Button Select
						fiveStoneManager.LoadSelect(fiveStoneList);

						// And Then Pick Ring
						state = GameState.PickRingState;
					}
					break;
				}
				else if (cell.isRingState)
				{
					// cancel current ring
					if (lastClicked == cell)
					{
						lastClicked.ring.state = RingState.Idle;
						lastClicked = null;
						break;
					}
					// if last clicked is ring state
					if (lastClicked != null && lastClicked.isRingState && lastClicked.ring.isSelected)
					{
						lastClicked.ring.state = RingState.Idle;
					}

					cell.ring.state = RingState.Selected;
				}
				lastClicked = cell;
				break;
			case GameState.PickRingState:
				if (!cell.isRingState)
					return;

				// remove lastClicked ring
				if (lastClicked == cell)
				{
					currentPlayer.RemoveRing(cell.ring);
					currentPlayer = GetNextPlayer();
					lastClicked = null;

					state = GameState.ProcessState;
				}
				else
				{
					lastClicked.ring.state = RingState.Idle;
					cell.ring.state = RingState.Selected;

					lastClicked = cell;
				}
				break;
		}

		SetupStatusText();
		SetupMap(lastClicked);
	}

	public void ReturnToRingSelectState(int index)
	{
		fiveStoneList[index].ForEach(cell => {
			cell.state = ButtonState.Empty;
		});
	}

	private void SetupStatusText()
	{
		string gameState = "";
		switch (state)
		{
			case GameState.SetupState:
				gameState = "Setup State";
				break;
			case GameState.ProcessState:
				gameState = "Process State";
				break;
			case GameState.PickRingState:
				gameState = "Pick Ring State";
				break;
			case GameState.EndState:
				gameState = "End State";
				break;
		}
		string playerState = (currentPlayer == player1) ? "Player1 Turn" : "Player2 Turn";

		statusText.text = gameState + "\n" + playerState;
	}

	private void SetupMap(ButtonCell cell)
	{
		switch (state)
		{
			case GameState.SetupState:
				break;
			case GameState.ProcessState:
				mapManager.DisableAllButtons();
				currentPlayer.EnableRingCells();
				if (cell != null && cell.isRingState)
					mapManager.ActivatePossibleButtons(cell);
				break;
		}
	}

	private Player GetNextPlayer()
	{
		return (currentPlayer == player1) ? player2 : player1;
	}
}
