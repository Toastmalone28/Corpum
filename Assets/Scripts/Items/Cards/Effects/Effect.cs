using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : ScriptableObject
{
    public string effectDescription;
    public GameManager gameManager;
    public abstract void Apply();
}
