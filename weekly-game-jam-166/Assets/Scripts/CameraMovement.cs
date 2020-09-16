using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float offset = 10f;
    public Transform playerTransform;

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.x > transform.position.x + offset) {
            MoveCam(playerTransform.position.x - (transform.position.x + offset));
        } else if (playerTransform.position.x < transform.position.x - offset) {
            MoveCam(playerTransform.position.x - (transform.position.x - offset));
        }
    }

    void MoveCam(float value)
    {
        transform.Translate(new Vector3(value, 0, 0));
    }
}
