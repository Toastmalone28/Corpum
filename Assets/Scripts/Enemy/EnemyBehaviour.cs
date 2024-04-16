using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour
{
    public Dictionary<StatsEnemies, float> enemyStats;
    public GameObject player;
    public AIDestinationSetter destinationSetter;
    public float distanceToPlayer;

    public Image healthBar;
    public List<GameObject> targets;
    private void Awake()
    {
        InitializeEnemyStats();
        destinationSetter = GetComponent<AIDestinationSetter>();
        player = GameObject.FindWithTag("Player");
        healthBar.type = Image.Type.Filled;
    }
    // Start is called before the first frame update
    public void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        destinationSetter.target = player.transform;
        healthBar.transform.LookAt(player.transform.position);
    }

    private void InitializeEnemyStats()
    {
        enemyStats = new Dictionary<StatsEnemies, float>
        {
            { StatsEnemies.maxHitPoints, 100f },
            { StatsEnemies.hitPoints, 100f },
            { StatsEnemies.armor, 1f },
            { StatsEnemies.damage, 10f }
        };
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (GameManager.instance.playerStats[StatsPlayer.instakill] == 1f)
        {
            if (Random.value * 100 < 5)
            {
                Destroy(gameObject);
                Debug.Log("INSTAKILL!!!111!!");
                return;
            }
        }
        if (collision.gameObject.CompareTag("bullet"))
        {
            DealDamage(GameManager.instance.gunStats[StatsGun.damage]);
        }
    }
    public void changeHealthVisibility()
    {
        healthBar.gameObject.SetActive(!healthBar.gameObject.activeSelf);
    }

    public void DealDamage(float damage)
    {
        enemyStats[StatsEnemies.hitPoints] -= damage * 100 / (enemyStats[StatsEnemies.armor] + 100);
        healthBar.GetComponentsInChildren<Image>()[1].fillAmount = enemyStats[StatsEnemies.hitPoints] / 100f;
        if (enemyStats[StatsEnemies.hitPoints] <= 0)
        {
            Destroy(gameObject);
        }
    }
    public GameObject GetAbilityTrigger()
    {
        foreach(GameObject child in targets)
        {
            if(child.tag == "AbilityTrigger")
            {
                return child.gameObject;
            }
        }
        return null;
    }

    public void ScaleEnemyStats(float value)
    {
        enemyStats[StatsEnemies.armor] *= value;
        enemyStats[StatsEnemies.damage] *= value;
        enemyStats[StatsEnemies.maxHitPoints] *= value;

        transform.localScale *= value;
    }
}
