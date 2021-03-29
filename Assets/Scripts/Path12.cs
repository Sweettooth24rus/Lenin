using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path12 : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            other.transform.position = new Vector3(-5f, 31f, 0);
        }
    }
}
