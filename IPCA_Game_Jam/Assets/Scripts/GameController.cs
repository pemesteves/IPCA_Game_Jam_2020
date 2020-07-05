using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject gameOverScreen;

    public Vector3 spawnPoint1, spawnPoint2, spawnPoint3, spawnPoint4;
    public Vector3 lightSpawnPoint1, lightSpawnPoint2, lightSpawnPoint3, lightSpawnPoint4, lightSpawnPoint5, lightSpawnPoint6;
    public GameObject lightOrbPrefab;
    public GameObject enemyPrefab, bossPrefab;
    private int round;
    private int points;

    public GameObject waveIncomingCanvas;
    public Text waveIncomingText;
    public GameObject[] waveIncomingPanels;

    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        round = 0;
        points = 0;
        IncomingRound();
        InvokeRepeating("SpawnLightOrb", 10.0f, 15.0f);
        Invoke("Round", 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey (KeyCode.Escape)) {
            Application.Quit();
        }
        scoreText.text = "SCORE: " + points;
    }

    public void GameOver()
    {
        GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
        //Disable Player Script
        gameObject.GetComponent<Player>().enabled = false;

        //Disable Camera Script
        Camera.main.GetComponent<CameraMovement>().enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        gameOverScreen.SetActive(true);

        CancelInvoke("IncomingRound");
        CancelInvoke("Round");
        CancelInvoke("SpawnEnemy");

        foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Enemy e = enemy.GetComponent<Enemy>();
            e.SetPlayerDead();
        }

    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Game");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    private void IncomingRound()
    {
        waveIncomingCanvas.SetActive(false);
        waveIncomingCanvas.SetActive(true);
        Image canvasImage = waveIncomingCanvas.GetComponent<Image>();
        canvasImage.color = NewColor(canvasImage.color, 1.0f);
        canvasImage.CrossFadeAlpha(0.0f, 4.0f, true);

        waveIncomingText.text = "Wave " + (round+1) + " incoming!";
        waveIncomingText.color = NewColor(waveIncomingText.color, 1.0f);
        waveIncomingText.CrossFadeAlpha(0.0f, 4.0f, true);

        foreach(GameObject c in waveIncomingPanels)
        {
            canvasImage = c.GetComponent<Image>();
            canvasImage.color = NewColor(canvasImage.color, 1.0f);
            canvasImage.CrossFadeAlpha(0.0f, 4.0f, true);

            canvasImage = c.transform.GetChild(0).GetComponent<Image>();
            canvasImage.color = NewColor(canvasImage.color, 1.0f);
            canvasImage.CrossFadeAlpha(0.0f, 4.0f, true);
        }
    }

    private Color NewColor(Color c, float a)
    {
        return new Color(c.r, c.g, c.b, a);
    }

    private void Round()
    {
        Invoke("IncomingRound", 0.5f * round * round + 5.0f);
        Invoke("Round", 0.5f * round * round + 10.0f);
        round++;
        points += 10;

        for(int i = 0; i < round; i++)
        {
            Invoke("SpawnEnemy", 3.0f);
        }
        if(round % 5 == 0)
        {
            for(int i = 0; i < round/5; i++)
            {
                Invoke("SpawnBoss", 3.0f);
            }
        }
    }

    private void SpawnEnemy()
    {
        int rand = Random.Range(0,4);
        Vector3 spawn = new Vector3(0,0,0);
        if(rand == 0)
        {
            spawn = spawnPoint1;
        } else if(rand == 1)
        {
            spawn = spawnPoint2;
        } else if(rand == 2)
        {
            spawn = spawnPoint3;
        } else if(rand == 3)
        {
            spawn = spawnPoint4;
        }
        GameObject e = Instantiate(enemyPrefab, spawn, Quaternion.identity) as GameObject;
        e.GetComponent<Enemy>().gc = this;
    }

    private void SpawnLightOrb()
    {
        int rand = Random.Range(0,6);
        Vector3 spawnPoint = new Vector3 (0,0,0);
        if(rand == 0)
        {
            spawnPoint = lightSpawnPoint1;
        } else if(rand == 1)
        {
            spawnPoint = lightSpawnPoint2;
        } else if(rand == 2)
        {
            spawnPoint = lightSpawnPoint3;
        } else if(rand == 3)
        {
            spawnPoint = lightSpawnPoint4;
        } else if(rand == 4)
        {
            spawnPoint = lightSpawnPoint5;
        } else if(rand == 5)
        {
            spawnPoint = lightSpawnPoint6;
        }
        Instantiate(lightOrbPrefab, spawnPoint, Quaternion.identity);
    }

    private void SpawnBoss()
    {
        int rand = Random.Range(0,4);
        Vector3 spawn = new Vector3(0,0,0);
        if(rand == 0)
        {
            spawn = spawnPoint1;
        } else if(rand == 1)
        {
            spawn = spawnPoint2;
        } else if(rand == 2)
        {
            spawn = spawnPoint3;
        } else if(rand == 3)
        {
            spawn = spawnPoint4;
        }
        GameObject e = Instantiate(bossPrefab, spawn, Quaternion.identity) as GameObject;
        e.GetComponent<Enemy>().gc = this;
    }

    public void KillEnemy(int value)
    {
        points += value;
    }
}
