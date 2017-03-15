using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TroopManagement;

public class TroopInfoPanel : MonoBehaviour {
    Text text;
    TroopStats troopStats;

    void Start()
    {
        text = GetComponentInChildren<Text>();
        gameObject.SetActive(false);
    }

    public void ShowInfo(TroopStats _stats, bool _comparing)
    {
        string _newText = "";
        if (!_comparing)
        {
            if (_stats == troopStats)
            {
                gameObject.SetActive(false);
                troopStats = null;
            }
            else
            {
                gameObject.SetActive(true);
                troopStats = _stats;
                _newText = TroopstatsToString(troopStats) + "\n" + "Hold left shift and select other troop to check relations!";
            }
        }
        else
        {
            if(troopStats == null)
            {
                _newText = "No other troop to compare with.";
            }
            else
            {
                print("compare");
                _newText = TroopstatsToString(troopStats) + "\n" + "Relation with " + _stats.troopName + ": " + troopStats.relations[_stats].Relation; 
            }
        }
        text.text = _newText;
    }

    string TroopstatsToString(TroopStats _stats)
    {
        string _newString;
        _newString = "Name: " + _stats.troopName + "\n" + "Troop Type: " + _stats.type.ToString() + "\n" + "Max Health: " + _stats.health.ToString() + "\n" + "Damage: " + _stats.damage.ToString() + "\n" + "Block percentage: " + _stats.blockChance.ToString();
        return _newString;
    }
}
