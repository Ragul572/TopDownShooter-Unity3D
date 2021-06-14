using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchControl : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody rb;
    private float nextAttack = 0.0f;
    public float attackRate;
    public FixedJoystick fixedJoystick;
    public Transform firePoint;
    public GameObject projectilePrefab;
    public float projectileSpeed;

    private void Update()
    {
        MoveDirection();
        Move();
        IncreaseAttackTimer();
    }

    private void Move()
    {
        rb.velocity = new Vector3(-fixedJoystick.Vertical * moveSpeed, rb.velocity.y, fixedJoystick.Horizontal * moveSpeed);
        PlayerAnimations.instance.Move(rb.velocity.magnitude);
    }
    public void MoveDirection()
    {
        if(rb.velocity.magnitude > 1f)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }
      
    }
    public void Fire()
    { 
        if (nextAttack >= attackRate)
        {
            
            AudioManager.instance.PlayGunShot();
            nextAttack = 0f;
            PlayerAnimations.instance.Attack();
           
        }
    }
    private void LaunchProjectile()        //Called By the animation event in the shooting animation clip
    {
        GameObject temp = Instantiate(projectilePrefab, firePoint.position,this.transform.localRotation);
        temp.GetComponent<Rigidbody>().AddForce(this.transform.forward * projectileSpeed, ForceMode.Impulse);
        Destroy(temp, 3f);
    }
    
   
    private void IncreaseAttackTimer()
    {
        if(nextAttack >= 0f)
        nextAttack += Time.deltaTime;
    }

   
    
}
