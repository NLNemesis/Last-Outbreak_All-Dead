using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    [Header("Controller")]
    public bool Freeze;
    private bool isMoving;

    [Header("Stats")]
    public float Speed;
    public float Rotate;
    private float moveSpeed;
    private float rotateSpeed;

    [Header("References")]
    private CharacterController cc;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            isMoving = true;
            //Rotate Player
            rotateSpeed = Input.GetAxisRaw("Horizontal") * Time.deltaTime * Rotate;
            this.gameObject.transform.Rotate(Vector3.up * rotateSpeed);
            //Move Player
            moveSpeed = Input.GetAxisRaw("Vertical") * Time.deltaTime * Speed;
            this.gameObject.transform.Translate(0, 0, moveSpeed);
        }
        else
        {
            isMoving = false;
        }
    }
}
