using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class LevelController : MonoBehaviour
{
    public GameObject popPanel;
    public Counter counter;
    public float timertowin;
    public PlayableDirector director;

    private int index;
    private bool level2end = false;
    private float time = 2.1f;

    private void Awake()
    {
        index = SceneManager.GetActiveScene().buildIndex;
        if (index != 0)
        {
            popPanel = GameObject.Find("UI/Popap/PopapPanel");
            counter = GameObject.Find("Popap").GetComponent<Counter>();
            timertowin = 90;
            if (index == 2)
                director = GameObject.Find("Player").GetComponent<PlayableDirector>();
        }
    }
    public void restart()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        Time.timeScale = 1;
    }

    public void mainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (index != 2)
                EndLevel();
            else
            {              
                director.Play();
                level2end = true;
            }
        }
    }

    public void NextLevel()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        if (index != 5)
        {
            SceneManager.LoadScene(index + 1);
            Time.timeScale = 1;
        }
        else if (index == 5)
        {
            Time.timeScale = 1;
            mainMenu();
        }

    }

    private void Update()
    {
        if (index == 5)
        {
            timertowin -= Time.deltaTime;
            if (timertowin <= 0)
                EndLevel();
            Debug.Log(timertowin);
        }

        if (level2end == true & index == 2)
        {
            time -= Time.deltaTime;
            if (time <= 0)
                EndLevel();
        }

        if(index == 3)
            Time.timeScale = 1;
    }

    public void EndLevel()
    {
        popPanel.SetActive(true);
        counter.ShowCounter();
        Time.timeScale = 0;
    }
}
