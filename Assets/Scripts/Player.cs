using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed, paddingLeft, paddingRight, paddingTop, paddingBottom;

    Vector2 rawInput;
    Vector2 minBounds, maxBounds;
    Shooter shooter;

    void Awake()
    {
        shooter = GetComponent<Shooter>();
    }

    void Start()
    {
        InitBounds();
    }

    void Update()
    {
        Move();
    }

    void InitBounds()
    {
        //minBounds = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        minBounds = new Vector2(-4.5f, -7.3f);
        //maxBounds = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        maxBounds = new Vector2(4.5f, 8.5f);
    }

    void Move()
    {
        Vector2 delta = rawInput * moveSpeed * Time.deltaTime;
        Vector2 newpos = new Vector2();
        //newpos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
        newpos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x, maxBounds.x);
        //newpos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);
        newpos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y, maxBounds.y);
        transform.position = newpos;
    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    void OnFire(InputValue value)
    {
        if (shooter != null) shooter.isFiring = value.isPressed;
    }
}
