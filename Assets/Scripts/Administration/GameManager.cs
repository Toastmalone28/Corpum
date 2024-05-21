using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState gameState;
    public Dictionary<StatsPlayer, float> playerStats;
    public Dictionary<StatsGun, float> gunStats;

    public static event Action<GameState> OnGameStateChanged;

    // Start is called before the first frame update
    void Start()
    {
        instance = GetComponent<GameManager>();
        InitializePlayerStats();
        InitializeGunStats();
        //Change this to menu as soon as main menu + loading screen is ready
        UpdateGameState(GameState.running);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void InitializePlayerStats()
    {
        playerStats = new Dictionary<StatsPlayer, float>();
        gunStats = new Dictionary<StatsGun, float>();
        playerStats.Add(StatsPlayer.maxHitPoints, 100f);
        playerStats.Add(StatsPlayer.hitPoints, 100f);
        playerStats.Add(StatsPlayer.armor, 1f);
        playerStats.Add(StatsPlayer.movementSpeed, 10f);
        playerStats.Add(StatsPlayer.instakill, 0f);
    }

    private void InitializeGunStats()
    {
        gunStats = new Dictionary<StatsGun, float>();
        gunStats.Add(StatsGun.clipCapacity, 10f);
        gunStats.Add(StatsGun.damage, 15f);
        gunStats.Add(StatsGun.critChance, 5f);
        gunStats.Add(StatsGun.critDamage, 20f);
        gunStats.Add(StatsGun.swordDamage, 25f);
    }
    public void UpdateGameState(GameState state)
    {
        gameState = state;

        switch (state)
        {
            case GameState.menu:
                HandleMenu();
                break;
            case GameState.loading:
                HandleLoading();
                break;
            case GameState.running:
                HandleRunning();
                break;
            case GameState.paused:
                HandlePaused();
                break;
            case GameState.cutscene:
                HandleCutscene();
                break;
            case GameState.victory:
                HandleVictory();
                break;
            case GameState.defeat:
                HandleDefeat();
                break;
            case GameState.dialogue:
                HandleDialogue();
                break;
            case GameState.inventory:
                HandleInventory();
                break;
        }

        OnGameStateChanged?.Invoke(state);
    }

    private void HandleInventory()
    {
        Time.timeScale = 0f;
    }

    private void HandleDialogue()
    {
        throw new NotImplementedException();
    }

    private void HandleDefeat()
    {
        Time.timeScale = 0f;
    }

    private void HandleVictory()
    {
        throw new NotImplementedException();
    }

    private void HandleCutscene()
    {
        throw new NotImplementedException();
    }

    private void HandlePaused()
    {
        Time.timeScale = 0f;
    }

    private void HandleRunning()
    {
        Time.timeScale = 1.0f;
    }

    private void HandleLoading()
    {
        throw new NotImplementedException();
    }

    private void HandleMenu()
    {
        throw new NotImplementedException();
    }
}
