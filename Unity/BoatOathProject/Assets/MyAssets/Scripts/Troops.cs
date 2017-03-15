using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TroopManagement
{
    public static class TroopUtility
    {
        public static string GenerateRandomName()
        {
            string _newName = "New Name";
            _newName = GetRandomFirstName() + " " + GetRandomLastName();
            return _newName;
        }

        static string GetRandomFirstName()
        {
            List<string> _names = new List<string>();

            #region Firts Names
            _names.Add("Ron");
            _names.Add("Harry");
            _names.Add("Voldemort");
            _names.Add("Hagrid");
            _names.Add("Hermelien");
            _names.Add("Bart");
            #endregion

            return _names[UnityEngine.Random.Range(0,_names.Count)];
        }

        static string GetRandomLastName()
        {
            List<string> _names = new List<string>();

            #region Last Names
            _names.Add("Potter");
            _names.Add("Weasly");
            _names.Add("Bartolomeus");
            _names.Add("Folkertsma");
            _names.Add("The Slayer");
            _names.Add("Brainfart");
            #endregion

            return _names[UnityEngine.Random.Range(0, _names.Count)];
        }
    }

    [Serializable]
    public class Party
    {
        public List<TroopStats> members;

        public delegate void OnPartyAdded(Party _changedParty, TroopStats _added);
        public static event OnPartyAdded onPlayerPartyAdd;
        public delegate void OnPartyRemoved(Party _changedParty, TroopStats _removed);
        public static event OnPartyRemoved onPlayerPartyRemove;

        public int TotalMembers
        {
            get
            {
                return members.Count;
            }
        }

        public void AddNewMember(string _memberName, TroopType _type)
        {
            TroopStats _newMemberStats = new TroopStats(_type);
            foreach(TroopStats ts in members)
            {
                if(ts.troopName == _memberName)
                {
                    Debug.Log("Added troopname already exists, adding 'VI'");
                    _memberName += " VI";
                    break;
                }
            }
            _newMemberStats.troopName = _memberName;
            members.Add(_newMemberStats);
            if(onPlayerPartyAdd != null)
                onPlayerPartyAdd(this, _newMemberStats);

            #region ForSoldierList
            //switch (_type)
            //{
            //    case TroopType.RegularViking:
            //        RegularViking _newViking = new RegularViking();
            //        _newViking.stats = _stats;
            //        regularVikings.Add(_newViking);
            //        break;
            //    case TroopType.Beserker:
            //        Beserker _newBeserker = new Beserker();
            //        _newBeserker.stats = _stats;
            //        beserkers.Add(_newBeserker);
            //        break;
            //}
            #endregion
        }

        public void RemoveMemberByName(string _memberToRemove)
        {
            TroopStats _removed = null;
            foreach (TroopStats ts in members)
            {
                if (ts.troopName == _memberToRemove)
                {
                    _removed = ts;
                    members.Remove(ts);
                    break;
                }
            }
            if (onPlayerPartyRemove != null && _removed != null)
                onPlayerPartyRemove(this, _removed);
        }

        public void RemoveMember(TroopStats _memberToRemove)
        {
            members.Remove(_memberToRemove);
            if (onPlayerPartyRemove != null && _memberToRemove != null)
                onPlayerPartyRemove(this,_memberToRemove);
        }
    }

    public enum TroopType
    {
        RegularViking,
        Beserker
    };

    [Serializable]
    public class Relationship
    {
        int relation = 0;
        public int Relation
        {
            get
            {
                return relation;
            }
        }
        public void ImproveRelation(int _amount)
        {
            relation += _amount;
        }
    }

    [Serializable]
    public class RelationshipManager
    {
        public void PopulateRelationships(Party _party)
        {
            if (_party.members.Count > 0)
            {
                foreach (TroopStats ts in _party.members)
                {
                    if (ts.relations == null)
                        ts.relations = new Dictionary<TroopStats, Relationship>();
                    foreach (TroopStats ts2 in _party.members)
                    {
                        if (ts != ts2)
                        {
                            if(ts2.relations == null)
                                ts2.relations = new Dictionary<TroopStats, Relationship>();
                            if (!ts.relations.ContainsKey(ts2))
                            {
                                Relationship _newRe = new Relationship();
                                ts.relations.Add(ts2, _newRe);
                                ts2.relations.Add(ts, _newRe);
                            }
                        }
                    }
                }
            }
        }

        public void TroopAdded(Party _party, TroopStats _addedTroop)
        {
            _addedTroop.relations = new Dictionary<TroopStats, Relationship>();
            foreach(TroopStats ts in _party.members)
            {
                if(ts != _addedTroop)
                {
                    Relationship _newRe = new Relationship();
                    ts.relations.Add(_addedTroop, _newRe);
                    _addedTroop.relations.Add(ts, _newRe);
                }
            }
        }

        public void TroopRemoved(Party _party, TroopStats _removed)
        {
            foreach (TroopStats ts in _party.members)
            {
                if (ts != _removed)
                {
                    ts.relations.Remove(_removed);
                }
                else
                {
                    Debug.LogError("RelationshipManager: Removed troop is still in party?");
                }
            }
        }
    }

    [Serializable]
    public class TroopStats
    {
        [Header("BaseTroop stats")]
        public string troopName = "New Troop";
        public TroopType type;
        public int health = 100;
        public int damage = 10;
        public float attackRange = 2f;
        [Range(0, 100)]
        public int blockChance = 30;
        public Dictionary<TroopStats, Relationship> relations = new Dictionary<TroopStats, Relationship>();

        public TroopStats(string _name, TroopType _type, int _hp, int _dmg, float _range, int _blockChance)
        {
            troopName = _name;
            type = _type;
            health = _hp;
            damage = _dmg;
            attackRange = _range;

            if (_blockChance > 100) blockChance = 100;
            else if (_blockChance < 0) blockChance = 0;
            else blockChance = _blockChance;

            relations = new Dictionary<TroopStats, Relationship>();
        }

        public TroopStats(TroopType _type)
        {
            TroopStats _newStats = GetStatsForType(_type);

            troopName = _newStats.troopName;
            type = _newStats.type;
            health = _newStats.health;
            damage = _newStats.damage;
            attackRange = _newStats.attackRange;
            blockChance = _newStats.blockChance;

            relations = new Dictionary<TroopStats, Relationship>();
        }

        public TroopStats GetStatsForType(TroopType _type)
        {
            TroopStats _newStats = null;

            switch (_type)
            {
                case TroopType.RegularViking:
                    _newStats = new TroopStats("NewRegularViking", TroopType.RegularViking, 100, 10, 1, 40);
                    break;
                case TroopType.Beserker:
                    _newStats = new TroopStats("NewBeserker", TroopType.Beserker, 80, 25, 1, 10);
                    break;
            }

            if (_newStats != null)
                return _newStats;
            else
            {
                Debug.LogError("Requested trooptype not setup in the GetStatsForType method!");
                return null;
            }
        }
    }

    public abstract class BaseTroop : MonoBehaviour
    {
        public TroopStats stats;

        public void TryHit(float damage)
        {

        }

        public abstract void Attack();

        public void MoveTowards()
        {

        }
    }

    public class MeleeTroop : BaseTroop
    {
        public override void Attack()
        {
            throw new NotImplementedException();
        }
    }

    public class RegularViking : MeleeTroop
    {

    }

    public class Beserker : MeleeTroop
    {

    }
}
