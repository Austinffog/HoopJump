using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class Player : MonoBehaviour
{
    private float speed = 20f;

    protected Joystick joystick;

    private string storeID = "3585764";
    private string rewardedVideoID = "rewardedVideo";

    private int no;

    public float health = 100f;
    public static int points;
    public Text pointAmount;
    private int highScore;
    public Text highScoreAmount;

    private float timer = 0f;
    public Text timerValue;

    public GameObject GameOverMenu;
    public static bool GameIsPaused = false;
    public GameObject PauseMenu;
    public Text Message1, Message2, Message3, Message4, Message5;
    public GameObject background;

    private void Start()
    {
        Advertisement.Initialize(storeID, false);
        joystick = FindObjectOfType<Joystick>();
        health = PlayerPrefs.GetFloat("health", health);

        no = Random.Range(1, 4);

        highScore = PlayerPrefs.GetInt("HighScore", highScore);
        highScoreAmount.text = "HS: " + highScore.ToString();

        GameOverMenu.SetActive(false);

    }


    void FixedUpdate()
    {
        
        points = PlayerPrefs.GetInt("Points", points);
        pointAmount.text = points.ToString();

        if (health <= 0)
        {
            GameOver();
        }

        HS();

        Movement();

        PointsScore();

        Message();

        timer += Time.fixedDeltaTime;
        timerValue.text = timer.ToString("F1");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.name == ("Player") && collision.gameObject.name == ("Ground"))
        {
            health -= 2;
            PlayerPrefs.SetFloat("health", health);
        }
    }

    private void HS()
    {
        if (points > highScore)
        {
            highScore = points;
            highScoreAmount.text = "HS: " + highScore.ToString();
        }
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();

    }

    private void Movement()
    {
        var rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = new Vector3(joystick.Horizontal * speed, rigidbody.velocity.y,
            joystick.Vertical * speed);

        if (joystick.Horizontal != 0f || joystick.Vertical != 0f)
        {
            // Create a new vector of the horizontal and vertical inputs.
            Vector3 targetDirection = new Vector3(joystick.Horizontal, 0f, joystick.Vertical);

            // Create a rotation based on this new vector assuming that up is the global y axis.
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);

            // Create a rotation that is an increment closer to the target rotation from the player's rotation.
            Quaternion newRotation = Quaternion.Lerp(rigidbody.rotation, targetRotation, 1.5f * Time.fixedDeltaTime);

            // Change the players rotation to this new rotation.
            rigidbody.MoveRotation(newRotation);
        }
    }

    private void GameOver()
    {
        GameOverMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void PauseGame()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Resume()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameOverMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        health = 100f;
        PlayerPrefs.SetFloat("health", health);
        points = 0;
        PlayerPrefs.SetInt("Points", points);

    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        GameOverMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        health = 100f;
        PlayerPrefs.SetFloat("health", health);
        points = 0;
        PlayerPrefs.SetInt("Points", points);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void PointsScore()
    {
        if (points < 50 & timer >= 100)
        {
            GameOver();
        }
        if (points < 200 & timer >= 300)
        {
            GameOver();
        }
        if (points < 500 & timer >= 500)
        {
            GameOver();
        }
        if (points < 1500 & timer >= 1000)
        {
            GameOver();
        }
    }

    private void Message()
    {
        if (timer >= 0f)
        {
            Message1.enabled = true;
            background.SetActive(true);
        }
        if (timer >= 3.5f)
        {
            Message1.enabled = false;
            background.SetActive(false);
        }
        if (timer >= 100.5f)
        {
            Message2.enabled = true;
            background.SetActive(true);
        }
        if (timer >= 103.5f)
        {
            Message2.enabled = false;
            background.SetActive(false);
        }
        if (timer >= 300.5f)
        {
            Message3.enabled = true;
            background.SetActive(true);
        }
        if (timer >= 303.5f)
        {
            Message3.enabled = false;
            background.SetActive(false);
        }
        if (timer >= 500.5f)
        {
            Message4.enabled = true;
            background.SetActive(true);
        }
        if (timer >= 503.5f)
        {
            Message4.enabled = false;
            background.SetActive(false);
        }
        if (timer >= 1000.5f)
        {
            Message5.enabled = true;
            background.SetActive(true);
        }
        if (timer >= 1003.5f)
        {
            Message5.enabled = false;
            background.SetActive(false);
        }
    }

    public void Reward()
    {

        if (Advertisement.IsReady(rewardedVideoID))
        {

            var options = new ShowOptions { resultCallback = Options };
            Advertisement.Show(rewardedVideoID, options);

        }

    }

    public void Options(ShowResult Result)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameOverMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        health = 100f;
        PlayerPrefs.SetFloat("health", health);

        switch (Result)
        {
            case ShowResult.Finished:

                if (no == 1)
                {
                    points = 15;
                    PlayerPrefs.SetInt("Points", points);
                }
                else if (no == 2)
                {
                    points = 1;
                    PlayerPrefs.SetInt("Points", points);
                }
                else if (no == 3)
                {
                    points = 40;
                    PlayerPrefs.SetInt("Points", points);
                }
                break;
        }
    }

}
