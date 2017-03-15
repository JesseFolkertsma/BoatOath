using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TroopManagement;

public class TravelUIManager : MonoBehaviour {

    [SerializeField]
    GameObject newTroopButton;
    [SerializeField]
    Transform troopButtonContainer;
    public Dictionary<TroopStats, TroopButton> totalButtons = new Dictionary<TroopStats, TroopButton>();
    public TroopInfoPanel troopInfoPanel;
    public GameObject troopPanel;
    public bool comparing = false;
    TravelModeManager tmManager;

    void Start()
    {
        Party.onPlayerPartyAdd += UpdateUI;
        tmManager = GetComponent<TravelModeManager>();
    }

    void OnDestroy()
    {
        Party.onPlayerPartyAdd -= UpdateUI;
    }

    public void ActivateTroopMenu()
    {
        bool isActive = troopPanel.activeInHierarchy;
        if (isActive)
        {
            troopPanel.SetActive(false);
            tmManager.controls.canMove = true;
        }

        else
        {
            troopPanel.SetActive(true);
            tmManager.controls.canMove = false;
        }
    }

    public void PopulateUI(Party _party)
    {
        foreach(TroopStats ts in _party.members)
        {
            AddNewButton(ts);
        }
    }

    public void UpdateUI(Party _party, TroopStats _changedTroop)
    {
        if(_party == GameManager.instance.playerParty.party)
        {
            foreach (TroopStats ts in totalButtons.Keys)
            {
                if (!_party.members.Contains(ts))
                {
                    RemoveTroop(ts);
                }
            }
            foreach (TroopStats ts in _party.members)
            {
                if (!totalButtons.ContainsKey(ts))
                {
                    AddNewButton(ts);
                }
            }
        }
    }

    void AddNewButton(TroopStats _ts)
    {
        print("Adding: " + _ts.troopName);
        GameObject _newButton = Instantiate(newTroopButton, troopButtonContainer.transform);
        TroopButton _button = _newButton.GetComponent<TroopButton>();
        _newButton.transform.localScale = Vector3.one;
        _button.Init(this, _ts);
        totalButtons.Add(_ts, _button);
    }

    void RemoveTroop(TroopStats _troop)
    {
        print("Removing: " + _troop);
        totalButtons.Remove(_troop);
    }
}
