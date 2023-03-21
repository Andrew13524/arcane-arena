using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndTurnButton : MonoBehaviour
{
    public Sprite Normal;
    public Sprite Highlighted;
    public Sprite Clicked;

    private Color transparentColor;
    private Color opaqueColor;

    private void Start()
    {
        transparentColor = new Color(0, 0, 0, 0);
        opaqueColor = new Color(255, 255, 255, 100);
    }

    public void OnPointerEnter()
    {
        if (TurnManager.Turn != Turn.PLAYER) return;
        gameObject.GetComponent<Image>().sprite = Highlighted;
    }
    public void OnPointerExit()
    {
        gameObject.GetComponent<Image>().sprite = Normal;
    }
    public void OnPointerDown()
    {
        gameObject.GetComponent<Image>().color = transparentColor; 
    }
    public void ResetSprite()
    {
        gameObject.GetComponent<Image>().color = opaqueColor;
        gameObject.GetComponent<Image>().sprite = Normal;
    }
}
