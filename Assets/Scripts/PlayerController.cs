using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Set fields for necessary data inside the game object
    // Fields are set by protection (private/public), type of data (e.g., component), and name ("rb2D")
    private Rigidbody2D rb2D;

    private float moveHorizontal;
    private float moveVertical;
    private float moveSpeed;

    private float jumpForce;
    private bool isJumping;


    // Start is called before the first frame update
    // Values can be properly set to fields here
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();

        moveSpeed = 3f;
        jumpForce = 60f;
        isJumping = false;
    }


    // Update is called once per frame
    // This is where the script will check for user inputs every frame and assign values to the move fields
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Jump"); // "Jump", as opposed to "Vertical" (will set y movement to "W")
    }
    

    // FixedUpdate directly corresponds to physics manipulation in every frame (more accurately than Update)
    // moveHorizontal and moveVertical receive their values from void Update
    void FixedUpdate()
    {
        if (moveHorizontal > 0.1f || moveHorizontal < -0.1f) // 0.1f allows a margin of error (though 0 will also work)
        {
            //AddForce and ForceMode2D are innate Unity methods
            rb2D.AddForce(new Vector2(moveHorizontal * moveSpeed, 0f), ForceMode2D.Impulse);
        }

        if (!isJumping && moveVertical > 0.1f)
        {
            rb2D.AddForce(new Vector2(0f, moveVertical * jumpForce), ForceMode2D.Impulse);
        }
    }


    // Collision detection on ground will prevent player from jumping infinitely as long as space/"w" is pressed
    // A single equal (=) means left is equal to right; two equals (==) asks to check if left and right are already equal
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            isJumping = false;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isJumping = true;
        }
    }
}
