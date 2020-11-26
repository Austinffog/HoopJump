using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public static float health;
    public static int points;

    public void Play()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1f;
        Player.GameIsPaused = false;
        health = 100f;
        PlayerPrefs.SetFloat("health", health);
        points = 0;
        PlayerPrefs.SetInt("Points", points);
    }


}
