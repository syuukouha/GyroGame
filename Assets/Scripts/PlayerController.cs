using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private UserInput userInput;
    public float Speed;
    // Use this for initialization
    void Start()
    {
        foreach (var item in GetComponents<UserInput>())
        {
            if (item.enabled)
            {
                userInput = item;
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += userInput.velocity * Speed;
    }
}
