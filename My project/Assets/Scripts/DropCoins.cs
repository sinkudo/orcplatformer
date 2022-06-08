using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCoins : MonoBehaviour
{
    [SerializeField] GameObject objectToSpawn;
    private int counter = 0;
    Transform position;
    Rigidbody2D rb;
    Player player;
    void Start()
    {
        //var trajectory = UnityEngine.Random.insideUnitCircle * velocity;
        position = GameObject.Find("Player").transform;
        player = GameObject.Find("Player").GetComponent<Player>();
        rb = objectToSpawn.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //spawnCoins();
    }
    public void spawnCoins()
    {
        counter = 0;
        print(transform.position);
        while (counter++ < 3)
        {
            var coin = GameObject.Instantiate(objectToSpawn);
            coin.SetActive(true);
            Rigidbody2D coinBody = coin.GetComponent<Rigidbody2D>();
            coin.transform.position = position.position + Vector3.up + Vector3.up;
            if(counter == 0)
                coinBody.AddForce(new Vector2(-4, 1), ForceMode2D.Impulse);
            if (counter == 1)
                coinBody.AddForce(new Vector2(0, 1), ForceMode2D.Impulse);
            if (counter == 2)
                coinBody.AddForce(new Vector2(4, 1), ForceMode2D.Impulse);
        }
        //}
    }
}
