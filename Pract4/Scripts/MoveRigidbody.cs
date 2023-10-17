using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRigidbody : MonoBehaviour
{
    private Rigidbody p_rigidbody;
    float speed = 100f;
    // Start is called before the first frame update
    void Start()
    {
       p_rigidbody = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        p_rigidbody.AddForce(horizontal * speed * Time.fixedDeltaTime, 0f, vertical * speed * Time.fixedDeltaTime);
    }
}
