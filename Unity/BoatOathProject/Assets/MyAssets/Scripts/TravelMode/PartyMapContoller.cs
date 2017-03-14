using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyMapContoller : MonoBehaviour {

    public float movementSpeed = .1f;

    Coroutine moveRoutine = null;

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
