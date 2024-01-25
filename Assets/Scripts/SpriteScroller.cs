using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteScroller : MonoBehaviour
{
    [SerializeField] Vector2 moveSpeed;

    Vector2 offset;
    Material mat;

    void Awake()
    {
        mat = GetComponent<SpriteRenderer>().material;
    }

    void Update()
    {
        mat.mainTextureOffset += moveSpeed * Time.deltaTime;
    }
}
