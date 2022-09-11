using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{
    private void Start()
    {
        gameObject.AddComponent<Rigidbody2D>();
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();

    }
}
