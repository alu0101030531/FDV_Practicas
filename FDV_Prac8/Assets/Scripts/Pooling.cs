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
