using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 6.0f;
    public float deactivateTimer = 2.0f;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        Invoke("Selfdestruct", deactivateTimer);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move() 
    {
        Vector3 temp = transform.position;
        temp.y += speed * Time.deltaTime;
        transform.position = temp;
    }

    void Selfdestruct() 
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag is "Enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            int pointsAwarded = enemy.Hit();
            gameManager.UpdateScore(pointsAwarded);
            Selfdestruct();
        } 
        else if (other.tag is "Asteroid")
        {
            Asteroid asteroid = other.gameObject.GetComponent<Asteroid>();
            int pointsAwarded = asteroid.Hit();
            gameManager.UpdateScore(pointsAwarded);
            Selfdestruct();
        }
        else if (other.tag is "Missile")
        {
            Missile missile = other.gameObject.GetComponent<Missile>();
            int pointsAwarded = missile.Hit();
            gameManager.UpdateScore(pointsAwarded);
            Selfdestruct();
        }
    }
}
