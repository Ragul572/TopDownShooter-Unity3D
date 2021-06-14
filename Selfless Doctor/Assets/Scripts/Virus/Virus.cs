using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum VirusState
{
    Moving,
    Attacking
}

public class Virus : MonoBehaviour
{
    public float virusHealth = 100;
    public float attackSpeed;
    public float moveSpeed;
    public float playerDetectRadius;
    public float attackDelayTime;
    private float currentAttackTime;
    private Rigidbody rb;
    public LayerMask whatisPlayer;
    private VirusState currentState = VirusState.Moving;
    private Transform goal;
    private Transform player;
    public GameObject burstVfx;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        goal = GameObject.FindGameObjectWithTag("Goal").transform;
       
    }


    public void Update()
    {
        Destroy();
        CheckForPlayer();
        switch (currentState)
        {
            case VirusState.Moving:
               MoveToGoal();
                break;
            case VirusState.Attacking:
                this.transform.LookAt(player.position, Vector3.up);
                Attack();
                break;
        }

    }

    private void Destroy()
    {
        if(virusHealth <= 0)
        {
            AudioManager.instance.PlayVirusBurst();
            GameObject temp = Instantiate(burstVfx, this.transform.position, Quaternion.identity);
            Destroy(temp, 0.5f);
            Destroy(this.gameObject);
        }
    }
    public void CheckForPlayer() 
    {

        if (Physics.CheckSphere(this.transform.position, playerDetectRadius, whatisPlayer))
        {
            //player Detected
            currentState = VirusState.Attacking;
        }
        else
        {
            currentState = VirusState.Moving;
        }
    } //Checks for player nearby
    public void Attack()
    {
        Vector3 dir = (player.position - this.transform.position).normalized;
        if(currentAttackTime >= attackDelayTime)
        {
            rb.velocity = dir * attackSpeed;
            currentAttackTime = 0;
        }
        currentAttackTime += Time.deltaTime;
    } 

    public void MoveToGoal()
    {
        Vector3 direction = (goal.position - this.transform.position).normalized;
        rb.velocity = direction * moveSpeed;

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, playerDetectRadius);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            AudioManager.instance.PlayHit();
            GameManager.instance.DamagePlayer();
        }
        if(collision.gameObject.tag == "Projectile")
        {
            this.virusHealth -= GameManager.instance.playerDamageAmount;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Goal"))
        {
            GameManager.instance.UpdateLungVisual(); 
            GameManager.instance.UpdateBeepVolume();
            Destroy(gameObject);
            
        }
    }
   
}
