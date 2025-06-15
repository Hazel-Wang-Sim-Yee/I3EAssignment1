using UnityEngine;

/*
* Author: Hazel Wang Sim Yee
* Date: 15/6/2025
* Description: This script is to be applied to objects that deal damage to the player
*/

public class Damage : MonoBehaviour
{
    // Set the amount of damage in the inspector
    [SerializeField]
    int damage = 40;
    private PlayerBehaviour player;

    void OnTriggerEnter(Collider collision)
    {
        //Apply damage to the player when they collide with this object
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player collided with damage object, applying damage.");
            if (player == null)
            {
                player = collision.gameObject.GetComponent<PlayerBehaviour>();
            }
            player.TakeDamage(damage);
        }
    }
}