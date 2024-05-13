using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : Weapon
{
    [Header("Numbers")]
    [SerializeField] float timeToDoAttack;
    [SerializeField] float distanceToDoAttack;

    [Space]
    [SerializeField] GameObject slashObject;

    float timeAttacked;
    float timeReloaded;
    bool isAttacking;
    bool canAttack;

    private void Update()
    {
        PerformAttack();
        ReloadAttack();       
    }

    void PerformAttack()
    {
        if (isAttacking)
        {
            SetColliderAndMesh(true);
            transform.localPosition += new Vector3((distanceToDoAttack / timeToDoAttack) * Time.deltaTime, 0, 0);
            timeAttacked += Time.deltaTime;

            if (timeAttacked >= timeToDoAttack)
            {
                timeReloaded = 0;
                isAttacking = false;
                canAttack = false;
            }
        }
        else
        {
            SetColliderAndMesh(false);
            transform.localPosition = new Vector3(-distanceToDoAttack / 2, transform.localPosition.y, transform.localPosition.z);
            timeAttacked = 0;
        }
    }

    void ReloadAttack()
    {
        if (!canAttack)
        {
            timeReloaded += Time.deltaTime;

            if (timeReloaded >= weaponHandler.reloadTime)
            {
                canAttack = true;
            }
        }
        else
        {
            timeReloaded = 0;
        }
    }

    public void StartAttack()
    {
        if (!canAttack) { return; }
        isAttacking = true;
    }

    void SetColliderAndMesh(bool enabled)
    {
        slashObject.SetActive(enabled);
    }
}
