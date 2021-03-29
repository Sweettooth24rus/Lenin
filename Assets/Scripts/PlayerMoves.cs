using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerMoves : MonoBehaviour
{

    public int speed;
    private Rigidbody2D rb;
    private bool faceRight = true;
    public int jumpForce;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        rb.MovePosition(rb.position + Vector2.right * moveX * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.UpArrow))
            rb.AddForce (transform.up * jumpForce);
        if ((moveX > 0) && (!faceRight))
            flip();
        else if ((moveX < 0) && (faceRight))
            flip();
        if (Input.GetKeyDown(KeyCode.DownArrow))
            transform.localScale = new Vector3(20f, 10f, 0f);
        if (Input.GetKeyUp(KeyCode.DownArrow))
            transform.localScale = new Vector3(20f, 20f, 0f);
    }

    void flip()
    {
        faceRight = !faceRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
