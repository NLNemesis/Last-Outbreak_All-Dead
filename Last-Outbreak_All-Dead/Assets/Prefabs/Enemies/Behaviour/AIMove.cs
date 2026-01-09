using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class AIMove : MonoBehaviour
{
    #region Variables
    [Header("Stats")]
    public bool freeze;
    public int health;
    public float speed;
    private float currentSpeed;
    private bool playerDetected;

    [Header("References")]
    private NavMeshAgent agent;
    private GameObject player;
    private Animator animator;
    private Transform startedPosition;
    private AIDetect aiDetect;

    [Header("Events")]
    public UnityEvent deathEvent;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = FindObjectOfType<PlayerMovement>().gameObject;
        startedPosition.position = this.transform.position;
        startedPosition.rotation = this.transform.rotation;
        aiDetect = GetComponentInChildren<AIDetect>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
            playerDetected = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
            playerDetected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (health > 0 && !freeze)
            HandleMovement();
    }

    #region Handle Movement
    void HandleMovement()
    {
        if (playerDetected)
        {
            agent.speed = speed;
            agent.SetDestination(player.transform.position);
            animator.Play("Walk");
        }
        else if (playerDetected && !player.activeSelf)
        {
            playerDetected = false;
            agent.speed = 0;
            agent.SetDestination(this.transform.position);
            animator.Play("Idle");
        }
        else
        {
            playerDetected = false;
            agent.speed = 0;
            agent.SetDestination(startedPosition.position);
            animator.Play("Walk");
        }
    }
    #endregion

    #region Take Damage
    public void TakeDamage(int value)
    {
        health -= value;

        if (health <= 0)
        {
            agent.enabled = false;
            animator.Play("Death");
            deathEvent.Invoke();
        }
        else
        {
            animator.Play("Hit");
        }
    }
    #endregion

    #region Toggle Movement
    public void Toggle_Freeze(bool value)
    {
        freeze = value;

        if (freeze)
            agent.speed = 0;
        else
            agent.speed = currentSpeed;
    }
    #endregion

    #region Toggle Variables (AI Detect)
    public void Toggle_canAttack(bool value)
    {
        aiDetect.canAttack = value;
    }
    public void Toggle_dealDamage(bool value)
    {
        aiDetect.dealDamage = value;
    }
    #endregion

    #region Attack - Deal Damage
    public void Attack()
    {
        freeze = true;
        aiDetect.canAttack = false;
        animator.Play("Attack");
    }

    public void DealDamage()
    {
        freeze = true;
        aiDetect.canAttack = false;
        aiDetect.dealDamage = false;
        //pm.TakeDamage(damage);
    }
    #endregion
}
