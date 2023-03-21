using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Turn { PLAYER, ENEMY }
public class TurnManager : MonoBehaviour
{
    public PlayerArea PlayerArea;
    public EndTurnButton EndTurnButton;
    public static Turn Turn;

    private void Start()
    {
        Turn = Turn.PLAYER;
    }

    public void OnTurnEnd()
    {
        StartCoroutine(ExecuteEnemyTurn());
    }

    private IEnumerator ExecuteEnemyTurn()
    {
        Turn = Turn.ENEMY;
        yield return new WaitForSeconds(3f);
        StartPlayerTurn();
    }
    private void StartPlayerTurn()
    {
        Turn = Turn.PLAYER;
        PlayerArea.IncreaseMaxMana();
        PlayerArea.RestoreMana();
        EndTurnButton.ResetSprite();
        PlayerArea.DrawCards(1);
    }
   
}
