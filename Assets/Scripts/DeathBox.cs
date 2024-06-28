using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        EnemyBehaviour currentEnemy = other.gameObject.GetComponentInParent<EnemyBehaviour>();
        if(currentEnemy != null)
            currentEnemy.UpdateEnemyState(EnemyStates.dying);
        if (other.tag == "Player")
        {
            GameManager.instance.UpdateGameState(GameState.defeat);
            return;
        }
    }
}
