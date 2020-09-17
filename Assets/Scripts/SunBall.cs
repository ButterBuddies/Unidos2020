using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunBall : MonoBehaviour
{
    public bool falling = false;

    Rigidbody2D _body;

    [SerializeField]
    float maxSpeed = 10f;

    bool touchFloor = false;
    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _body.velocity = new Vector2(Mathf.Clamp(_body.velocity.x, -maxSpeed, maxSpeed), Mathf.Clamp(_body.velocity.y, -maxSpeed, maxSpeed));
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
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
                break;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Floor":
                touchFloor = false;
                break;
        }
    }


    IEnumerator FloorPunt()
    {
        if(touchFloor)
        {
            yield return new WaitForSeconds(1.25f);
            if (touchFloor)
            {
                _body.AddForce(Vector2.up, ForceMode2D.Impulse);
            }
        }
    }
}
