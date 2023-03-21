using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using TMPro;
using System;

public class PlayerArea : MonoBehaviour
{
    public Player Player;
    public GameObject CardHolderPrefab;
    public GameObject BattleGround;
    public CardPrefab CardPrefab;
    public Text ManaText;

    private GridLayoutGroup cardsGrid;
    private List<Card> hand;
    private List<Card> drawPile;
    private int maxMana;
    private int currentMana;
    private float centerX;
    private float offset;

    private void Start()
    {
        hand = new List<Card>();
        drawPile = Player.Deck;
        centerX = gameObject.transform.localPosition.x;
        cardsGrid = CardHolderPrefab.GetComponent<GridLayoutGroup>();
        maxMana = 1;
        RestoreMana();
        DrawCards(1);
    }

    public void IncreaseMaxMana()
    {
        maxMana += 1;
        UpdateManaText();
    }
    public void DecreaseMana(int amount)
    {
        currentMana -= amount;
        UpdateManaText();
    }
    public void RestoreMana()
    {
        currentMana = maxMana;
        UpdateManaText();
    }
    public void PlayCard(GameObject card)
    {
        var battleGround = BattleGround.GetComponent<BattleGround>();
        if (battleGround.Cards.Count >= 5) return;

        var cardPrefab = card.GetComponent<CardPrefab>();
        if (currentMana >= Convert.ToInt32(cardPrefab.Card.Mana))
        {
            cardPrefab.OnBattleGround = true;
            card.transform.SetParent(BattleGround.transform);
            card.transform.localScale = new Vector3(1, 1, 1);
            battleGround.Cards.Add(cardPrefab.Card);
            hand.Remove(cardPrefab.Card);
            DecreaseMana(Convert.ToInt32(cardPrefab.Card.Mana));
        }
    }
    public void DrawCards(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            hand.Add(drawPile[Random.Range(0, drawPile.Count)]);
        }
        DestroyCards();
        SpawnCards();
        CenterCards();
    }

    private void DestroyCards()
    {
        foreach(Transform child in CardHolderPrefab.transform)
        {
            Destroy(child.gameObject);
        }
    }
    private void SpawnCards()
    {
        foreach (Card card in hand)
        {
            CardPrefab.Card = card;
            var instCard = Instantiate(CardPrefab);
            instCard.transform.SetParent(CardHolderPrefab.transform, false);
        }
    }
    private void CenterCards()
    {
        offset = (centerX + ((cardsGrid.spacing.x + cardsGrid.cellSize.x) * (hand.Count - 1))) / 2;
        CardHolderPrefab.transform.localPosition = new Vector3(centerX - offset, 0, 0);
    }
    private void UpdateManaText()
    {
        ManaText.text = currentMana + "/" + maxMana;
    }
}
