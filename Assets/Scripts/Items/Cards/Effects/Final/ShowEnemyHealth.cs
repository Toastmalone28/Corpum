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
        foreach (Room room in RoomController.instance.loadedRooms)
        {
            foreach (var enemy in room.enemies)
            {
                showHealth.AddListener(enemy.GetComponent<EnemyBehaviour>().changeHealthVisibility);
            }
        }
        showHealth.Invoke();
    }
}
