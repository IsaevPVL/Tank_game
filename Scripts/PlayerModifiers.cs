using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerModifiers : MonoBehaviour
{
    public PlayerControl playerControl;


    public void ResetAll(){
        playerControl.maxLevel = 3;
    }

    [ContextMenu("UnlockMaxSpeed")]
    public void UnlockMaxSpeed(){
        playerControl.maxLevel = 4;
    }
}