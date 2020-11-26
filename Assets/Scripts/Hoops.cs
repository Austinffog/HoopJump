using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Hoops : MonoBehaviour
{
    public GameObject[] randomObject;
    public Vector3 spawnValues;
    public float spawnWait;
    public float spawnMostWait;
    public float spawnLeastWait;
    public int startWait;

    int objectsRandom;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitFirst());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        spawnWait = Random.Range(spawnLeastWait, spawnMostWait);

    }

    IEnumerator waitFirst()
    {
        yield return new WaitForSeconds(startWait);

        while (true)
        {
            objectsRandom = Random.Range(0, 6);
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), Random.Range(0, spawnValues.y), Random.Range(-spawnValues.z, spawnValues.z));

            Instantiate(randomObject[objectsRandom], spawnPosition + transform.TransformPoint(0, 0, 0), gameObject.transform.rotation);

            yield return new WaitForSeconds(spawnWait);
        }

    }

}