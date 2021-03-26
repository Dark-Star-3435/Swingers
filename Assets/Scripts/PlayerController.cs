using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public delegate void PlayerDelegate();
    public static event PlayerDelegate OnPlayerDied;
    public static event PlayerDelegate OnPlayerScored;
    public Animator animator; // This allows the code to access the animations

    public float jumpForce = 250; // player goes high
    
    bool grounded; // for is grounded

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Grounded", grounded); //used to tell the animator when the player is grounded
        if (Input.GetKeyDown(KeyCode.Space)) // when spacebar is pressed
        {
            if (grounded == true)
            {
                rb.velocity = Vector3.zero;
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Force); // add upward force
            }
        }
    }

    //poggies

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "ScoreZone") // if player touches a collider tagged score zone it triggers event Player Scored
        {
            OnPlayerScored();
        }

        if (collision.gameObject.tag == "DeadZone") // tells player they suck
        {
            rb.simulated = false; // stops player movement when they lose
            OnPlayerDied();
        }
    }

    
    private void OnCollisionEnter2D(Collision2D collision) //if player is in contact with the ground grounded = true 
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
            Time.timeScale += 0.01f; // time scale speeds up gradualy over time but only when in contact with the ground tag
        }
    }

    private void OnCollisionExit2D(Collision2D collision) //when player leaves ground then grounded = false and player cannot jump in mid air
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = false;
        }
        
    }
}
