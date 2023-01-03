using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public float speed = 7.0f;
    public float fireCooldown = 0.8f;
    public bool isShielded = false;
    public bool canFire;
    public bool laserEnabled;

    private float currentCooldown = 0.0f;
    private int playerLevel;

    [SerializeField]
    private GameObject playerBullet;
    [SerializeField]
    private Transform firePoint1;
    [SerializeField]
    private Transform firePoint2;
    [SerializeField]
    private Transform firePoint3;

    public LineRenderer lineRenderer;

    private Camera cam;
    private Vector3 upLeft;
    private Vector3 downRight;
    private Collider2D col;
    
    // Start is called before the first frame update
    void Start()
    {
        playerLevel = 1;
        canFire = true;
        laserEnabled = false;
        lineRenderer.enabled = false;
        cam = Camera.main;
        upLeft = cam.ScreenToWorldPoint(new Vector3(0, Screen.height, transform.position.y - cam.transform.position.y));
        downRight = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0, transform.position.y - cam.transform.position.y));
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Shoot();
    }

    void Move() 
    {
        Vector3 temp = transform.position;
        temp.x += Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        temp.y += Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;
        
        if (temp.x > downRight.x - col.bounds.size.x/2) 
        {
            temp.x = downRight.x - col.bounds.size.x/2;
        } else if (temp.x < upLeft.x + col.bounds.size.x/2)
        {
            temp.x = upLeft.x + col.bounds.size.x/2;
        }
        if (temp.y > upLeft.y - col.bounds.size.y/2) 
        {
            temp.y = upLeft.y - col.bounds.size.y/2;
        } else if (temp.y < downRight.y + col.bounds.size.y/2) 
        {
            temp.y = downRight.y + col.bounds.size.y/2;
        }
        
        transform.position = temp;
    }

    void Shoot() 
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown < 0.0f) 
        {
            canFire = true;
        }
        
        if (Input.GetButton("Jump")) 
        {
            if (canFire) 
            {
                SoundManager.PlaySound("laserSound");
                canFire = false;
                currentCooldown = fireCooldown;
                switch (playerLevel)
                {
                    case 1:
                        Instantiate(playerBullet, firePoint2.position, Quaternion.identity);
                        break;
                    case 2:
                        Instantiate(playerBullet, firePoint1.position, Quaternion.identity);
                        Instantiate(playerBullet, firePoint3.position, Quaternion.identity);
                        break;
                    case 3:
                        Instantiate(playerBullet, firePoint1.position, Quaternion.identity);
                        // Laser do dammage
                        Instantiate(playerBullet, firePoint3.position, Quaternion.identity);
                        break;
                    default:
                        Debug.Log("Invalid level: How did this even happen?");
                        break;
                }
            }
            if (playerLevel == 3)
            {
                lineRenderer.enabled = true;
                lineRenderer.gameObject.SetActive(true);
            }
        }
        else
        {
            lineRenderer.enabled = false;
            lineRenderer.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag is "Enemy" || other.tag is "EnemyBullet" || other.tag is "Asteroid" || other.tag is "Missile")
        {
            if (isShielded)
            {
                transform.Find("Shield").gameObject.GetComponent<Shield>().ShieldOff();
                if (other.gameObject.GetComponent<Animator>())
                {
                    Animator otherAnimator = other.gameObject.GetComponent<Animator>();
                    otherAnimator.SetTrigger("explode");
                }
                else {
                    Destroy(other.gameObject);
                }
            }
            else
            {
                SceneManager.LoadScene("Game Over");
            }
        }
    }

    public void LevelUp(int level)
    {
        playerLevel = level;
        if (level == 3) //Move the Fire Points around so they fit better
        {
            Vector3 temp1 = firePoint1.position;
            Vector3 temp2 = firePoint2.position;
            Vector3 temp3 = firePoint3.position;
            temp1.x -= 0.02f;
            temp1.y += 0.08f;
            temp2.y -= 0.245f;
            temp3.x += 0.02f;
            temp3.y += 0.08f;
            firePoint1.position = temp1;
            firePoint2.position = temp2;
            firePoint3.position = temp3;
        }
    }
}
