using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    Animator animator;

    void Update()
    {
        if(Input.GetKeyDown("mouse 0"))
        {
            Attack();
        }
    }

    void Attack()
    {
        animator.Play("Player2Attack");
    }
}
