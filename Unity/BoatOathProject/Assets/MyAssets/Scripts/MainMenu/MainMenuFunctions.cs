using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TroopManagement;

public class MainMenuFunctions : MonoBehaviour {

    [SerializeField]
    GameObject newLevelPanel;
    [SerializeField]
    Text inputText;

    public void OpenNewLevelPanel()
    {
        newLevelPanel.SetActive(true);
    }

    public void CloseNewLevelPanel()
    {
        newLevelPanel.SetActive(false);
    }

    public void NewGame()
    {
        GameManager.instance.playerParty.party = new Party(inputText.text);
        SceneManager.LoadScene("MapScene");
    }

    public void LoadGame()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
