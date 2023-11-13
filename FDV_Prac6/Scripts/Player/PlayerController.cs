using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 12f;
    public float thrust = 1000f;
    private Rigidbody2D rb2D;
    private SpriteRenderer sprite_renderer;
     private Animator animator;
    private bool groundCheck = true;
    bool movingRight = true;
    // Start is called before the first frame update
    void Start()
    {
        UpdateCoins.OnSetThrust += SetThrust;
       rb2D = gameObject.GetComponent<Rigidbody2D>(); 
       animator = GetComponent<Animator>();
       sprite_renderer = GetComponent<SpriteRenderer>();
    }
    

    void SetThrust(float value) {
        thrust = value;
    }

    // Update is called once per fram
    void Update()
    {
    }

    void FixedUpdate(){
        float axisX = Input.GetAxis("Horizontal");
        float force = axisX * speed * Time.fixedDeltaTime;
        if (force < 0f && movingRight) {
            sprite_renderer.flipX = !sprite_renderer.flipX;
            movingRight = !movingRight;
       } else if (force > 0f && !movingRight) {
            sprite_renderer.flipX = !sprite_renderer.flipX;
            movingRight = !movingRight;

       }
       animator.SetFloat("speed", Mathf.Abs(force));
        rb2D.AddForce(new Vector2(force, 0f), ForceMode2D.Impulse);

        float jump = Input.GetAxis("Jump");
        if (groundCheck && jump > 0.1f) {
            Debug.Log("Jumping");
            groundCheck = !groundCheck;
            rb2D.AddForce(new Vector2(0f, thrust * jump * Time.fixedDeltaTime), ForceMode2D.Impulse);
       }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") || collision.gameObject.layer == LayerMask.NameToLayer("SpeedZone")) {
            groundCheck = true;
        }
    }
}
