using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    #region Variables
    [Header("Controls")]
    public GrabType Type;
    public enum GrabType {Up, Down}

    [Header("Interaction")]
    public Sprite sprite;
    private bool CanInteract;

    [Header("References")]
    private PlayerMovement PM;
    private Animator playerAnimator;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        PM = FindObjectOfType<PlayerMovement>();
        playerAnimator = FindObjectOfType<PlayerMovement>().GetComponent<Animator>();
    }

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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && CanInteract)
        {
            PM.freeze = true;
            if (Type == GrabType.Up)
                playerAnimator.Play("Pick_Up");
            else
                playerAnimator.Play("Pick_Down");
            this.gameObject.SetActive(false);
        }
    }
}
