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
