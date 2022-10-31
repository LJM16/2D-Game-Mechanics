using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    public float Speed = 10;

    private Rigidbody2D _playerRB;
    public GameObject ExplosionFX;
    // Start is called before the first frame update
    void Start()
    {
        _playerRB = GetComponent<Rigidbody2D>();
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
            Instantiate(ExplosionFX, transform.position, ExplosionFX.transform.rotation);
            gameObject.SetActive(false);
            SceneManager.LoadScene(0);
        }
    }
}
