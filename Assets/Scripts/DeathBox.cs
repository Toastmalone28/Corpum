using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
       if(other.GetComponent<EnemyBehaviour>() != null)
        other.GetComponent<EnemyBehaviour>().UpdateEnemyState(EnemyStates.dying);
       else if (other.tag == "Player")
        {
            GameManager.instance.UpdateGameState(GameState.defeat);
        }
    }
}
