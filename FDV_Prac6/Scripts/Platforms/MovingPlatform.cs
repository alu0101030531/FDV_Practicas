using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{ 
    public int minX = 3;
    public int maxX = 3;
    public float speed = 5f;
    private Vector2 initialPos;
    private int target;
    // Start is called before the first frame update
    void Start()
    {
       initialPos = transform.position; 
       target = maxX;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(initialPos.x + maxX);
        if (transform.position.x >= initialPos.x + maxX) {
            target = minX;
        } else if (transform.position.x <= initialPos.x + minX) {
            target = maxX;
        }
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(initialPos.x + target, 0f), speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            collision.gameObject.transform.parent = transform;
        }
    }

    void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            collision.gameObject.transform.parent = null;
        }
    }
}
