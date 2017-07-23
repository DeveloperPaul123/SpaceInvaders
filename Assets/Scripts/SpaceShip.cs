using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour {

    public float speed = 30f;

    public GameObject bullet;

    public bool isExploding = false;

    public Sprite explodedShipImage;

    private void FixedUpdate()
    {
        //Get input for vertical input
        float horzPress = Input.GetAxisRaw("Horizontal");
        float vertPress = Input.GetAxisRaw("Vertical");
        // set the velocity
        GetComponent<Rigidbody2D>().velocity =
            new Vector2(horzPress, vertPress) * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "AlienBullet")
        {
            Explode(collision);
        }
    }

    public void Explode(Collision2D collision)
    {
        if (!isExploding)
        {
            isExploding = true;
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.shipExplosion);
            GetComponent<SpriteRenderer>().sprite = explodedShipImage;
            Destroy(collision.gameObject);
            DestroyObject(gameObject, 0.5f);
        }
    }
    // Update is called once per frame
    void Update () {
		if(Input.GetButtonDown("Jump"))
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.bulletFire);
        }
	}
}
