using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
[CustomEditor(typeof(AIPartyHelper))]
public class CEAIPartyHelper : Editor {
    
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        AIPartyHelper myScript = (AIPartyHelper)target;
        if (GUILayout.Button("Populate Party"))
        {
            myScript.Populate();
        }
    }
}
