using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileRange : MonoBehaviour
{
    private GameObject parent;
    private CombatRanged parentCombatRanged;
    private bool isParentDefender;

    void Start()
    {
        parent = transform.parent.gameObject;
        parentCombatRanged = parent.GetComponent<CombatRanged>();
        isParentDefender = parent.GetComponent<GameUnit>().GetIsDefender();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        AttackRange unitInRange = collision.gameObject.GetComponent<AttackRange>();
        if (unitInRange != null)
        {
            GameUnit collisionUnit = unitInRange.transform.parent.gameObject.GetComponent<GameUnit>();
            if (collisionUnit != null 
                && isParentDefender != collisionUnit.GetIsDefender()
                && !collisionUnit.IsInvisible)
            {
                parentCombatRanged.UnitOnRangeCombat();
            }
        }
    }


    void OnTriggerExit2D(Collider2D collision)
    {
        AttackRange unitInRange = collision.gameObject.GetComponent<AttackRange>();
        if (unitInRange != null)
        {
            GameUnit collisionUnit = unitInRange.transform.parent.gameObject.GetComponent<GameUnit>();
            if (collisionUnit != null && isParentDefender != collisionUnit.GetIsDefender())
            {
                parentCombatRanged.UnitOffRangeCombat();
            }
        }
    }
}
