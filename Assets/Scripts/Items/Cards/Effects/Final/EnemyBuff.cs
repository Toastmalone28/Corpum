using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Effects/EnemyBuff")]
public class EnemyBuff : Effect
{
    public float value;
    public UnityEvent buffEnemies;
    public override void Apply()
    {
        foreach(var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            //buffEnemies.AddListener(enemy.GetComponent<EnemyBehaviour>().ScaleEnemyStats(value));
            buffEnemies.AddListener(new UnityAction(() => { enemy.GetComponent<EnemyBehaviour>().ScaleEnemyStats(value); }));
        }
        buffEnemies.Invoke();
    }
}

