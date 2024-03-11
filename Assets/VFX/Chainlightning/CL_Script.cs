using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CL_Script : MonoBehaviour
{
    public GameObject Sparkball;
    public GameObject Lightning;

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.F))
        {

            Vector3 Startlocation = transform.forward;
            Quaternion Q = transform.rotation * Quaternion.Euler(90, 0, 0);

           // GameObject attack = Instantiate((GameObject)AssetDatabase.LoadAssetAtPath(Sparkball / Lightning, typeof(GameObject)), Startlocation, Q);


            Plane playerplane = new Plane(Vector3.up, transform.position);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float hitdist;

            if (playerplane.Raycast(ray, out hitdist))
            {
                Vector3 targetpoint = ray.GetPoint(hitdist);
            }

        }
    }
}
