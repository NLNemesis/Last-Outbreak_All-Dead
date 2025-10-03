using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    #region Variables
    public enum CameraType {Static, Rotate, Follow}
    public CameraType Type;
    public bool FrontCamera;

    [Header("Rotate = True")]
    private Transform PlayerTransform;

    [Header("References")]
    private PlayerMovement PM;
    #endregion
    // Start is called before the first frame update
    void OnEnable()
    {
        PlayerTransform = FindObjectOfType<PlayerMovement>().GetComponent<Transform>();
        PM = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Type == CameraType.Rotate)
        {
            this.transform.LookAt(PlayerTransform.position);
        }
        else if (Type == CameraType.Follow)
        {
            if (FrontCamera)
            {
                this.transform.position = PM.FrontCamera.transform.position;
                this.transform.rotation = PM.FrontCamera.transform.rotation;
            }
            else
            {
                this.transform.position = PM.ReversedCamera.transform.position;
                this.transform.rotation = PM.ReversedCamera.transform.rotation;
            }
        }

    }
}
