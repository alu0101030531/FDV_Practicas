using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public delegate void ResetScore();
public delegate void SetHighScore();

public class GroundChecker : MonoBehaviour
{
    public static ResetScore OnResetScore;
    public static SetHighScore OnSetHighScore;
    public TMP_Text score;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Floor")
        {
            Debug.Log("Touched Floor"); 
            score.text = "Score: 0";
            OnSetHighScore();
            OnResetScore();
        }
    }
}
