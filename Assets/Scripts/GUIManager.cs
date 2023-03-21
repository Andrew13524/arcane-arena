using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    public TurnManager TurnManager;
    public PlayerArea PlayerArea;

    public void OnCard_Clicked(GameObject card)
    {
        PlayerArea.PlayCard(card);
    }
    public void OnEndTurn_Clicked()
    {
        TurnManager.OnTurnEnd();
    }
}
