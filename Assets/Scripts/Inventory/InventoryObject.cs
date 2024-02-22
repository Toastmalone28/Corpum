using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Inventory", menuName="Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public List<Card_Object> Container = new();
    public void AddItem(Card_Object _card)
    {
        //Bedingungen zum hinzufügen implementieren
        Container.Add(_card);
    }
}
