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

    private void Awake()
    {
        keybind = player.GetComponent<PlayerInteraction>().keybinds;
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState state)
    {
        inventoryMenu.SetActive(state == GameState.inventory);
    }

    private void Update()
    {
        OpenInventory();
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
