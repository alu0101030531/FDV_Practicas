# Pruebas configuración de físicas
Ninguno de los objetos será físico.
![alt-text](https://github.com/alu0101030531/FDV_Practicas/blob/main/FDV_Prac6/Readme_images/1.gif "1")

Un objeto tiene físicas y el otro no.
![alt-text](https://github.com/alu0101030531/FDV_Practicas/blob/main/FDV_Prac6/Readme_images/2.gif "1")

Ambos objetos tienen físicas.
![alt-text](https://github.com/alu0101030531/FDV_Practicas/blob/main/FDV_Prac6/Readme_images/3-5.gif "1")

Ambos objetos tienen físcas y uno de ellos tiene 10 veces más masa que el otro.
![alt-text](https://github.com/alu0101030531/FDV_Practicas/blob/main/FDV_Prac6/Readme_images/3.gif "1")

Un objeto tiene físicas y el otro es IsTrigger.
![alt-text](https://github.com/alu0101030531/FDV_Practicas/blob/main/FDV_Prac6/Readme_images/4.gif "1")

Ambos objetos son físicos y uno de ellos está marcado como IsTrigger.
![alt-text](https://github.com/alu0101030531/FDV_Practicas/blob/main/FDV_Prac6/Readme_images/5.gif "1")

Uno de los objetos es cinemático.
![alt-text](https://github.com/alu0101030531/FDV_Practicas/blob/main/FDV_Prac6/Readme_images/6.gif "1")
# Incorporación de elementos físicos en la escena
## Objeto estático que ejerce de barrera infranqueable
![alt-text](https://github.com/alu0101030531/FDV_Practicas/blob/main/FDV_Prac6/Readme_images/7.gif "1")
## Zona que impulse objetos que caen en ella
Añadimos un script que cada vez que ocurra una colisión tome el rigidbody del objeto y llame a AddForce
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedZone : MonoBehaviour
{
    // Start is called before the first frame update
    public float thrust = 10000f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }    

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            Debug.Log(collision.gameObject.name);
            Debug.Log(thrust);
            Rigidbody2D rb2D = collision.gameObject.GetComponent<Rigidbody2D>();
            rb2D.AddForce(transform.right * thrust, ForceMode2D.Impulse);
        }
    }
}
```
![alt-text](https://github.com/alu0101030531/FDV_Practicas/blob/main/FDV_Prac6/Readme_images/8.gif "1")

# Objeto que es arrastrado por otro a una distancia fija
Para ello hacemos uso del componente fixed joint y añadimos una planta en la cabeza del personaje a través del joint
![alt-text](https://github.com/alu0101030531/FDV_Practicas/blob/main/FDV_Prac6/Readme_images/9.gif "1")
# Incluye dos capas que asignes a diferentes tipos de objetos y que permita evitar colisiones entre ellos.
Se añade un tilemap "Nature" y desabilitan las colisiones entre default y este para poder pasar por encima de los tiles que se incluyan
# Vídeo con las plantas

# Creación de tilemaps
Se han creado varios tilemaps para crear el fondo del juego, las plataformas, los obstáculos y elementos decorativos
# Vídeos tilemaps y juego

# Control del personaje
Se ha creado un controller para el personaje que implementa el movimiento y salto de este usando físicas, se comprueba si el personaje está tocando el suelo para saltar y se anima correctamente a este
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 12f;
    public float thrust = 1000f;
    private Rigidbody2D rb2D;
    private SpriteRenderer sprite_renderer;
     private Animator animator;
    private bool groundCheck = true;
    bool movingRight = true;
    // Start is called before the first frame update
    void Start()
    {
       rb2D = gameObject.GetComponent<Rigidbody2D>(); 
       animator = GetComponent<Animator>();
       sprite_renderer = GetComponent<SpriteRenderer>();
    }
    

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate(){
        float axisX = Input.GetAxis("Horizontal");
        float force = axisX * speed * Time.fixedDeltaTime;
        if (force < 0f && movingRight) {
            sprite_renderer.flipX = !sprite_renderer.flipX;
            movingRight = !movingRight;
       } else if (force > 0f && !movingRight) {
            sprite_renderer.flipX = !sprite_renderer.flipX;
            movingRight = !movingRight;

       }
       animator.SetFloat("speed", Mathf.Abs(force));
        rb2D.AddForce(new Vector2(force, 0f), ForceMode2D.Impulse);

        float jump = Input.GetAxis("Jump");
        if (groundCheck && jump > 0.1f) {
            Debug.Log("Jumping");
            groundCheck = !groundCheck;
            rb2D.AddForce(new Vector2(0f, thrust * jump * Time.fixedDeltaTime), ForceMode2D.Impulse);
       }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground")) {
            groundCheck = true;
        }
    }
}
```
![alt-text](https://github.com/alu0101030531/FDV_Practicas/blob/main/FDV_Prac6/Readme_images/9.gif "1")

# Tipos de plataformas
Creamos una plataforma movil estableciendo un min y max, cuando el jugador se suba en este lo hacemos hijo de la plataforma para que pueda moverse con esta
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{ 
    public int minX = 3;
    public int maxX = 3;
    public float speed = 5f;
    private Vector2 initialPos;
    private int target;
    // Start is called before the first frame update
    void Start()
    {
       initialPos = transform.position; 
       target = maxX;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(initialPos.x + maxX);
        if (transform.position.x >= initialPos.x + maxX) {
            target = minX;
        } else if (transform.position.x <= initialPos.x + minX) {
            target = maxX;
        }
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(initialPos.x + target, 0f), speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            collision.gameObject.transform.parent = transform;
        }
    }

    void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            collision.gameObject.transform.parent = null;
        }
    }
}
```
![alt-text](https://github.com/alu0101030531/FDV_Practicas/blob/main/FDV_Prac6/Readme_images/11.gif "1")
Creamos una plataforma inactiva hija de un padre con collider, cuando este entra en contacto con el jugador activamos la plataforma para que sea visible.
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisiblePlatform : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
```
![alt-text](https://github.com/alu0101030531/FDV_Practicas/blob/main/FDV_Prac6/Readme_images/10.gif "1")
Por último creamos unas monedas las cuales aumentan nuestra puntuación que se ve reflejada en la UI, al llegar a 5 puntos aumenta nuestra fuerza de salto.
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void SetScore(int value);

public class CoinCollection : MonoBehaviour
{
    public int coin_value = 5;
     public static SetScore OnSetScore;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player") {
            OnSetScore(coin_value);
            this.gameObject.SetActive(false);
        }
    }
}
```
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public delegate void SetThrust(float value);

public class UpdateCoins : MonoBehaviour
{
    public static SetThrust OnSetThrust;
    public int boost_thrust = 5;
    public float thrust_scale = 2000f;
    public TMP_Text coins;
    private string score_text = "Coins: ";
    private int num_coins = 0;
    // Start is called before the first frame update
    void Start()
    {
       CoinCollection.OnSetScore += UpdateScore; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     void UpdateScore(int value) {
        num_coins += value;
        if (num_coins >= boost_thrust && OnSetThrust != null) {
            OnSetThrust(thrust_scale);
        }
        coins.text = score_text + num_coins.ToString();
    }
}
```
