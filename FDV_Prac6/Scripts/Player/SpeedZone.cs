using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedZone : MonoBehaviour
{
    // Start is called before the first frame update
    public float thrust = 10000f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }    

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            Debug.Log(collision.gameObject.name);
            Debug.Log(thrust);
            Rigidbody2D rb2D = collision.gameObject.GetComponent<Rigidbody2D>();
            rb2D.AddForce(transform.right * thrust, ForceMode2D.Impulse);
        }
    }
}
