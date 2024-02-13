using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public KeyCode interactionKey = KeyCode.E;
    private Camera main;
    public GameObject canvas;
    private TextMeshProUGUI text;

    private void Awake()
    {
        main = GetComponentInChildren<Camera>();
        canvas = GameObject.Find("Canvas");
        text = canvas.GetComponentInChildren<TextMeshProUGUI>();
    }
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(main.transform.position, main.transform.forward, out hit, 5f))
        {
            Card card = hit.collider.GetComponent<Card>();
            if (card != null)
            {
                Collider itemCollider = card.GetComponent<Collider>();
                if (itemCollider != null && hit.collider is BoxCollider)
                {
                    text.enabled = true;
                }
                if (Input.GetKeyDown(interactionKey))
                {
                    //card.Interact();
                }
            }
        }
        else
        {
            text.enabled = false;
        }
    }
        
}
