using UnityEngine;

/*
* Author: Hazel Wang Sim Yee
* Date: 15/6/2025
* Description: Script for when player wins the game.
*/

public class Win : MonoBehaviour
{
    private PlayerBehaviour player;

    void OnTriggerEnter(Collider collision) // Function to handle the player winning the game
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (player == null)
            {
                player = collision.gameObject.GetComponent<PlayerBehaviour>();
            }
            player.Win(); // Trigger win function in PlayerBehaviour
        }
    }
}
