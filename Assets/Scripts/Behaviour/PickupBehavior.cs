using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickupBehavior : MonoBehaviour
{
    public static event System.Action<GameObject> OnXPPickup;
    Transform playerPos;
    float duration = .35f;

    private void Awake()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("XpPickup")) return;
        StartCoroutine(PullObjectsToPlayer(collision.gameObject));
    }

    IEnumerator PullObjectsToPlayer(GameObject objectToPull)
    {
        Vector2 startingPos = objectToPull.transform.position;
        
        float t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / duration;
            Vector2 targetPos = playerPos.position;
            objectToPull.transform.position = Vector2.Lerp(startingPos, targetPos, t);
            yield return null;
        }
        OnXPPickup?.Invoke(objectToPull);
    }
}
