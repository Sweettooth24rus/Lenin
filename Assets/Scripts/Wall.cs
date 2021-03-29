using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public bool l1;
    public bool l2;
    public bool l3;
    public bool l4;

    public GameObject lever1;
    public GameObject lever2;
    public GameObject lever3;
    public GameObject lever4;

    // Start is called before the first frame update
    void Start()
    {
        l1 = true;
        l2 = true;
        l3 = true;
        l4 = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!l1 && !l2 && !l3 && l4)
            Destroy(this);
    }
}
