using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    public GameObject itemPosition;
    public InventoryObject availableItems;
    public List<GameObject> prefabs;

    private GameObject spawnedItem;
    private void Awake()
    {
        int value = Random.Range(0, 100);
        if(value >= 0 && value <= 40)
        {
            SpawnPrefab(prefabs[0]);
            Activate(Rarity.common);
        }
        if (value >= 41 && value <= 70)
        {
            SpawnPrefab(prefabs[1]);
            Activate(Rarity.rare);
        }
        if (value >= 71 && value <= 90)
        {
            SpawnPrefab(prefabs[2]);
            Activate(Rarity.epic);

        }
        if (value >= 91 && value <= 100)
        {
            SpawnPrefab(prefabs[3]);
            Activate(Rarity.legendary);
        }
    }
    public Card_Object PickRandomItem(Rarity rarity)
    {
        List<Card_Object> filteredItems = new List<Card_Object>();

        foreach (Card_Object item in availableItems.Container)
        {
            if (item.rarity == rarity)
            {
                filteredItems.Add(item);
            }
        }
        if (filteredItems.Count == 0)
        {
            Debug.LogWarning("No items found with the specified type: " + rarity);
            return null;
        }

        int randomIndex = Random.Range(0, filteredItems.Count);
        return filteredItems[randomIndex];
    }
    public void SpawnPrefab(GameObject prefab)
    {
        spawnedItem = Instantiate(prefab, itemPosition.transform.position, itemPosition.transform.rotation, itemPosition.transform);
    }
    public void Activate(Rarity rarity)
    {
        Card temp = spawnedItem.GetComponent<Card>();
        temp.card = PickRandomItem(rarity);
        temp.Activate();
    }
}
