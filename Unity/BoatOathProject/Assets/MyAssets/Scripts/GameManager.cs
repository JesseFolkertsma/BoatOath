using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public enum GameState
    {
        Travelmode,
        Battlemode
    };

    public GameState gameState = GameState.Travelmode;

    public static GameManager instance;
    public PartyManager playerParty;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        playerParty = FindObjectOfType<PartyManager>();
    }

    public void EnterBattlemode()
    {

    }

    public void EnterTravelmode()
    {

    }

}
