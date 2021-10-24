﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhipCombat : MonoBehaviour
{
    public int attackDamage = 1;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            coll.GetComponent<Skeleton>().TakeDamage(attackDamage);
        }
    }
}
