using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelModeManager : MonoBehaviour {
    
    public TravelModeControls controls;
    public TravelUIManager ui;

    void Start()
    {
        controls = GetComponent<TravelModeControls>();
        ui = GetComponent<TravelUIManager>();
        ui.PopulateUI(GameManager.instance.playerParty.party);
    }
}
