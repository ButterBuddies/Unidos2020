using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunBall : MonoBehaviour
{
    public bool falling = false;

    public bool start = false;
    Rigidbody2D _body;
    GoalScript goalScript;
    AudioSource source;
    SpriteRenderer rend;
    public Color32 qColor, cColor;

    public AudioClip[] hitSoundsQuetz;
    public AudioClip[] hitSoundsCat;

    [SerializeField]
    float maxSpeed = 10f;

    bool touchFloor = false;
    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        goalScript = FindObjectOfType<GoalScript>();
        source = GetComponent<AudioSource>();
        rend = GetComponent<SpriteRenderer>();
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
                    if (!source.isPlaying)
                    {
                        source.pitch = Random.Range(0.9f, 1.1f);
                        source.volume = 0.65f;
                        source.PlayOneShot(hitSoundsQuetz[Random.Range(0, hitSoundsQuetz.Length)]);
                        rend.color = qColor;
                        StartCoroutine(ColorFlash());
                    }

                }
                else
                {
                    goalScript.lastHit = 2;
                    if (!source.isPlaying)
                    {
                        source.pitch = Random.Range(1f, 1.2f);
                        source.volume = 0.65f;
                        source.PlayOneShot(hitSoundsCat[Random.Range(0, hitSoundsCat.Length)]);
                        rend.color = cColor;
                        StartCoroutine(ColorFlash());
                    }
                }
                if (collision.gameObject.GetComponent<PlayerMovement>().grounded)
                {
                    Rigidbody2D _colbod = collision.gameObject.GetComponent<Rigidbody2D>();
                    _body.AddForce(_colbod.velocity.normalized, ForceMode2D.Impulse);
                    _colbod.AddForce(-_colbod.velocity.normalized, ForceMode2D.Impulse);
                    
                }
                else
                {
                    Debug.Log("Push");
                    Rigidbody2D _colbod = collision.gameObject.GetComponent<Rigidbody2D>();
                    _body.AddForce(_colbod.velocity.normalized, ForceMode2D.Impulse);
                    _colbod.AddForce(-_colbod.velocity.normalized, ForceMode2D.Impulse);
                }
                break;
            case "Floor": touchFloor = true;
                StartCoroutine(FloorPunt());
                _body.AddForce(_body.velocity, ForceMode2D.Impulse);
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

    IEnumerator ColorFlash()
    {
        yield return new WaitForSeconds(0.2f);
        rend.color = Color.white;
    }


    IEnumerator FloorPunt()
    {
        if(touchFloor)
        {
            yield return new WaitForSeconds(0.5f);
            if (touchFloor)
            {
                _body.AddForce(Vector2.up*3f, ForceMode2D.Impulse);
            }
        }
    }
}
