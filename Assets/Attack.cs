using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    /*    public enum AttackDirection
        {
            left, right
        }*/


    public float damage = 3;
    public Collider2D AttackCollider;
    Vector2 AttackOffset;

    private void Start()
    {
        AttackOffset = transform.position;
    }

 /*   public void attack()
    {
        switch (attackDirection)
        {
            case AttackDirection.left:
                attackLeft();
                break;
            case AttackDirection.right:
                attackRight();
                break;
        }
    }*/

    public void attackRight()
    {
        AttackCollider.enabled = true;
        transform.localPosition = AttackOffset;
    }

    public void attackLeft()
    {
        print("left hit!");
        AttackCollider.enabled = true;
        transform.localPosition = new Vector2(AttackOffset.x * -1, AttackOffset.y);
    }

    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.tag == "Enemy")
        {
            EnemyScript enemy = hit.GetComponent<EnemyScript>();
            if (enemy != null)
            {
                enemy.enemyHealth -= damage;
            }
        }
    }

    public void StopAttack()
    {
        AttackCollider.enabled = false;
    }
        
}
