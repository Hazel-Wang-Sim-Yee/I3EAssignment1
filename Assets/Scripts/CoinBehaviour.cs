using UnityEngine;

/*
* Author: Hazel Wang Sim Yee
* Date: 15/6/2025
* Description: This script handles the behavior of a coin in the game.
*/

public class CoinBehaviour : MonoBehaviour
{
    //Set coin value and sound in the inspector
    [SerializeField]
    int coinValue = 10;

    [SerializeField]
    AudioClip coinSound;

    // Enable function when player walks into the coin
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerBehaviour player = other.GetComponent<PlayerBehaviour>();
            if (player != null)
            {
                player.AddScore(coinValue);// Add the coin value to the player's score
                Debug.Log("Coin collected! Value: " + coinValue);
                AudioSource.PlayClipAtPoint(coinSound, transform.position);// Play the coin sound at the coin's position
                Destroy(gameObject);// Destroy the coin object after collection
            }
        }
    }

}
