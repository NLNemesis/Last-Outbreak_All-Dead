using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    #region Variables
    private bool Holds;

    [Header("Controller")]
    public int ammoID;
    public RuntimeAnimatorController controller;

    [Header("Stats")]
    public int ammo;
    public int mag;
    public float reloadDelay;
    public float recoil;
    private bool canUseAction = true;
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
    private Inventory inventory;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponentInParent<PlayerMovement>();
        animator = GetComponentInParent<Animator>();
        animator.runtimeAnimatorController = controller;
        inventory = GetComponentInParent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleWeapon();
        HandleReload();
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
            if (Input.GetMouseButton(0))
                Holds = true;
            else 
                Holds = false;

            if (Holds && canUseAction && ammo > 0)
            {
                StartCoroutine(ShootHandle());
            }
        }
    }

    IEnumerator ShootHandle()
    {
        canUseAction = false;
        //Shoot Rotation
        if (verticalMove > 0.1f)
            animator.Play("Shoot_Up");
        else if (verticalMove < -0.1f)
            animator.Play("Shoot_Down");
        else
            animator.Play("Shoot_Center");
        //Effects
        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
            ammo--;
        }
        yield return new WaitForSeconds(damageDelay);
        Debug.DrawRay(rayPoint.position, rayPoint.forward * range, Color.red, 1f);
        yield return new WaitForSeconds(recoil - damageDelay);
        canUseAction = true;
    }
    #endregion

    #region Handle Reload
    void HandleReload()
    {
        if (Input.GetKeyDown(KeyCode.R) && aiming && canUseAction && ammo < mag)
        {
            StartCoroutine(Reloading());
        }
    }

    IEnumerator Reloading()
    {
        canUseAction = false;
        inventory.AmmoType[ammoID] = ammo;
        if (inventory.AmmoType[ammoID] - mag > 0)
        {
            ammo = mag;
            inventory.AmmoType[ammoID] -= mag;
        }
        else
        {
            ammo = inventory.AmmoType[ammoID];
            inventory.AmmoType[ammoID] = 0;
        }
        yield return new WaitForSeconds(reloadDelay);
        canUseAction = true;
    }
    #endregion
}
