using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TroopManagement;
using System;

[RequireComponent(typeof(GoapAgent))]
public class AIPartyManager : MonoBehaviour, IEngageable, IGoap {
    public Party party;
    
    [Header("AI Variables")]
    public float movementSpeed = .1f;
    public float spotRange = 15f;

    public int troopCount
    {
        get
        {
            return party.TotalMembers;
        }
    }

    public void PopulateParty(int _groupSize)
    {
        party.partyName = TroopUtility.GenerateRandomRaiderpartyName();
        party.AddNewBoat("BoatName", BoatType.TestBoat);
        for (int i = 0; i < _groupSize; i++)
        {
            party.AddNewMember(TroopUtility.GenerateRandomName(), TroopType.RegularViking);
        }
    }

    public void Attack(Party _attacker)
    {
        Debug.Log("Oh noes! I gots attacked by " + _attacker + "!");
    }

    void OnMouseEnter()
    {

    }

    void OnMouseOver()
    {

    }

    void OnMouseExit()
    {

    }

    public HashSet<KeyValuePair<string, object>> CreateGoalState()
    {
        HashSet<KeyValuePair<string, object>> goal = new HashSet<KeyValuePair<string, object>>();

        goal.Add(new KeyValuePair<string, object>("GetLoot", true));

        return goal;
    }

    public HashSet<KeyValuePair<string, object>> GetWorldState()
    {
        HashSet<KeyValuePair<string, object>> worldData = new HashSet<KeyValuePair<string, object>>();

        //worldData.Add(new KeyValuePair<string, object>("HasFood", true));

        return worldData;
    }

    public bool MoveAgent(GoapAction nextAction)
    {
        //navAgent.Resume();
        //navAgent.SetDestination(nextAction.target.transform.position);
        transform.position = Vector3.MoveTowards(transform.position, nextAction.target.transform.position, movementSpeed);

        if (Vector3.Distance(transform.position, nextAction.target.transform.position) < nextAction.ReachDistance())
        {
            nextAction.SetInRange(true);
            //navAgent.Stop();

            return true;
        }
        else
            return false;
    }
    #region Plan failed/finished
    public void PlanAborted(GoapAction aborter)
    {
        Debug.Log("<color=red>Plan Aborted</color> " + GoapAgent.PrettyPrint(aborter));
    }

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
    #endregion
}
