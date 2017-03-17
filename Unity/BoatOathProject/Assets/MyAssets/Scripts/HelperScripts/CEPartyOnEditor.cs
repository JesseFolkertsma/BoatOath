using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PartyOnEditor))]
public class CEPartyOnEditor : Editor {
    
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PartyOnEditor myScript = (PartyOnEditor)target;
        if (GUILayout.Button("Add Troop"))
        {
            myScript.AddTroop();
        }
        if (GUILayout.Button("Randomize Name"))
        {
            myScript.RandomizeName();
        }
        if (GUILayout.Button("Add Boat"))
        {
            myScript.AddBoat();
        }
    }
}
