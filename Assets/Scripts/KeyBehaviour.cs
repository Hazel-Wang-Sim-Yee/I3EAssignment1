using UnityEngine;

/*
* Author: Hazel Wang Sim Yee
* Date: 15/6/2025
* Description: Script to handle the behaviour of keys in the game.
*/

public class KeyBehaviour : MonoBehaviour
{
    // Set key values in the inspector
    [SerializeField]
    public string KeyName;

    [SerializeField]
    AudioClip keySound;

    // Function for collecting the key
    public void collect(PlayerBehaviour player)
    {
        if (player != null)
        {
            Debug.Log(KeyName);
            AudioSource.PlayClipAtPoint(keySound, transform.position); // play key collection sound at the key's position
            player.GetKey(); // Add the key to the player's inventory
            Destroy(gameObject); // Destroy the key object after collection
        }

    }
}
