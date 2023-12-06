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
