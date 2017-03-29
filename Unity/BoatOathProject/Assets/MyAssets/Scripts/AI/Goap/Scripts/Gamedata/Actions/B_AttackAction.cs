using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_AttackAction : GoapAction {

    bool hit = false;
    BattleTroop targetTroop = null;
    BattleTroop thisTroop = null;

    [SerializeField]
    bool hasStarted = false;
    bool attacked = false;

    float startTime = 0;
    float attackSpeed = 1;

    void Start()
    {
        reachDistance = GetComponent<BattleTroop>().stats.attackRange;
        thisTroop = GetComponent<BattleTroop>();
        //attackSpeed = thisTroop.stats.a
    }

    public B_AttackAction()
    {
        AddEffect("HitEnemy", true);
    }

    public override bool CheckProceduralPrecondition(GameObject _agent)
    {
        BattleTroop[] troops = FindObjectsOfType<BattleTroop>();
        BattleTroop closest = null;
        float dist = 0;

        foreach (BattleTroop bt in troops)
        {
            if (bt.side == thisTroop.side) continue;
            float dist2 = Vector3.Distance(transform.position, bt.transform.position);
            if(closest == null)
            {
                closest = bt;
                dist = dist2;
            }
            else if (dist > dist2)
            {
                closest = bt;
                targetTroop = bt;
                dist = dist2;
            }
        }

        if(closest != null) target = closest.gameObject;
        
        return closest != null;
    }

    public override bool IsDone()
    {
        return hit;
    }

    public override bool Preform(GameObject _agent)
    {
        if (!hasStarted)
        {
            hasStarted = true;
            thisTroop.anim.SetTrigger("Attack");
        }
        if (attacked)
        {
            if (startTime == 0)
                startTime = Time.time + attackSpeed;
            if (Time.time > startTime)
                hit = true;
        }
        return true;
    }

    public void Attacked()
    {
        attacked = true;
    }

    public override bool RequiresInRange()
    {
        return true;
    }

    public override void Reset()
    {
        hit = false;
        targetTroop = null;
        hasStarted = false;
        attacked = false;
        startTime = 0;
    }
}
