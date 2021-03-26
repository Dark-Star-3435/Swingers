using UnityEngine;

public class Obtacles : MonoBehaviour
{
    public float speed = 10.0f; // speed of obstacles
    private Rigidbody2D rb;
    private Vector2 screenBounds;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-speed, 0);
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z)); // creates cameras borders

    }
    void Update()
    {
        if (transform.position.x < screenBounds.x * -2)
        {
            Destroy(this.gameObject); // deletes game objects that fall outside of camera view
        }
    }
}
