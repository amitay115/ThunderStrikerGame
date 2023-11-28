using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetsSpawner : MonoBehaviour
{
    [SerializeField]
    float maxX;

    [SerializeField]
    float spawnInterval;


    public GameObject[] Assets;

    public static AssetsSpawner instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
    }


    // Start is called before the first frame update
    void Start()
    {
        // SpawnCandy();
        StartSpawningAssets();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void SpawnAsset()
    {
        int rand = Random.Range(0, Assets.Length);

        float randomX = Random.Range(-maxX, maxX);

        Vector3 randomPos = new Vector3(randomX, transform.position.y, transform.position.z);


        Instantiate(Assets[rand], randomPos, transform.rotation);

    }
    IEnumerator SpawnAssets()
    {
        yield return new WaitForSeconds(2f);

        while (true)
        {
            SpawnAsset();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void StartSpawningAssets()
    {
        StartCoroutine("SpawnAssets");
    }

    public void StopSpawningAssets()
    {
        StopCoroutine("SpawnAssets");
    }
}
