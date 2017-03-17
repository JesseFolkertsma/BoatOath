using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TroopManagement;

[ExecuteInEditMode]
[RequireComponent(typeof(PartyManager))]
public class PartyOnEditor : MonoBehaviour {

    [Header ("New Troop stats")]
    public string newTroopName = "New Troop";
    public TroopType newTroopType;

    [Header("New boat stats")]
    public string newBoatName = "New Boat";
    public BoatType newBoatType;

    public void AddTroop()
    {
        GetComponent<PartyManager>().party.AddNewMember(newTroopName, newTroopType);
    }

    public void RandomizeName()
    {
        newTroopName = TroopUtility.GenerateRandomName();
    }

    public void AddBoat()
    {
        GetComponent<PartyManager>().party.AddNewBoat(newBoatName, newBoatType);
    }
}
