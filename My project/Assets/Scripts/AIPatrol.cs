//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class AIPatrol : MonoBehaviour
//{
//    // Start is called before the first frame update
//    private bool isPatroling;
//    private bool mustTurn = false;
//    private Rigidbody2D rb;
//    private Enemy enemy;
//    private Player player;
//    private Transform groundCheckPos;
//    private LayerMask ground;
//    private GameObject gameobjPlayer;
//    void Start()
//    {
//        isPatroling = true;
//        enemy = GetComponent<Enemy>();
//        gameobjPlayer = GameObject.Find("Player");
//        player = gameobjPlayer.GetComponent<Player>();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        print(enemy.getAgroDist() + " " + player.transform.position);
//        //float distToPlayer = Vector2.Distance(transform.position, player.transform.position);
//        //if (distToPlayer <= enemy.getAgroDist())
//        //    StartHunting();
//        //else if ((int)enemy.getStartPos().x != (int)transform.position.x)
//        //    StopHunting();
//        //Debug.DrawLine(transform.position - new Vector3(enemy.getAgroDist(), 0), transform.position + new Vector3(enemy.getAgroDist(), 0));
//    }
//    private void FixedUpdate()
//    {
//        //if (isPatroling)
//        //    mustTurn = Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, ground);
//    }
//    //private void Patrol()
//    //{
//    //    float dir = mus
//    //    if (mustTurn)
//    //        Flip();
//    //    rb.velocity = new Vector2();
//    //}
//    private void Flip()
//    {
//        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
//    }
//    protected float Destination(Vector3 target)
//    {
//        float movex = transform.position.x < target.x ? 1f : -1f;
//        return movex;
//    }
//    protected void Flip(Vector3 target)
//    {
//        Vector3 enemypos = transform.position;
//        if (enemypos.x < target.x && transform.localScale.x == -1 ||
//            enemypos.x > target.x && transform.localScale.x == 1)
//            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
//    }
//    protected void StartHunting()
//    {
//        //float moveX = transform.position.x < player.transform.position.x ? -1f : 1f;
//        float moveX = Destination(player.transform.position);
//        Flip(player.transform.position);
//        enemy.Move(moveX * enemy.getSpeed());
//        var d = player.transform.position - transform.position;
//    }
//    protected void StopHunting()
//    {
//        float moveX = Destination(enemy.getStartPos());
//        enemy.Move(moveX * enemy.getSpeed());
//        Flip(enemy.getStartPos());
//    }
//}
