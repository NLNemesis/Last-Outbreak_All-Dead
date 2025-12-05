using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    #region Variables
    [Header("Controller")]
    public int gunID;
    public RuntimeAnimatorController controller;

    [Header("References")]
    private PlayerMovement pm;
    private Animator animator;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponentInParent<PlayerMovement>();
        animator = GetComponentInParent<Animator>();
        animator.runtimeAnimatorController = controller;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    #region Handle Movement
    private bool aiming;
    private float horizontalMove;
    private float verticalMove;

    void HandleMovement()
    {
        //Start Aiming
        if (Input.GetMouseButton(1) && !aiming)
        {
            aiming = true;
            animator.SetBool("aiming", true);
            animator.SetTrigger("aim");
            pm.FreezePlayer();
        }

        //While Aiming
        if (aiming)
        {
            //Rotate Player
            horizontalMove = Input.GetAxisRaw("Horizontal") * Time.deltaTime * pm.rotateSpeed;
            pm.gameObject.transform.Rotate(0, horizontalMove, 0);
            //Move Player
            verticalMove = Input.GetAxisRaw("Vertical");
            if (verticalMove > 0.1f)
            {
                animator.SetBool("Up", true);
                animator.SetBool("Center", false);
                animator.SetBool("Down", false);
            }
            else if (verticalMove < -0.1f)
            {
                animator.SetBool("Up", false);
                animator.SetBool("Center", false);
                animator.SetBool("Down", true);
            }
            else
            {
                animator.SetBool("Up", false);
                animator.SetBool("Center", true);
                animator.SetBool("Down", false);
            }
        }

        //Stop Aiming
        if (Input.GetMouseButtonUp(1) && aiming)
        {
            aiming = false;
            animator.SetBool("aiming", false);
            animator.SetTrigger("aim");
            pm.UnfreezePlayer();
        }
    }
    #endregion
}
