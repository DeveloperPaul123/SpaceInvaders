using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour {

    public bool isExploding = false;

    public float speed = 10f;

    private Rigidbody2D rigidBody;

    public Sprite startImage;
    public Sprite alternativeImage;
    public Sprite explodedShipImage;

    private SpriteRenderer spriteRenderer;

    public float secondsTilChange = 0.5f;

    public GameObject alienBullet;

    public float minimumFireRateTime = 0.9f;
    public float maximumFireRateTime = 3.0f;
    public float baseFireWaitTime = 1.5f;

    // Use this for initialization
    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();

        rigidBody.velocity = Vector2.right * speed;

        StartCoroutine(ChangeAlienSprite());

        baseFireWaitTime = baseFireWaitTime + Random.Range(minimumFireRateTime, maximumFireRateTime);
    }

    void TurnAround(int direction)
    {
        Vector2 newVelocity = rigidBody.velocity;
        newVelocity.x = speed * direction;
        rigidBody.velocity = newVelocity;
    }
	
    void MoveDown()
    {
        Vector2 newPos = transform.position;
        newPos.y -= 1;
        transform.position = newPos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "LeftWall")
        {
            TurnAround(1);
            MoveDown();
        }
        if(collision.gameObject.name == "RightWall")
        {
            TurnAround(-1);
            MoveDown();
        }
    }
    public IEnumerator ChangeAlienSprite()
    {
        while(true)
        {
            if(spriteRenderer.sprite == startImage)
            {
                spriteRenderer.sprite = alternativeImage;
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.alienBuzz1);
            }
            else
            {
                spriteRenderer.sprite = startImage;
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.alienBuzz2);
            }

            yield return new WaitForSeconds(secondsTilChange);
        }
    }

    private void FixedUpdate()
    {
        if(Time.time > baseFireWaitTime)
        {
            baseFireWaitTime = baseFireWaitTime + Random.Range(minimumFireRateTime, maximumFireRateTime);
            Instantiate(alienBullet, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.shipExplosion);
            collision.gameObject.GetComponent<SpriteRenderer>().sprite = explodedShipImage;
            Destroy(gameObject);
            Destroy(collision.gameObject, 0.5f);
        }
    }
}
