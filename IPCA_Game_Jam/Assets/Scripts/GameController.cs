using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject gameOverScreen;

    public Vector3 spawnPoint1, spawnPoint2, spawnPoint3, spawnPoint4;
    public GameObject enemyPrefab;
    private int round;


    public GameObject gameOverScreen;

    // Start is called before the first frame update
    void Start()
    {
        round = 0;
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
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Game");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    private void Round()
    {
        Invoke("Round", 0.5f * round * round + 10.0f);
        round++;
        // Maybe set a trigger for canvas to display something announcing new round
        Debug.Log("New round" + round);

        for(int i = 0; i < round; i++)
        {
            Invoke("SpawnEnemy", 3.0f);
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

    public void GameOver()
    {
        GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
        //Disable Player Script
        gameObject.GetComponent<Player>().enabled = false;

        //Disable Camera Script
        Camera.main.GetComponent<CameraMovement>().enabled = false;

        gameOverScreen.SetActive(true);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Game");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
