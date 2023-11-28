using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastBallControl : MonoBehaviour
{
    
    private CircleCollider2D cc;

    public float force = 250f;
    private Rigidbody2D rigid2D;
    public ParticleSystem BallExplode;
    public ParticleSystem ExplodeParticle;
    private AudioSource audioSource;
    public float a = 1f;
    public bool onCollide = false;
    

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
       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Laser")
        {
            BallExplode.transform.position = transform.position;
            ExplodeParticle.transform.position = transform.position;

            // cc.enabled = false;
            BallExplode.Play();
            ExplodeParticle.Play();
            audioSource.Play();
            Destroy(gameObject);
            

            // StartCoroutine("DestroyAndCreate");

        }
    }
}
