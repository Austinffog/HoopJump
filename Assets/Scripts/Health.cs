using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private Image HealthBar;
    public static float health = 100f;


    // Start is called before the first frame update
    void Start()
    {
        HealthBar = GetComponent<Image>();   
    }

    // Update is called once per frame
    void Update()
    {
        health = PlayerPrefs.GetFloat("health", health);
        HealthBar.fillAmount = health / 100;
    }
}
