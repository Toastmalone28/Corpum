using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Card : MonoBehaviour
{
    public Card_Object card;

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
        //add more logic later
        player.GetComponent<PlayerStats>().inventory.AddItem(card);
        Destroy(gameObject);
    }
}
