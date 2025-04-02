using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject asteroid;

    public int numDestroyed;
    public int score;

    public TextMeshProUGUI scoreText;

    private Vector2[] spawnPositions = new Vector2[5];

    // Start is called before the first frame update
    void Start()
    {
        spawnPositions[0] = new Vector2(0,6);
        spawnPositions[1] = new Vector2(3, 6);
        spawnPositions[2] = new Vector2(5,6);
        spawnPositions[3] = new Vector2(-3,6);
        spawnPositions[4] = new Vector2(-5, 6);

        SpawnAsteroid();
    }

    // Update is called once per frame
    void Update()
    {
        if (numDestroyed == 5)
        {
            SpawnAsteroid();
            numDestroyed += 1;
        }
        else if (numDestroyed == 21)
        {
            SpawnAsteroid();
            numDestroyed += 1;
        }
        else if (numDestroyed == 52)
        {
            SpawnAsteroid();
            numDestroyed += 1;
        }
        else if (numDestroyed == 103)
        {
            SpawnAsteroid();
            numDestroyed += 1;
        }

        scoreText.text = "Score: " + score;
    }

    public void SpawnAsteroid()
    {
        int randomIndex = Random.Range(0, spawnPositions.Length);

        Instantiate(asteroid, spawnPositions[randomIndex], Quaternion.identity);
    }
}
