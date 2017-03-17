using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TroopManagement;

public class TravelUIManager : MonoBehaviour {

    public static TravelUIManager instance;
    [SerializeField]
    GameObject newTroopButton;
    [SerializeField]
    Transform troopButtonContainer;
    [SerializeField]
    RectTransform hoverInfo;
    public Dictionary<TroopStats, TroopButton> totalButtons = new Dictionary<TroopStats, TroopButton>();
    public TroopInfoPanel troopInfoPanel;
    public GameObject troopPanel;
    public bool comparing = false;
    TravelModeManager tmManager;

    void Start()
    {
        instance = this;
        Party.onPlayerPartyAdd += UpdateUI;
        Party.onPlayerPartyRemove += UpdateUI;
        tmManager = GetComponent<TravelModeManager>();
    }

    void OnDestroy()
    {
        Party.onPlayerPartyRemove -= UpdateUI;
        Party.onPlayerPartyAdd -= UpdateUI;
    }

    public void OnMouseEnterBoat(Party _partyInfo)
    {
        hoverInfo.GetComponentInChildren<Text>().text = _partyInfo.partyName += "\n" + "Troops: " + _partyInfo.TotalMembers.ToString();
    }

    public void OnMouseOverBoat()
    {

    }

    public void OnMouseExitBoat()
    {

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
                    UpdateUI(_party, _changedTroop);
                    return;
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
        print("Removing: " + _troop.troopName);
        TroopButton _button;
        totalButtons.TryGetValue(_troop, out _button);
        Destroy(_button.gameObject);
        totalButtons.Remove(_troop);
    }
}
