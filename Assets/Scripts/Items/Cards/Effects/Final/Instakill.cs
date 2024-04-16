using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Instakill")]
public class Instakill : Effect
{
    public override void Apply()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene!");
        }
        gameManager.GetComponent<GameManager>().playerStats[StatsPlayer.instakill] += 1f;
    }
}
