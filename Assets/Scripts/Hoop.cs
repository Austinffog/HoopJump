using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoop : MonoBehaviour
{
    private int points;

    // Update is called once per frame
    void Update()
    {
        points = PlayerPrefs.GetInt("Points", points);
    }

    public void OnTriggerExit(Collider other)
    {
        if (gameObject.CompareTag("Hoop") && other.gameObject.name == ("Player"))
        {
            points += 5;
            PlayerPrefs.SetInt("Points", points);
            Destroy(gameObject);
        }
    }
}
