using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditor.UI;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    private float originalX;
    private float maxOffset = 5.0f;
    private float enemyPatroltime = 2.0f;
    private int moveRight = -1;
    private Vector2 velocity;
    public Animator ani;
    

    private Rigidbody2D enemyBody;
    public Vector3 startPosition = new Vector3(-7.9f, 4.32f , 0.0f);

    public GameObject self;
    public BoxCollider2D selfCollider;
    public EdgeCollider2D edgeCollider;
    private bool dead;

    public GameManager gameManager;

    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        // get the starting position
        originalX = transform.position.x;
        ComputeVelocity();
        dead = false;
    }
    void ComputeVelocity()
    {
        velocity = new Vector2((moveRight) * maxOffset / enemyPatroltime, 0);
    }
    void Movegoomba()
    {
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
    }

    // note that this is Update(), which still works but not ideal. See below.
    void Update()
    {
        if (!dead)
        {



            if (Mathf.Abs(enemyBody.position.x - originalX) < maxOffset)
            {// move goomba
                Movegoomba();
            }
            else
            {
                // change direction
                moveRight *= -1;
                ComputeVelocity();
                Movegoomba();
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.CompareTag("Feet"))
        {
            self.layer = LayerMask.NameToLayer("Secret");

            gameManager.IncreaseScore(1);
            selfCollider.enabled = false;
            //edgeCollider.enabled = false;
            ani.SetTrigger("stepped");
            dead = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
         
    }
    public void GameRestart()
    {
        transform.localPosition = startPosition;
        originalX = transform.position.x;
        moveRight = -1;
        ComputeVelocity();
        self.SetActive(true);
        ani.SetTrigger("restart");
        selfCollider.enabled = true;
        //edgeCollider.enabled = true;
        dead = false;
        self.layer = LayerMask.NameToLayer("Enemies");
    }

    public void inactive()
    {
        self.SetActive(false);
    }
}