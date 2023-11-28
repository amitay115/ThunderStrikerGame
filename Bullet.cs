using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    

    [Range(1, 20)]
    [SerializeField] public float speed = 7f;
    [SerializeField] public float superSpeed = 20f;
    [Range(1, 10)]
    [SerializeField] private float lifeTime = 3f;
    private float timer;
    public bool fastStrikes;
    private Rigidbody2D rb;
    // public PlayerController player;
    // public ParticleSystem laserEndParticles;
    public static Bullet instance;
    void Awake()
    {
        instance = this;

    }
    private void OnEnable()
    {
        instance = this;
    }
    private void Start()
    {
        fastStrikes = false;
        rb = GetComponent<Rigidbody2D>();
        

        // Destroy(gameObject, lifeTime);

    }
    private void FixedUpdate()
    {
      /*  if (fastStrikes)
            rb.velocity = transform.up * superSpeed;
        else*/
            rb.velocity = transform.up * speed;
        
    }
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Ball")
        {
           

            Destroy(gameObject, 0.05f);
            PlayerController.main.EndShot();
            Particles.part.DestroyParticles();
            Beam.beam.DestroyBeam();
        }

        else if (collider.gameObject.tag == "Boundary")
        {

            StartCoroutine("DestroyRay");

            /*   timer = lifeTime*1000f;
               while (timer > 0f)
               {
                   timer -= Time.deltaTime;

               }
               if (timer <= 0)
               {
                   Destroy(gameObject);
                   PlayerController.main.EndShot();
               }*/


        }

    }
    public void AddFastStrikes()
    {
        print("fast strike");
        fastStrikes = true;
        StartCoroutine("FaststrikeTime");
    }
    IEnumerator DestroyRay()
    {

        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
        PlayerController.main.EndShot();
        Particles.part.DestroyParticles();
        Beam.beam.DestroyBeam();
    }
    IEnumerator FaststrikeTime()
    {

        yield return new WaitForSeconds(18f);
        fastStrikes = false;
    }
}
