using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HandleTank : MonoBehaviour
{
    public string xAxis = "Horizontal";
    public string yAxis = "Vertical";

    public string txAxis = "Horizontal2";
    public string tyAxis = "Vertical2";

    public string fire = "TriggerRight";

    public int maxHP = 20;
    public int hp = 20;

    public float invincibilityTime = .1f;

    public float speed = 5;

    public int armour = 1;

    public GameObject died;
    public GameObject explosion;

    public static float score = 0;
    static public bool newHighScore = false;
    float highScore = 0;

    public Slider health;
    public Image healthColor;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    public Animator damageIndicator;

    private float invincibility = 0;
    Rigidbody2D rb;
    Transform turret;
    Transform muzzle;

    // Start is called before the first frame update
    void Start()
    {
        hp = maxHP;

        score = 0;

        highScore = PlayerPrefs.GetFloat("HighScore", 0);

        newHighScore = false;

        Time.timeScale = 1;

        rb = GetComponent<Rigidbody2D>();

        ShowHealth();

        turret = transform.Find("Turret");
        muzzle = turret?.Find("Muzzle");

        if (muzzle == null || turret == null)
        {
            Debug.LogError("Couldn't find muzzle or turret!");

            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (score > highScore)
        {
            newHighScore = true;

            highScore = score;

            PlayerPrefs.SetFloat("HighScore", highScore);

            highScoreText.text = $"High Score: {highScore}";
        }

        scoreText.text = $"Score: {score}";

        Vector2 dir =
            new Vector2(
                Input.GetAxis(xAxis),
                Input.GetAxis(yAxis));

        if (dir.sqrMagnitude > .05f)
        {
            float angle = Vector2.SignedAngle(Vector2.up, dir);

            transform.eulerAngles = new Vector3(0, 0, angle);
        }

        rb.velocity = speed * dir;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DieAlready(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DieAlready(collision.gameObject);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        DieAlready(collision.gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        DieAlready(collision.gameObject);
    }


    private void DieAlready(GameObject collision)
    {
        if (collision.gameObject.CompareTag("Pick Up"))
        {
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Lethal"))
        {
            if (invincibility < Time.time)
            {
                invincibility = Time.time + invincibilityTime;

                var s = collision.GetComponent<Stats>();

                int damage = s.damage - armour;

                damage = Mathf.Max(damage, 0);

                hp -= damage;

                if (hp == 0)
                {
                    damageIndicator.SetTrigger("Shielded");
                }
                else
                {
                    damageIndicator.SetTrigger("Damaged");
                }

                ShowHealth();

                if (hp <= 0)
                {
                    died.SetActive(true);

                    Destroy(Instantiate(explosion, transform.position, Quaternion.identity), 2);

                    Destroy(gameObject);
                }
            }
        }
    }

    public void ShowHealth()
    {
        float HPValue = (float)hp / (float)maxHP;

        health.value = HPValue;

        if (HPValue > .5f)
        {
            healthColor.color = Color.green;

            return;
        }

        if (HPValue > .4f)
        {
            healthColor.color = Color.yellow;

            return;
        }

        healthColor.color = Color.red;
    }
}
