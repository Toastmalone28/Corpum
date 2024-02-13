using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public InventoryObject inventory;
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
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.playerStats[StatsPlayer.hitPoints] <= 0)
        {
            SceneManager.LoadScene(4, LoadSceneMode.Single);
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("bam");
        if (collision.gameObject.CompareTag("Enemy") && !isHit)
        {
            GameManager.instance.playerStats[StatsPlayer.hitPoints] -= 10; //hier später den jeweiligen Gegner DMG einfügen
            Debug.Log(GameManager.instance.playerStats[StatsPlayer.hitPoints]);
            isHit = true;
            StartCoroutine(HitTimer());
        }


    }
}
