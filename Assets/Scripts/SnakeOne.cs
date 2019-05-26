using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SnakeOne : MonoBehaviour
{
    // Current Movement Direction
    // (by default it moves to the right)
    Vector2 dir = Vector2.right;

    // Keep Track of Tail
    List<Transform> tail = new List<Transform>();

    // Did the snake eat something?
    bool ate = false;

    // Tail Prefab
    public GameObject tailPrefab;

    // Food Prefab
    public GameObject foodPrefab;

    // Borders
    public Transform borderTop;
    public Transform borderBottom;
    public Transform borderLeft;
    public Transform borderRight;

    public GameObject snake1;



    // Use this for initialization
    void Start()
    {
        InvokeRepeating("Move", 0.05f, 0.05f);
    }

    // Update is called once per frame
    void Update()
    {
        // Move in a new Direction?
        if (Input.GetKey(KeyCode.RightArrow))
            dir = Vector2.right;
        else if (Input.GetKey(KeyCode.DownArrow))
            dir = -Vector2.up;    // '-up' means 'down'
        else if (Input.GetKey(KeyCode.LeftArrow))
            dir = -Vector2.right; // '-right' means 'left'
        else if (Input.GetKey(KeyCode.UpArrow))
            dir = Vector2.up;
    }

    void Move()
    {
        // Save current position (gap will be here)
        Vector2 v = transform.position;

        // Move head into new direction (now there is a gap)
        transform.Translate(dir);

        // Ate something? Then insert new Element into gap
        if (ate)
        {
            // Load Prefab into the world
            GameObject g = (GameObject)Instantiate(tailPrefab,
                                                  v,
                                                  Quaternion.identity);

            // Keep track of it in our tail list
            tail.Insert(0, g.transform);

            // Reset the flag
            ate = false;
        }
        // Do we have a Tail?
        else if (tail.Count > 0)
        {
            // Move last Tail Element to where the Head was
            tail.Last().position = v;

            // Add to front of list, remove from the back
            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
       
        // Collided with a food prefab.
        if (coll.name.StartsWith("FoodPrefab"))
        {
            Debug.Log("Collided with food");
            // Get longer in next Move call
            ate = true;

            // Remove the Food
            Destroy(coll.gameObject);

        }
        // Collided with a border
        else if (coll.gameObject.tag == "Borders")
        {
            Debug.Log("Collided with border");
            // x position between left & right border
            int x = (int)Random.Range(-50, 60);

            // y position between top & bottom border
            int y = (int)Random.Range(-30, 30);

            GameObject[] tail1 = GameObject.FindGameObjectsWithTag("TailPrefab1");
            foreach (GameObject tailObject in tail1)
            {
                Destroy(tailObject.gameObject);
            }
            tail.Clear();
            snake1.transform.position = new Vector2(x, y);

        }
        // Collided with Tail
        else
        {
            Debug.Log("Collided with tail");
            GameObject[] tail1 = GameObject.FindGameObjectsWithTag("TailPrefab1");
            foreach (GameObject tailObject in tail1)
            {
                Destroy(tailObject.gameObject);
            }
            tail.Clear();
        }
    }

    
}