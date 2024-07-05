using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    Player player;

    public float groundHeight;
    public float groundRight;
    public float screenRight;
    public float groundPosition;
    BoxCollider2D collider;

    bool didGenerateGround=false;

    public Obstacle boxTemplate;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        collider = GetComponent<BoxCollider2D>();
        groundHeight = transform.position.y + (collider.size.y / 2) +1 ;
        groundPosition = transform.position.y;
        Debug.Log(groundHeight);
        screenRight = Camera.main.transform.position.x + 10;
       

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 pos = transform.position;
        pos.x -= player.velocity.x * Time.fixedDeltaTime;

        groundRight = transform.position.x + (collider.size.x / 2);
        Debug.Log(groundRight);

        if(groundRight < -10)
        {
            Destroy(gameObject);
            return;
        }
        if (!didGenerateGround)
        {
            if (groundRight < screenRight)
            {
                didGenerateGround = true;
                generateGround();
            }
        }
        
        transform.position = pos;
    }

    void generateGround()
    {
        GameObject go = Instantiate(gameObject);
        BoxCollider2D goCollider = go.GetComponent<BoxCollider2D>();
        Vector2 pos;

        pos.y = groundPosition;
        pos.x = screenRight;
        go.transform.position = pos;

        Ground goGround = go.GetComponent<Ground>();

        int obstacleNum = Random.Range(0, 4);
        for(int i = 0; i < obstacleNum; i++)
        {
            GameObject box = Instantiate(boxTemplate.gameObject);
            float y =groundHeight;
            float halfWidth = goCollider.size.x / 2 -1;
            float left = go.transform.position.x - halfWidth;
            float right = go.transform.position.x + halfWidth;
            float x = Random.Range(left, right);
            Vector2 boxPos = new Vector2(x,y);
            box.transform.position = boxPos;
        }
    }
}
