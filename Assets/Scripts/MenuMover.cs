using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMover : MonoBehaviour
{
    Rigidbody2D _body;
    public bool player1 = true;
    int playerNumber = 0;
    private void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        if (player1)
        {
            playerNumber = 1;
        }
        else
        {
            playerNumber = 2;
        }
    }

    private void Update()
    {
        if(Input.GetAxis("HorizontalPlayer" + playerNumber)!= 0)
        {
            _body.AddForce(new Vector2(Input.GetAxis("HorizontalPlayer" + playerNumber) * Time.deltaTime * 1000f,0f));
        }
        _body.velocity = _body.velocity = new Vector2(Mathf.Clamp(_body.velocity.x, -100f, 100f), _body.velocity.y);

    }
}
