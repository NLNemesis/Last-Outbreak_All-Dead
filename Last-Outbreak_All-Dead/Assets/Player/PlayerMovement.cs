using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
<<<<<<< Updated upstream
    [Header("Controller")]
    public bool freeze;
    [HideInInspector]public bool isMoving;

    [Header("Stats")]
    public float speed;
    public float rotateSpeed;
    private float horizontalMove;
    private float verticalMove;
=======
    public bool isMoving;
    public float speed;
    public float rotateSpeed;
    private float H_Move;
    private float V_Move;

    [Header("References")]
    public Transform ReversedCamera;
    public Transform FrontCamera;
>>>>>>> Stashed changes
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< Updated upstream
        if (freeze) return;

=======
>>>>>>> Stashed changes
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            isMoving = true;
            //Rotate Player
<<<<<<< Updated upstream
            horizontalMove = Input.GetAxisRaw("Horizontal") * Time.deltaTime * rotateSpeed;
            this.gameObject.transform.Rotate(0, horizontalMove, 0);
            //Move Player
            verticalMove = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;
            this.gameObject.transform.Translate(verticalMove, 0, 0);
=======
            H_Move = Input.GetAxisRaw("Horizontal") * Time.deltaTime * rotateSpeed;
            this.transform.Rotate(0, H_Move, 0);
            //Move Player
            V_Move = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;
            this.transform.Translate(V_Move, 0, 0);
>>>>>>> Stashed changes
        }
        else
        {
            isMoving = false;
        }
    }
}
