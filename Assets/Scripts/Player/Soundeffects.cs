using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundeffects : MonoBehaviour
{
    public AudioSource steps;

    // Update is called once per frame
    void Update()
    {
        if(!steps.isPlaying)
        {
            if (Input.GetKey(KeyCode.W) || 
                Input.GetKey(KeyCode.A) || 
                Input.GetKey(KeyCode.S) || 
                Input.GetKey(KeyCode.D))
            {
                steps.Play();
            }
        }
    }
}
