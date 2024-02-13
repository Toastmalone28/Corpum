using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Card : MonoBehaviour
{
    public Card_Object card;
    private TextMeshPro itemText;
    private GameObject player;

    public bool interactable;

    private void Awake()
    {
        itemText = GetComponentInChildren<TextMeshPro>();
        itemText.text = card.name;
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
        Debug.Log("Fortnite battlepass");
    }
}
