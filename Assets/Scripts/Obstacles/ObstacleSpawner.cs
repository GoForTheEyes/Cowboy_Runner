using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour {

    [SerializeField]
#pragma warning disable 0649
    private GameObject[] obstacles;
#pragma warning restore 0649


    private List<GameObject> obstaclesForSpawning = new List<GameObject>();
    private Vector3 spawnPosition;

    private void Awake()
    {
        InitializeObstacles();
        spawnPosition = new Vector3(transform.position.x, transform.position.y, -2f);
    }

    // Use this for initialization
    void Start()
    {
        StartCoroutine(SpawnRandomObstacle());
    }

    void InitializeObstacles()
    {
        int index = 0;
        for (int i = 0; i < obstacles.Length * 3; i++)
        {
            GameObject obj = Instantiate(obstacles[index], spawnPosition, Quaternion.identity);
            obj.transform.parent = transform;
            obstaclesForSpawning.Add(obj);
            obstaclesForSpawning[i].SetActive(false);
            index++;
            //it makes sure we iterate a new cycle over the obstacles array
            if (index == obstacles.Length) index = 0;

        }
    }

    void Shuffle()
    {
        for (int i = 0; i < obstaclesForSpawning.Count; i++)
        {
            GameObject temp = obstaclesForSpawning[i];
            int random = Random.Range(i, obstaclesForSpawning.Count);
            obstaclesForSpawning[i] = obstaclesForSpawning[random];
            obstaclesForSpawning[random] = temp;
        }

    }

    IEnumerator SpawnRandomObstacle()
    {
        yield return new WaitForSeconds(Random.Range(1.5f, 4.5f));
        int index = Random.Range(0, obstaclesForSpawning.Count);
        while (true)
        {
            //checks if obstacle is already in game
            if (!obstaclesForSpawning[index].activeInHierarchy)
            {
                obstaclesForSpawning[index].SetActive(true);
                //reset position in case the obstacle was previously instantiated
                obstaclesForSpawning[index].transform.position = spawnPosition;
                break;
            } else
            {
                index = Random.Range(0, obstaclesForSpawning.Count);
            }
        }

        //keep iterating infinitely
        StartCoroutine(SpawnRandomObstacle());
    }



	
	// Update is called once per frame
	void Update () {
		
	}
}
