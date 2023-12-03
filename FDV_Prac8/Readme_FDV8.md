# SONIDO EN UNITY
## Añadir AudioSource a una esfera y reproducir en bucle y desde que comience la escena
Le añadimos el componente AudioSource a una de las esferas y marcamos Play On Awake y Loop

![Captura de pantalla 2023-12-03 163851](https://github.com/alu0101030531/FDV_Practicas/assets/43813200/51946f89-a9b0-4315-8510-a31b64af2728)


## Crear una fuente de audio con el efecto doppler activado
![image](https://github.com/alu0101030531/FDV_Practicas/assets/43813200/a8ac6ad8-7c44-4f8d-8e34-e74f2e38ab37)

El efecto doppler lo podemos configurar utilizando el volumen rolloff, con la controlar la caída en volumen, spread para determinar el ángulo de propagación del sonido y el nivel de Doppler que modifica cuánto efecto doppler se aplica.

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 20f;
    public float threshold = 30f;
    private float initialX;
    // Start is called before the first frame update
    void Start()
    {
       initialX = transform.position.x; 
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x >= initialX + threshold || transform.position.x <= initialX - threshold) { 
            speed = -speed;
        }
        float movement = speed * Time.deltaTime;
        transform.Translate(new Vector3(movement, 0f, movement));
    }
}
```
##  Configurar un mezclador de sonidos
Añadimos dos grupos uno music y otro spatial, añadimos el efecto de echo a spatial y SFX Reverb a music
![Captura de pantalla 2023-12-03 165657](https://github.com/alu0101030531/FDV_Practicas/assets/43813200/0a5817e8-dd9f-4563-be4c-69c080e534ae)

![Captura de pantalla 2023-12-03 165712](https://github.com/alu0101030531/FDV_Practicas/assets/43813200/0ec32d9b-ed6b-4aac-b83c-ef820d525678)


## Audio vía Scripts

## Implementar un script que al pulsar la tecla p accione el movimiento de una esfera en la escena y reproduzca un sonido en bucle hasta que se pulse la tecla s.
Creamos un script que en el update registre las teclas p y s. Cuando se pulse la p reproduce un sonido y activa el movimientos del GameObject, cuando se pulsa la s para el sonido y el movimiento
![alt-text](https://github.com/alu0101030531/FDV_Practicas/blob/main/FDV_Prac8/Readme_Images/1.gif "1")
```
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
```

## Implementar un script en el que el cubo-player al colisionar con las esferas active un sonido. Modificar en función de la velocidad
Añadimos un evento OnCollisionEnter y OnCollisionExit para detectar las colisiones con el jugador y reproducir un sonido. Además obtenemos la velocidad del player y multiplicamos el volumen por este
```using System.Collections;
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
```

## Agregar un sonido de fondo a la escena que se esté reproduciendo continuamente desde que esta se carga. Usar un mezclador para los sonidos.

Agregamos un sonido con el spacial blend en 2D y utilizamos el Grupo del Audio Mixer de  music
![Captura de pantalla 2023-12-03 174418](https://github.com/alu0101030531/FDV_Practicas/assets/43813200/f54578d0-1423-43e2-b5a4-5beaff291629)


## Crear un script para simular el sonido que hace el cubo-player cuando está movimiento en contacto con el suelo (mecánica para reproducir sonidos de pasos).
Comprobamos si el player se está moviendo, si ya hemos ejecutado el sonido no hacemos nada, en caso de que estemos parados volvemos a poner walking a false y paramos el sonido
```
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
```

## En tu escena 2D activa un sonido cada vez que el jugador alcance una nueva plataforma.
Cada vez que colisionemos con el jugador activamos un sonido
![alt-text](https://github.com/alu0101030531/FDV_Practicas/blob/main/FDV_Prac8/Readme_Images/2.gif "1")
```
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
```
