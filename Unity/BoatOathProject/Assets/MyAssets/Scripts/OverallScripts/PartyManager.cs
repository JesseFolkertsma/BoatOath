using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TroopManagement;

[System.Serializable]
public class PartyManager : MonoBehaviour
{
    public Party party;
    public RelationshipManager relationManager;

    void Start()
    {
        Party.onPlayerPartyAdd += relationManager.TroopAdded;
        Party.onPlayerPartyRemove += relationManager.TroopRemoved;
        relationManager.PopulateRelationships(party);
    }

    void OnDestroy()
    {
        Party.onPlayerPartyAdd -= relationManager.TroopAdded;
        Party.onPlayerPartyRemove -= relationManager.TroopRemoved;
    }
}
