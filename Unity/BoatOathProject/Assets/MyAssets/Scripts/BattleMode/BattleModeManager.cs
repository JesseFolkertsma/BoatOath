using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleModeManager : MonoBehaviour {

    public enum BattleState
    {
        Planning,
        Battle
    };

    public BattleState battleState = BattleState.Planning;

    BattleUIManager uiManager;

    void Start()
    {
        uiManager = GetComponent<BattleUIManager>();
        if(battleState == BattleState.Planning)
            uiManager.Init();
    }
}
