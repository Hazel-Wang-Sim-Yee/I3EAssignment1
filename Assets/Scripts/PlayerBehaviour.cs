using UnityEngine;
using TMPro;
using UnityEngine.UI;

/*
* Author: Hazel Wang Sim Yee
* Date: 15/6/2025
* Description: This script is to handle the player's behaviour in the game.
*/

public class PlayerBehaviour : MonoBehaviour
{
    CoinBehaviour currentCoin;
    DoorBehaviour currentDoor;

    KeyBehaviour currentKey;

    GiftBox currentChest;

    int score = 0;

    public float Health = 100;

    public float MaxHealth = 100;

    bool canInteract = false;

    bool dead = false;

    bool keyGet = false;

    bool goldKeyGet = false;

    bool chestOpen = false;

    string keyName;

    string doorName;

    bool SpecialText = false;

    [SerializeField]
    Transform checkPoint;

    [SerializeField]
    Transform deathScreen;

    [SerializeField]
    AudioClip damageSound;

    [SerializeField]
    AudioClip achievementSound;

    [SerializeField]
    AudioSource BGM;

    AudioSource playerAudioSource;

    public int endScore;

    public int deathCount;


    [SerializeField]
    Transform spawnPoint;

    [SerializeField]
    float IntDist = 10f;

    [SerializeField]
    TextMeshProUGUI scoreText;

    [SerializeField]
    TextMeshProUGUI healthText;

    [SerializeField]
    TextMeshProUGUI mainText;

    [SerializeField]
    Transform mainTextBody;

    [SerializeField]
    TextMeshProUGUI mainTextSpecial;

    [SerializeField]
    Image keyImage;

    [SerializeField]
    Image keyImageGold;


    [SerializeField]
    Sprite keySprite;

    [SerializeField]
    private HealthBarBehaviour HealthBar;

    [SerializeField]
    Transform winScreen;

    [SerializeField]
    TextMeshProUGUI deathCountText;

    [SerializeField]
    TextMeshProUGUI endScoreText;

    [SerializeField]
    TextMeshProUGUI coinsCollectedText;

    void Start()
    {
        scoreText.text = "Coins: " + score.ToString() + "/16";
        healthText.text = "Health: " + Health.ToString() + "/" + MaxHealth.ToString(); //Set UI texts
        checkPoint = GameObject.FindGameObjectWithTag("Checkpoint").transform; // Find the checkpoint in the scene
        // Initialize the player state
        keyImage.enabled = false;
        keyImage.sprite = keySprite;
        keyImageGold.sprite = keySprite;
        keyImageGold.enabled = false;
        HealthBar.SetMaxHealth(MaxHealth);
        deathScreen.gameObject.SetActive(false);
        mainTextBody.gameObject.SetActive(false);
        winScreen.gameObject.SetActive(false);
        mainTextSpecial.gameObject.SetActive(false);
        deathCount = 0;
    }

    void Update()
    {
        //Raycast setup
        RaycastHit hitInfo;
        Debug.DrawRay(spawnPoint.position, spawnPoint.forward * IntDist, Color.red);

        if (Physics.Raycast(spawnPoint.position, spawnPoint.forward, out hitInfo, IntDist))
        {
            // Check if raycast hit objects that are interactable
            if (hitInfo.collider.gameObject.CompareTag("Key"))
            {
                // If the raycast hit a key, set the currentKey and display interaction text
                canInteract = true;
                currentKey = hitInfo.collider.gameObject.GetComponent<KeyBehaviour>();
                mainTextBody.gameObject.SetActive(true);
                mainText.text = "Press E to pick up key";
            }
            else if (hitInfo.collider.gameObject.CompareTag("Door"))
            {
                // If the raycast hit a door, set the currentDoor and display interaction text
                canInteract = true;
                currentDoor = hitInfo.collider.gameObject.GetComponent<DoorBehaviour>();
                mainTextBody.gameObject.SetActive(true);
                mainText.text = "Press E to open door";
            }
            else if (hitInfo.collider.gameObject.CompareTag("Chest") && !chestOpen)
            {
                // If the raycast hit a chest, set the currentChest and display interaction text
                canInteract = true;
                currentChest = hitInfo.collider.gameObject.GetComponent<GiftBox>();
                mainTextBody.gameObject.SetActive(true);
                mainText.text = "Press E to open chest";
            }
            else
            {
                // If the raycast hit nothing interactable, reset interaction state
                canInteract = false;
                currentKey = null;
                currentDoor = null;
                currentChest = null;
                mainTextBody.gameObject.SetActive(false);
            }
        }
        else if (currentKey != null)
        {
            // If the raycast did not hit anything but we have a currentKey, reset interaction state
            currentKey = null;
            canInteract = false;
        }
        else if (SpecialText)
        {
            // If special text is active, keep it displayed and disable main text
            mainTextSpecial.gameObject.SetActive(true);
            mainText.gameObject.SetActive(false);
        }
    }

