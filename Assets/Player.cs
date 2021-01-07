using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float health = 50f;
    private float originalHealth;
    public Text healthText;
    public Text orbText;
    public Text gunText;
    private readonly char[] numArray = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
    public Color healthWarningColor;
    public Color deathColor;
    public Color normalHealthColor;
    public float bandAidHealthBoost = 30f;
    private int goldenOrbs = 0;
    private string[] gun = { "Pistol", "Rifle" };
    private string currentGun;
    public GameObject pistol;
    public GameObject rifle;
    public Shooter M4_8Shooter;
    public Shooter M1911Shooter;
    public GameObject MainCamera;
    public int amountOfAmmoToAdd = 30;

    // For Dying UI
    public Text youDiedText;
    public Button playAgainButton;

    // Winning UI
    public Text youWonText;

    // Beginning UI
    public Text beginText;
    public Button playButton;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0.0f;
        Cursor.lockState = CursorLockMode.None;

        int indexOfFirstNumber = healthText.text.IndexOfAny(numArray);
        // Change health text UI
        healthText.text = healthText.text.Substring(0, indexOfFirstNumber) + health;
        originalHealth = health + 30; // Allow user to have 30 extra health


        // Get shooters from guns
        M4_8Shooter = MainCamera.transform.Find("M4_8").GetComponent<Shooter>();
        M1911Shooter = MainCamera.transform.Find("M1911").GetComponent<Shooter>();
    }

    // Update is called once per frame
    void Update()
    {
        int indexOfFirstNumber = healthText.text.IndexOfAny(numArray);

        health = int.Parse(healthText.text.Substring(indexOfFirstNumber));
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // Bandaid
        if (other.gameObject.tag.Equals("Bandaid"))
        {
            // Increase health but make sure health doesn't go over original max health
            health = Mathf.Min(health + bandAidHealthBoost, originalHealth);

            int indexOfFirstNumber = healthText.text.IndexOfAny(numArray);
            healthText.text = healthText.text.Substring(0, indexOfFirstNumber) + health;

            if (health <= originalHealth * .5)
            {
                // Make text orange to warn player of low health
                healthText.color = healthWarningColor;
            }
            else
            {
                healthText.color = normalHealthColor;
            }

            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag.Equals("Golden Orb"))
        {
            goldenOrbs += 1;
            orbText.text = "Golden Orbs: " + goldenOrbs + "/5";

            if (goldenOrbs >= 5)
            {
                Win();
            }
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag.Equals("Rifle"))
        {
            // Change gun text
            // First, get index of first space
            // Then write rifle
            int indexOfFirstSpace = gunText.text.IndexOfAny(new char[] { ' ' });
            gunText.text = "Rifle" + gunText.text.Substring(indexOfFirstSpace);


            currentGun = "Rifle";
            ChangeGun("Rifle");

            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag.Equals("Bullets"))
        {
            if (currentGun == "Pistol")
            {

                M1911Shooter.AddMoreAmmo(amountOfAmmoToAdd);
            }
            else if (currentGun == "Rifle")
            {
                M4_8Shooter.AddMoreAmmo(amountOfAmmoToAdd);
            }

            Destroy(other.gameObject);
        }
    }

    private void Win()
    {
        Cursor.lockState = CursorLockMode.None;
        // Freeze scene
        Time.timeScale = 0.0f;

        youWonText.gameObject.SetActive(true);
        playAgainButton.gameObject.SetActive(true);
    }

    private void Die()
    {
        // Make text read bc player is dead
        healthText.color = deathColor;
        Cursor.lockState = CursorLockMode.None;
        // Freeze scene
        Time.timeScale = 0.0f;

        youDiedText.gameObject.SetActive(true);
        playAgainButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }

    public void BeginGame()
    {
        beginText.gameObject.SetActive(false);
        playButton.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
        //currentGun = "Pistol";
        ChangeGun("Pistol");

        //SceneManager.LoadScene(0);
    }
    // Changes gun
    private void ChangeGun(string gun)
    {
        currentGun = gun;

        if (gun == "Pistol")
        {
            pistol.SetActive(true);
            // Set all other guns as false
            rifle.SetActive(false);
        }
        else if (gun == "Rifle")
        {
            rifle.SetActive(true);
            // Set all other funs as false
            pistol.SetActive(false);
        }
    }

    public void TakeDamage(float damageToTake)
    {
        int indexOfFirstNumber = healthText.text.IndexOfAny(numArray);
        health = int.Parse(healthText.text.Substring(indexOfFirstNumber));
        health -= damageToTake;


        if (health <= 0)
        {
            health = 0;
            // Change health text UI
            healthText.text = healthText.text.Substring(0, indexOfFirstNumber) + health;
            Die();
        }
        else if(health <= originalHealth * .5)
        {
            // Make text orange to warn player of low health
            healthText.color = healthWarningColor;
            healthText.text = healthText.text.Substring(0, indexOfFirstNumber) + health;
        }
        else
        {
            // Change health text UI
            healthText.text = healthText.text.Substring(0, indexOfFirstNumber) + health;
            healthText.color = normalHealthColor;
        }
    }
}
