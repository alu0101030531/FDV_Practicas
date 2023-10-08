# Vectores y movimiento en línea recta

## Movimiento hacia objetivos
La clase MoveTowards, se encarga de mover al personaje hacia un objetivo. Para ello utilizamos Slerp para rotar suavemente y Translate para mover al personaje.

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowards : MonoBehaviour
{
    public float speed = 3f;
    public float rotationSpeed = 0.01f;
    private float threshold = 1f;
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
            Vector3 goal_point = new Vector3(goal.position.x, transform.position.y, goal.position.z);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(goal_point), rotationSpeed * Time.deltaTime);
            Debug.DrawRay(this.transform.position, direction.normalized, Color.red);
            transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
        }
    }
}
```
## Seguimiento de Waypoints
Definimos una serie de transforms que representarán los waypoints a utilizar.

![image](https://github.com/alu0101030531/FDV_Practicas/assets/43813200/1e092752-47dd-4c2d-9f53-d06a9af4cf82)

La Clase FollowWayPoints recibe los transforms de los waypoints, mueve y rota al player hacia estos, cambiando de waypoint una vez la distancia entre este y el jugador es menor que un umbral.
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWayPoints : MonoBehaviour
{
    float threshold = 0.1f;
    int waypoint_index = 0;
    public Transform Waypoints;
    public float speed = 12f;
    public float rotationSpeed = 20f;

    void Start()
    {
        
    }

    Vector3 GetGoalDirection(Transform goal) {
        return goal.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (waypoint_index < Waypoints.childCount) {
            Transform goal = Waypoints.GetChild(waypoint_index);
            Vector3 direction = GetGoalDirection(goal);
            if (direction.magnitude >= threshold) {
                Vector3 goal_point = new Vector3(goal.position.x, transform.position.y, goal.position.z);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(goal_point), rotationSpeed * Time.deltaTime);
                Debug.DrawRay(this.transform.position, direction.normalized, Color.red);
                transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
            } else {
                waypoint_index++;
            }
        } else {
            waypoint_index = 0;
        }
        
        }
    }
```
![alt-text](https://github.com/alu0101030531/FDV_Practicas/blob/main/FDV_Prac3/Readme_Images/1.gif "waypoint")
## Usando Waypoint Circuit y Tracker
Añadimos los Scripts Waypoint Progress Tracker al jugador y al gameObject  que contiene todos los waypoints. Tenemos que configurar el target En el Waypoint progress tracker y el target en nuestro script de movimiento para
funcione correctamente
![image](https://github.com/alu0101030531/FDV_Practicas/assets/43813200/0ec34ee4-d568-4068-a142-de8ca246b9f3)
![image](https://github.com/alu0101030531/FDV_Practicas/assets/43813200/d98c4d23-6bbc-4b78-a187-fa02473838f5)
![alt-text](https://github.com/alu0101030531/FDV_Practicas/blob/main/FDV_Prac3/Readme_Images/2.gif "waypoint")
![alt-text](https://github.com/alu0101030531/FDV_Practicas/blob/main/FDV_Prac3/Readme_Images/3.gif "waypoint")
## Movimiento usando rigidbody
Utilizamos el método rigidbody.AddForce para mover la cápsula en el eje x y z
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRigidbody : MonoBehaviour
{
    private Rigidbody p_rigidbody;
    float speed = 100f;
    // Start is called before the first frame update
    void Start()
    {
       p_rigidbody = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        p_rigidbody.AddForce(horizontal * speed * Time.fixedDeltaTime, 0f, vertical * speed * Time.fixedDeltaTime);
    }
}
```
![alt-text](https://github.com/alu0101030531/FDV_Practicas/blob/main/FDV_Prac3/Readme_Images/4.gif "rigidbody")





