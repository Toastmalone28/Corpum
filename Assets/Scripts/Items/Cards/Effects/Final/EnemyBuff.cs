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
        foreach(Room room in RoomController.instance.loadedRooms)
        {
            foreach (var enemy in room.enemies)
            {
                buffEnemies.AddListener(new UnityAction(() => { enemy.GetComponent<EnemyBehaviour>().ScaleEnemyStats(value); }));
            }
        }
        buffEnemies.Invoke();
    }
}

