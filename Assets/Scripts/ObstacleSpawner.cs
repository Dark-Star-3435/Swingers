using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject groundPrefab; // finds the game object (ground) to spawn as platforms
    public float respawnTime = 1.0f; // respawns platform/obstacle every 1 second
    private Vector2 screenBounds;

    void Start()
    {
        int vertical = (int)(Screen.height * 0.75f);// Andrew: "thanks I hate it". James: "that's not how maths works"
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, vertical, Camera.main.transform.position.z));
        StartCoroutine(ObstacleWave());
    }
    private void SpawnGround()
    {
        GameObject platform = Instantiate(groundPrefab) as GameObject; // spawns platforms within a certain range (lower half of the screen

        platform.transform.position = new Vector2(screenBounds.x * 2, Random.Range(-screenBounds.y, screenBounds.y) - screenBounds.y);
    }
    IEnumerator ObstacleWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime); // waits for 1 second and...
            SpawnGround(); // spawns ground platform
        }
    }
}
