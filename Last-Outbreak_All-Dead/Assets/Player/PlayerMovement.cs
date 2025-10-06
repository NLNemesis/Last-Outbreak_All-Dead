using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    [Header("Controls")]
    public KeyCode RotateKey;

    [Header("Controller")]
    public bool freeze;
    [HideInInspector]public bool isMoving;

    [Header("Stats")]
    public float currentSpeed;
    public float speed;
    public float sprint;
    public float rotateSpeed;
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
    private bool inputShift;
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

            if (Input.GetButton("Sprint"))
            {
                currentSpeed = sprint;
                inputShift = true;
            }
            else
            {
                currentSpeed = speed;
                inputShift = false;
            }
        }
        else
        {
            isMoving = false;
        }

        if (Input.GetKeyDown(RotateKey))
        {
            freeze = true;
            animator.Play("Turn180");
            StartCoroutine(RotatePlayer());
        }
    }

    IEnumerator RotatePlayer()
    {
        float Timer = 0;
        float Duration = 1;
        while (Timer < Duration)
        {
            Timer += Time.deltaTime;
            float Step = Timer / Duration;
            horizontalMove = Step * -1f;
            this.gameObject.transform.Rotate(0, horizontalMove, 0);
            yield return null;
        }
        freeze = false;
    }
    #endregion

    #region Handle Animations
    void HandleAnimations()
    {
        //Movement
        if (inputV == 1)
        {
            if (inputShift) animator.Play("Running");
            else animator.Play("Walk");
        }
        else if (inputV == -1)
        {
            if (inputShift) animator.Play("Running_Reverse");
            else animator.Play("Walk_Reverse");
        }
        else if (inputV == 0 && inputH == 0)
            animator.Play("Idle");

        //Rotation
        if (inputH != 0 && inputV == 0)
            animator.Play("Walk");
    }

    public void Unfreeze()
    {
        freeze = false;
    }
    #endregion
}