    //function to set the player's health
    // This function can be called to change the player's health by a specified amount
    // It will clamp the health to be between 0 and MaxHealth
    public void SetHealth(float healthChange)
    {
        // Change the player's health by the specified amount
        Health += healthChange;
        Debug.Log("Player health changed by: " + healthChange + ", current health: " + Health);
        Health = Mathf.Clamp(Health, 0, MaxHealth);
        HealthBar.SetHealth(Health);
    }

    // Function to respawn the player at the last checkpoint
    // This function will reset the player's position to the checkpoint and restore their health
    void Respawn()
    {
        // Respawn the player at the checkpoint with max health
        gameObject.transform.position = checkPoint.position;
        Physics.SyncTransforms();
        SetHealth(MaxHealth);
        HealthBar.SetHealth(Health);
        Time.timeScale = 1f; // Resume the game
        dead = false;
        healthText.text = "Health: " + Health.ToString() + "/" + MaxHealth.ToString();
        Debug.Log("Player revived at checkpoint");
        //add to death count
        deathCount++;
        // remove death screen
        deathScreen.gameObject.SetActive(false);
    }

    // Function to handle player taking damage
    // This function will reduce the player's health by a specified amount and play a damage sound
    // If the health reaches 0, it will trigger the death sequence
    public void TakeDamage(int damage)
    {
        SetHealth(-damage);
        playerAudioSource = GetComponent<AudioSource>();
        playerAudioSource.clip = damageSound;
        playerAudioSource.Play();
        Debug.Log("Player took damage, current health: " + Health);
        healthText.text = "Health: " + Health.ToString() + "/" + MaxHealth.ToString();
        if (Health <= 0)
        {
            dead = true;
            Debug.Log("Player is dead");
            Time.timeScale = 0f; // Pause the game
            deathScreen.gameObject.SetActive(true);
        }
    }

    // Function to handle player winning the game
    // This function will calculate the final score, display the win screen, and set the rank based on the score
    public void Win()
    {
        endScore = score - deathCount;//calculate end score
        Debug.Log("Player has won the game with score: " + endScore);
        winScreen.gameObject.SetActive(true);// Show the win screen
        // Update the UI texts for win screen
        coinsCollectedText.text = "Coins Collected: " + score.ToString() + "/16";
        deathCountText.text = "Deaths: " + deathCount.ToString();
        if (endScore >= 16)
        {
            endScoreText.text = "S";
            endScoreText.color = Color.yellow; // Change color for S rank
        }
        else if (endScore > 7)
        {
            endScoreText.text = "A";
            endScoreText.color = Color.green; // Change color for A rank
        }
        else if (endScore > 5)
        {
            endScoreText.text = "B";
            endScoreText.color = Color.blue; // Change color for B rank
        }
        else if (endScore > 0)
        {
            endScoreText.text = "C";
            endScoreText.color = Color.magenta; // Change color for C rank
        }
        else
        {
            endScoreText.text = "F";
            endScoreText.color = Color.red; // Change color for F rank
        }
        Time.timeScale = 0f; // Pause the game
        BGM.Stop();// Stop background music
    }

    // Function to add score when collecting coins
    // This function will increase the player's score by a specified amount and update the score UI
    public void AddScore(int amount)
    {
        score += amount;// Increase the score by the specified amount
        Debug.Log("Score: " + score);
        scoreText.text = "Coins: " + score.ToString() + "/16";// Update the score UI
        // Check for achievements after adding score
        AchivementCheck();
    }

