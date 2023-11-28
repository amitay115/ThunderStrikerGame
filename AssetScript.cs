using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetScript : MonoBehaviour
{
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       // Destroy(gameObject);

        if (collision.gameObject.tag == "Player")
        {
            audioSource.Play();
            if (transform.gameObject.tag == "Life")
                GameManager.instance.IncrementLives();
            if (gameObject.tag == "Shield")
                GameManager.instance.IncrementShield();
            if (gameObject.tag == "Thunder")
                GameManager.instance.IncrementThunder();
            Destroy(gameObject, 0.05f);
            
        }

        else if (GetComponent<Collider>().gameObject.tag == "BoundaryDown")
        {

            StartCoroutine("DestroyAsset");
        }

    }
    IEnumerator DestroyAsset()
    {

        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
       
    }
}
