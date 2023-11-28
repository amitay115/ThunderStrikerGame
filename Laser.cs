using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Camera cam;
    public LineRenderer lineRenderer;
    public Transform firePoint;


    public bool rayUp;
    public bool rayCreate;
    public bool hitBoundary;
    public bool hitBall;
    Vector2 endPos;
    Vector2 checkPoint;
    
    [SerializeField] private float speed = 5f;

    void Start()
    {
        DisableLaser();
        rayUp = false;
        rayCreate = false;
        hitBoundary = false;
        hitBall = false;
        lineRenderer.SetPosition(0, firePoint.position);
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.W)) && (rayCreate == false))
        {
            checkPoint = new Vector2(firePoint.position.x, firePoint.position.y);
            endPos = new Vector2(firePoint.position.x, firePoint.position.y);
            EnableLaser();
            rayUp = true;
            rayCreate = true;
            UpdateLaser();
        }
        if (rayUp == true)
        {
            endPos += Vector2.up * speed;
            lineRenderer.SetPosition(1, endPos);
        }
        Vector2 direction = endPos - checkPoint;
        RaycastHit2D hit = Physics2D.Raycast(checkPoint, direction.normalized, direction.magnitude);
        

        if (hit)
        {
            
            if(hit.collider.tag == "Boundary")
            {
                print("hitBoundary");
                rayUp = false;
                StartCoroutine("BoundaryDisable");
                hitBoundary = true;
                
            }
            if (hit.collider.tag == "Ball")
            {
                print("hitBall");
                rayUp = false;
                StartCoroutine("BallDisable");
                hitBall = true;
                
            }
        }
    }

    void EnableLaser()
    {
        lineRenderer.enabled = true;
    }

    void UpdateLaser()
    {
       // var mousePos = (Vector2)cam.ScreenToWorldPoint(Input.mousePosition);

        lineRenderer.SetPosition(0, firePoint.position);

        //lineRenderer.SetPosition(1, mousePos);
    }

    void DisableLaser()
    {
        lineRenderer.enabled = false;
    }
    IEnumerator BoundaryDisable()
    {

        yield return new WaitForSeconds(4f);
        lineRenderer.enabled = false;
        rayCreate = false;
    }
    IEnumerator BallDisable()
    {

        yield return new WaitForSeconds(0.1f);
        lineRenderer.enabled = false;
        rayCreate = false;
    }
}
