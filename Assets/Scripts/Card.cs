using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Card", menuName ="Card")]
public class Card : ScriptableObject
{
    public string Name;
    public string Description;
    public string Mana;
    public string Attack;
    public string Health;
    public Sprite Artwork;
}
