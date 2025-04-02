using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public GameObject target;
    public ParticleSystem explosion;
    public GameManager gm;
    public SpriteRenderer sr;
    public CircleCollider2D col;
    public AudioSource audioSource;

    public bool moving = true;
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Earth");
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
        moveSpeed = Random.Range(0.5f, 2f);
        explosion = GetComponent<ParticleSystem>();
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<CircleCollider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if Superman hit an asteroid
        if (collision.gameObject.CompareTag("Player"))
        {
            explosion.Play();
            audioSource.Play();
            moving = false;
            sr.enabled = false;
            col.enabled = false;
            Destroy(this.gameObject, 1f);
            gm.SpawnAsteroid();
        }

        if (collision.gameObject.CompareTag("Finish"))
        {
            explosion.Play();
            audioSource.Play();

        }
    }
}
