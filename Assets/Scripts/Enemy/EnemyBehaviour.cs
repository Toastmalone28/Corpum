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
    public EnemyStates enemyState;
    public Animator animator;
    public Image healthBar;
    public List<GameObject> targets;
    private float damageTimer = 0.2f;
    private bool isDamageable = true;
    private bool isBoss;

    public static event System.Action<EnemyStates> OnEnemyStateChanged;
    private void Awake()
    {
        InitializeEnemyStats();
        destinationSetter = GetComponent<AIDestinationSetter>();
        player = GameObject.FindWithTag("Player");
        healthBar.type = Image.Type.Filled;
        animator = GetComponentInChildren<Animator>();
        if (this.name.Contains("Chimera"))
            isBoss = true;
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
            { StatsEnemies.maxHitPoints, 30f },
            { StatsEnemies.hitPoints, 30f },
            { StatsEnemies.armor, 1f },
            { StatsEnemies.damage, 2.5f }
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
        if (collision.gameObject.CompareTag("bullet") && isDamageable)
        {
            DealDamage(GameManager.instance.gunStats[StatsGun.damage]);
            isDamageable = false;
            StartCoroutine(DamageTimer());
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerSword") && isDamageable)
        {
            DealDamage(GameManager.instance.gunStats[StatsGun.swordDamage]);
            isDamageable = false;
            Debug.Log("Hit");
            if (!isBoss)
            {
                ApplyKnockback();
            }
            
            StartCoroutine(DamageTimer());
            
        }
    }

    IEnumerator DamageTimer()
    {
        float timer = 0f;

        while (timer < damageTimer)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        isDamageable = true;
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
            UpdateEnemyState(EnemyStates.dying);
        }
        Debug.Log(StatsEnemies.hitPoints);
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
    public void UpdateEnemyState(EnemyStates state)
    {
        enemyState = state;

        switch (state)
        {
            case EnemyStates.spawning:
                HandleSpawning();
                break;
            case EnemyStates.active:
                HandleActive();
                break;
            case EnemyStates.inactive:
                HandleInactive();
                break;
            case EnemyStates.dying:
                HandleDying();
                break;
        }

        OnEnemyStateChanged?.Invoke(state);
    }
    private void HandleSpawning()
    {
        throw new System.NotImplementedException();
    }
    private void HandleActive()
    {
        throw new System.NotImplementedException();
    }

    private void HandleInactive()
    {
        throw new System.NotImplementedException();
    }

    private void HandleDying()
    {
        RoomController.instance.currRoom.enemies.Remove(this);
        Destroy(gameObject);
    }

    public void ApplyKnockback()
    {

        Vector3 knockbackDirection = this.transform.position - player.transform.position;
        knockbackDirection.Normalize();
        knockbackDirection.y += 0.2f;
        Vector3 targetPosition = this.transform.position + knockbackDirection * 1f;
        StartCoroutine(PerformKnockback(targetPosition));
    }

    private IEnumerator PerformKnockback(Vector3 targetPosition)
    {
        float elapsed = 0f;
        Vector3 initialPosition = this.transform.position;

        while (elapsed < 1f)
        {
            // Lerp zwischen der aktuellen Position und der Zielposition
            this.transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsed / 0.2f);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Stelle sicher, dass die Endposition exakt gesetzt wird
        this.transform.position = targetPosition;
    }

}
