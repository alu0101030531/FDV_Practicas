using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 12;
    void Start()
    {
    }

    void OnDestroy() 
    {
    }

    // Update is called once per frame
    void Update()
    {
       float x = Input.GetAxis("Horizontal");
       float z = Input.GetAxis("Vertical");

       transform.Translate(x * speed * Time.deltaTime, 0f, z * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground")) {
            GetComponent<AudioSource>().Play();
        }
    }
    

}