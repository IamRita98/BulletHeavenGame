using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickupBehavior : MonoBehaviour
{
    public static event System.Action<GameObject> OnXPPickup;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("XpPickup")) return;
        OnXPPickup?.Invoke(collision.gameObject);
    }
}
