using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveForce = 10f;
    public float jumpForce = 11f;
    public float maxVelocity = 22f;

    private float movementX;

    public Rigidbody2D myBody;

    private SpriteRenderer sr;

    private Animator anim;
    private string WALK_ANIMATION = "Walk";
    private string GROUND_TAG = "Ground";

    private string ENEMY_TAG = "Enemy";

    private bool isGrounded;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>(); // equivalent to dragging and dropping the component in Unity
        anim = GetComponent<Animator>();

        sr = GetComponent<SpriteRenderer>();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMoveKeyboard();
        AnimatePlayer();

        
    }

    private void FixedUpdate()
    {
        PlayerJump();
    }

    void PlayerMoveKeyboard()
    {
        movementX = Input.GetAxisRaw("Horizontal"); // default state is zero, press left arrow key -> negative, right arrow key -> positive

        transform.position += new Vector3(movementX, 0f, 0f) * Time.deltaTime * moveForce;  // delta time is time between each two frames
           
    }

    void AnimatePlayer()
    {
        if (movementX > 0) // going to the right side
        {
            anim.SetBool(WALK_ANIMATION, true);
            sr.flipX = false;

        }
        else if (movementX < 0) // going to the left side
        {
            anim.SetBool(WALK_ANIMATION, true);
            sr.flipX = true;
        }
        else
        {
            anim.SetBool(WALK_ANIMATION, false);

        }
       

    }

    void PlayerJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isGrounded = false;
            myBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GROUND_TAG))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag(ENEMY_TAG))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(ENEMY_TAG))
        {
            Destroy(gameObject);
        }
    }
}
