using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    #region Variables
    public enum GrabType {Up,Dowm}
    public GrabType Type;
    private bool CanInteract;
    public KeyCode InteractionKey;

    [Header("References")]
    private PlayerMovement PM;
    private Animator animator;
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
            CanInteract = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
            CanInteract = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        PM = FindObjectOfType<PlayerMovement>();
        animator = FindObjectOfType<PlayerMovement>().GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(InteractionKey) && CanInteract)
        {
            CanInteract = false;
            if (Type == GrabType.Up)
                animator.Play("Pick_Up");
            else
                animator.Play("Pick_Down");
            this.gameObject.SetActive(false);
        }
    }
}
