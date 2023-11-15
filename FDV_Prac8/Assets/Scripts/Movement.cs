using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 20f;
    public float threshold = 30f;
    private float initialX;
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
        transform.Translate(new Vector3(movement, 0f, movement));
    }
}