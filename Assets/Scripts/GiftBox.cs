using UnityEngine;

/*
* Author: Hazel Wang Sim Yee
* Date: 15/6/2025
* Description: Script for chest that spawns coins after interaction
*/


public class GiftBox : MonoBehaviour
{
    // Set chest values in the inspector
    [SerializeField]
    GameObject gift;

    [SerializeField]
    Transform spawnPoint;

    [SerializeField]
    float amountToSpawn = 1f;

    [SerializeField]
    Transform ChestHinge;

    AudioSource chestAudioSource;
    public void Interact() // Function to open the chest and spawn coins
    {
        openChest();
        // Spawn coins around the spawn point
        while (amountToSpawn > 0)
        {
            float spawnPointX = Random.Range(spawnPoint.position.x - 0.7f, spawnPoint.position.x + 0.7f);
            float spawnPointY = spawnPoint.position.y;
            float spawnPointZ = Random.Range(spawnPoint.position.z - 0.3f, spawnPoint.position.z + 0.3f);
            spawnPoint.position = new Vector3(spawnPointX, spawnPointY, spawnPointZ);
            amountToSpawn--;
            GameObject newGift = Instantiate(gift, spawnPoint.position, spawnPoint.rotation);
        }
    }

    void openChest() // Function to open the chest
    {
        Debug.Log("Gift box opened!");
        ChestHinge.Rotate(new Vector3(0, 0, 90), Space.Self);
        // Play the chest opening sound
        chestAudioSource = GetComponent<AudioSource>();
        chestAudioSource.Play();
    }
}
