using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TroopManagement;

public class BattleUIManager : MonoBehaviour {

    [SerializeField]
    GameObject planningPanel;
    [SerializeField]
    Transform troopContent;
    [SerializeField]
    Transform shipContent;
    [SerializeField]
    GameObject newTroopButton;
    [SerializeField]
    GameObject newBoatButton;

    public void Init()
    {
        foreach (TroopStats ts in GameManager.instance.playerParty.party.members)
        {
            AddNewTroopButton(ts);
        }
        foreach (BoatStats bs in GameManager.instance.playerParty.party.boats)
        {
            AddNewBoatButton(bs);
        }
    }

    void AddNewTroopButton(TroopStats _ts)
    {
        print("Adding: " + _ts.troopName);
        GameObject _newButton = Instantiate(newTroopButton, troopContent);
        TroopButton _button = _newButton.GetComponent<TroopButton>();
        _button.Init(null, _ts);
        _newButton.transform.localScale = Vector3.one;
    }

    void AddNewBoatButton(BoatStats _bs)
    {
        print("Adding: " + _bs.boatName);
        GameObject _newButton = Instantiate(newBoatButton, shipContent);
        BoatButton _button = _newButton.GetComponent<BoatButton>();
        _button.Init(_bs);
        _newButton.transform.localScale = Vector3.one;
    }
}
