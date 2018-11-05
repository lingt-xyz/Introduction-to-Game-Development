using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameController : MonoBehaviour
{

    public int level;
    public GameObject enemyA;
    public GameObject enemyB;
    public GameObject weaponUpgrade;
    public GameObject boss;
    public int destroyEnemyA;
    public int destroyEnemyB;

    public float startWait;

    public Text scoreText;
    public Text progressText;
    public Slider healthBar;

    private bool gameOver;
    private bool gameEnd;
    private bool restart;
    private int score;

    // Use this for initialization
    void Start()
    {
        gameOver = false;
        gameEnd = false;
        restart = false;
        // TODO
        //progressText.text = "Game Start!";
        scoreText.text = "Scroe: 0";
        score = 0;
        destroyEnemyA = 0;
        destroyEnemyB = 0;
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
                case 1:
                    Instantiate(enemyA, new Vector3(0f, 2f, 35f), Quaternion.Euler(0, 180, 0));
                    //yield return new WaitForSeconds(0.5f);

                    Instantiate(enemyA, new Vector3(2f, 3f, 35f), Quaternion.Euler(0, 180, 0));
                    Instantiate(enemyA, new Vector3(-2f, 3f, 35f), Quaternion.Euler(0, 180, 0));
                    //yield return new WaitForSeconds(0.5f);

                    Instantiate(enemyA, new Vector3(4f, 4f, 35f), Quaternion.Euler(0, 180, 0));
                    Instantiate(enemyA, new Vector3(-4f, 4f, 35f), Quaternion.Euler(0, 180, 0));
                    yield return new WaitForSeconds(15);
                    break;
                case 2:
                    Instantiate(weaponUpgrade, weaponUpgrade.transform.position, Quaternion.identity);
                    yield return new WaitForSeconds(5.0f);
                    for (var i=0; i<5; i++)
                    {
                        GameObject gameObject = Instantiate(enemyB, new Vector3(-8, 4f, 35f), Quaternion.Euler(0, 180, 0));
                        Dodge dodge = gameObject.GetComponent<Dodge>();
                        dodge.speed = -2;
                        yield return new WaitForSeconds(1.5f);
                    }
                    yield return new WaitForSeconds(8.0f);
                    for (var i = 0; i < 5; i++)
                    {
                        GameObject gameObject = Instantiate(enemyB, new Vector3(8, 4f, 27f), Quaternion.Euler(0, 180, 0));
                        Dodge dodge = gameObject.GetComponent<Dodge>();
                        dodge.speed = 2;
                        yield return new WaitForSeconds(1.5f);
                    }
                    yield return new WaitForSeconds(8.0f);
                    var objectList = new List<GameObject>();
                    for (var i = 0; i < 5; i++)
                    {
                        GameObject gameObject = Instantiate(enemyB, new Vector3(-8, 4f, 20f), Quaternion.Euler(0, 180, 0));
                        Dodge dodge = gameObject.GetComponent<Dodge>();
                        dodge.speed = -2;
                        objectList.Add(gameObject);
                        yield return new WaitForSeconds(1.5f);
                    }
                    foreach (var gameObject in objectList)
                    {
                        if (gameObject != null)
                        {
                            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                        }
                    }

                    yield return new WaitForSeconds(5.0f);
                    break;
                case 3:
                    Instantiate(weaponUpgrade, weaponUpgrade.transform.position, Quaternion.identity);
                    yield return new WaitForSeconds(5.0f);
                    healthBar.gameObject.SetActive(true);
                    Instantiate(boss, new Vector3(0f, 3f, 25f), Quaternion.Euler(0, 180, 0));
                    yield return new WaitForSeconds(10);
                    break;
                default:
                    yield return new WaitForSeconds(10);
                    break;
            }


            // Caculate score
            if (level == 1 && destroyEnemyA == 5)
            {
                score += 500;
                UpdateScore();
            }
            else if (level == 2 && destroyEnemyB == 10)
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
            }
            else if (gameEnd)
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
