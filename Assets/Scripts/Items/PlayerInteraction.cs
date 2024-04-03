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
        UseActiveItem();
        UpdateItemCooldown();
    }

    private void UpdateItemCooldown()
    {
        foreach (Card_Object card in GetComponent<PlayerStats>().activeCards.Container)
        {
            card.coolDownTimer -= Time.deltaTime;
            Debug.Log(card.coolDownTimer);
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
        //Klasse Keybinds ist extrem langsam??
        if (Input.GetKeyDown(KeyCode.Alpha1)){
            if(GetComponent<PlayerStats>().activeCards.Container[0] != null
            && GetComponent<PlayerStats>().activeCards.Container[0].coolDownTimer <= 0.0f)
            {
                GetComponent<PlayerStats>().activeCards.Container[0].effect.Apply();
                GetComponent<PlayerStats>().activeCards.Container[0].coolDownTimer =
                GetComponent<PlayerStats>().activeCards.Container[0].effect.coolDown;
            }
        }

        if (Input.GetKeyDown(keybinds.active2))
        {
            if(GetComponent<PlayerStats>().activeCards.Container[1] != null
            && GetComponent<PlayerStats>().activeCards.Container[1].coolDownTimer <= 0.0f)
            {
                GetComponent<PlayerStats>().activeCards.Container[1].effect.Apply();
                GetComponent<PlayerStats>().activeCards.Container[1].coolDownTimer =
                GetComponent<PlayerStats>().activeCards.Container[1].effect.coolDown;
            }
        }
    }
}
