using JetBrains.Annotations;
using System;
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
    public AudioSource shotSFX;

    //private int ammo = 10;
    private WeaponStates weaponState;
    private float reloadTime = 3.3f;
    private float shootTime = 0.5f;
    private float swordTime = 1.6f;
    private int amountToShoot = 1;


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
    IEnumerator SwordTimer()
    {
        float timer = 0f;

        while (timer < swordTime)
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
            StartCoroutine(Shoot());
        }

        if (Input.GetKeyDown(KeyCode.R) && weaponState == WeaponStates.Ready && GameManager.instance.gunStats[StatsGun.clipCapacity] < 10) 
        {
            Reload();
        }

        if (Input.GetMouseButtonDown(1))
        {
            Swordstrike();
        }
    }

    private void Swordstrike()
    {
        character.SetTrigger("swordstrike");
        weaponState = WeaponStates.Swordstrike;
        StartCoroutine(SwordTimer());
    }

    public void Reload()
    {
        character.SetTrigger("reload");
        GameManager.instance.gunStats[StatsGun.clipCapacity] = 10;
        weaponState = WeaponStates.Reload;
        StartCoroutine(ReloadTimer());
    }
    public IEnumerator Shoot()
    {
        for (int i = 0; i < amountToShoot; i++)
        {
            Instantiate(projectile, barrel.position, orientation.transform.rotation);
            yield return new WaitForSeconds(0.1f);
        }
        character.SetTrigger("shoot");
        shotSFX.Play();
        //?
        GameManager.instance.gunStats[StatsGun.clipCapacity]--;
        weaponState = WeaponStates.Shooting;
        StartCoroutine(ShotTimer());
    }

    public void addMultiShot(int amount)
    {
        amountToShoot += amount;
    }

}
