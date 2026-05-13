using UnityEngine.InputSystem;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float movementX;
    private float movementY;

    private int health = 3;

    public float speed = 10;

    public Slider hpBar;

    public RoundManager roundManager;

    public AudioClip pickUpSound;
    public AudioClip damageSound;

    private AudioSource audioSource;

    private bool canTakeDamage = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        hpBar.maxValue = health;
        hpBar.value = health;
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed, ForceMode.Acceleration);
        rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, 8f);
    }

    public void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickUp"))
        {
            audioSource.PlayOneShot(pickUpSound);
            roundManager.CollectItem();
            other.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision) 
    { 
        if (collision.gameObject.CompareTag("Enemy") && canTakeDamage) 
        {
            TakeDamage(1);
        } 
    }

    private void TakeDamage(int damage)
    {
        audioSource.PlayOneShot(damageSound);
        health--;
        hpBar.value = health;
        if (health <= 0)
        {
            roundManager.LoseGame();
            gameObject.SetActive(false);
        }
        StartCoroutine(DamageCooldown());
    }

    private IEnumerator DamageCooldown()
    {
        canTakeDamage = false;

        yield return new WaitForSeconds(1f);

        canTakeDamage = true;
    }
}
