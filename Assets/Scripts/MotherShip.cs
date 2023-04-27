using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherShip : MonoBehaviour
{
    public PowerUp[] powerupsToDrop;
    public float speed = 2.0f;

    private Vector3 _direction = Vector2.right;
    private float timer = 0.0f;
    public float waitMin = 1.0f;
    public float waitMax = 3.0f;
    private float pauseTime;
    public float waitTime;
    private bool pauseMothership = true;

    private void Awake()
    {
        this.waitTime = Random.Range(this.waitMin, this.waitMax);
        this.timer += Time.deltaTime;
        this.pauseTime = this.timer;
    }

    private void Update()
    {
        this.timer += Time.deltaTime;

        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        if (!this.pauseMothership)
        {
            this.transform.position += _direction * this.speed * Time.deltaTime;
        }

        if (
            _direction == Vector3.right
            && this.transform.position.x - 5.0 >= (rightEdge.x - 1.0f)
            && !this.pauseMothership
        )
        {
            this.pauseMothership = true;
            this.pauseTime = timer;
        }
        else if (
            _direction == Vector3.left
            && this.transform.position.x + 5.0 <= (leftEdge.x + 1.0f)
            && !this.pauseMothership
        )
        {
            this.pauseMothership = true;
            this.pauseTime = timer;
        }

        if (this.pauseMothership && this.timer > this.pauseTime + this.waitTime)
        {
            SwapDirection();
        }

        // StartCoroutine(Pause());

        //Put the thread to sleep
        // wait a few seconds
        // yield return new WaitForSeconds(5);
        // }
    }

    void SwapDirection()
    {
        _direction.x *= -1.0f;
        this.pauseMothership = false;
        this.waitTime = Random.Range(this.waitMin, this.waitMax);
        // wait for a random amount of time
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {
            int powerupNum = Random.Range(0, powerupsToDrop.Length - 1);
            Instantiate(
                this.powerupsToDrop[powerupNum],
                this.transform.position,
                Quaternion.identity
            );
            this.gameObject.SetActive(false);
        }
    }
}
