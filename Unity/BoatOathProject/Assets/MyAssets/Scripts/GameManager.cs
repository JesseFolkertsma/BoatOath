using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TroopManagement;

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

    public void EnterBattlemode(Party _target)
    {
        StartCoroutine(LoadBattleScene());
    }

    IEnumerator LoadBattleScene()
    {
        SceneManager.LoadScene("BattleScene", LoadSceneMode.Additive);
        yield return new WaitForEndOfFrame();
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("BattleScene"));
    }

    public void EnterTravelmode()
    {

    }

}
