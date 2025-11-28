using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    #region Variables
    public int gunID;
    public RuntimeAnimatorController controller;

    private bool aiming;
    private float horizontalMove;
    private float verticalMove;

    [Space(10)]
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

    void HandleMovement()
    {
        if (Input.GetMouseButton(1) && !aiming)
        {
            aiming = true;
            animator.SetBool("aiming", true);
        }

        if (Input.GetMouseButtonUp(1) && aiming)
        {
            aiming = false;
            animator.SetBool("aiming", false);
        }
    }
}
