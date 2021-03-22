using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject groundPrefab;
    public float respawnTime = 1.0f;
    private Vector2 screenBounds;
    private Vector2 groundBounds;

    void Start()
    {
        int vertical = (int)(Screen.height * 0.75f);// Andrew: "thanks I hate it". James: "that's not how maths works"
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, vertical, Camera.main.transform.position.z));
        StartCoroutine(ObstacleWave());
    }
    private void SpawnGround()
    {
        GameObject a = Instantiate(groundPrefab) as GameObject;

        a.transform.position = new Vector2(screenBounds.x * 2, Random.Range(-screenBounds.y, screenBounds.y) - screenBounds.y);
    }
    IEnumerator ObstacleWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            SpawnGround();
        }
    }
}
