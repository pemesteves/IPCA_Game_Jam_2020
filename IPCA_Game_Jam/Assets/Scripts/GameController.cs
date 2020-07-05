using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject gameOverScreen;

    public Vector3 spawnPoint1, spawnPoint2, spawnPoint3, spawnPoint4;
    public GameObject enemyPrefab, bossPrefab;
    private int round;

    public GameObject waveIncomingCanvas;
    public Text waveIncomingText;
    public GameObject[] waveIncomingPanels;

    // Start is called before the first frame update
    void Start()
    {
        round = 0;
        IncomingRound();
        Invoke("Round", 5.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameOver()
    {
        GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
        //Disable Player Script
        gameObject.GetComponent<Player>().enabled = false;

        //Disable Camera Script
        Camera.main.GetComponent<CameraMovement>().enabled = false;

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

        for(int i = 0; i < round; i++)
        {
            Invoke("SpawnEnemy", 3.0f);
        }
        if(round % 5 == 0)
        {
            Invoke("SpawnBoss", 3.0f);
        }
    }

    private void SpawnEnemy()
    {
        int rand = Random.Range(0,4);
        if(rand == 0)
        {
            Instantiate(enemyPrefab, spawnPoint1, Quaternion.identity);
        } else if(rand == 1)
        {
            Instantiate(enemyPrefab, spawnPoint2, Quaternion.identity);
        } else if(rand == 2)
        {
            Instantiate(enemyPrefab, spawnPoint3, Quaternion.identity);
        } else if(rand == 3)
        {
            Instantiate(enemyPrefab, spawnPoint4, Quaternion.identity);
        }
    }

    private void SpawnBoss()
    {
        int rand = Random.Range(0,4);
        if(rand == 0)
        {
            Instantiate(bossPrefab, spawnPoint1, Quaternion.identity);
        } else if(rand == 1)
        {
            Instantiate(bossPrefab, spawnPoint2, Quaternion.identity);
        } else if(rand == 2)
        {
            Instantiate(bossPrefab, spawnPoint3, Quaternion.identity);
        } else if(rand == 3)
        {
            Instantiate(bossPrefab, spawnPoint4, Quaternion.identity);
        }
    }
}
