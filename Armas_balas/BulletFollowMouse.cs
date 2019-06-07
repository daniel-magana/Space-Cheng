using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFollowMouse : MonoBehaviour {
    Quaternion p;
    public float velocidadRotacion = 100f;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate () {
        Vector3 mouse = Input.mousePosition;
        mouse = Camera.main.ScreenToWorldPoint(mouse);

        Vector2 dir = rb.position - new Vector2(mouse.x, mouse.y);

        dir.Normalize();

        float rotacion= Vector3.Cross(dir, transform.up).z;

        rb.angularVelocity = rotacion * velocidadRotacion;
    }
}
