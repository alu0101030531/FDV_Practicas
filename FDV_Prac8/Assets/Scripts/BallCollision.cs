using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour
{
    // Start is called before the first frame update
    private float minVol = 0.05f;
    void Start()
    {
        
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Player") {
            Debug.Log("a player");
            AudioSource audio =  GetComponent<AudioSource>();
            float velocity = collision.gameObject.GetComponent<PlayerMovement>().GetVelocity();
            audio.volume = velocity * minVol;
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
