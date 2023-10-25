# Vectores y movimiento en línea recta

La clase MoveTowards nos permite establecer un objetivo y moverlo hacia él utilizando LookAt y Translate.
Hacemos uso del Debug.DrawRay para dibujar un rayo en la dirección de movimiento del cuadrado
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowards : MonoBehaviour
{
    public float speed = 12;
    public float rotationSpeed = 20;
    private float threshold = .1f;
    public Transform goal; 
    void Start()
    {
    }

    Vector3 GetGoalDirection() {
        return goal.position - transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 direction = GetGoalDirection();
        if (direction.magnitude >= threshold) {
            transform.LookAt(goal.position);
            Debug.DrawRay(this.transform.position, direction.normalized, Color.red);
            transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
        }
    }
}
```
Añadimos una clase CameraMovement para poder mover la cámara con el ratón, utilizamos Mouse X y Mouse Y.
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    public float rotationSpeed = 100f;
    private float xrotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float rotationX = Input.GetAxis("Mouse X");
        float rotationY = Input.GetAxis("Mouse Y");
        xrotation -= (rotationY * rotationSpeed * Time.deltaTime); 
        transform.localRotation = Quaternion.Euler(Mathf.Clamp(xrotation, -90f, 90f), 0f, 0f);
        player.Rotate(Vector3.up * rotationX * rotationSpeed * Time.deltaTime);
    }
}
```
![alt-text](https://github.com/alu0101030531/FDV_Practicas/blob/main/FDV_Prac2/Readme_Images/1.gif "Seguimiento")
![alt-text](https://github.com/alu0101030531/FDV_Practicas/blob/main/FDV_Prac2/Readme_Images/2.gif "DrawRay")
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAway : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 12;
    void Start()
    {
        InvokeRepeating("Move", 1f, 2f);
    }

    void Move() {
        Vector3 position = new Vector3(Random.Range(0, 25), Random.Range(0, 25), Random.Range(0, 25));
        transform.position = position;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
```
Implementamos una clase que nos permita mover al jugador usando las teclas wasd sin simulación física, agregamos la velocidad como una variable pública que podemos ajustar desde el inspector. En El update detectamos
si se pulsa la tecla espacio y en ese caso aumentamos la velocidad de movimiento

Añadimos unas esferas a la escena con la tag de "Ball" y cuando colisionamos con estas les cambiamos el color y le damos una fuerza con Add.Force()
```
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
```
Aquí controlamos que la pelota está en el suelo y reseteamos el score del jugador
```
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public delegate void ResetScore();
public delegate void SetHighScore();

public class GroundChecker : MonoBehaviour
{
    public static ResetScore OnResetScore;
    public static SetHighScore OnSetHighScore;
    public TMP_Text score;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Floor")
        {
            Debug.Log("Touched Floor"); 
            score.text = "Score: 0";
            OnSetHighScore();
            OnResetScore();
        }
    }
}
```
![alt-text](https://github.com/alu0101030531/FDV_Practicas/blob/main/FDV_Prac2/Readme_Images/3.gif "Collision")
