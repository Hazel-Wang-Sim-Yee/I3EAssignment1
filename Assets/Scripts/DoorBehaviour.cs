using UnityEngine;

/*
* Author:   Hazel Wang Sim Yee
* Date: 15/6/2025
* Description:  Handles the behaviour of doors in the game
*/

public class DoorBehaviour : MonoBehaviour
{
    // Set values for the door in the inspector
    [SerializeField]
    public float delayTime = 2f;

    float originalYRotation;

    [SerializeField]
    public string doorName;

    public AudioSource doorAudioSource;

    [SerializeField]
    AudioClip doorOpenSound;

    [SerializeField]
    AudioClip doorCloseSound;

    [SerializeField]
    AudioClip doorLockSound;

    void Start() //record doors' starting rotation
    {
        // Store the original Y rotation of the door
        originalYRotation = transform.eulerAngles.y;
    }

    public void Interact() // Function to open the door
    {
        Vector3 doorRotation = transform.eulerAngles;

        if (doorRotation.y == originalYRotation)
        {
            doorRotation.y += 90f;
            Invoke("CloseDoor", delayTime); // Close the door after a delay
        }
        transform.eulerAngles = doorRotation;
        //Play door open sound
        doorAudioSource.clip = doorOpenSound;
        doorAudioSource.Play();
    }

    void CloseDoor() // Function to close the door
    {
        Vector3 doorRotation = transform.eulerAngles;
        doorRotation.y -= 90f;// Closes door
        // Plays the door close sound   
        transform.eulerAngles = doorRotation;
        doorAudioSource.clip = doorCloseSound;
        doorAudioSource.Play();
    }

    public void LockedDoor() // Function for when door is locked
    {
        //Play's locked door sound
        doorAudioSource.clip = doorLockSound;
        doorAudioSource.Play();
        Debug.Log("Door is locked: " + doorName);
    }
}
