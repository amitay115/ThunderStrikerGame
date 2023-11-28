using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitPlayer : MonoBehaviour
{
    public BoxCollider2D col;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            GameManager.instance.DecreaseLives();
            //PlayerController.main.Dead();
            StartCoroutine("GotHit");
        }

    }
    IEnumerator GotHit()
    {
        col.isTrigger = true;
        PlayerController.main.gotHit = true;
        yield return new WaitForSeconds(2f);
        col.isTrigger = false;
        PlayerController.main.gotHit = false;

    }
}
