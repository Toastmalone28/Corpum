using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public int CountX;

    public int CountY;

    public int CountZ;

    public GameObject TheObject;

    private void OnValidate()
    {
        Apply();
    }

    private void Apply()
    {
        if (UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
            return;

        if (TheObject == null)
            return;

        Renderer renderer = TheObject.GetComponent<Renderer>();

        if  (renderer != null)
        {
            foreach(Transform t in transform)
            {
                    UnityEditor.EditorApplication.delayCall += () =>
                    {
                        DestroyImmediate(t.gameObject);
                };
            }
        }

        for (int i = 0; i < CountX; i++)
        {
            for (int j = 0; j < CountY; j++)
            {
                for (int k = 0; k < CountZ; k++)
                {
                   Vector3 pos = new Vector3(i * (transform.localPosition.x + renderer.bounds.size.x),
                   j * (transform.localPosition.y + renderer.bounds.size.y),
                   k * (transform.localPosition.z + renderer.bounds.size.z));

                    GameObject go = Instantiate(TheObject, pos, Quaternion.identity, transform);
                    go.name = TheObject.name + "_" + i + "_" + j + "_" + k;


                }
               
               
            }
            
        }
    }
}