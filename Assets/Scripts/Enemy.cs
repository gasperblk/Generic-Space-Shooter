using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 0.5f;
    public float fireCooldown = 3.0f;
    public float oscillationSize = 1.0f;
    public float oscillationSpeed = 0.5f;
    public int pointValue;
    public float deactivateTimer = 15.0f;
    [Range(0.0f, 1.0f)]
    public float powerUpChance = 0.2f;
    public enum EnemyType{V1, V2, V3}
    public EnemyType enemyType;
    public int health = 1;

    private SpriteRenderer sprite;
    private float currentCooldown;
    private Vector3 startingPosition;

    [SerializeField]
    private GameObject enemyBullet;
    [SerializeField]
    private GameObject missile;

    private List<Transform> firePoints = new List<Transform>();

    [SerializeField]
    private GameObject fireSpeedPowerUp;
    [SerializeField]
    private GameObject moveSpeedPowerUp;
    [SerializeField]
    private GameObject shieldPowerUp;
    [SerializeField]
    private GameObject levelUpPowerUp;  

    private Animator animator;

    private Camera cam;
    private Vector3 downLeft;
    private Collider2D col;

    private bool powerUpDropped;
    private bool isDestroyed;

    // Start is called before the first frame update
    void Start()
    {   
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            firePoints.Add(gameObject.transform.GetChild(i).gameObject.transform);
        }
        sprite = GetComponent<SpriteRenderer>();
        currentCooldown = fireCooldown;
        startingPosition = transform.position;
        animator = GetComponent<Animator>();
        cam = Camera.main;
        downLeft = cam.ScreenToWorldPoint(new Vector3(0, 0, transform.position.y - cam.transform.position.y));
        col = GetComponent<Collider2D>();
        powerUpDropped = false;
        isDestroyed = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Shoot();
    }

    void Move()
    {   
        Vector3 tempY = transform.position;
        tempY.y -= speed * Time.deltaTime;
        if (enemyType is EnemyType.V2)
        {
            Vector3 tempX = startingPosition;
            tempX.x += oscillationSize * Mathf.Sin(Time.time * oscillationSpeed);
            tempY.x = tempX.x;
        }
        transform.position = tempY;
        if (transform.position.y < downLeft.y + col.bounds.size.y/2)
        {
            if (!isDestroyed)
            {
                isDestroyed = true;
                Selfdestruct();
            }
        }
    }

    void Shoot()
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown < 0.0f)
        {
            currentCooldown = fireCooldown;

            for (int i = 0; i < firePoints.Count; i++)
            {
                if (enemyType is EnemyType.V3 && i == 1)
                {
                    Instantiate(missile, firePoints[i].position, missile.transform.rotation);
                }
                else
                {
                    Instantiate(enemyBullet, firePoints[i].position, enemyBullet.transform.rotation);
                }
            }
        }
    }

    public void Selfdestruct()
    {   
        if (!powerUpDropped)
        {
            float dropPowerUp = Random.Range(0.0f, 1.0f);
            float whichPowerUp = Random.Range(0.0f, 1.0f);
            GameObject powerUpToSpawn;
            
            if (dropPowerUp < (powerUpChance + Score.GetScore()/10000))
            {
                if (enemyType is EnemyType.V3)
                {
                    if (whichPowerUp > 0.7f)
                    {
                        powerUpToSpawn = levelUpPowerUp;
                    }
                    else
                    {
                        powerUpToSpawn = shieldPowerUp;
                    }
                }
                else
                {
                    if (whichPowerUp >= 0.5f)
                    {
                        powerUpToSpawn = moveSpeedPowerUp;
                    }
                    else
                    {
                        powerUpToSpawn = fireSpeedPowerUp;
                    }
                }
                Instantiate(powerUpToSpawn, transform.position, Quaternion.identity);
            }
            powerUpDropped = true;
        }
        SoundManager.PlaySound("explosionSound");
        animator.SetTrigger("explode");
    }

    public int Hit()
    {   
        health--;
        if (health > 0)
        {
            SoundManager.PlaySound("hitSound");
            sprite.color = Color.red;
            StartCoroutine(ChangeColorBackAfter(0.2f, Color.white));
            return 0;
        }
        else
        {
            Selfdestruct();
            return pointValue;
        }
    }

    IEnumerator ChangeColorBackAfter(float duration, Color newColor)
    {   
        yield return new WaitForSeconds(duration);
        sprite.color = newColor;
    }
}
