using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    #region Variables
    public enum CameraType {Static, Rotate, Follow}
    public CameraType Type;
    public bool FrontCamera;

    [Header("References")]
    private Transform PlayerTransform;
    private PlayerMovement PM;
    #endregion

    // Start is called before the first frame update
    void OnEnable()
    {
        PM = FindObjectOfType<PlayerMovement>();
        PlayerTransform = FindObjectOfType<PlayerMovement>().GetComponent<Transform>();
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
                this.transform.position = PM.ReverseCamera.transform.position;
                this.transform.rotation = PM.ReverseCamera.transform.rotation;
            }
        }
    }
}
