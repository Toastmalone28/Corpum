using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GunBehaviour : MonoBehaviour
{

    public GameObject projectile;
    public Transform barrel;
    private Animator character;
    public GameObject orientation;
    //private int ammo = 10;
    private WeaponStates weaponState;
    private float reloadTime = 3.3f;
    private float shootTime = 0.5f;

    IEnumerator ShotTimer()
    {
        float timer = 0f;

        while (timer < shootTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        weaponState = WeaponStates.Ready;
    }
    IEnumerator ReloadTimer()
    {
        float timer = 0f;

        while (timer < reloadTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        weaponState = WeaponStates.Ready;
    }
    // Start is called before the first frame update
    void Start()
    {
        character = gameObject.GetComponentInChildren<Animator>();
        weaponState = WeaponStates.Ready;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && GameManager.instance.gunStats[StatsGun.clipCapacity] > 0 && weaponState == WeaponStates.Ready)
        {
            GameObject.Instantiate(projectile, barrel.position, orientation.transform.rotation);
            character.SetTrigger("shoot");
            GameManager.instance.gunStats[StatsGun.clipCapacity]--;
            weaponState = WeaponStates.Shooting;
            StartCoroutine(ShotTimer());
        }

        if (Input.GetKeyDown(KeyCode.R) && weaponState == WeaponStates.Ready && GameManager.instance.gunStats[StatsGun.clipCapacity] < 10) 
        {
            character.SetTrigger("reload");
            GameManager.instance.gunStats[StatsGun.clipCapacity] = 10;
            weaponState = WeaponStates.Reload;
            StartCoroutine(ReloadTimer());
        }

      /*  if (character.GetCurrentAnimatorStateInfo(0).IsName("Human FPS|Human FPSAction"))
        {
            weaponState = WeaponStates.Ready;
        }*/
      
        //Debug.Log(weaponState);
    }
}
