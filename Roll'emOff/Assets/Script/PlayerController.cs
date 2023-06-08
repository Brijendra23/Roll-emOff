using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody playerRb;
    public GameObject focalPoint;
    public bool hasPowerup = false;
    private float powerupStrength = 10f;
    private float powerupTimer = 7.0f;
    public GameObject powerupIndicator;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
        
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed);
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f,0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            hasPowerup = true;
            FindObjectOfType<SpawnManager>().numberPowerup--;
            powerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());

        }
    }
    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(powerupTimer);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy")&& hasPowerup)
        {
            Rigidbody enemyRb=collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayfromPlayer = collision.gameObject.transform.position - transform.position;
            enemyRb.AddForce(awayfromPlayer*powerupStrength, ForceMode.Impulse);

        }
    }
}
