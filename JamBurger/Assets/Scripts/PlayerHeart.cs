using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class PlayerHeart : MonoBehaviour {

    public int hearts = 3;
    public int maxHearts = 3;

    //TEST
    public float xPushed = 20f;
    public float yPushed = 15f;
    private float timer;
    private bool invincible;

    private Animator animatotor;
    private Rigidbody2D rb;
    private SpriteRenderer skin;

    [SerializeField] HeartSystem hs;

    private void Start() {
        hs.DrawHeart(hearts, maxHearts);
        //TEST
        rb = GetComponent<Rigidbody2D>();
        animatotor = GetComponent<Animator>();
        skin = GetComponent<SpriteRenderer>();
    }

    public void damagePlayer(int dmg) {
        if (hearts > 0) {
            if (!invincible)
            {
                hearts -= dmg;
                invincible = true;
                hs.DrawHeart(hearts, maxHearts);
                animatotor.SetTrigger("damaged");
                SoundManagerScript.playSound("playerHit");
                rb.velocity = new Vector2(xPushed, yPushed);
                skin.color = Color.clear;
                timer = Time.time;
                StartCoroutine(recovery());
            }
        }
        if (hearts <= 0)
        {
            StartCoroutine(DEATH());
        }
    }

    public void healPlayer(int dmg)
    {
        if(hearts < maxHearts){
            hearts += dmg;
            hs.DrawHeart(hearts, maxHearts);
        }   
    }

    void OnCollisionStay2D(Collision2D touched)
    {
        
        if (touched.gameObject.tag == "Enemy")
        {
            Debug.Log("TOUCHER XD");
            damagePlayer(1);
        }
    }

    IEnumerator recovery()
    {
        yield return new WaitForSeconds(0.1f);
        skin.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        skin.color = Color.clear;
        yield return new WaitForSeconds(0.1f);
        skin.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        skin.color = Color.clear;
        yield return new WaitForSeconds(0.1f);
        skin.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        skin.color = Color.clear;
        yield return new WaitForSeconds(0.1f);
        skin.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        skin.color = Color.clear;
        yield return new WaitForSeconds(0.1f);
        skin.color = Color.white;
        yield return new WaitForSeconds(0.4f);
        skin.color = Color.white;
        invincible = false;
    }

    IEnumerator DEATH()
    {
        Debug.Log("T MORT");
        hearts = 0;
        animatotor.SetTrigger("ded");
        rb.velocity = new Vector2(rb.velocity.x, 5f);
        rb.constraints = RigidbodyConstraints2D.None;
        rb.AddTorque(50f);
        skin.color = Color.red;
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Map_1");
    }
}
