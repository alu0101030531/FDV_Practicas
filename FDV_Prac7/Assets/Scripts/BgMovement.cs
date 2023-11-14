using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgMovement : MonoBehaviour
{
    public GameObject background;
    public Camera main_camera;
    public Vector2 initialPos = new Vector2(0f, 0f);
    public GameObject background_texture;
    public GameObject[] background_layers;
    public float speed_factor = 11f;
    public float speed = 12f;
    private List<GameObject> background_layers_instances;
    private GameObject first_background;
    private GameObject second_background;
    private GameObject test;
    private Dictionary<string, float> bg_size;
    private Dictionary<string, float> bg_2_size;
    public int BackgroundMode = 0;

    void Start()
    {
        switch(BackgroundMode) {
            case 0:
            case 1:
                first_background = Instantiate(background, initialPos, Quaternion.identity) as GameObject;
                first_background.name = "the first";
                bg_size = GetBackgroundSize(first_background);
                float maxX = bg_size["gameObjectMaxSize"] * 2f;
                second_background = Instantiate(background, initialPos + new Vector2(maxX, 0f), Quaternion.identity) as GameObject;
                bg_2_size = GetBackgroundSize(second_background);
                break;
            case 2:
                test = Instantiate(background_texture, initialPos + new Vector2(background_texture.transform.position.x, background_texture.transform.position.y), Quaternion.identity) as GameObject;
                break;
            case 3:
                background_layers_instances = new List<GameObject>();
                foreach(GameObject background_layer in background_layers){
                    GameObject bg_1 = Instantiate(background_layer, initialPos + new Vector2(background_layer.transform.position.x, background_layer.transform.position.y), Quaternion.identity) as GameObject;
                    bg_1.name = "first";
                    background_layers_instances.Add(bg_1);
                    background_layers_instances.Add(Instantiate(background_layer, initialPos + new Vector2(background_layer.transform.position.x, background_layer.transform.position.y) + new Vector2(GetBackgroundSize(bg_1)["gameObjectMaxSize"] * 2f, 0f), Quaternion.identity) as GameObject);
                }
                break;
        }
    }

    public void SetScrollMode(int mode) {
        BackgroundMode = mode;
    }

    float GetCameraWidth() {
        return main_camera.orthographicSize * 2f * main_camera.aspect;
    }

    Dictionary<string, float> GetBackgroundSize(GameObject currentBackground) {
        Dictionary<string, float> backgroundSizes = new Dictionary<string, float>();
        float maxX = -Mathf.Infinity;
        float minX = Mathf.Infinity;
        float gameobject_max_size = 0f;
        float gameobject_min_size = 0f;
        foreach (Transform child in currentBackground.transform) {
            if (child.position.x > maxX) {
                maxX = child.position.x;
                gameobject_max_size = child.gameObject.GetComponent<SpriteRenderer>().bounds.max.x;
            }
            if (child.position.x < minX) {
                minX = child.position.x;
                gameobject_min_size = child.gameObject.GetComponent<SpriteRenderer>().bounds.min.x;
            }
        }
        backgroundSizes.Add("maxX", maxX);
        backgroundSizes.Add("gameObjectMaxSize", gameobject_max_size);
        backgroundSizes.Add("gameObjectMinSize", gameobject_min_size);
        return backgroundSizes;
    }


    void BackgroundScrollA(ref GameObject bg1, ref GameObject bg2, bool verbose, float bg_speed) {
        bg_size = GetBackgroundSize(bg1);
        bg_2_size = GetBackgroundSize(bg2);
        if (verbose) {
            Debug.Log("Name: " + bg1.name);
            Debug.Log("minX: " + bg_size["gameObjectMinSize"]);
            Debug.Log("maxX: " + bg_size["gameObjectMaxSize"]);
        }
        if (bg_size["gameObjectMaxSize"] <= main_camera.transform.position.x - GetCameraWidth() / 2f) {
            bg1.transform.position = new Vector2(bg_2_size["gameObjectMaxSize"] + Mathf.Sqrt(Mathf.Pow(bg_2_size["gameObjectMinSize"] - bg_2_size["gameObjectMaxSize"], 2f)) / 2f, bg1.transform.position.y);
            GameObject temp_bg = bg1;
            bg1 = bg2;
            bg2 = temp_bg;
        }
        bg1.transform.Translate(Vector2.left* bg_speed * Time.deltaTime);
        bg2.transform.Translate(Vector2.left * bg_speed * Time.deltaTime);
    }

    void BackgroundScrollB() {
        bg_size = GetBackgroundSize(first_background);
        bg_2_size = GetBackgroundSize(second_background);
        if (main_camera.transform.position.x - GetCameraWidth() / 2f >= bg_size["gameObjectMaxSize"]) {
            first_background.transform.position = new Vector2(bg_2_size["gameObjectMaxSize"] + Mathf.Sqrt(Mathf.Pow(bg_2_size["gameObjectMinSize"] - bg_2_size["gameObjectMaxSize"], 2f)) / 2f, first_background.transform.position.y);
            GameObject temp_bg = first_background;
            first_background = second_background;
            second_background = temp_bg;
        }

    }

    void BackgroundTextureScroll() {
        Material mat = test.GetComponent<Renderer>().sharedMaterial;
        mat.mainTextureOffset += new Vector2(0.01f * Time.deltaTime, 0f);
    }

    void ParallaxA() {

    }

    // Update is called once per frame
    void Update()
    {
        switch(BackgroundMode) {
            case 0:
                BackgroundScrollA(ref first_background, ref second_background, false, speed); 
                break;
            case 1:
                BackgroundScrollB();
                break;
            case 2:
                BackgroundTextureScroll();
                break;
            case 3:
                float currentSpeed = speed;
                for (int background_index = background_layers_instances.Count - 1; background_index > 0; background_index -= 2) {
                    GameObject bg_1 = background_layers_instances[background_index];
                    GameObject bg_2 = background_layers_instances[background_index - 1];
                    BackgroundScrollA(ref bg_2, ref bg_1, false, currentSpeed);
                    background_layers_instances[background_index] = bg_1;
                    background_layers_instances[background_index - 1] = bg_2;
                    currentSpeed -= speed_factor;
                }
                break;
            case 4:
                break;
        }
    }
}