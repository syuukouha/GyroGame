using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : UserInput
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        velocity = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
    }
}
