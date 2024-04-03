using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New KeybindSet", menuName = "KeybindSet")]
public class Keybinds : ScriptableObject
{
    public KeyCode forward = KeyCode.W;
    public KeyCode back = KeyCode.S;
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;
    public KeyCode jump = KeyCode.Space;
    public KeyCode interact = KeyCode.E;

    public KeyCode shoot = KeyCode.Mouse0;
    public KeyCode reload = KeyCode.R;

    public KeyCode inventory = KeyCode.I;

    public KeyCode active1 = KeyCode.Alpha1;
    public KeyCode active2 = KeyCode.Alpha2;
}
