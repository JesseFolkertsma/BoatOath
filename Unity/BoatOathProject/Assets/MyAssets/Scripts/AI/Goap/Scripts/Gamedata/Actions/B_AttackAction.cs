using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_AttackAction : GoapAction {

    bool hit = false;
    BattleTroop targetTroop = null;

    float startTime = 0;
    public float harvestDuration = 2;

    public B_AttackAction()
    {
        AddEffect("HitEnemy", true);
    }

    public override bool CheckProceduralPrecondition(GameObject _agent)
    {
        BattleTroop[] troops = FindObjectsOfType<BattleTroop>();
        GameObject closest = FindObjectOfType<PartyMapContoller>().gameObject;
        float dist = Vector3.Distance(transform.position, closest.transform.position);

        foreach (BattleTroop bt in troops)
        {
            if (bt.side == GetComponent<BattleTroop>().side) continue;
            float dist2 = Vector3.Distance(transform.position, bt.transform.position);
            if (dist > dist2)
            {
                closest = bt.gameObject;
                targetTroop = bt;
                dist = dist2;
            }
        }
        target = closest.gameObject;

        return closest != null;
    }

    public override bool IsDone()
    {
        return hit;
    }

    public override bool Preform(GameObject _agent)
    {
        if (startTime == 0)
            startTime = Time.time;

        if (Time.time - startTime > harvestDuration)
        {
            hit = true;
        }
        return true;
    }

    public override bool RequiresInRange()
    {
        return true;
    }

    public override void Reset()
    {
        hit = false;
        targetTroop = null;
        startTime = 0;
    }
}
