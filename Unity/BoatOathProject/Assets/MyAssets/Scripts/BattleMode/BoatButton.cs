using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TroopManagement;

public class BoatButton : MonoBehaviour
{
    public BoatStats stats;
    public Image image;
    public Text nameText;

    public void Init(BoatStats _stats)
    {
        nameText = GetComponentInChildren<Text>();
        stats = _stats;
        nameText.text = _stats.boatName;
        image.sprite = _stats.sprite;
    }

    public void PressButton()
    {
        if (GameManager.instance.gameState == GameManager.GameState.Battlemode)
        {

        }
    }
}
