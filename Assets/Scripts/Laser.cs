using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private Transform FirePoint;

    private GameManager gameManager;
    private Player player;
    private Vector3 positionOffset;
    private Quaternion rotationOffset;

    public float fireCooldown;
    private float currentCooldown = 0.0f;
    private bool canFire;
    private Material laserMaterial;
    Color laserColor;
    Color laserSecondColor;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        laserMaterial = GetComponent<LineRenderer>().sharedMaterial;
        laserColor = new Color(0.0f, 0.5f, 1.0f);
        laserSecondColor = Color.yellow;
        laserMaterial.SetColor("Color_Laser", laserColor);
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        fireCooldown = player.fireCooldown;
        canFire = true;
        Transform parent = FirePoint;
        positionOffset = parent.position - transform.position;
        rotationOffset = Quaternion.Inverse(parent.localRotation * transform.localRotation);
        FirePoint = parent;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ReadyToFire();
    }

    void LaserShoot()
    {
        RaycastHit2D[] hits;
        hits = Physics2D.RaycastAll(FirePoint.position, transform.up, 100.0f);

        if (player.laserEnabled)
        {   
            laserMaterial.SetColor("Color_Laser", laserSecondColor);
            StartCoroutine(ChangeColorBackAfter(0.2f, laserColor));
            
            for (int i = 0; i < hits.Length; i++)
            {
                Collider2D hit = hits[i].collider;
                if (hit.tag is "Enemy")
                {
                    Enemy enemy = hit.gameObject.GetComponent<Enemy>();
                    int pointsAwarded = enemy.Hit();
                    gameManager.UpdateScore(pointsAwarded);
                }
                else if (hit.tag is "Asteroid")
                {
                    Asteroid asteroid = hit.gameObject.GetComponent<Asteroid>();
                    int pointsAwarded = asteroid.Hit();
                    gameManager.UpdateScore(pointsAwarded);
                }

            }
        }
        
    }

    IEnumerator ChangeColorBackAfter(float duration, Color newColor)
    {   
        yield return new WaitForSeconds(duration);
        laserMaterial.SetColor("Color_Laser", newColor);
    }

    void ReadyToFire()
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown < 0.0f)
        {
            canFire = true;
        }
        if (canFire)
        {
            LaserShoot();
            canFire = false;
            currentCooldown = fireCooldown;
        }

    }
}
