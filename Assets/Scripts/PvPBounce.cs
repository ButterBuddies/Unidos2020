using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PvPBounce : MonoBehaviour
{
    Rigidbody2D _body;

    private void Start()
    {
        _body = GetComponentInParent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       // Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log(gameObject.name + " bounced off of" + collision.gameObject.name);
            _body.AddForce(Vector2.down * 50f, ForceMode2D.Impulse);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 50f, ForceMode2D.Impulse);
        }
    }
}
