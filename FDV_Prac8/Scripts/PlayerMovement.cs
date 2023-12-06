using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 12;
    public AudioClip footsteps;
    private AudioSource audio_source;
    private bool walking = false;
    private Vector3 previousPosition;
    private Rigidbody rb;
    private float velocity;
    void Start()
    {
        audio_source = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        audio_source.loop = true;

    }

    void OnDestroy() 
    {
    }

    void SetVelocity() {
        velocity = ((transform.position - previousPosition) / Time.deltaTime).magnitude;
        previousPosition = transform.position;

    }

    public float GetVelocity() {
        return velocity;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        if ((Mathf.Abs(x) >= 0.01f || Mathf.Abs(z) >= 0.01f)) {
            if (!walking) {
                walking = true;
                audio_source.Play();
            }
        } else {
            walking = false;
            audio_source.Pause();
        } 

        SetVelocity();

        transform.Translate(x * speed * Time.deltaTime, 0f, z * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground")) {
            GetComponent<AudioSource>().Play();
        }
    }
}