# Eventos y Movimiento rectilíneo.

## Implementar una UI que permita configurar con qué velocidad te moverás: turbo o normal. También debe mostar la cantidad de objetos recolectados y si chocas con alguno especial restar fuerza.
Se ha implementado una UI con un botón que permite cambiar de velocidad entre turbo y normal, Para ello se utiliza la función OnClick para establecer la velocidad del jugador. 
```
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
```
Se usa un textMeshPro para mostrar las esferas que ha recolectado el jugador, para ello con un delegado en el evento OnTriggerEnter lanzamos una señal para cambiar el score que recibe CollectCoins quien guarda el score actual y actualiza el textMeshPro
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectCoins : MonoBehaviour
{
    public TMP_Text coins;
    private string score_text = "Coins Collected: ";
    private int num_coins = 0;
    // Start is called before the first frame update
    void Start()
    {
       PlayerMovement.OnSetScore += UpdateScore; 
    }

    void UpdateScore(int value) {
        num_coins += value;
        coins.text = score_text + num_coins.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
```
Al chocar con las vallas que están en el mapa le restamos velocidad al jugador, las vallas tienen un rigidbody que nos permiten detectar las colisiones en OnCollisionEnter y disminuimos ahí la velocidad.
![alt-text](https://github.com/alu0101030531/FDV_Practicas/blob/main/FDV_Prac4/Readme_Images/1.gif "1")
![alt-text](https://github.com/alu0101030531/FDV_Practicas/blob/main/FDV_Prac4/Readme_Images/2.gif "2")
## Agregar a tu escena un objeto que al ser recolectado por el jugador haga que otros objetos obstáculos se desplacen de su trayectoria.
Agregamos un objeto que cuando lo recolecta el jugador lanza un evento para mover todos los obstáculos de la escena
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 FinalPos;
    private bool move = false;
    public float speed = 5f;
    void Start()
    {
        FinalPos = new Vector3(transform.position.x, 5f, transform.position.z); 
        PlayerMovement.OnMoveObjects += MoveObjectToPos; 
    }

    void MoveObjectToPos()
    {
        move = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
            transform.position = Vector3.MoveTowards(transform.position, FinalPos, speed * Time.deltaTime);
    }
}
```
![alt-text](https://github.com/alu0101030531/FDV_Practicas/blob/main/FDV_Prac4/Readme_Images/3.gif "3")
## Agrega un objeto que te teletransporte a otra zona de la escena.
Agregamos un objeto con el tag teleport, que si colisionamos, cambiamos la posición del player.
```
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
```
![alt-text](https://github.com/alu0101030531/FDV_Practicas/blob/main/FDV_Prac4/Readme_Images/6.gif "6")
## Agrega un personaje que al clickar sobre un botón de la UI se dirija hacia un objetivo estático en la escena.
Se agrega un botón que en la función OnClick activa un script que mueve un cubo. Para ello reutilizamos la función de la clase MoveTowards para implementar el movimiento
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

    public virtual void Start()
    {
        enabled = false;     
    }

    Vector3 GetGoalDirection() {
        return goal.position - transform.position;
    }

    public void Move()
    {
        Vector3 direction = GetGoalDirection();
        if (direction.magnitude >= threshold) {
            transform.LookAt(goal.position);
            Debug.DrawRay(this.transform.position, direction.normalized, Color.red);
            transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
       }

    }


    // Update is called once per frame
    void Update()
    {
      
    }
}
```
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsUI : MoveTowards
{
    // Start is called before the first frame update

    public void StartMoving()
    {
        enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
       base.Move();
    }
}
```
![alt-text](https://github.com/alu0101030531/FDV_Practicas/blob/main/FDV_Prac4/Readme_Images/4.gif "4")
## Agrega un personaje que siga el movimiento del jugador cuando éste llegue al límite de un recinto que incluyas en la escena.
Volvemos a utilizar la clase MoveTowards para mover a una esfera hacia el jugador cuando este pisa los bordes del mapa
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsBorders : MoveTowards
{
    // Start is called before the first frame update
    bool in_borders = false;
    override public void Start()
    {
        PlayerMovement.OnBorderEnter += OnBorderEnter;
    }

    void OnBorderEnter()
    {
        in_borders = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (in_borders)
        {
            base.Move(); 
        }
    }
}
```
![alt-text](https://github.com/alu0101030531/FDV_Practicas/blob/main/FDV_Prac4/Readme_Images/5.gif "5")
