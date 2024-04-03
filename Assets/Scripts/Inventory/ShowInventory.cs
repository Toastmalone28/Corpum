using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShowInventory : MonoBehaviour
{
    public InventoryObject inventory;

    public int X_START, Y_START;
    public int X_SPACE_BETWEEN_ITEM, Y_SPACE_BETWEEN_ITEM, NUMBER_OF_COLUMN;
    Dictionary<Card_Object, GameObject> itemsDisplayed = new Dictionary<Card_Object, GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        CreateDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDisplay();
        UpdateUICooldown();
    }

    private void UpdateUICooldown()
    {
        foreach(KeyValuePair<Card_Object,GameObject> item in itemsDisplayed)
        {
            item.Value.GetComponent<Image>().fillAmount = item.Key.fillAmount;
        }
    }

    public void UpdateDisplay()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            if (!itemsDisplayed.ContainsKey(inventory.Container[i]))
            {
                var obj = Instantiate(inventory.Container[i].prefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                //obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].name;
                UpdateCardText(obj, i);
                itemsDisplayed.Add(inventory.Container[i], obj);
            }
        }
    }

    private void UpdateCardText(GameObject obj, int index)
    {
        foreach (var item in obj.GetComponentsInChildren<TextMeshProUGUI>())
        {
            switch (item.name)
            {
                case "Name":
                    item.text = inventory.Container[index].name;
                    break;
                case "Description":
                    item.text = inventory.Container[index].cardDescription;
                    break;
                case "Lore":
                    item.text = inventory.Container[index].cardFlavortext;
                    break;
            }
        }
    }

    public void CreateDisplay()
    {
        for (int i = 0; i < inventory.Container.Count; i++) 
        {
            var obj  = Instantiate(inventory.Container[i].prefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].name;

        }
    }
    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + X_SPACE_BETWEEN_ITEM *(i % NUMBER_OF_COLUMN), Y_START + (-Y_SPACE_BETWEEN_ITEM * (i/NUMBER_OF_COLUMN)), 0f);
    }
}