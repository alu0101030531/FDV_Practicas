using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 5f;
    public float threshold = 3f;
    private float initialX;
    private bool right = true;
    // Start is called before the first frame update
    void Start()
    {
       initialX = transform.position.x; 
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x >= initialX + threshold || transform.position.x <= initialX - threshold) { 
            speed = -speed;
        }
        float movement = speed * Time.deltaTime;
        transform.Translate(new Vector2(movement, 0f));
    }
}
