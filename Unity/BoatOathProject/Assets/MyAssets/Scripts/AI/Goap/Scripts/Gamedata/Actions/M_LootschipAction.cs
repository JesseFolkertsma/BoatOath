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
        AIPartyManager closest = null;
        float dist = 0;

        foreach (AIPartyManager s in ships)
        {
            if (s == GetComponent<AIPartyManager>()) continue;
            if (closest == null)
            {
                closest = s;
                dist = Vector3.Distance(transform.position, s.transform.position);
            }
            else
            {
                float dist2 = Vector3.Distance(transform.position, s.transform.position);
                if (dist > dist2)
                {
                    closest = s;
                    dist = dist2;
                }
            }
        }
        GameObject _player = FindObjectOfType<PartyMapContoller>().gameObject;
        if (Vector3.Distance(transform.position, _player.transform.position) < Vector3.Distance(transform.position, closest.transform.position) || closest == null)
        {
            targetShip = _player.GetComponent<PartyMapContoller>();
            target = _player;
            return true;
        }
        if (closest != null)
        {
            targetShip = closest.GetComponent<AIPartyManager>();
            target = closest.gameObject;
        }

        return closest != null;
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
