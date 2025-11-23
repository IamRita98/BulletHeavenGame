using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickupBehavior : MonoBehaviour
{
    public static event System.Action<float> OnXPPickup;

    private void OnTriggerEnter(Collider other)
    {
        
        if (!other.gameObject.CompareTag("Pickup"))return;
        OnXPPickup?.Invoke(0f);

    }
}
