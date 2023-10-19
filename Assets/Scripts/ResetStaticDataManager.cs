using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStaticDataManager : MonoBehaviour
{
    private void Awake() 
    {
        Enemy.ResetStaticData();
        HeartObjectScript.ResetStaticData();
        BombSpawner.ResetStaticData();
        JumpPadScript.ResetStaticData();
        KeyBehaviourScript.ResetStaticData();
    }
}
