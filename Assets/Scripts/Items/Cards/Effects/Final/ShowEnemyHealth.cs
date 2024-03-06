using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Effects/ShowHealth")]
public class ShowEnemyHealth : Effect
{
    public UnityEvent showHealth;
    public override void Apply()
    {
        foreach(var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            showHealth.AddListener(enemy.GetComponent<EnemyBehaviour>().changeHealthVisibility);
        }
        showHealth.Invoke();
    }
}
