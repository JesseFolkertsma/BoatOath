using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TroopManagement;

public class M_LootschipAction : GoapAction
{ 
    bool looted = false;
    IEngageable targetShip = null;

    float startTime = 0;
    public float harvestDuration = 2;

    public M_LootschipAction()
    {
        AddEffect("GetLoot", true);
    }

    public override bool CheckProceduralPrecondition(GameObject _agent)
    {
        AIPartyManager[] ships = FindObjectsOfType<AIPartyManager>();
        GameObject closest = FindObjectOfType<PartyMapContoller>().gameObject;
        targetShip = closest.GetComponent<PartyMapContoller>();
        float dist = Vector3.Distance(transform.position, closest.transform.position);

        foreach (AIPartyManager s in ships)
        {
            if (s == GetComponent<AIPartyManager>()) continue;
            float dist2 = Vector3.Distance(transform.position, s.transform.position);
            if (dist > dist2)
            {
                closest = s.gameObject;
                targetShip = s;
                dist = dist2;
            }
        }
        target = closest.gameObject;

        if (dist <= GetComponent<AIPartyManager>().spotRange) return true;
        else return false;
    }

    public override bool IsDone()
    {
        return looted;
    }

    public override bool Preform(GameObject _agent)
    {
        if (startTime == 0)
            startTime = Time.time;

        if (Time.time - startTime > harvestDuration)
        {
            looted = true;
            targetShip.Attack(GetComponent<AIPartyManager>().party);
        }
        return true;
    }

    public override bool RequiresInRange()
    {
        return true;
    }

    public override void Reset()
    {
        looted = false;
        targetShip = null;
        startTime = 0;
    }
}
