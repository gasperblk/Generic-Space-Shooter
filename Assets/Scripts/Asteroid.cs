using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float speed = 1.0f;
    public float deactivateTimer = 11.0f;
    public int pointValue = 1;

    private float rotationSpeed;

    private Animator animator;
    private Camera cam;
    private Vector3 downLeft;
    private Collider2D col;

    private bool isDestroyed;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rotationSpeed = speed * ((Random.Range(0,2) * 2) -1);
        cam = Camera.main;
        downLeft = cam.ScreenToWorldPoint(new Vector3(0, 0, transform.position.y - cam.transform.position.y));
        col = GetComponent<Collider2D>();
        isDestroyed = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move() 
    {
        Vector3 temp = transform.position;
        temp.y -= speed * Time.deltaTime;
        this.gameObject.transform.Rotate(0.0f, 0.0f, rotationSpeed, Space.Self);
        transform.position = temp;
        if (transform.position.y < downLeft.y + col.bounds.size.y/2)
        {
            if (!isDestroyed)
            {
                isDestroyed = true;
                Selfdestruct();
            }
        }
    }

    public int Hit()
    {
        Selfdestruct();
        return pointValue;
    }

    public void Selfdestruct() 
    {   
        SoundManager.PlaySound("asteroidExplosionSound");
        Debug.Log(GetInstanceID());
        animator.SetTrigger("explode");
    }

}
