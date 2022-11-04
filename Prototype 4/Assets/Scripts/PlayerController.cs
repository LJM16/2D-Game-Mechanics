using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    public float Speed = 10;
    public float PowerupStrength = 5;

    private Rigidbody2D _playerRB;
    public GameObject ExplosionFX;
    public GameObject PowerupIndicator;
    public bool HasPowerup = false;
    private Rigidbody2D _PlayerRB;
    private SpriteRenderer _playerSR;
    // Start is called before the first frame update
    void Start()
    {
        _playerRB = GetComponent<Rigidbody2D>();
        _playerSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector2 direction = new Vector2(horizontalInput, verticalInput);
        _playerRB.AddForce(direction * Speed);   
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Wall"))
        {
            StartCoroutine(GameOverRoutine());
        }

         if(other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            PowerupIndicator.gameObject.SetActive(true);
            HasPowerup = true;
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Enemy") && HasPowerup)
        {
            Rigidbody2D enemyRB = other.gameObject.GetComponent<Rigidbody2D>();

            Vector2 awayFromPlayer = (other.gameObject.transform.position - transform.position);

            enemyRB.AddForce(awayFromPlayer * PowerupStrength, ForceMode2D.Impulse);
            PowerupIndicator.gameObject.SetActive(false);
            HasPowerup = false;
        }
    }

     IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(5);
        PowerupIndicator.gameObject.SetActive(false);
        HasPowerup = false;
    }

    IEnumerator GameOverRoutine()
    {
        Instantiate(ExplosionFX, transform.position, ExplosionFX.transform.rotation);
        //gameObject.SetActive(false);
        PowerupIndicator.gameObject.SetActive(false);
        HasPowerup = false;
        _playerSR.enabled = false;
        yield return new WaitForSeconds(1.1f);
        SceneManager.LoadScene(0);
    }
}
