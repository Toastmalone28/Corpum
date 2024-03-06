using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Dictionary<StatsPlayer, float> playerStats;
    public Dictionary<StatsGun, float> gunStats;

    // Start is called before the first frame update
    void Start()
    {
        instance = GetComponent<GameManager>();
        InitializePlayerStats();
        InitializeGunStats();
        
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
    }

    private void InitializeGunStats()
    {
        gunStats = new Dictionary<StatsGun, float>();
        gunStats.Add(StatsGun.clipCapacity, 10f);
        gunStats.Add(StatsGun.damage, 15f);
        gunStats.Add(StatsGun.critChance, 5f);
        gunStats.Add(StatsGun.critDamage, 20f);
    }
}
