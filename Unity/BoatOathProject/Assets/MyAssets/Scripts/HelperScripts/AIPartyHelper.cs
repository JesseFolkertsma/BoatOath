using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(AIPartyManager))]
public class AIPartyHelper : MonoBehaviour {

    public int createGroupSize = 10;

    public void Populate()
    {
        GetComponent<AIPartyManager>().PopulateParty(createGroupSize);
    }
}
