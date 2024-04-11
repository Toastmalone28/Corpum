using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.Runtime.CompilerServices.RuntimeHelpers;

public class PlayerInteraction : MonoBehaviour
{
    public Keybinds keybinds;

    public Scene scene;
    private Camera main;
    private TextMeshProUGUI text;
    public GameObject canvas;
    public GameObject UIManager;
    private UserInterfaceController uiController;

    private void Awake()
    {
        main = GetComponentInChildren<Camera>();
        canvas = GameObject.FindGameObjectWithTag("Hud");
        text = canvas.GetComponentInChildren<TextMeshProUGUI>();
        text.enabled = false;
        scene = SceneManager.GetActiveScene();
        uiController = UIManager.GetComponent<UserInterfaceController>();
        //keybinds = GetComponent<Keybinds>();
    }
    void FixedUpdate()
    //event hinzufügen für performance reasons
    {
        ItemCheck();
        
        UpdateItemCooldown();
    }

    private void Update()
    {
        UseActiveItem();
    }

    private void UpdateItemCooldown()
    {
        foreach (Card_Object card in GetComponent<PlayerStats>().activeCards.Container)
        {
            card.coolDownTimer -= Time.deltaTime;
            //Debug.Log(card.coolDownTimer);
            UpdateUICooldown(card);
        }
        foreach (Card_Object card in GetComponent<PlayerStats>().passiveCards.Container)
        {
            card.coolDownTimer -= Time.deltaTime;
            UpdateUICooldown(card);
        }
    }
    public void UpdateUICooldown(Card_Object card)
    {
        card.fillAmount = 1 - Mathf.Clamp01(card.coolDownTimer / card.effect.coolDown);
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
                if (Input.GetKey(keybinds.interact))
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


    //sauber machen
    //sauber machen
    //sauber machen
    private void UseActiveItem()
    {
        PlayerStats stats = GetComponent<PlayerStats>();

        //Klasse Keybinds ist extrem langsam??
        if(stats.activeCards.Container.Count > 0)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Card_Object card1 = stats.activeCards.Container[0];
                Debug.Log("Input detected: 1");
                if (card1 != null
                && card1.coolDownTimer <= 0.0f)
                {
                    card1.effect.Apply();
                    card1.coolDownTimer =
                    card1.effect.coolDown;
                }
            }
        }
        

        if (Input.GetKeyDown(keybinds.active2))
        {
            Card_Object card2 = stats.activeCards.Container[1];
            if (card2 != null
            && card2.coolDownTimer <= 0.0f)
            {
                card2.effect.Apply();
                card2.coolDownTimer =
                card2.effect.coolDown;
            }
        }
    }
}
