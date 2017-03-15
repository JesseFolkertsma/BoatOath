using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TroopManagement;

public class TroopButton : MonoBehaviour {

    public TravelUIManager manager;
    public TroopStats stats;
    public Image image;
    public Text nameText;

    public void Init(TravelUIManager _manager, TroopStats _stats)
    {
        nameText = GetComponentInChildren<Text>();
        manager = _manager;
        stats = _stats;
        nameText.text = _stats.troopName;
    }

    public void PressButton()
    {
        manager.troopInfoPanel.ShowInfo(stats, manager.comparing);
    }
}
