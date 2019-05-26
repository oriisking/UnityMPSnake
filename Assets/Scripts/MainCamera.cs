using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{

    // Borders
    public Transform borderTop;
    public Transform borderBottom;
    public Transform borderLeft;
    public Transform borderRight;

    // Food Prefab
    public GameObject foodPrefab;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
        
        if (GameObject.Find("FoodPrefab(Clone)") == null)
        {
            //Create A new food somewhere eles
            SpawnFood();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

    }
    void SpawnFood()
    {
        // x position between left & right border
        int x = (int)Random.Range(-50, 60);

        // y position between top & bottom border
        int y = (int)Random.Range(-30, 30);

        // Instantiate the food at (x, y)
        Instantiate(foodPrefab,
                    new Vector2(x, y),
                    Quaternion.identity); // default rotation
    }
}
