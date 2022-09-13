using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 0;
    private Rigidbody2D rigidBody = null;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float posX = rigidBody.position.x + speed * Time.deltaTime;
        rigidBody.MovePosition(new Vector2(posX, rigidBody.position.y));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
            speed = -speed;
    }
}
