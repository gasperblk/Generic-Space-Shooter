using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    
    [Header("Asteroid Spawning")]
    public GameObject asteroid;
    public float spawnCooldownAst = 3.0f;
    public float spawnCooldownVariabitlityAst = 1.0f;
    [Header("Enemy Spawning")]
    public float spawnCooldownEnemy = 6.0f;
    public float spawnCooldownVariabitlityEnemy = 1.0f;
    [SerializeField]
    private List<GameObject> enemyList = new List<GameObject>();
    [SerializeField]
    private List<GameObject> formationList = new List<GameObject>();
    
    private float currentCooldownEnemy;
    private float currentCooldownAst;

    Camera cam;
    Vector3 upLeft;
    Vector3 upRight;

    // Start is called before the first frame update
    void Start()
    {   
        cam = Camera.main;
        upLeft = cam.ScreenToWorldPoint(new Vector3(0, Screen.height, transform.position.y - cam.transform.position.y));
        upRight = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, transform.position.y - cam.transform.position.y));
        currentCooldownAst = spawnCooldownAst;
        currentCooldownEnemy = spawnCooldownEnemy;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnAsteroid();
        SpawnEnemy();
    }

    void SpawnAsteroid()
    {
        currentCooldownAst -= Time.deltaTime;
        if (currentCooldownAst < 0.0f)
        {
            currentCooldownAst = spawnCooldownAst + Random.Range(-spawnCooldownVariabitlityAst, spawnCooldownVariabitlityAst);

            Vector3 spawnPositionAst = new Vector3(Random.Range(upLeft.x, upRight.x), upLeft.y, 1);

            Instantiate(asteroid, spawnPositionAst, asteroid.transform.rotation);

            //Reduce max cooldown - increase difficulty
            spawnCooldownAst -= Time.deltaTime;
        }
    }

    void SpawnEnemy()
    {
        currentCooldownEnemy -= Time.deltaTime;
        if (currentCooldownEnemy < 0.0f)
        {
            currentCooldownEnemy = spawnCooldownEnemy;

            int enemyIndex = Random.Range(0, enemyList.Count);
            int formationIndex = Random.Range(0, formationList.Count);
            
            Vector3 randomPos = new Vector3(Random.Range(upLeft.x, upRight.x), upRight.y, 1);
            Vector3 spawnPositionEnemy = randomPos;

            if (enemyList[enemyIndex].GetComponent<Enemy>().enemyType is Enemy.EnemyType.V3) {
                Instantiate(enemyList[enemyIndex], spawnPositionEnemy, enemyList[enemyIndex].transform.rotation);
            }
            else
            {
                for (int i = 0; i < formationList[formationIndex].transform.childCount; i++) 
                {   
                    Vector3 formationPos = formationList[formationIndex].transform.GetChild(i).transform.position;
                    spawnPositionEnemy.x += formationPos.x;
                    spawnPositionEnemy.y += formationPos.y;

                    Instantiate(enemyList[enemyIndex], spawnPositionEnemy, enemyList[enemyIndex].transform.rotation);
                    spawnPositionEnemy = randomPos;
                }
            }
            //Instantiate(enemyList[enemyIndex], spawnPositionEnemy, enemyList[enemyIndex].transform.rotation);
        }
    }
}
