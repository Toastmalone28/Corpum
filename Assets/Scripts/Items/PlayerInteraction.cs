using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class PlayerInteraction : MonoBehaviour
{
    public KeyCode interactionKey = KeyCode.E;
    public KeyCode inventoryKey = KeyCode.I;
    public KeyCode activeItemKey = KeyCode.Alpha1;
    public KeyCode activeItemKey2 = KeyCode.Alpha2;

    public Scene scene;
    private Camera main;
    private TextMeshProUGUI text;
    public GameObject canvas;
    public GameObject inventoryMenu;

    private float coolDownTimer1 = 0f;
    private float coolDownTimer2 = 0f;

    private void Awake()
    {
        main = GetComponentInChildren<Camera>();
        canvas = GameObject.FindGameObjectWithTag("Hud");
        text = canvas.GetComponentInChildren<TextMeshProUGUI>();
        scene = SceneManager.GetActiveScene();
    }
    void Update()
    //event hinzufügen für performance reasons
    {
        ItemCheck();
        OpenInventory();
        UseActiveItem();
        updateCoolDown();
    }

    private void updateCoolDown()
    {
        coolDownTimer1 -= Time.deltaTime;
        coolDownTimer2 -= Time.deltaTime;
    }

    private void ItemCheck()
    {
        if (Physics.Raycast(main.transform.position, main.transform.forward, out RaycastHit hit, 5f))
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
    private void OpenInventory()
    {
        if (Input.GetKeyDown(inventoryKey))
        {
            Debug.Log("Fortnite");
            inventoryMenu.SetActive(!inventoryMenu.activeSelf);
        }
    }

    //sauber machen 
    private void UseActiveItem()
    {
        if(Input.GetKeyDown(activeItemKey) 
            && GetComponent<PlayerStats>().activeCards.Container[0] != null 
            && coolDownTimer1 <= 0f) 
        {
            GetComponent<PlayerStats>().activeCards.Container[0].effect.Apply();
            coolDownTimer1 = GetComponent<PlayerStats>().activeCards.Container[0].effect.coolDown;
        }
        if (Input.GetKeyDown(activeItemKey2) 
            && GetComponent<PlayerStats>().activeCards.Container[1] != null 
            && coolDownTimer2 <= 0f)
        {
            GetComponent<PlayerStats>().activeCards.Container[1].effect.Apply();
            coolDownTimer2 = GetComponent<PlayerStats>().activeCards.Container[1].effect.coolDown;
        }
    }
}
