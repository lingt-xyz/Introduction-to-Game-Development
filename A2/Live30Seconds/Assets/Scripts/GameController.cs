using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameController : MonoBehaviour
{

    public int level;
    public GameObject enemyShipA;
    public GameObject enemyShipB;
    public GameObject weaponUpgrade;
    public GameObject boss;
    public int destroyEnemyShipA;
    public int destroyEnemyShipB;

    public float startWait;
    public float waveWait;

    public Text scoreText;
    public Text progressText;
    public Slider healthBar;


    private float spawnWait;
    private bool gameOver;
    private bool gameEnd;
    private bool restart;
    private int score;

    // Use this for initialization
    void Start()
    {
        spawnWait = 0.5f;
        gameOver = false;
        gameEnd = false;
        restart = false;
        // TODO
        //progressText.text = "Game Start!";
        scoreText.text = "Scroe: 0";
        score = 0;
        destroyEnemyShipA = 0;
        destroyEnemyShipB = 0;
        healthBar.gameObject.SetActive(false);
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
                    break;
                // Enemy ships B: In large sinusoidal trajectories containing 5 ships
                case 2:
                    Instantiate(weaponUpgrade, weaponUpgrade.transform.position, Quaternion.identity);
                    yield return new WaitForSeconds(3.0f);
                    for (int i = 0; i < 5; i++)
                    {
                        GameObject gameObject = Instantiate(enemyShipB, enemyShipB.transform.position, Quaternion.identity);
                        yield return new WaitForSeconds(spawnWait);
                    }
                    break;
                case 3:
                    Instantiate(weaponUpgrade, weaponUpgrade.transform.position, Quaternion.identity);
                    yield return new WaitForSeconds(3.0f);
                    healthBar.gameObject.SetActive(true);
                    Instantiate(boss, new Vector3(0f, 0f, 12f), Quaternion.identity);
                    break;
                default:
                    break;
            }

            yield return new WaitForSeconds(waveWait);

            // Caculate score
            if (level == 1 && destroyEnemyShipA == 5)
            {
                score += 500;
                UpdateScore();
            }
            else if (level == 2 && destroyEnemyShipB == 5)
            {
                score += 1000;
                UpdateScore();
            }
            level++;

            if (gameOver)
            {
                //progressText.text = "Press 'R' for Restart";
                SceneManager.LoadScene("Main");
                restart = true;
                break;
            }else if (gameEnd)
            {
                progressText.text = "Press 'R' for Restart";
                restart = true;
            }
        }
    }

    internal void Win()
    {
        progressText.text = "You Win!";
        gameEnd = true;
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
