using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator playerAnim;
    public static PlayerAnimations instance { get; private set; }
    private void Awake()
    {
        instance = this;
        playerAnim = GetComponent<Animator>();
    }
    public void Move(float z)
    {
        playerAnim.SetFloat("Move", z);
        
       
    }
    public void Attack()
    {
        playerAnim.SetTrigger("Attack");
       
    }
    public bool isPlaying(string stateName)
    {
        if (playerAnim.GetCurrentAnimatorStateInfo(0).IsName(stateName) &&
                playerAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            return true;
        else
            return false;
    }
}
