using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooling : MonoBehaviour
{
    public GameObject prefab;
    public int poolSize = 10;
    private List<GameObject> pool;
    private int currentItem = 0;
    private int spawnTime = 3;

    void Start() {
        pool = new List<GameObject>();
        for (int item = 0; item < poolSize; item++) {
            pool.Add(Instantiate(prefab, transform.position, Quaternion.identity) as GameObject);
            pool[item].SetActive(false);
       }
    }

    IEnumerator RePoolItem(GameObject item) {
        yield return new WaitForSeconds(spawnTime);
        item.SetActive(false);
    }

    void Fire() {
        if (Input.GetKeyDown(KeyCode.F)) {
            GameObject firingItem = pool[currentItem];
            firingItem.transform.position = transform.position; 
            firingItem.SetActive(true);
            StartCoroutine(RePoolItem(firingItem));
            currentItem++;
            if (currentItem >= poolSize) {
                currentItem = 0;
            }
        }
    }

    void UpdatePool() {
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
        UpdatePool();
    }
}
