using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    Rigidbody2D _pbody;
    PlayerMovement movement;

    bool doubleJump = false;

    bool jumpButtonDown = false;
    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        _pbody = GetComponent<Rigidbody2D>();
        movement.jumpForce *= 0.75f;
    }

 
    private void Update()
    {
        jumpButtonDown = false;
        
        if(Input.GetButtonDown("JumpPlayer" + movement.playerNumber))
        {
            jumpButtonDown = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (movement.grounded)
        {
            doubleJump = true;
        }

        if (jumpButtonDown)
        {

            if (!movement.grounded&&doubleJump)
            {
                movement.Jump(1.25f);
                doubleJump = false;
            }
            jumpButtonDown = false;
        }
    }
}
