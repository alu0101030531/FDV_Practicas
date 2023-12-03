using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopUntilKeyPress : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 12f;
    public float initialX;
    public float threshold = 5f;
    public string play_key = "p";
    private bool move = false;
    private AudioSource _MyAudioSource;
    void Start()
    {
        initialX = transform.position.x; 
        _MyAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(play_key)) {
            _MyAudioSource.Play(0);
            move = true;
       }
       if (Input.GetKeyDown("s")) {
            _MyAudioSource.Stop();
            move = false;
       }
       if (move) {
            if (transform.position.x >= initialX + threshold || transform.position.x <= initialX - threshold) { 
                speed = -speed;
            }
            float movement = speed * Time.deltaTime;
            transform.Translate(new Vector3(movement, 0f, movement));
       }
    }
}
