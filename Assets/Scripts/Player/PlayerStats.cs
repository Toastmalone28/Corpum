using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public InventoryObject passiveCards;
    public InventoryObject activeCards;
    public Image healthbar;
    public float iframes = 0.5f;
    private bool isHit;
    IEnumerator HitTimer()
    {
        float timer = 0f;

        while (timer < iframes)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        isHit = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Add event for health changes!!!
        if (GameManager.instance.playerStats[StatsPlayer.hitPoints] <= 0)
        {
            GameManager.instance.UpdateGameState(GameState.defeat);
        }
        //Add event for health changes!!!
        healthbar.fillAmount = GameManager.instance.playerStats[StatsPlayer.hitPoints] / 100f;
    }
    public void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("bam");
        if (collision.gameObject.CompareTag("Enemy") && !isHit)
        {
            GameManager.instance.playerStats[StatsPlayer.hitPoints] -= 10; //hier später den jeweiligen Gegner DMG einfügen
            isHit = true;
            StartCoroutine(HitTimer());
        }
        if (collision.gameObject.CompareTag("BossBullet") && !isHit)
        {
            GameManager.instance.playerStats[StatsPlayer.hitPoints] -= 5;
            isHit = true;
            StartCoroutine(HitTimer());
        }
    }
    private void OnApplicationQuit()
    {
        passiveCards.Container.Clear();
        activeCards.Container.Clear();
    }

}
