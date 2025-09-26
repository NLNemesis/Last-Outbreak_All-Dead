using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    [Header("Controller")]
    public bool Freezed;
    public bool IsMoving;
    [Header("Stats")]
    public float Speed;
    public float Rotate;
    private float MoveSpeed;
    private float RotateSpeed;
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
            RotateSpeed = Input.GetAxisRaw("Horizontal") * Time.deltaTime * Rotate;
            MoveSpeed = Input.GetAxisRaw("Vertical") * Time.deltaTime * Speed;
            this.gameObject.transform.Rotate(0, RotateSpeed, 0);
            this.gameObject.transform.Translate(0, 0, MoveSpeed);
        }
        else
        {
            IsMoving = false;
        }
    }
}
