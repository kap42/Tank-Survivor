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

    public float speed = 5;

    public GameObject died;
    public GameObject explosion;

    public static float score = 0;
    static public bool newHighScore = false;
    float highScore = 0;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    Rigidbody2D rb;
    Transform turret;
    Transform muzzle;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;

        highScore = PlayerPrefs.GetFloat("HighScore", 0);

        newHighScore = false;

        Time.timeScale = 1;

        rb = GetComponent<Rigidbody2D>();

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

    private void DieAlready(GameObject collision)
    {
        if (collision.gameObject.CompareTag("Pick Up"))
        {
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Lethal"))
        {
            died.SetActive(true);

            Destroy(Instantiate(explosion, transform.position, Quaternion.identity), 2);

            Destroy(gameObject);
        }
    }
}
