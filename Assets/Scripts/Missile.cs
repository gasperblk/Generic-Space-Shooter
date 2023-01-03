using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float speed = 1.0f;
    public float rotationSpeed = 200.0f;
    public int pointValue = 0;
    private Transform target;
    private Rigidbody2D rb2D;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Invoke("Selfdestruct", 10.0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector2 direction = (Vector2)target.position - rb2D.position;
        direction.Normalize();

        float toRotate = Vector3.Cross(direction, transform.up).z;

        rb2D.angularVelocity = -toRotate * rotationSpeed;

        rb2D.velocity = transform.up * speed;
    }

    public int Hit()
    {
        Selfdestruct();
        return pointValue;
    }

    public void Selfdestruct()
    {
        SoundManager.PlaySound("explosionSound");
        animator.SetTrigger("explode");
    }
}
