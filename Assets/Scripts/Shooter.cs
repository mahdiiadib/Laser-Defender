using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10, projectileLifetime = 5, baseFiringRate = 0.2f;
    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float firingRateVariance = 0, minFiringRate = 0.1f;

    Coroutine firingCoroutine;
    AudioPlayer audioPlayer;

    [HideInInspector] public bool isFiring;

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Start()
    {
        if (useAI)
        {
            isFiring = true;
            projectileSpeed *= -1;
        }
        else
        {
            firingRateVariance = 0;
        }
    }

    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if (!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject p = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            p.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            audioPlayer.PlayShootingClip();
            Destroy(p, projectileLifetime);
            float timeToFireNext = Mathf.Clamp(Random.Range(baseFiringRate - firingRateVariance, baseFiringRate + firingRateVariance), minFiringRate, float.MaxValue);
            yield return new WaitForSeconds(timeToFireNext);
        }
    }
}
