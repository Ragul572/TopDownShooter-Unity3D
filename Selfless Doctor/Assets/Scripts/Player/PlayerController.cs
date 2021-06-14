using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody rb;
    private float nextAttack = 0.0f;
    public float attackRate;
    


    public void Update()
    {
        CalculateMoveDirection();
        //CalculateMoveDirection();
        // Move(); Uncomment for pc
        Move();
        if (Input.GetKeyDown(KeyCode.F ) && nextAttack >= attackRate)
        {
            nextAttack = 0f;
            Attack();
        }
        nextAttack += Time.deltaTime;

    }
    
    private void Move()
    {
        float inputZ = Mathf.Abs(Input.GetAxis("Vertical"));
        //this.transform.Translate(Vector3.forward * inputZ * moveSpeed * Time.deltaTime);
        rb.velocity = transform.forward * inputZ * moveSpeed;
        PlayerAnimations.instance.Move(inputZ);
    }
    public void CalculateMoveDirection()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0f, 0f, 89));
        Vector3 direction = mousePos - transform.position;
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);

    }
    public void Attack()
    {
        PlayerAnimations.instance.Attack();
    }
   
}
