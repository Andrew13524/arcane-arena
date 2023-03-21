using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardPrefab : MonoBehaviour
{
    public Card Card;
    public Button Hitbox;

    public Image Artwork;
    public Text NameText;
    public Text DescriptionText;
    public Text ManaText;
    public Text AttackText;
    public Text HealthText;
    public float HoverSpeed;
    public float HoverHeight;
    public bool OnBattleGround;

    private Canvas cardCanvas;
    private Vector3 originalPosition;
    private Vector3 originalScale;
    private Vector3 hoverPosition;
    private Vector3 hoverScale;
    private bool isPointerOver;
    private bool hoverUp;
    private bool hoverDown;
    private bool originalPositionSet;

    private void Awake()
    {
        cardCanvas = gameObject.GetComponent<Canvas>();
    }
    private void Start()
    {
        originalScale = gameObject.transform.localScale;
        hoverScale = new Vector3(originalScale.x * 2, originalScale.y * 2, originalScale.z * 2);
        Artwork.sprite = Card.Artwork;
        NameText.text = Card.Name;
        DescriptionText.text = Card.Description;
        ManaText.text = Card.Mana;
        AttackText.text = Card.Attack;
        originalPositionSet = false;
        HealthText.text = Card.Health;
        isPointerOver = false;
        hoverUp = false;
        hoverDown = false;
        OnBattleGround = false;
        Hitbox.onClick.AddListener(delegate { GameObject.FindGameObjectWithTag("GUIManager").GetComponent<GUIManager>().OnCard_Clicked(gameObject); });
    }
    public void OnPointerEnter()
    {
        if (OnBattleGround) return;

        if (!originalPositionSet)
        {
            originalPosition = gameObject.transform.localPosition;
            originalPositionSet = true;
        }
        cardCanvas.overrideSorting = true;

        isPointerOver = true;
        hoverPosition = new Vector3(originalPosition.x, originalPosition.y + HoverHeight, originalPosition.z);

        cardCanvas.sortingOrder = 1;
        hoverUp = true;
        StartCoroutine(HoverUp());
    }

    public void OnPointerExit()
    {
        if (OnBattleGround)
        {
            cardCanvas.overrideSorting = false;
            return;
        };

        cardCanvas.sortingOrder = 0;
        isPointerOver = false;
        cardCanvas.overrideSorting = false;
        hoverDown = true;
        StartCoroutine(HoverDown());
    }

    private IEnumerator HoverUp()
    {
        while (isPointerOver &&
              hoverUp &&
              !OnBattleGround &&
              Vector3.Distance(gameObject.transform.localPosition, hoverPosition) > 0.05f)
        {
            gameObject.transform.localPosition = Vector3.Lerp(gameObject.transform.localPosition, hoverPosition, HoverSpeed * Time.deltaTime);
            gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale, hoverScale, HoverSpeed * Time.deltaTime);
            yield return null;
        }
    }
    private IEnumerator HoverDown()
    {
        while (!isPointerOver &&
              hoverDown &&
              !OnBattleGround &&
              Vector3.Distance(originalPosition, gameObject.transform.localPosition) > 0.05f)
        {
            gameObject.transform.localPosition = Vector3.Lerp(gameObject.transform.localPosition, originalPosition, HoverSpeed * Time.deltaTime);
            gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale, originalScale, HoverSpeed * Time.deltaTime);
            yield return null;
        }
    }
    public IEnumerator ScaleDown()
    {
        while (Vector3.Distance(gameObject.transform.localScale, originalScale) > 0)
        {
            gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale, originalScale, HoverSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
