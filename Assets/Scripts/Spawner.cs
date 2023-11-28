using UnityEngine;
using System.Collections.Generic;


public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnableObject
    {
        public GameObject prefab;
        [Range(0f, 1f)]
        public float spawnChance;
    }

    //public static Spawner Instance { get; private set; }
    [SerializeField]
    GameManager gameManager;

    public SpawnableObject[] objects;
    public float minSpawnRate = 1f;
    public float maxSpawnRate = 2f;

    private List<GameObject> obs;

    private void Awake()
    {
        obs = new List<GameObject>();
    }

    private void OnEnable()
    {
        //Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
        Spawn();
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Spawn()
    {
        float spawnChance = Random.value;

        foreach (var obj in objects)
        {
            if (spawnChance < obj.spawnChance)
            {
                GameObject obstacle = Instantiate(obj.prefab, transform);
                Obstacles o = obstacle.GetComponent<Obstacles>();
                o.spawner = this;
                o.gameManager = gameManager;
                obs.Add(obstacle);
                break;
            }

            spawnChance -= obj.spawnChance;
        }

        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }

    public void ClearGame()
    {
        foreach (GameObject obstacle in obs)
        {
            Destroy(obstacle);
        }

        obs.Clear();
    }

    public void RemoveObs(GameObject o)
    {
        obs.Remove(o);
    }

}
