using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    [Header("Controller")]
    public bool freeze;
    [HideInInspector]public bool isMoving;

    [Header("Stats")]
    public float speed;
    public float rotateSpeed;
    private float horizontalMove;
    private float verticalMove;

    [Header("Camera Transform")]
    public Transform Front;
    public Transform Reversed;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (freeze) return;

        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            isMoving = true;
            //Rotate Player
            horizontalMove = Input.GetAxisRaw("Horizontal") * Time.deltaTime * rotateSpeed;
            this.gameObject.transform.Rotate(0, horizontalMove, 0);
            //Move Player
            verticalMove = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;
            this.gameObject.transform.Translate(verticalMove, 0, 0);
        }
        else
        {
            isMoving = false;
        }
    }
}
