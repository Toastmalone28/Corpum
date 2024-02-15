using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteraction : MonoBehaviour
{
    public KeyCode interactionKey = KeyCode.E;
    public KeyCode activeItemKey = KeyCode.Q;

    public Scene scene;
    private Camera main;
    private TextMeshProUGUI text;
    public GameObject canvas;
    public GameObject inventoryMenu;

    private void Awake()
    {
        main = GetComponentInChildren<Camera>();
        canvas = GameObject.FindGameObjectWithTag("Hud");
        text = canvas.GetComponentInChildren<TextMeshProUGUI>();
        scene = SceneManager.GetActiveScene();
    }
    void Update()
    {
        itemCheck();
        openInventory();
    }

    private void itemCheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(main.transform.position, main.transform.forward, out hit, 5f))
        {
            Card card = hit.collider.GetComponentInParent<Card>();
            if (card != null)
            {
                Collider itemCollider = card.GetComponent<Collider>();
                if (itemCollider != null)
                {
                    text.enabled = true;
                }
                if (Input.GetKey(interactionKey))
                {
                    card.Interact();
                }
            }
        }
        else
        {
            text.enabled = false;
        }
    }
    private void openInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("Fortnite");
            inventoryMenu.gameObject.SetActive(!inventoryMenu.gameObject.activeSelf);
        }
    }
}
