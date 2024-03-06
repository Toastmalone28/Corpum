using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footsteps : MonoBehaviour
{
    public AudioSource Corpum_Footsteps, sprintSound;

    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                Corpum_Footsteps.enabled = false;
                sprintSound.enabled = true;
                sprintSound.Play();
            }
            else
            {
                Corpum_Footsteps.enabled = true;
                sprintSound.enabled = false;
            }
        }
        else
        {
            Corpum_Footsteps.enabled = false;
            sprintSound.enabled = false;
        }
    }
}