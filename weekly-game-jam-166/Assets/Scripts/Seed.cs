using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    public float treeVerticalOffset;

    public GameObject tree;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == 8) {
            Vector3 pos = transform.position;

            pos.y += treeVerticalOffset;
            Instantiate(tree, pos, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
