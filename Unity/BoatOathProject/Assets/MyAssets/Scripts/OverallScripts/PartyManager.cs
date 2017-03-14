using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TroopManagement;

public class PartyManager : MonoBehaviour
{
    public Party party;
    public RelationshipManager relationManager;

    void Start()
    {
        relationManager.PopulateRelationships(party);
    }
}
