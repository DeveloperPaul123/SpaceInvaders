using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBullet : MonoBehaviour {

    public float speed = 30f;

    private Rigidbody2D rigidBody;

    public Sprite explodedShipImage;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = Vector2.down * speed;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            Destroy(gameObject);
        }
        else if(collision.tag == "Player")
        {
            SpaceShip ship = collision.gameObject.GetComponent<SpaceShip>();
            if (!ship.isExploding)
            {
                ship.isExploding = true;
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.shipExplosion);
                ship.GetComponent<SpriteRenderer>().sprite = explodedShipImage;
                Destroy(gameObject);
                DestroyObject(ship.gameObject, 0.5f);
            }
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
