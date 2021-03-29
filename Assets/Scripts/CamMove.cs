using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    public GameObject player;
    public float hor = 15f;
    public float ver = 25f;

    // Update is called once per frame
    void Update() {
        if (player.transform.position.y > transform.position.y + ver / 2)
            transform.position = new Vector3(transform.position.x, transform.position.y + ver, -10f);
        else if (player.transform.position.y < transform.position.y - ver / 2)
            transform.position = new Vector3(transform.position.x, transform.position.y - ver, -10f);
        if (player.transform.position.x > transform.position.x + hor / 2)
            transform.position = new Vector3(transform.position.x + hor, 0, -10f);
        else if (player.transform.position.x < transform.position.x - hor / 2)
            transform.position = new Vector3(transform.position.x - hor, 0, -10f);
    }
}
