##Práctica pooling de objetos

Creamos un prefab bullet que usaremos para nuestro pool, este contiene un AudioSource y un script para que pueda desactivarse cuando colisiona con el jugador y reproducir un sonido.
El script de Bullet es el siguiente; Cuando entramos en el evento OnTriggerEnter, llamamos a una subrutina que reproduce el sonido y una vez este acabe se desactiva el objeto bullet.

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    public AudioSource audio_collect;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator playSound() {
        audio_collect.Play();
        yield return new WaitForSeconds(0.5f);
        this.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider col) {
        StartCoroutine(playSound());
    }
}
```
Por otro lado, tenemos un script Pooling que maneja todo el sistema de pool de objetos. En el start creamos el pool con el número que se haya indicado de objetos, además crearemos un contador para el objeto.

En el método Spawn, iteraremos sobre el pool dándole una posición aleatoria y activando el gameObject. Reproducimos un sonido cada vez que se activa este y comprobamos que el contador de spawn no haya llegado a 3, en dicho caso se elimina el objeto de la pool.
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooling : MonoBehaviour
{
    public GameObject prefab;
    public int poolSize = 10;
    private List<GameObject> pool;
    private List<int> pool_counter;
    private int currentItem = 0;
    private int spawnTime = 1;
    public AudioSource audio_collect;

    void Start() {
        pool = new List<GameObject>();
        pool_counter = new List<int>();
        for (int item = 0; item < poolSize; item++) {
            pool.Add(Instantiate(prefab, transform.position, Quaternion.identity) as GameObject);
            pool[item].SetActive(false);
            pool_counter.Add(0);
       }
       StartCoroutine(Spawn());
    }

    IEnumerator RePoolItem(GameObject item) {
        yield return new WaitForSeconds(spawnTime);
        item.SetActive(false);
    }

    IEnumerator Spawn() {
        while (true) {
            yield return new WaitForSeconds(spawnTime);
            GameObject firingItem = pool[currentItem];
            if (pool_counter[currentItem] >= 3) {
               audio_collect.Play();
               Destroy(firingItem);
               pool_counter.RemoveAt(currentItem);
               pool.RemoveAt(currentItem); 
            } else {
                firingItem.transform.position = new Vector3(transform.position.x  + Random.Range(-10.0f, 10.0f), 0f, transform.position.z  + Random.Range(-10.0f, 10.0f)); 
                firingItem.SetActive(true);
                audio_collect.Play();
                //StartCoroutine(RePoolItem(firingItem));
                pool_counter[currentItem]++;
                currentItem++;
                if (currentItem >= pool.Count) {
                    currentItem = 0;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
```
![alt-text](https://github.com/alu0101030531/FDV_Practicas/blob/main/FDV_Prac8/Readme_Images/3.gif "1")