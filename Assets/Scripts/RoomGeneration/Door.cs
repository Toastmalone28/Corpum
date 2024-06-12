using System;
using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    public enum DoorType
    {
        left, right, top, bottom
    }

    public DoorType doorType;

    internal void ChangeState()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}