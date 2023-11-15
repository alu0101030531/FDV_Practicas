using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Player") {
            Debug.Log("a player");
           AudioSource audio =  GetComponent<AudioSource>();
           audio.Play(); 
        }
    }

    void OnCollisionExit(Collision collision) {
        if (collision.gameObject.tag == "Player") {
            Debug.Log("a player");
            AudioSource audio = GetComponent<AudioSource>();
            audio.Stop();
        }
    }
}
