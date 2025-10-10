using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    [Header("Controller")]
    public bool freeze;
    [HideInInspector]public bool isMoving;

    [Header("Stats")]
    public float speed;
    public float sprint;
    public float rotateSpeed;
    private float currentSpeed;
    private float horizontalMove;
    private float verticalMove;

    [Header("Camera Transform")]
    public Transform FrontCam;
    public Transform BackCam;

    [Header("References")]
    [HideInInspector] public Animator animator;

    [Space(10)]
    private float inputV;
    private float inputH;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (freeze) return;

        inputV = Input.GetAxisRaw("Vertical");
        inputH = Input.GetAxisRaw("Horizontal");

        HandleMovement();
        HandleAnimations();
    }

    #region Handle Movement
    void HandleMovement()
    {
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            isMoving = true;
            #region Movement-Rotation
            //Rotate Player
            horizontalMove = Input.GetAxisRaw("Horizontal") * Time.deltaTime * rotateSpeed;
            this.gameObject.transform.Rotate(0, horizontalMove, 0);
            //Move Player
            verticalMove = Input.GetAxisRaw("Vertical") * Time.deltaTime * currentSpeed;
            this.gameObject.transform.Translate(verticalMove, 0, 0);
            #endregion
        }
        else
        {
            isMoving = false;
            horizontalMove = 0;
            verticalMove = 0;
        }
    }
    #endregion

    #region Handle Animations
    void HandleAnimations()
    {
        if (inputH != 0 && inputV == 0)

        if (verticalMove > 0)
            animator.Play("Walk");
        else if (verticalMove < 0)
            animator.Play("Walk_Reverse");
        else if (horizontalMove == 0 && verticalMove == 0)
            animator.Play("Idle");

        if (horizontalMove != 0 && verticalMove == 0)
            animator.Play("Walk");
    }
    #endregion
}
