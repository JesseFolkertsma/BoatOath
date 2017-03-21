using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TroopManagement;
using XMLSaving;

public class GameManager : MonoBehaviour {

    public enum GameState
    {
        MainMenu = 1,
        Travelmode = 2,
        Battlemode = 3
    };

    public GameState gameState = GameState.Travelmode;

    public static GameManager instance;
    public PartyManager playerParty;
    public XMLSaver saver = new XMLSaver();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        playerParty = GetComponentInChildren<PartyManager>();
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

    public void SaveGame()
    {
        if (gameState == GameState.Travelmode)
        {
            PartySaveData _pData = new PartySaveData(playerParty.party, playerParty.transform.position);
            List<PartySaveData> _bots = new List<PartySaveData>();
            foreach(AIPartyManager ai in FindObjectsOfType<AIPartyManager>())
            {
                _bots.Add(new PartySaveData(ai.party, ai.transform.position));
            }
            saver.SaveGame(_pData, _bots);
        }
    }

    public void LoadGame()
    {
        SaveFile _saveFile = saver.LoadGame();
        playerParty.party = _saveFile.playerParty.partyData;
        playerParty.transform.position = _saveFile.playerParty.partyPosition;
    }
}
