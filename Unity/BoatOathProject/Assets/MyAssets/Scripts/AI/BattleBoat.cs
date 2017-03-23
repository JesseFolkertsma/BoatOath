using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TroopManagement;

public class BattleBoat : MonoBehaviour {

    public BoatStats stats;
    public List<BattleBoat> connectedBoats = new List<BattleBoat>();
    public List<BaseTroop> troopsOnBoat = new List<BaseTroop>();
    public List<BaseTroop> ReachableTroops
    {
        get
        {
            List<BaseTroop> _bsList = troopsOnBoat;
            foreach(BattleBoat bb in connectedBoats)
            {
                foreach(BaseTroop bt in bb.troopsOnBoat)
                {
                    _bsList.Add(bt);
                }
            }
            return _bsList;
        }
    }

    public void ControlBoat()
    {

    }
}
