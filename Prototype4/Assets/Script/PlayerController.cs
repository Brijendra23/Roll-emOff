using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    public float speed = 100.0f;
    private Rigidbody playerRb;
    public bool hasPowerup = false;
    private float powerupStrength = 10f;
    private float powerupTimer = 7.0f;
    public GameObject powerupIndicator;
    public bool isGameover = false;
    public GameObject gameOverMenu;
    public int score = 0;
    public static int highScore;
    public TMP_Text scoreText;
    public TMP_Text highScoreText;
    [SerializeField] private VariableJoystick joystick;
    public AudioSource audioRoll;
    
    
    
   


    private void Awake()
    {
        scoreText.text = "Score: "+ score.ToString();
        DataPersistance.Instance.LoadHighScore();
        scoreText.text=score.ToString();
        highScoreText.text="HighScore: "+ highScore.ToString();
        

    }




    public void UpdateHighScore(int score)
    {
        if (score <= highScore) { return; }
        if (score > highScore)
        {
            highScore = score;
            DataPersistance.Instance.SaveHighScore();
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    
    }

    // Update is called once per frame
    void Update()
    {
        
        float forwardInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 direction= Vector3.forward*joystick.Vertical*speed*Time.deltaTime+
            Vector3.right*joystick.Horizontal*speed*Time.deltaTime;
        


        playerRb.AddForce(Vector3.forward* forwardInput *speed);
        playerRb.AddForce(Vector3.right * horizontalInput * speed);
        playerRb.AddForce(direction,ForceMode.VelocityChange);
       
      
        
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f,0);
        if (gameObject.transform.position.y < -6.0f)
        {
            Destroy(gameObject);
            joystick.gameObject.SetActive(false);
            isGameover = true;
            UpdateHighScore(score);
            
        }

        if(isGameover) { gameOverMenu.SetActive(true); }
        
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!hasPowerup)
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
    }
    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(powerupTimer);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)

    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            audioRoll.PlayOneShot(audioRoll.clip);
           
            if (hasPowerup)
            {
                Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
                Vector3 awayfromPlayer = collision.gameObject.transform.position - transform.position;
                enemyRb.AddForce(awayfromPlayer * powerupStrength, ForceMode.Impulse);


            } 
        }
    }
   
    


}
