using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TroopManagement;
using System;

public class PartyMapContoller : MonoBehaviour, IEngageable {

    public float movementSpeed = .1f;

    Coroutine moveRoutine = null;

    public int troopCount
    {
        get
        {
            return GameManager.instance.playerParty.party.TotalMembers;
        }
    }

    public void Attack(Party _attacker)
    {
        GameManager.instance.EnterBattlemode(_attacker);
    }

    public void MoveTo(Vector3 _position)
    {
        if (moveRoutine != null)
            StopCoroutine(moveRoutine);

        moveRoutine = StartCoroutine(MoveLoop(_position));
    }

    IEnumerator MoveLoop(Vector3 _position)
    {
        float _dist = Vector3.Distance(transform.position, _position);
        while(_dist > .1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, _position, movementSpeed);
            _dist = Vector3.Distance(transform.position, _position);
            yield return null;
        }
        print("DestinationReached!");
    }
}
