using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls spawns.

public class SceneController : MonoBehaviour
{
    // Serialized variable for linking to the prefab object.
    [SerializeField] GameObject eggPrefab, beePrefab;
    private GameObject egg, bee;
    private int eggCount;

    public int beeRarity = 5; // Larger number is rarer.
    private int xPos, yPos, zPos, chance;

    void Update()
    {
        // Spawns more targets when there are no more eggs.
        eggCount = GameObject.FindGameObjectsWithTag("Egg").Length;
        if (eggCount <= 0)
        {
            StartCoroutine(Spawn(10));
        }
    }

    private IEnumerator Spawn(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            // Random spawn position.
            xPos = Random.Range(-12, 12);
            yPos = Random.Range(5, 12);
            zPos = Random.Range(-12, 12);

            // Rolling a 0 will spawn a bee.
            chance = Random.Range(0, beeRarity);
            // Debug.Log("Bee roll: " + chance);

            if (chance < 1)
            {
                // Spawns bee.
                bee = Instantiate(beePrefab) as GameObject;
                bee.transform.position = new Vector3(xPos, yPos, zPos);
            }
            else
            {
                // Spawns egg.
                egg = Instantiate(eggPrefab) as GameObject;
                egg.transform.position = new Vector3(xPos, yPos, zPos);
            }

            // Wait a few seconds to spawn the next instance.
            yield return new WaitForSeconds(Random.Range(1, 3));
        }
    }
}
