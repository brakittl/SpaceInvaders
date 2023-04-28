using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float fallSpeed = 8.0f;

    private void Awake() { }

    private void Update()
    {
        // Vector3 rotationToAdd = new Vector3(15, 0, 0);
        // transform.Rotate(rotationToAdd);
        // transform.Rotate(new Vector3(0, 45, 0) * Time.deltaTime);
        this.transform.Rotate(Vector3.forward * 90 * Time.deltaTime);
        this.transform.position += Vector3.down * Time.deltaTime * this.fallSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Boundary"))
        {
            Destroy(this.gameObject);
        }
    }
}
