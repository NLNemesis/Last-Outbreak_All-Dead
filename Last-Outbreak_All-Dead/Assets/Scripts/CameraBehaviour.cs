using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    #region Variables
    public enum CameraType {Static,LookAt,Follow}
    public CameraType Type;

    [Header("Controls")]
    public bool Front;

    [Header("References")]
    private Transform PlayerTransform;
    private PlayerMovement PM;
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        PM = FindObjectOfType<PlayerMovement>();
        PlayerTransform = FindObjectOfType<PlayerMovement>().GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Type == CameraType.LookAt)
        {
            this.transform.LookAt(PlayerTransform.position);
        }
        else if (Type == CameraType.Follow)
        {
            if (Front)
            {
                this.transform.position = PM.FrontCam.transform.position;
                this.transform.rotation = PM.FrontCam.transform.rotation;
            }
            else
            {
                this.transform.position = PM.BackCam.transform.position;
                this.transform.rotation = PM.BackCam.transform.rotation;
            }
        }
    }
}
