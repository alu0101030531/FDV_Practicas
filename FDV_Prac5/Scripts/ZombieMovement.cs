using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer sprite_renderer;
    private Rigidbody2D rb;
    public float speed = 200f;
    public float thrust = 50f;
    bool movingRight = true;
    bool groundCheck = true;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sprite_renderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       float horizontal = Input.GetAxis("Horizontal");
       float animator_speed = speed * horizontal;
       if (animator_speed < 0f && movingRight) {
            sprite_renderer.flipX = !sprite_renderer.flipX;
            movingRight = !movingRight;
       } else if (animator_speed > 0f && !movingRight) {
            sprite_renderer.flipX = !sprite_renderer.flipX;
            movingRight = !movingRight;

       }
       animator.SetFloat("speed", Mathf.Abs(animator_speed));
       rb.AddForce(new Vector2(animator_speed * Time.fixedDeltaTime, 0f));

        float jump = Input.GetAxis("Vertical");
       if (groundCheck && jump > 0.1f) {
        Debug.Log("Jumping");
        groundCheck = !groundCheck;
        rb.AddForce(new Vector2(0f, thrust * jump * Time.fixedDeltaTime), ForceMode2D.Impulse);
       }
    }
    void Update()
    {
           }

    void OnCollisionEnter2D(Collision2D other)
    {
        groundCheck = true;
        if (other.gameObject.tag == "Goblin") {
            animator.SetBool("isDead", true);
        }
    }
}
