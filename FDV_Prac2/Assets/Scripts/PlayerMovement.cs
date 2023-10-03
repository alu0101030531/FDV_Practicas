using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 12;
    public float ball_force = 20f;
    public TMP_Text score;
    public TMP_Text high_score_text;
    float high_score = 0;
    float score_number;
    void Start()
    {
        GroundChecker.OnResetScore += OnResetScore;
        GroundChecker.OnSetHighScore += OnSetHighScore;
    }

    void OnDestroy() 
    {
        GroundChecker.OnResetScore -= OnResetScore;
        GroundChecker.OnSetHighScore -= OnSetHighScore;
    }

    // Update is called once per frame
    void Update()
    {
       float x = Input.GetAxis("Horizontal");
       float z = Input.GetAxis("Vertical");

       transform.Translate(x * speed * Time.deltaTime, 0f, z * speed * Time.deltaTime);
       
       if (Input.GetKeyDown("space"))
       {
        speed +=1;
       }

       if (Input.GetKeyDown("b"))
       {
        speed -= 1;
       }
    }

    void OnCollisionEnter(Collision other) 
    {
        Debug.Log(other.gameObject.name);
        Renderer gameObjectRender = other.gameObject.GetComponent<Renderer>();
        gameObjectRender.material.SetColor("_Color", Color.red);
        if (other.gameObject.tag == "Ball") {
            Debug.Log("Ball");
            Rigidbody ball_rigid_body = other.gameObject.GetComponent<Rigidbody>();
            ball_rigid_body.AddForce(transform.up + transform.forward * ball_force * Time.fixedDeltaTime, ForceMode.Impulse);
            score_number += 1;
            score.text = "Score: " + score_number.ToString();
        }

    }

    void OnResetScore()
    {
        score_number = 0;
        score.text = "Score: " + score_number.ToString();
    }

    void OnSetHighScore()
    {
        Debug.Log("Score number" + score_number.ToString());
        if (score_number > high_score)
        {
            high_score = score_number;
            high_score_text.text = "HighScore: " + high_score.ToString();
        }
    }

    IEnumerator ChangeColor(Renderer otherRenderer, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        otherRenderer.material.SetColor("_Color", Color.green);
    }
    void OnCollisionExit(Collision other)
    {
        Debug.Log(other.gameObject.name);
        StartCoroutine(ChangeColor(other.gameObject.GetComponent<Renderer>(), 2f));
    }
}
