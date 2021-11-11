using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    private CombatMelee parentCombatMelee;
    private bool isParentDefender;

    public void Start()
    {
        parentCombatMelee = transform.parent.gameObject
            .GetComponent<CombatMelee>();
        isParentDefender = transform.parent.gameObject
            .GetComponent<GameUnit>().GetIsDefender();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        AttackRange unitInRange = collision.gameObject.GetComponent<AttackRange>();
        if (unitInRange != null)
        {
            GameUnit collisionUnit = unitInRange.transform.parent.gameObject.
                GetComponent<GameUnit>();
            if (collisionUnit != null)
            {
                if(isParentDefender != collisionUnit.GetIsDefender())
                {
                    parentCombatMelee.StartMeleeCombat(collisionUnit.gameObject);
                }
                else
                {
                    CombatMelee collisionUnitMelee = collisionUnit.GetComponent<CombatMelee>();
                    if(collisionUnitMelee.OnCombat || collisionUnitMelee.OnIdle)
                        parentCombatMelee.StartMeleeFriendlyQueue();
                }
            }
        }
    }


    void OnTriggerExit2D(Collider2D collision)
    {
        AttackRange unitInRange = collision.gameObject.GetComponent<AttackRange>();
        if (unitInRange != null)
        {
            GameUnit collisionUnit = unitInRange.transform.parent.gameObject.
                GetComponent<GameUnit>();
            if (collisionUnit != null)
            {
                if (isParentDefender != collisionUnit.GetIsDefender())
                {
                    parentCombatMelee.EndMeleeCombat();
                }
                else
                {
                    parentCombatMelee.EndMeleeFriendlyQueue();
                }
            }
        }
    }
}
