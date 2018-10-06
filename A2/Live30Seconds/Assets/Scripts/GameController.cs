using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public int level;
    public GameObject enemyShipA;
    public GameObject enemyShipB;
    public GameObject weaponUpgrade;

    public float startWait;
    public float waveWait;

    public Text scoreText;
    public Text progressText;


    private float spawnWait;
    private bool gameOver;
    private bool restart;
    private int score;

    // Use this for initialization
    void Start()
    {
        spawnWait = 0.5f;
        gameOver = false;
        restart = false;
        // TODO
        //progressText.text = "Game Start!";
        scoreText.text = "Scroe: 0";
        StartCoroutine(SpawnWaves());
    }

    // Update is called once per frame
    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("Normal");
            }
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            // Level design

            switch (level)
            {
                // Leve1 1 : Enemy ships A: In triangle formations containing 5 ships
                case 1:
                    Instantiate(enemyShipA, new Vector3(0f, 0f, 16f), Quaternion.identity);
                    yield return new WaitForSeconds(spawnWait);
                    Instantiate(enemyShipA, new Vector3(2.5f, 0f, 16f), Quaternion.identity);
                    Instantiate(enemyShipA, new Vector3(-2.5f, 0f, 16f), Quaternion.identity);
                    yield return new WaitForSeconds(spawnWait);
                    Instantiate(enemyShipA, new Vector3(5f, 0f, 16f), Quaternion.identity);
                    Instantiate(enemyShipA, new Vector3(-5f, 0f, 16f), Quaternion.identity);
                    level++;
                    break;
                // Enemy ships B: In large sinusoidal trajectories containing 5 ships
                case 2:
                    for (int i = 0; i < 5; i++)
                    {
                        GameObject gameObject = Instantiate(enemyShipB, new Vector3(5.0f, 0.0f, 16.0f), Quaternion.identity);
                        yield return new WaitForSeconds(spawnWait);
                    }
                    level++;
                    break;
                case 3:
                    break;
                default:
                    break;
            }



            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                progressText.text = "Press 'R' for Restart";
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        progressText.text = "Game Over!";
        gameOver = true;
    }
}
