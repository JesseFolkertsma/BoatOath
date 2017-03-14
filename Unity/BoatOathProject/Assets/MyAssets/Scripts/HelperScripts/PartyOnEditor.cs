using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TroopManagement;

[ExecuteInEditMode]
[RequireComponent(typeof(PartyManager))]
public class PartyOnEditor : MonoBehaviour {

    public string newTroopName = "New Troop";
    public TroopType newTroopType;

    public void AddTroop()
    {
        GetComponent<PartyManager>().party.AddNewMember(newTroopName, newTroopType);
    }

    public void RandomizeName()
    {
        newTroopName = TroopUtility.GenerateRandomName();
    }
}
