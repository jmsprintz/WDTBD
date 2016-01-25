using UnityEngine;
using System.Collections;

public class TurretLaser : MonoBehaviour
{
    public float speed = .1f;
    Vector3 pos;
    private Rigidbody rigidbody;

    // Use this for initialization
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ShipController.freezer == true)
        {
            transform.position = pos;
        }
        if (rigidbody.position.y > 18) Destroy(gameObject);
        Vector3 target = ShipController.S.gameObject.transform.position;
        target.z = transform.position.z;
        if (paddle)
        {
            target.x = pos.x + 10 * paddleVector.x;
            target.y = pos.y + 10 * paddleVector.y;
            transform.position = Vector3.MoveTowards(gameObject.transform.position, target, speed);
            pos = transform.position;
        }
        else
        {
            target.x = pos.x + 10 * rigidbody.velocity.x;
            target.x = pos.x + 10 * rigidbody.velocity.y;
            pos = transform.position;
        }
        
        if (gameObject.transform.position.y > 15 || gameObject.transform.position.y < -15
            || gameObject.transform.position.x > 20 || gameObject.transform.position.x < -20)
            Destroy(gameObject);
    }

    public bool paddle = false;
    Vector2 paddleVector;
    public void Paddle(Vector2 norm)
    {
        paddle = true;
        paddleVector = norm;
        speed = .3f;
        Invoke("PaddleOff", 5);
    }
    void PaddleOff()
    {
        paddle = false;
        speed = .1f;
    }

    void Beam()
    {
        bool alive = true;
        if (alive)
        {
            alive = false;
            //EnemySpawner.S.loseEnemy();
            Destroy(gameObject);
        }
    }
}
