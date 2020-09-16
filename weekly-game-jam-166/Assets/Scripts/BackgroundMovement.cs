using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    public float speed = 1f;

    public Vector2 originalPosition;

    void FixedUpdate()
    {
        transform.Translate(new Vector3(speed * Time.fixedDeltaTime, 0, 0));
    }

    void OnBecameInvisible()
    {
        transform.position = new Vector3(originalPosition.x, originalPosition.y, 0);
    }
}
