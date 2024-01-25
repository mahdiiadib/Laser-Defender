using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 50;
    [SerializeField] int score = 1;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] bool applyCameraShake;
    [SerializeField] bool isPlayer;

    CameraShake cameraShake;
    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;
    GameManager gameManager;

    public int CurrentHealth => health;

    void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer dd = other.GetComponent<DamageDealer>();
        if (dd != null)
        {
            TakeDamage(dd.Damage);
            PlayHitEffect();
            if (applyCameraShake && cameraShake != null) cameraShake.Play();
            dd.Hit();
        }
        else
        {
            if (applyCameraShake && cameraShake != null) cameraShake.Play();
            scoreKeeper.ModifyScore(score);
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        if (audioPlayer != null) audioPlayer.PlayDamageClip();
        if (health <= 0)
        {
            if (!isPlayer)
            {
                scoreKeeper.ModifyScore(score);
                Destroy(gameObject);
            }
            else
            {
                FindObjectOfType<Player>().enabled = false;
                GetComponentInChildren<SpriteRenderer>().enabled = false;
                gameManager.LoadGameOver();
            }
            
        }
    }

    void PlayHitEffect()
    {
        if (hitEffect != null)
        {
            ParticleSystem ps = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(ps.gameObject, ps.main.duration + ps.main.startLifetime.constantMax);
        }
    }
}