    // Function to handle interactions with objects
    // This function will check if the player can interact upon entering player's trigger range with an object and perform the interaction
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            Debug.Log("Player is looking at " + other.gameObject.name);
            currentKey = other.gameObject.GetComponent<KeyBehaviour>();//retrieve key information
            keyName = currentKey.KeyName;// Get the key name
            canInteract = true; // Set canInteract to true
            currentKey = other.gameObject.GetComponent<KeyBehaviour>();
            canInteract = true;
        }
        else if (other.CompareTag("Door"))
        {
            canInteract = true;// Set canInteract to true
            currentDoor = other.GetComponent<DoorBehaviour>();// Retrieve door information
            doorName = currentDoor.doorName;// Get the door name
            canInteract = true;
            currentDoor = other.GetComponent<DoorBehaviour>();
        }
        else if (other.CompareTag("Checkpoint"))
        {
            checkPoint = other.transform;// Set the checkpoint
            Debug.Log("Checkpoint set to: " + checkPoint.position);
        }
        else if (other.CompareTag("Chest"))
        {
            canInteract = true; // Set canInteract to true
            currentChest = other.gameObject.GetComponent<GiftBox>(); // Retrieve chest information
        }
    }

    // Function to handle player interaction with objects
    void OnInteract()
    {
        // Check if the player can interact with an object
        if (canInteract)
        {
            if (currentKey != null)
            {
                Debug.Log("interacting with object");
                currentKey.collect(this); //Collect key
                if (currentKey.KeyName == "GoldKey") // Check if the key is gold
                {
                    goldKeyGet = true; // Set gold key flag
                    keyImageGold.enabled = true; // Enable gold key image
                }
                else // If the key is a regular key
                {
                    keyGet = true; // Set regular key flag
                    keyImage.enabled = true; // Enable regular key image
                }
                canInteract = false; // Reset interaction state
            }
            else if (currentDoor != null)
            {
                Debug.Log("Interacting with door");
                if (keyGet && currentDoor.doorName == "Door") // check if player has key and door is correct
                {
                    Debug.Log("The door is unlocked");
                    currentDoor.Interact();//open door
                }
                else if (goldKeyGet && currentDoor.doorName == "DoorSpecial") // check if player has gold key and door is special
                {
                    currentDoor.Interact();//open door
                }
                else // If player cannot open the door
                {
                    Debug.Log("The door is locked");
                    // Display door locked message
                    mainText.gameObject.SetActive(false);
                    mainTextBody.gameObject.SetActive(true);
                    mainTextSpecial.gameObject.SetActive(true);
                    mainTextSpecial.text = "The door is locked";
                    SpecialText = true;
                    currentDoor.LockedDoor(); // play SFX
                    Invoke("SpecialTextClose", 3f); // Close special text after 3 seconds
                    Debug.Log("You need a key to open this door!");
                }
                canInteract = false; // Reset interaction state
            }
            else if (currentChest != null) // If the player is interacting with a chest
            {
                if (goldKeyGet) // Check if player has the gold key
                {
                    Debug.Log("Interacting with gift box");
                    currentChest.Interact();// Open chest
                    canInteract = false;
                    chestOpen = true;
                }
                else //player does not have the gold key
                {
                    Debug.Log("It looks like the chest needs a different key to open");
                    // Display message that a special key is needed
                    canInteract = false;
                    mainText.gameObject.SetActive(false);
                    mainTextBody.gameObject.SetActive(true);
                    mainTextSpecial.gameObject.SetActive(true);
                    mainTextSpecial.text = "You need a special key to open this chest";
                    SpecialText = true;
                    Invoke("SpecialTextClose", 3f); // Close locked text after 3 seconds
                }
            }
            else // if there are no interactable objects in range
            {
                Debug.Log("No interactable object in range");
                canInteract = false;
            }
        }
        if (dead) // If the player is dead
        {
            Respawn();// Respawn the player
        }
    }

    // Function to close the special text after a delay
    // Special text is used for messages that require special attention, like locked doors or chests
    // This function will disable the special text and enable the main text
    void SpecialTextClose()
    {
        SpecialText = false;
        mainTextSpecial.gameObject.SetActive(false);
        mainText.gameObject.SetActive(true);
    }

    // Function to collect a key
    // Function will have different behavior based on the key type
    public void GetKey()
    {
        if (keyName == "GoldKey") // If the key is gold
        {
            goldKeyGet = true;
            keyImageGold.enabled = true;
        }
        else // If the key is a regular key
        {
            keyGet = true;
            Debug.Log("Key collected!");
            keyImage.enabled = true;
        }
    }

    // Function to check if player has collected all coins
    void AchivementCheck()
    {
        Debug.Log("Checking achievements with score: " + score);
        if (score >= 16)
        {
            // If player has collected all coins, display achievement message
            Debug.Log("Achievement unlocked: Coin Collector");
            mainTextBody.gameObject.SetActive(true);
            mainText.text = "You have collected all coins!";
            // Play achievement sound
            playerAudioSource = GetComponent<AudioSource>();
            playerAudioSource.clip = achievementSound;
            playerAudioSource.Play();
            // Close text after delay
            Invoke("CloseAchievement", 3f);
        }
    }

    // Function to close the achievement text
    void CloseAchievement()
    {
        mainTextBody.gameObject.SetActive(false);
    }

    // Function to handle player if they walk away from interactible objects
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Key")) // If the player exits the trigger of a key
        {
            // Reset the currentKey, interaction state and hide the interaction text
            currentKey = null;
            canInteract = false;
            mainTextBody.gameObject.SetActive(false);
        }
        else if (other.CompareTag("Door")) // If the player exits the trigger of a door
        {
            // Reset the currentDoor, interaction state and hide the interaction text
            currentDoor = null;
            canInteract = false;
            mainTextBody.gameObject.SetActive(false);
        }
        else if (other.CompareTag("Chest")) // If the player exits the trigger of a chest
        {
            // Reset the currentChest, interaction state and hide the interaction text
            currentChest = null;
            canInteract = false;
            mainTextBody.gameObject.SetActive(false);
        }
    }
}