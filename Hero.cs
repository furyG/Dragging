using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public static Hero H;
    [Header("Set In Inspector")]
    public GameObject particlePrefab;

    [Header("Set DYnimcally")]
    public Vector3 pos;
    public bool pushed;
    public float pushedTime;
    public Vector3 mousePos;
    public Vector3 OriginPos;
    public Vector3 OriginScale;
    public Color colorG;
    public Color colorR;

    protected Collider col = null;
    protected Material mat;

    private void Awake()
    {
        H = this;
        col = GetComponent<Collider>();
        mat = GetComponent<MeshRenderer>().material;
    }
    private void Start()
    {
        OriginPos = transform.position;
        OriginScale = transform.localScale;
    }
    private void OnMouseDown()
    {
        mat.color = colorG;
        pushed = true;
    }
    private void OnMouseUp()
    {
        mat.color = colorR;
        pushed = false;
    }
    private void Update()
    {
        if (GameController.G.playing)
        {
            pos = transform.position;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            if (Input.GetKey(KeyCode.Mouse0) && pushed)
            {
                pushedTime = Time.time;
                mousePos.y = OriginPos.y;
                if (mousePos.x >= 2) mousePos.x = 2f;
                if (mousePos.x <= -2) mousePos.x = -2f;
                transform.position = new Vector3(mousePos.x, mousePos.y, 0f);
                if (OriginScale.x <= 2f)
                    OriginScale += Vector3.one * Time.deltaTime;
            }
            OriginScale -= Vector3.one * Time.deltaTime * 0.5f;

            if (OriginScale.x <= 0.3f && GameController.G.playing) GameController.G.playing = false;
            transform.localScale = OriginScale;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameController.G.playing = false;
        for(int i = 0; i < 5; i++)
        {
            GameObject particleSpawn = Instantiate<GameObject>(particlePrefab);
            particleSpawn.transform.position = collision.contacts[0].point;
            Renderer partRend = particleSpawn.GetComponent<Renderer>();
            partRend.material.color = mat.color;
            Rigidbody partRb = particleSpawn.GetComponent<Rigidbody>();
            partRb.AddForce(Random.insideUnitSphere * 2f);
        }
    }
}
