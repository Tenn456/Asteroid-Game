using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used ChatGPT to assist with code dealing with dragging and launching Superman.

public class Superman : MonoBehaviour
{
    private Rigidbody2D rb;
    private Camera mainCamera;
    private Vector2 startPos;
    private Vector2 dragStartPosition;
    private Vector2 dragEndPosition;
    private Vector2 dragDirection;
    private Vector2 endDirection;
    private bool isDragging = false;
    private bool canDrag = true;
    public bool hit;

    public float speed = 10f; 
    public float maxDragDistance = 3f;

    private Collider2D superManCollider;

    public GameManager gm;

    private Vector2 mousePosition;

    private Coroutine reset;

    void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        superManCollider = GetComponent<Collider2D>();
        mainCamera = Camera.main;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    void Update()
    {
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0) && superManCollider.OverlapPoint(mousePosition) && !isDragging && canDrag)
        {
            StartDrag();
        }

        if (isDragging)
        {
            Drag();
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            Launch();
        }
    }

    void StartDrag()
    {
        rb.constraints = RigidbodyConstraints2D.None;
        rb.velocity = Vector2.zero;
        dragStartPosition = mousePosition; // Sets the starting position of the drag
        canDrag = false;
        isDragging = true;
    }

    void Drag()
    {
        // Calculate the direction and distance from the start position
        dragDirection = mousePosition - dragStartPosition;

        // Limit the drag distance to the maxDragDistance (clamp the position)
        if (dragDirection.magnitude > maxDragDistance)
        {
            dragDirection = dragDirection.normalized * maxDragDistance;
        }

        // Calculate the new position
        Vector3 newPosition = dragStartPosition + dragDirection;

        // Restrict the ball's position to never go higher than a certain y value (maxY)
        newPosition.y = Mathf.Min(newPosition.y,-2.6f);

        // Set the ball's position to the clamped position
        transform.position = newPosition;
    }

    void Launch()
    {
        // Calculate end position by adding the start position with the length and direction of the drag
        dragEndPosition = dragStartPosition + dragDirection;

        // Calculate which way superman will be flung by subtracting the end position from the starting position.
        endDirection = dragStartPosition - dragEndPosition;

        // Apply force to the ball based on drag direction and distance
        rb.AddForce(endDirection * speed, ForceMode2D.Impulse);

        reset = StartCoroutine(Reset());

        isDragging = false;
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(1f);

        // Reset the Astroid
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.position = startPos;
        canDrag = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if Superman hit an asteroid
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            // Reset the Astroid
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            transform.position = startPos;
            canDrag = true;
            StopCoroutine(reset);

            gm.numDestroyed += 1;
            gm.score += 1;
        }
    }
}

