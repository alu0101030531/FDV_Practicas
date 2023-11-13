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
