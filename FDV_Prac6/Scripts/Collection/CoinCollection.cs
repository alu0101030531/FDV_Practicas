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
