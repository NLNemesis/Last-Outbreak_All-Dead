using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    public UnityEvent Event;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
            Event.Invoke();
    }
}
