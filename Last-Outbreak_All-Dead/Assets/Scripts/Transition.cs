using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Transition : MonoBehaviour
{
    #region Variables
    private bool canInteract;

    public int cameraIndex;
    public GameObject[] WorldCamera;

    private PlayerMovement PM;
    private GameObject player;
    public Transform newTransform;

    public UnityEvent InteractionEvent;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        PM = FindObjectOfType<PlayerMovement>();
        player = PM.gameObject;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Player")
            canInteract = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
            canInteract = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canInteract)
        {
            canInteract = false;
            InteractionEvent.Invoke();
            //Enable Camera
            for (int i = 0; i < WorldCamera.Length; i++)
                WorldCamera[i].SetActive(false);
            WorldCamera[cameraIndex].SetActive(true);
            //Teleport Player
            player.SetActive(false);
            player.transform.position = newTransform.position;
            player.transform.rotation = newTransform.rotation;
            player.SetActive(true);
        }
    }
}
