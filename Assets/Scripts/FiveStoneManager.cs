using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class FiveStoneManager : MonoBehaviour
{
    public MapManager mapManager;
    public GameObject prevButton;
    public GameObject nextButton;
    public GameObject confirmButton;
    public int stoneIndex = 0;
    public bool isActive = false;
    private List<List<ButtonCell>> fiveStoneList = new List<List<ButtonCell>>();

    public void LoadSelect(List<List<ButtonCell>> availableFiveStone)
    {
        isActive = true;
        stoneIndex = 0;
        fiveStoneList = availableFiveStone;
        
        ActiveFiveStone(true);
        if (fiveStoneList.Count == 1)
        {
            StartCoroutine("Wait");
        }
        else
        {
            prevButton.SetActive(true);
            nextButton.SetActive(true);
            confirmButton.SetActive(true);
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
        RemoveFiveStone();
    }

    public void RemoveFiveStone()
    {
        var fiveStone = fiveStoneList[stoneIndex];
        for (int i = 0; i < 5; ++i)
        {
            fiveStone[i].stoneHighlight.SetActive(false);
            fiveStone[i].state = ButtonState.Empty;
        }
        prevButton.SetActive(false);
        nextButton.SetActive(false);
        confirmButton.SetActive(false);
        GameManager.Instance.ReturnToRingSelectState(stoneIndex);
    }

    public void ActiveFiveStone(bool isActive)
    {
        var fiveStone = fiveStoneList[stoneIndex];
        for (int i = 0; i < 5; ++i)
        {
            fiveStone[i].stoneHighlight.SetActive(isActive);
        }
    }

    public void PrevClick()
    {
        if (!isActive) return;

        ActiveFiveStone(false);

        if (stoneIndex == 0)
            stoneIndex = fiveStoneList.Count - 1;
        else
            stoneIndex--;

        ActiveFiveStone(true);
    }

    public void NextClick()
    {
        if (!isActive) return;

        ActiveFiveStone(false);

        if (stoneIndex == fiveStoneList.Count - 1)
            stoneIndex = 0;
        else
            stoneIndex++;

        ActiveFiveStone(true);
    }

    public void ConfirmButton()
    {
        RemoveFiveStone();
    }
}