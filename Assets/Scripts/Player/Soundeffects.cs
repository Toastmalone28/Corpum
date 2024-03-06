using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundeffects : MonoBehaviour
{
    private AudioSource steps;
    // Start is called before the first frame update
    void Start()
    {
        steps = GetComponent<AudioSource>();
    }

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
