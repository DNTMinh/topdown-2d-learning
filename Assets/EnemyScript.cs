using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float enemyhealth = 1;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

    }

    public float enemyHealth
    {
        set{
            enemyhealth = value;
            if(enemyhealth <= 0)
            {
                Defeated();
            }
        }
        get{ 
            return enemyhealth;
        }
    }

    private void Defeated()
    {
        animator.SetTrigger("defeated");
    }

    private void removeEnemy()
    {
        Destroy(gameObject);
    }
}
