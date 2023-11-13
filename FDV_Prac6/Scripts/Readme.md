# Pruebas configuración de físicas
# Vídeos de las configuraciones
# Incorporación de elementos físicos en la escena

## Objeto estático que ejerce de barrera infranqueable
# Vídeo del objeto
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
# Vídeo Plataforma

# Objeto que es arrastrado por otro a una distancia fija
Para ello hacemos uso del componente fixed joint y añadimos una planta en la cabeza del personaje a través del joint
# Vídeo planta
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