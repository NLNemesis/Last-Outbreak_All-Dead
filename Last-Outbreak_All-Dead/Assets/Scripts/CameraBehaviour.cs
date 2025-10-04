using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    #region Variables
    public enum CameraType {Static, Rotate, Follow}
    public CameraType Type;
    public bool Front;

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
            this.transform.Rotate(PlayerTransform.position);
        }
        else if (Type == CameraType.Follow)
        {
            if (Front)
            {
                this.transform.position = PM.Front.position;
                this.transform.rotation = PM.Front.rotation;
            }
            else
            {
                this.transform.position = PM.Reversed.position;
                this.transform.rotation = PM.Reversed.rotation;
            }
        }
    }
}
