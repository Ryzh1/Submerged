﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAttack : MonoBehaviour
{
	public int attackDamage = 20;


	public Vector3 attackOffset;
	public float attackRange = 1f;
	public LayerMask attackMask;
	public bool attack;
    private void Update()
    {
		
        if (attack)
        {
			Attack();
			attack = false;
        }
    }

	//On animation key
    public void Attack()
	{
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

		Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
		if (colInfo != null)
		{

			colInfo.GetComponent<PlayerMovement>().TakeDamage(attackDamage);
		}
	}



	void OnDrawGizmosSelected()
	{
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

		Gizmos.DrawWireSphere(pos, attackRange);
	}
}

