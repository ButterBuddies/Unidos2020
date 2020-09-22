using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D _body;
    Animator _anim;
    SpriteRenderer _rend;

    [SerializeField]
    bool player1 = true;
    [SerializeField]
    float movementAccel = 3f;
    [SerializeField]
    float maxSpeed = 10f;
    [SerializeField]
    public float jumpForce = 10f;

    private BoxCollider2D _box;
    public bool grounded = false;

    public string playerNumber = "0";

    bool jumpButtonDown = false;
    float moveHor = 0f;

    

    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _box = GetComponent<BoxCollider2D>();
        _anim = GetComponent<Animator>();
        _rend = GetComponent<SpriteRenderer>();
        if (player1)
        {
            playerNumber = "1";
        }
        else
        {
            playerNumber = "2";
        }
    }
    
    // Update is called once per frame
    private void Update()
    {
        if (grounded && Input.GetButtonDown("JumpPlayer" + playerNumber))
        {
            jumpButtonDown = true; 
        }
    }

    void FixedUpdate()
    {
        float direction = Input.GetAxis("HorizontalPlayer" + playerNumber) * Time.deltaTime * 100f;

        if (direction < 0)
        {
            _anim.SetBool("Run",true);
            _rend.flipX = false;
        }
        else if(direction >0)
        {
            _anim.SetBool("Run", true);
            _rend.flipX = true;
        }
        else
        {
            _anim.SetBool("Run", false);
        }

        Vector3 max = _box.bounds.max;
        Vector3 min = _box.bounds.min;
        Vector2 corner1 = new Vector2(max.x-0.2f, min.y - .1f);
        Vector2 corner2 = new Vector2(min.x - 0.2f, min.y - .2f);
        Collider2D hit = Physics2D.OverlapArea(corner1, corner2,9);
        


        if (hit != null)
        {
            grounded = true;
            _anim.SetBool("Grounded", true);
        }
        else
        {
            grounded = false;
            _anim.SetBool("Grounded", false);
        }

        if (grounded)
        {
            direction*= movementAccel;
        }
        else
        {
            direction = direction * (movementAccel / 2f);
        }
        if (jumpButtonDown)
        {
            Jump(1);
        }

            _body.velocity = new Vector2(Mathf.Clamp(direction, -maxSpeed, maxSpeed), _body.velocity.y);


    }

    public void Jump(float forceMultiplier)
    {
        _body.AddForce(Vector2.up * jumpForce*forceMultiplier, ForceMode2D.Impulse);
        jumpButtonDown = false;
    }



}
