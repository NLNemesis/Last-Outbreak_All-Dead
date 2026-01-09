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
    private PlayerMovement pm;
    private Animator animator;
    private Transform startedPosition;

    [Header("Events")]
    public UnityEvent deathEvent;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        pm = FindObjectOfType<PlayerMovement>();
        player = FindObjectOfType<PlayerMovement>().gameObject;
        startedPosition.position = this.transform.position;
        startedPosition.rotation = this.transform.rotation;
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
            agent.speed = currentSpeed;
            agent.SetDestination(startedPosition.position);
            animator.Play("Idle");
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
    public void Freeze()
    {
        freeze = true;
        agent.speed = 0;
    }
    public void Unfreeze()
    {
        freeze = false;
        agent.speed = currentSpeed;
    }
    #endregion
}
