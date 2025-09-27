using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    [Header("Controller")]
    public bool Freeze;
    public bool IsMoving;

    [Header("Stats")]
    public float Speed;
    public float Rotate;
    private float move;
    private float rotate;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            IsMoving = true;
            //Rotate Player
            rotate = Input.GetAxisRaw("Horizontal") * Time.deltaTime * Rotate;
            this.gameObject.transform.Rotate(0, rotate, 0);
            //Move Player
            move = Input.GetAxisRaw("Vertical") * Time.deltaTime * Speed;
            this.gameObject.transform.Translate(0, 0, move);
        }
        else
        {
            IsMoving = false;
        }

    }
}
