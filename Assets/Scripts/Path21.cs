using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path21 : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            other.transform.position = new Vector3(98.6f, -7.2f, 0);
        }
    }
}
