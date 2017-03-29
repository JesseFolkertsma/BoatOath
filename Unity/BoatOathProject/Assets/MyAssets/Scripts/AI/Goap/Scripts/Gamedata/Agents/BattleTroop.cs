using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TroopManagement;

public class BattleTroop : MonoBehaviour, IGoap
{
    public enum Side
    {
        Player,
        Enemy01
    };

    [Header("Troop Stats")]
    public TroopStats stats;

    [Header("Troop variables")]
    public Side side;
    public float moveSpeed = 1;
    public float attackSpeed;

    [HideInInspector]
    public Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public HashSet<KeyValuePair<string, object>> GetWorldState()
    {
        HashSet<KeyValuePair<string, object>> worldData = new HashSet<KeyValuePair<string, object>>();

        worldData.Add(new KeyValuePair<string, object>("HitEnemy", false));

        return worldData;
    }

    public HashSet<KeyValuePair<string, object>> CreateGoalState()
    {
        HashSet<KeyValuePair<string, object>> goal = new HashSet<KeyValuePair<string, object>>();

        goal.Add(new KeyValuePair<string, object>("HitEnemy", true));

        return goal;
    }

    public bool MoveAgent(GoapAction nextAction)
    {
        if (Vector3.Distance(transform.position, nextAction.target.transform.position) < nextAction.ReachDistance())
        {
            nextAction.SetInRange(true);
            return true;
        }
        else
        {
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, nextAction.target.transform.position, step);
            return false;
        }
    }

    #region Prettyprints

    public void PlanFailed(HashSet<KeyValuePair<string, object>> failedGoal)
    {
        Debug.Log("<color=red>Plan failed!</color> " + GoapAgent.PrettyPrint(failedGoal));
    }

    public void PlanFound(HashSet<KeyValuePair<string, object>> goal, Queue<GoapAction> action)
    {
        Debug.Log("<color=green>Plan found</color> " + GoapAgent.PrettyPrint(action, goal));
    }

    public void ActionsFinished()
    {
        Debug.Log("<color=blue>Actions completed</color>");
    }

    public void PlanAborted(GoapAction aborter)
    {
        Debug.Log("<color=red>Plan Aborted</color> " + GoapAgent.PrettyPrint(aborter));
    }
    #endregion
}
