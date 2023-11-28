using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{
    [Range(1, 10)]
    [SerializeField] public float speed = 5f;
    [SerializeField] public float superSpeed = 20f;
    [Range(1, 10)]
    [SerializeField] private float lifeTime = 3f;
    public bool fastStrikes=false;
    public static Particles part;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Awake()
    {
        part = this;
        
    }
    private void OnEnable()
    {
        part = this;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //transform.position = Vector3.zero;

    }

    // Update is called once per frame
    void Update()
    {

       /* if (fastStrikes)
            rb.velocity = transform.up * superSpeed;
        else*/
            rb.velocity = transform.up * speed;
    }
    private void FixedUpdate()
    {
       // rb.velocity = transform.up * speed;


    }
    public void DestroyParticles()
    {
        Destroy(gameObject);
    }
    public void AddFastStrikesParticles()
    {
        print("fast strike particles");
        fastStrikes = true;
        StartCoroutine("FaststrikeTime");
    }
    IEnumerator FaststrikeTime()
    {

        yield return new WaitForSeconds(18f);
        fastStrikes = false;
    }
}
