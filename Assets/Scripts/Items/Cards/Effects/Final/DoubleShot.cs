using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/DoubleShot")]
public class DoubleShot : Effect
{
    public override void Apply()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<GunBehaviour>().SetDoubleShot();
    }
}
