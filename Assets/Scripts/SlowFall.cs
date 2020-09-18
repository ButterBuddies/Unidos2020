using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowFall : MonoBehaviour
{
    PlayerMovement movement;

    Rigidbody2D _pbody;

    bool jumpButtonDown = false, jumpButtonUp = false;
    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        _pbody = GetComponent<Rigidbody2D>();
    }
    /*
    private void Update()
    {
        jumpButtonDown = false;
        jumpButtonUp = false;

        if(Input.GetButtonDown("JumpPlayer" + movement.playerNumber))
        {
            jumpButtonDown = true;
        }
        if(Input.GetButtonUp("JumpPlayer" + movement.playerNumber))
        {
            jumpButtonUp =true;
        }
    }*/

    // Update is called once per frame
    void FixedUpdate()
    {
        if (movement.grounded)
        {
            StopCoroutine(Fall());
        }
       else if (Input.GetButtonDown("JumpPlayer" + movement.playerNumber))
        {

                _pbody.velocity /= 4f;
                _pbody.gravityScale = 0.1f;
                StartCoroutine(Fall());

        }

        if (Input.GetButtonUp("JumpPlayer" + movement.playerNumber))
        {
            _pbody.gravityScale = 1f;
        }
    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(0.75f);
        _pbody.gravityScale = 1f;
    }
}
