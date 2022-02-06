using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 4f;
    public int vie = 2;

    private GameObject limitD;
    private GameObject limitG;
    private float direction = 1f;
    private Rigidbody2D rb;
    private SpriteRenderer skin;
    private Animator animatotor;
    private float timer;
    private bool poufTesMort;
    private bool invincible;

    // Start is called before the first frame update
    void Start()
    {
        limitD = transform.Find("limitD").gameObject;
        limitG = transform.Find("limitG").gameObject;
        limitD.transform.parent = null;
        limitG.transform.parent = null;
        limitD.GetComponent<SpriteRenderer>().enabled = false;
        limitG.GetComponent<SpriteRenderer>().enabled = false;
        rb = GetComponent<Rigidbody2D>();
        animatotor = GetComponent<Animator>();
        skin = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (vie > 0)
        {
            rb.velocity = new Vector2(speed * direction, rb.velocity.y);

            if (transform.position.x > limitD.transform.position.x)
            {
                direction = -1f;
                skin.flipX = false;
            }
            if (transform.position.x < limitG.transform.position.x)
            {
                direction = 1f;
                skin.flipX = true;
            }
        }

        if (vie <= 0 && !poufTesMort)
        {
            jeSuisMouru();
        }
        if (skin.color != Color.white)
        {
            skin.color = Color.Lerp(Color.red, Color.white, (Time.time - timer) * 3f);
        }
    }

    void jeSuisMouru()
    {
        poufTesMort = true;
        animatotor.SetTrigger("death");
        rb.velocity = new Vector2(rb.velocity.x, 5f);
        rb.constraints = RigidbodyConstraints2D.None;
        rb.AddTorque(50f);
        skin.color = Color.red;
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 3f);
    }
    public void takeDamage(int damage)
    { if (!invincible) {
            vie = vie - damage;
            invincible = true;
            skin.color = Color.red;
            timer = Time.time;
            StartCoroutine(recovery());
     }
    }

    IEnumerator recovery()
    {
        yield return new WaitForSeconds(0.1f);
            invincible = false;
    }
    void OnTriggerEnter2D(Collider2D truc)
    {
        if (truc.tag == "bullet")
        {
            takeDamage(1);
        }

    }
}

