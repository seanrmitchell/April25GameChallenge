using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakables : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D hit)
    {
        if (hit.gameObject.layer == 7)
        {
            Debug.Log("Hit!");
            StartCoroutine(WaitingBreak());
        }
    }

    IEnumerator WaitingBreak()
    {
        Debug.Log("Breaking...");
        yield return new WaitForSeconds(1);

        Debug.Log("Broken");
        gameObject.SetActive(false);
    }
}
