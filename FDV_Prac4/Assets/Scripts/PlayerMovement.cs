using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public delegate void SetScore(int value);
public delegate void MoveObjects();
public delegate void BorderEnter();

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public static SetScore OnSetScore;
    public static MoveObjects OnMoveObjects;
    public static BorderEnter OnBorderEnter;
    public float speed = 12f;
    public int coin_value = 1;
    public float speed_reduction = 1f;

    void Start()
    {
        MovementSpeed.OnChangeSpeed += ChangeSpeed;

    }

    void ChangeSpeed(float _speed)
    {
        speed = _speed;
    }

    void OnDestroy() 
    {
        MovementSpeed.OnChangeSpeed -= ChangeSpeed;
    }

    // Update is called once per frame
    void Update()
    {
       float x = Input.GetAxis("Horizontal");
       float z = Input.GetAxis("Vertical");
       // Adding comment

       transform.Translate(x * speed * Time.deltaTime, 0f, z * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision other) 
    {
        if (speed > 0f) {
            speed -= speed_reduction;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.tag == "Coin")
        {
            other.gameObject.SetActive(false);
            if (OnSetScore != null)
                OnSetScore(coin_value);
        }

        if (other.tag == "Key")
        {
            other.gameObject.SetActive(false);
            if (OnMoveObjects != null)
                OnMoveObjects();
        }

        if (other.tag == "Teleport")
        {
            Teleport();
        }

        if (other.tag == "Terrain")
        {
            Debug.Log("a");
            OnBorderEnter();
        }
    }

    void Teleport()
    {
        transform.position = new Vector3(0f, 0f, 0f);
    }
   //     Renderer gameObjectRender = other.gameObject.GetComponent<Renderer>();
   //     gameObjectRender.material.SetColor("_Color", Color.red);
   //     if (other.gameObject.tag == "Ball") {
   //         Debug.Log("Ball");
   //         Rigidbody ball_rigid_body = other.gameObject.GetComponent<Rigidbody>();
   //         ball_rigid_body.AddForce(transform.up + transform.forward * ball_force * Time.fixedDeltaTime, ForceMode.Impulse);
   //         score_number += 1;
   //         score.text = "Score: " + score_number.ToString();
   //     }

   // }

   // void OnResetScore()
   // {
   //     score_number = 0;
   //     score.text = "Score: " + score_number.ToString();
   // }

   // void OnSetHighScore()
   // {
   //     Debug.Log("Score number" + score_number.ToString());
   //     if (score_number > high_score)
   //     {
   //         high_score = score_number;
   //         high_score_text.text = "HighScore: " + high_score.ToString();
   //     }
   // }

   // IEnumerator ChangeColor(Renderer otherRenderer, float seconds)
   // {
   //     yield return new WaitForSeconds(seconds);
   //     otherRenderer.material.SetColor("_Color", Color.green);
   // }
   // void OnCollisionExit(Collision other)
   // {
   //     Debug.Log(other.gameObject.name);
   //     StartCoroutine(ChangeColor(other.gameObject.GetComponent<Renderer>(), 2f));
   // }
}
