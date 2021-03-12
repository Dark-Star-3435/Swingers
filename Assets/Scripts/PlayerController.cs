using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public delegate void PlayerDelegate();
    public static event PlayerDelegate OnPlayerDied;
    public static event PlayerDelegate OnPlayerScored;
    public Animator animator; // This allows the code to access the animations

    public float jumpForce = 250;
    public Vector3 startPos;
    
    bool grounded;

    Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        GameManager.OnGameStarted += OnGameStarted;
        GameManager.OnGameOverConfirmed += OnGameOverConfirmed;
    }
    private void OnDisable()
    {
        GameManager.OnGameStarted -= OnGameStarted;
        GameManager.OnGameOverConfirmed -= OnGameOverConfirmed;
    }

    void OnGameStarted()
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.simulated = true; //enables physics when game has started
    }

    void OnGameOverConfirmed()
    {
        transform.localPosition = startPos;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Grounded", grounded); //used to tell the animator when the player is grounded
        if (Input.GetKeyDown(KeyCode.Space)) // when spacebar is pressed
        {
            if (grounded == true)
            {
                rigidbody.velocity = Vector3.zero;
                rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Force); // add upward force
            }
        }

        if (Input.GetKey(KeyCode.LeftControl)) // when control is pressed
        {
             //reduced scale of character by half on the Y axis to feign croutching
             Vector3 theScale = transform.localScale;
             theScale.y = 0.5f;
             transform.localScale = theScale;
            
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            Vector3 theScale = transform.localScale;
            theScale.y = 2;
            transform.localScale = theScale;
        }

    }
    //poggies

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "ScoreZone")
        {
            OnPlayerScored();
        }

        if (collision.gameObject.tag == "DeadZone")
        {
            rigidbody.simulated = false; // stops player movement when they lose
            OnPlayerDied();
        }
    }

    
    private void OnCollisionEnter2D(Collision2D collision) //if player is in contact with the ground grounded = true 
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) //when player leaves ground then grounded = false and player cannot jump in mid air
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = false;
        }
        //doin your mum
    }
}
