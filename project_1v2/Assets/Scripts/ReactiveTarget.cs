using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour
{
    void Start()
    {
        // Remove after some time.
        StartCoroutine(Delete());
    }

    // Method called by the shooting script.
    public void ReactToHit()
    {
        // Add to points.
        PlayerCharacter.points++;
        // Debug.Log("Points: " + PlayerCharacter.points);

        // Destroy what's shot.
        Destroy(this.gameObject);
    }

    public IEnumerator Delete()
    {
        yield return new WaitForSeconds(12.0f);
        Destroy(this.gameObject);
    }
}
