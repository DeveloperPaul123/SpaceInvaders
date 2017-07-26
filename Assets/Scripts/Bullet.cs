using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour {

    public float speed = 30f;

    private Rigidbody2D rigidBody;

    public Sprite explodedAlienImage;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        // move up by default
        rigidBody.velocity = Vector2.up * speed;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Wall")
        {
            Destroy(gameObject);
        }
        if(collision.tag == "Alien")
        {
            Alien alien = collision.gameObject.GetComponent<Alien>();

            if (!alien.isExploding)
            {
                // increment our score.
                IncreaseTextUiScore();
                alien.isExploding = true;
                // play the sound.
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.alienDies);
                // render explosion
                alien.GetComponent<SpriteRenderer>().sprite = explodedAlienImage;
                // destroy the bullet
                Destroy(gameObject);
                // destroy the 
                DestroyObject(alien.gameObject, 0.5f);
            }
            
        }
    }

    void IncreaseTextUiScore()
    {
        var textUiComp = GameObject.Find("Score").GetComponent<Text>();
        int score = int.Parse(textUiComp.text);
        score += 100;
        textUiComp.text = score.ToString();
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
