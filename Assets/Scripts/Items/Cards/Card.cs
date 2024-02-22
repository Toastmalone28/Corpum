using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Card : MonoBehaviour
{
    public Card_Object card;
    public Effect effect;

    private Canvas canvas;
    private TMP_Text itemText;
    private GameObject player;

    public bool interactable;

    private void Awake()
    {
        canvas = GetComponentInChildren<Canvas>();
        itemText = canvas.GetComponentInChildren<TMP_Text>();
        itemText.text = card.name;
        itemText.enabled = false;
        player = GameObject.Find("Player");
    }

    void Update()
    {
        itemText.transform.LookAt(player.transform.position);
    }


    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactable = true;
            Debug.Log("Player entered pickup area");
            itemText.enabled = true;
        }
    }
    protected void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactable = false;
            Debug.Log("Player left pickup area");
            itemText.enabled = false;
        }
    }
    public void Interact()
    {
       if (card is Card_active)
        {
            if (player.GetComponent<PlayerStats>().activeCards.Container.Count < 2)
            {
                player.GetComponent<PlayerStats>().activeCards.AddItem(card);
            }
            else
            {
                Debug.Log("Player already has 2 active items");
                return;
            }
        }
        else
        {
            player.GetComponent<PlayerStats>().passiveCards.AddItem(card);
            effect.Apply();
        }
        Destroy(gameObject);
    }
}
