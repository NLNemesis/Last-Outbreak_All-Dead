using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    #region Variables
    [Header("Controller")]
    public int gunID;
    public RuntimeAnimatorController controller;

    [Header("Stats")]
    public int ammo;
    public int mag;
    public float recoil;
    private bool canShoot = true;
    public float damageDelay;
    public int damage;

    [Header("System")]
    public ParticleSystem muzzleFlash;
    public Transform rayPoint;
    public float range;
    public LayerMask layer;

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
        HandleWeapon();
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
                animator.SetBool("up", true);
                animator.SetBool("center", false);
                animator.SetBool("down", false);
            }
            else if (verticalMove < -0.1f)
            {
                animator.SetBool("up", false);
                animator.SetBool("center", false);
                animator.SetBool("down", true);
            }
            else
            {
                animator.SetBool("up", false);
                animator.SetBool("center", true);
                animator.SetBool("down", false);
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

    #region Handle Weapon
    void HandleWeapon()
    {
        if (aiming)
        {
            if (Input.GetMouseButton(0) && canShoot && ammo > 0)
            {
                StartCoroutine(ShootHandle());
            }
        }
    }

    IEnumerator ShootHandle()
    {
        canShoot = false;
        yield return new WaitForSeconds(recoil);
        canShoot = true;
    }
    #endregion
}
