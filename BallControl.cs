using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    [SerializeField] private GameObject newBallLeft;
    [SerializeField] private GameObject newBallRight;
    private CircleCollider2D cc;
    
    public float force = 200f;
    private Rigidbody2D rigid2D;
    public ParticleSystem BallExplode;
    public ParticleSystem ExplodeParticle;
    private AudioSource audioSource;
    public float a = 1f;
    public bool onCollide=false;
    private Vector2 preHitVelocity;

    void Awake()
    {

        rigid2D = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        cc = GetComponent<CircleCollider2D>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        rigid2D.velocity = rigid2D.velocity.normalized * force;
        rigid2D.AddForce(new Vector2(a, 2f) * force);

    }

    // Update is called once per frame
    void Update()
    {
        preHitVelocity = rigid2D.velocity;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Laser")
        {
            
            // BallExplode.transform.position = transform.position;
            // ExplodeParticle.transform.position = transform.position;
            Instantiate(BallExplode, transform.position, transform.rotation);
            Instantiate(ExplodeParticle, transform.position, transform.rotation);
            GameManager.instance.IncrementScore();
            // cc.enabled = false;
            BallExplode.Play(true);
            ExplodeParticle.Play(true);
            audioSource.Play();
            Destroy(gameObject);
            if (!onCollide)
            {
                onCollide = true;
                Instantiate(newBallLeft, transform.position, transform.rotation);
                Instantiate(newBallRight, transform.position, transform.rotation);
                
            }
            
            // StartCoroutine("DestroyAndCreate");

        }
    }
    /* private void OnMouseDown()
     {
         Destroy(gameObject);

     }*/
    /* IEnumerator DestroyAndCreate()
     {
         Destroy(gameObject, 0.01f);
         yield return new WaitForSeconds(0.1f);
         Instantiate(newBallLeft, transform.position, transform.rotation);
         Instantiate(newBallRight, transform.position, transform.rotation);
     }*/
    void OnCollisionStay2D(Collision2D other)
    {
        Vector2 avgNormal = Vector2.zero;
        for (int i = 0; i < other.contactCount; i++)
        {
            avgNormal += other.GetContact(i).normal;
        }
        avgNormal /= other.contactCount;
        rigid2D.velocity = Vector2.Reflect(preHitVelocity, avgNormal);
    }
    
}
