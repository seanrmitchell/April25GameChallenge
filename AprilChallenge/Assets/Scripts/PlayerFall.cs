using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PlayerFall : MonoBehaviour
{
    [SerializeField]
    AudioSource death;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            death.Play();
            StartCoroutine(DeathWait());
        }
    }

    IEnumerator DeathWait()
    {
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
;