using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunBall : MonoBehaviour
{
    public bool falling = false;

    public bool start = false;
    Rigidbody2D _body;
    GoalScript goalScript;

    [SerializeField]
    float maxSpeed = 10f;

    bool touchFloor = false;
    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        goalScript = FindObjectOfType<GoalScript>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (start)
        {
            _body.velocity = new Vector2(Mathf.Clamp(_body.velocity.x, -maxSpeed, maxSpeed), Mathf.Clamp(_body.velocity.y, -maxSpeed, maxSpeed));
        }
        else
        {
            _body.velocity = Vector2.zero;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                if (collision.gameObject.GetComponent<PlayerMovement>().player1)
                {
                    goalScript.lastHit = 1;
                }
                else
                {
                    goalScript.lastHit = 2;
                }
                if (collision.gameObject.GetComponent<PlayerMovement>().grounded)
                {
                    Rigidbody2D _colbod = collision.gameObject.GetComponent<Rigidbody2D>();
                    _body.AddForce(_colbod.velocity.normalized / 10f, ForceMode2D.Impulse);
                    _colbod.AddForce(-_colbod.velocity.normalized, ForceMode2D.Impulse);
                    
                }
                else
                {
                    Rigidbody2D _colbod = collision.gameObject.GetComponent<Rigidbody2D>();
                    _body.AddForce(_colbod.velocity.normalized / 3f, ForceMode2D.Impulse);
                    _colbod.AddForce(-_colbod.velocity.normalized / .5f, ForceMode2D.Impulse);
                }
                break;
            case "Floor": touchFloor = true;
                StartCoroutine(FloorPunt());
                _body.AddForce(_body.velocity * 2f, ForceMode2D.Impulse);
                break;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Floor":
                touchFloor = false;
                StopCoroutine(FloorPunt());
                break;
        }
    }


    IEnumerator FloorPunt()
    {
        if(touchFloor)
        {
            yield return new WaitForSeconds(0.5f);
            if (touchFloor)
            {
                _body.AddForce(Vector2.up*5f, ForceMode2D.Impulse);
            }
        }
    }
}
