using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceController : MonoBehaviour
{
    public GameObject player;
    private Keybinds keybind;
    public GameObject inventoryMenu;
    //public GameObject deathScreen;
    public GameObject loadingScreen;
    public static UserInterfaceController instance;


    private void Awake()
    {
        instance = this;
        instance.keybind = player.GetComponent<PlayerInteraction>().keybinds;
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState state)
    {
        instance.inventoryMenu.SetActive(state == GameState.inventory);
        //instance.deathScreen.SetActive(state == GameState.defeat);
        if (state == GameState.loading || state == GameState.running)
            Loading(state);
    }

    private void Update()
    {
        OpenInventory();
    }

    private void Loading(GameState state)
    {
        loadingScreen.SetActive(state == GameState.loading);
    }

    private void OpenInventory()
    {
        if (Input.GetKeyDown(keybind.inventory))
        {
            if (GameManager.instance.gameState == GameState.running)
            {
                GameManager.instance.UpdateGameState(GameState.inventory);
            }
            else if (GameManager.instance.gameState == GameState.inventory)
            {
                GameManager.instance.UpdateGameState(GameState.running);
            }
        }
    }
}
