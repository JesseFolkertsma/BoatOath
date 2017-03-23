using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TroopManagement;

public class RegularViking : MeleeTroop {
    
	void Awake ()
    {
        BaseStart();
	}
	
	void Update ()
    {
        fsm.Update(gameObject);
	}
}
