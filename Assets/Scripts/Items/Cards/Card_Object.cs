using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "New Default Object", menuName = "Inventory System/Items/Default")]
public class Card_Object : ScriptableObject
{
    public string cardDescription;
    public string cardFlavortext;
    public float curseAmount;
    public float curseMax;
    public Rarity rarity;
    public GameObject prefab;
    public Effect effect;
    public float coolDownTimer = 0f;
    public float fillAmount;
}
