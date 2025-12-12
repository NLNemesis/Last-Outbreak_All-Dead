using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class AIMove : MonoBehaviour
{
    #region Variables
    [Header("Stats")]
    public int health;
    public float moveSpeed;
    private float currentSpeed;

    [Header("Controller")]
    private bool canSeePlayer;

    [Header("References")]
    private NavMeshAgent agent;
    private Animator animator;
    private GameObject player;

    [Header("Events")]
    public UnityEvent detectedEvent;
    public UnityEvent lossAgroEvent;
    public UnityEvent deathEvent;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.speed = moveSpeed;
        currentSpeed = moveSpeed;
    }

    #region On Triggers
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            canSeePlayer = true;
            player = other.gameObject;
            detectedEvent.Invoke();
        }
    }
    #endregion

    // Update is called once per frame
    void Update()
    {
        #region State 2.0 Follow Player
        if (canSeePlayer)
        {
            agent.SetDestination(player.transform.position);
        }
        #endregion
    }
}
