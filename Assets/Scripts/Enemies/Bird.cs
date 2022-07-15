using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird: MonoBehaviour{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float timeToSwitchDirection;
    private BoxCollider2D boxCollider2d;
    private Rigidbody2D rigidbody2D;
    private bool isLeft = true;
    private float timeToSwitchDirectionCounter;

    private void Awake()
    {
        TryGetComponent(out boxCollider2d);
        TryGetComponent(out rigidbody2D);
    }

    // Start 
    void Start()
    {
        timeToSwitchDirectionCounter = timeToSwitchDirection;
    }

    // Update
    void Update()
    {
        timeToSwitchDirectionCounter -= Time.deltaTime;
        if (timeToSwitchDirectionCounter <= 0)
        {
            isLeft = !isLeft;
            float newAngle = isLeft ? 0 : 180;
            transform.eulerAngles = new Vector2(0, newAngle);
            timeToSwitchDirectionCounter = timeToSwitchDirection;
        }

        // Fly
        float walkDirection = isLeft ? -1 : 1;
        rigidbody2D.velocity = Vector2.right * speed * walkDirection;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            FindObjectOfType<GameController>().ActivateDefeatPanel();
        }
    }
}