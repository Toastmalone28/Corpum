using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    //Player is in the main Menu
    menu,
    //Loading screen
    loading,
    //Game is running
    running,
    //Game is paused after pressing the pause button
    paused,
    //Cutscene is playing
    cutscene,
    //End of the game has been reached
    victory,
    //Player has been defeated
    defeat,
    //Player entered dialogue with an NPC
    dialogue,
    //Player opened the inventory
    inventory
}
