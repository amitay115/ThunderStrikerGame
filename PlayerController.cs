using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Bullet variables
    [SerializeField] private GameObject bulletPrefab;
    public ParticleSystem laserStartParticles;
    public ParticleSystem laserStartBeam;
    public ParticleSystem laserEndParticles;
    public ParticleSystem laserEndBeam;
    

    public Animator animator;
    [SerializeField] private Transform firingPoint;
    public bool isCreated;
    

    Rigidbody rb;
    public static PlayerController main;
    public bool canMove = true;
    [SerializeField]
    float maxPos;
    [SerializeField]
    float moveSpeed;
    public float particleSpeed = 0.005f;
    Vector2 checkPoint;
    public float InputX;
    public bool dead, gotHit;

    public AudioClip[] clips;
    AudioSource _source;

    
    void Awake()
    {
        main = this;
        rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        main = this;
    }
    void Start()
    {
        gotHit = false;
        dead = false;
        _source = GetComponent<AudioSource>();
        EndShot();
    }

    
    void Update()
    {
        if (!dead)
        {
            if (!gotHit)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow) && !isCreated)
                {
                    shoot();
                    StartCoroutine("ShootRay");
                    _source.PlayOneShot(clips[1]);

                    //animator.Play("Shoot");
                    isCreated = true;
                }
            }
        }

        // InputX = Input.GetAxis("Horizontal");
        // animator.SetFloat("Horizontal", InputX);

       
    }
    void FixedUpdate()
    {
        if (canMove)
        {
            Move();
            InputX = Input.GetAxis("Horizontal");
            animator.SetFloat("Horizontal", InputX);
            
        }
        if (!canMove)
        {
            rb.velocity = Vector3.zero;
            animator.SetFloat("Horizontal", 0f);
            
        }

    }
    private void Move()
    {
        float InputX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector3(InputX * moveSpeed, rb.velocity.y, rb.velocity.z);


        // transform.position += Vector3.right * InputX * moveSpeed * Time.deltaTime;

        float xPos = Mathf.Clamp(transform.position.x, -maxPos, maxPos);

        transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
    }
    private void shoot()
    {

        laserStartParticles.Play(true);
        laserStartBeam.Play(true);
        laserStartParticles.gameObject.transform.position = firingPoint.position + Vector3.down;
        laserStartBeam.gameObject.transform.position = firingPoint.position + Vector3.down;
        Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
        Instantiate(laserEndParticles, firingPoint.position , firingPoint.rotation);
        Instantiate(laserEndBeam, firingPoint.position , firingPoint.rotation);
    }
    public void EndShot()
    {
        
        isCreated = false;
        laserStartParticles.Stop(true);
        laserStartBeam.Stop(true);
        laserEndBeam.Stop(true);
        laserEndParticles.Stop(true);
        laserEndBeam.Play(false);
        laserEndParticles.Play(false);
    }
    public void Dead()
    {
        dead = true;
        canMove = false;
        InputX = 0f;
        animator.Play("Dead");
        _source.PlayOneShot(clips[0]);

    }
    IEnumerator ShootRay()
    {
        canMove = false;
        animator.Play("Shoot");
        yield return new WaitForSeconds(0.2f);
        canMove = true;
        
    }

}
