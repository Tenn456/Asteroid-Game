using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverText;
    public AudioSource bgm;
    public AudioSource loseSong;

    private void Start()
    {
        loseSong = GetComponent<AudioSource>();
        gameOverText.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            Time.timeScale = 0;
            gameOverText.SetActive(true);
            bgm.Stop();
            loseSong.Play();
        }
    }

    public void Replay()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene");
    }
}
