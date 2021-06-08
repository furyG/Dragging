using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Set In Inspector: Enemy")]
    public bool distancedEnemy;
    public float fallSpeed;
    public float multiScale;
    public float chanceOfSpawn;

    [Header("Set Dynimcally: Enemy")]
    public Vector3 distance;
    public Vector3 scale = Vector3.one;
    public Vector3 position;

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++) transform.GetChild(i).localScale += scale;
        if (distancedEnemy)
        {
            transform.GetChild(1).localPosition += distance;
            transform.position = new Vector3(0, GameController.G.Height + 6);
        }
        position = transform.position;
    }

    private void Update()
    {
        if (GameController.G.playing) Move();
        if (transform.position.y <= -GameController.G.Height) Destroy(gameObject);
    }
    public void Move()
    {
        position.y -= fallSpeed * Time.deltaTime;
        transform.position = position;
    }
}
