using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController G;

    [Header("Set in Inspector")]
    public float enemySpawnTime;
    public bool playing;
    public Vector3 HeroPos;
    public Text Score, scoreTxt;
    public GameObject heroPrefab, spawnHero;
    public int score;
    public List<GameObject> enemies;

    [Header("Set Dynamically")]
    public GameObject ANCHOR;
    public Vector3 heroOriginScale;
    public GameObject button;
    public float Height;
    public float Width;

    private void Awake()
    {
        scoreTxt = Score.GetComponent<Text>();
        G = this;
    }

    private void Start()
    {
        ANCHOR = new GameObject("GO_ANCHOR");
        playing = false;
        Height = Camera.main.orthographicSize;
        Width = Height * Camera.main.aspect;
    }

    void StartLevel()
    {
        for(int i = 0; i < ANCHOR.transform.childCount; i++) Destroy(ANCHOR.transform.GetChild(i).gameObject);
        score = 0;
        Destroy(spawnHero);
        spawnHero = Instantiate<GameObject>(heroPrefab);
        spawnHero.transform.position = HeroPos;
        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        if (playing)
        {
            score++;
            scoreTxt.text = ("Score: " + score);
            int index = Random.Range(0, enemies.Count);
            GameObject spawnEnemy = Instantiate<GameObject>(enemies[index]);
            Enemy script = spawnEnemy.GetComponent<Enemy>();
            script.fallSpeed += score * 0.1f;
            script.scale.y += score * 0.15f;
            if (script.distancedEnemy) script.distance.x -= score * 0.05f;
            spawnEnemy.transform.position = new Vector3(Random.Range(-Width, Width), Height + 6);
            spawnEnemy.transform.SetParent(ANCHOR.transform);
            Invoke("SpawnEnemy", enemySpawnTime - score * 0.1f);
        }
    }

    private void Update()
    {
        button.SetActive(!playing);
    }

    public void Button()
    {
        playing = !playing;
        button.SetActive(!playing);
        if (playing) StartLevel();
    }
}
