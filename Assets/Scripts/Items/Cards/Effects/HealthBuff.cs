using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/HealthBuff")]
public class HealthBuff : Effect
{
    public float amount;

    public void Awake()
    {

    }

    public override void Apply()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene!");
        }
        gameManager.GetComponent<GameManager>().playerStats[StatsPlayer.hitPoints] += amount;
    }
}
