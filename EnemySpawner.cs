using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject followerPrefab;
    [SerializeField]
    private GameObject rammerPrefab;
    [SerializeField]
    private GameObject rangerPrefab;
    [SerializeField]
    private GameObject fastShotPrefab;

    [SerializeField]
    private float followerInterval = 3.5f;
    [SerializeField]
    private float rammerInterval = 6.5f;
    [SerializeField]
    private float rangerInterval = 10f;
    [SerializeField]
    private float fastShotInterval = 30f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy(followerInterval, followerPrefab));
        StartCoroutine(spawnEnemy(rammerInterval, rammerPrefab));
        StartCoroutine(spawnEnemy(rangerInterval, rangerPrefab));
        StartCoroutine(spawnEnemy(fastShotInterval, fastShotPrefab));


    }
    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0), Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
