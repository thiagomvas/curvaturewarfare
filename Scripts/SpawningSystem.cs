using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningSystem : MonoBehaviour
{
    [SerializeField] private float speedCap;
    [SerializeField] private SoundManager sm;
    [SerializeField] private GameController gc;
    [SerializeField] private Boundaries[] boundaries;
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private GameObject playerRef;
    [SerializeField] private PlayerSide[] sides;
    [SerializeField] private GameObject disabledVFX;


    [Header("Settings")]
    [SerializeField] private int nOfIncrements;
    [Space]
    [SerializeField] private float spawnDelay;
    [SerializeField] private float minSpawnDelay;
    [Space]
    [SerializeField] private float difficultyMultiplier;
    [SerializeField] private float increment;
    [Space]
    [SerializeField] private float incrementDelay;
    [SerializeField] private float bossSpawnDelay;

    float timer;
    float nextIncrement;
    float nextSpawn;
    float nextBoss;
    void Start()
    {
        nextIncrement = incrementDelay;
        nextBoss = bossSpawnDelay;
        difficultyMultiplier = 1;
    }

    void Update()
    {
        timer += Time.deltaTime;

        //Increase difficulty multiplier
        if (timer >= nextIncrement)
        {
            nextIncrement = timer + incrementDelay;

            // Adds an increment to the multiplier
            difficultyMultiplier += increment;


            // Takes the previous increment, and multiplies it by (1 + multiplier/80)
            increment *= 1f + difficultyMultiplier / 75f; 
            


            increment = Mathf.Round(increment * 1000f) / 1000f;
            nOfIncrements += 1;

            if (spawnDelay > 2f) spawnDelay -= spawnDelay * difficultyMultiplier / 25;
            else if (spawnDelay > 1f) spawnDelay -= 0.1f;
        }

        
        if(timer >= nextSpawn)
        {
            nextSpawn = timer + spawnDelay;
            Spawn(0);
        }

        if(timer >= nextBoss)
        {
            nextBoss = timer + bossSpawnDelay;
            Spawn(1);
        }
    }
    

    private void Spawn(int i)
    {
        int r = Random.Range(0, boundaries.Length);
        Vector2 spawnAt = new Vector2(Random.Range(boundaries[r].minX, boundaries[r].maxX), 
                                      Random.Range(boundaries[r].minY, boundaries[r].maxY));

        //int s = Random.Range(0, enemies.Length);
        GameObject enemyObj = Instantiate(enemies[i], spawnAt, Quaternion.identity);

        Enemy enemy = enemyObj.GetComponent<Enemy>();

        enemy.player = playerRef;
        enemy.MaxHealth *= difficultyMultiplier;
        enemy.speed *= difficultyMultiplier;

        if (enemy.speed > speedCap) enemy.speed = speedCap;


        enemy.pointsOnKill = (int)(enemy.pointsOnKill * difficultyMultiplier);
        if (i == 1) enemy.moneyOnKill = (int)(enemy.moneyOnKill * difficultyMultiplier/2);
        if (i == 1) enemy.disabledSide = sides[Random.Range(0, sides.Length)];
        if (i == 1) enemy.disabledVFX = disabledVFX;
        enemy.gc = gc;
        enemy.sm = sm;
    }

}

[System.Serializable]
public class Boundaries
{
    public int minX;
    public int maxX;
    public int minY;
    public int maxY;

}
