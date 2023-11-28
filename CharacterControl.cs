using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    
    //public bool isCreated;
    // [Range(0.1f, 1f)]
    // [SerializeField] private float fireRate = 0.5f;

    Rigidbody rigid;
    public static CharacterControl instance;
    public bool moveAble = true;
    [SerializeField]
    float maxPosition;
    [SerializeField]
    float moveSpeed;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        rigid = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        instance = this;
    }
    void Start()
    {
        //isCreated = false;
    }

    // Update is called once per frame
    void Update()
    {
        

        float InputX = Input.GetAxis("Horizontal");

       
    }
    void FixedUpdate()
    {
        if (moveAble)
        {
            Move();
        }
    }
    private void Move()
    {
        float InputX = Input.GetAxis("Horizontal");
        rigid.velocity = new Vector3(InputX * moveSpeed, rigid.velocity.y, rigid.velocity.z);


        // transform.position += Vector3.right * InputX * moveSpeed * Time.deltaTime;

        float xPos = Mathf.Clamp(transform.position.x, -maxPosition, maxPosition);

        transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
    }
    
    
}
