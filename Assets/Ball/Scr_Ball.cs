using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Ball : MonoBehaviour
{
    //Shoot
    private Vector2 FirstPos = Vector2.zero;
    private Vector2 SecondPos = Vector2.zero;
    public float Force = 75f;
    public Scr_LaunchLine LaunchLine;

    public float MaxDistance = 10;
    public float DefaultGravity;

    private float DirectionAngle;

    private GameObject Spawn;

    public LayerMask Ground;

    private void Awake()
    {
        DefaultGravity = GetComponent<Rigidbody2D>().gravityScale;
        Spawn = GameObject.FindGameObjectWithTag("Spawn");
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Device Mouse
        if (Input.touchCount > 0)
        {
            //Si no toca parte del menú (Raycast + layers)
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                FirstPos = Camera.main.ScreenToWorldPoint(touch.position);
                SecondPos = Camera.main.ScreenToWorldPoint(touch.position) - new Vector3(0.1f, 0);
            }

            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                if ((Vector2)Camera.main.ScreenToWorldPoint(touch.position) != FirstPos)
                {
                    SecondPos = Camera.main.ScreenToWorldPoint(touch.position);
                }

                
                DirectionAngle = Mathf.Atan2(SecondPos.y - FirstPos.y, SecondPos.x - FirstPos.x) * 180 / Mathf.PI;

                LaunchLine.velocity = ((Force / GetComponent<Rigidbody2D>().mass) * CalculateThrowForce());
                LaunchLine.angle = DirectionAngle;
                LaunchLine.g = Mathf.Abs(Physics2D.gravity.y * DefaultGravity);
                LaunchLine.RenderArc();
                LaunchLine.active = true;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                //Lanzar bola
                GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                Vector3 dir = Quaternion.AngleAxis(DirectionAngle, Vector3.forward) * Vector3.right;
                GetComponent<Rigidbody2D>().AddForce(dir * Force * CalculateThrowForce(), ForceMode2D.Impulse);
                LaunchLine.active = false;
            }
        }


        //Computer Mouse
        if (Input.GetMouseButtonDown(0))
        {
            FirstPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            SecondPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - new Vector3(0.2f, 0);
        }

        if (Input.GetMouseButton(0))
        {
            if ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) != FirstPos)
            {
                SecondPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

            //Angle
            DirectionAngle = Mathf.Atan2(SecondPos.y - FirstPos.y, SecondPos.x - FirstPos.x) * 180 / Mathf.PI;

            LaunchLine.velocity = ((Force / GetComponent<Rigidbody2D>().mass) * CalculateThrowForce());
            LaunchLine.angle = DirectionAngle;
            LaunchLine.g = Mathf.Abs(Physics2D.gravity.y * DefaultGravity);
            LaunchLine.RenderArc();
            LaunchLine.active = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            //Lanzar bola
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            Vector3 dir = Quaternion.AngleAxis(DirectionAngle, Vector3.forward) * Vector3.right;
            GetComponent<Rigidbody2D>().AddForce(dir * Force * CalculateThrowForce(), ForceMode2D.Impulse);
            LaunchLine.active = false;
        }


        //Check if outside the room
        if (transform.position.y < -25)
        {
            Respawn();
        }

        //Stop gravity bouncing bug
        if (Physics2D.OverlapCircle(transform.position - new Vector3(0, 1), 0.1f, Ground))
        {
            Debug.Log("Ground motherfucker");
            if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) < 0.1f && Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y) < 0.6f)
            {
                if (GetComponent<Rigidbody2D>().velocity.y > 0)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                }
                GetComponent<Rigidbody2D>().gravityScale = 0;
                GetComponent<Rigidbody2D>().freezeRotation = true;
            }
            else
            {
                GetComponent<Rigidbody2D>().freezeRotation = false;
                GetComponent<Rigidbody2D>().gravityScale = 13;
            }

        }
        else
        {
            GetComponent<Rigidbody2D>().freezeRotation = false;
            GetComponent<Rigidbody2D>().gravityScale = 13;
        }

    }

    private float CalculateThrowForce()
    {
        if (FirstPos != SecondPos)
        {
            float Percentage = Mathf.Clamp((Vector2.Distance(FirstPos, SecondPos) / MaxDistance), 0, 1);
            return Percentage;
        }
        else
        {
            return 0;
        }
    }

    private void Respawn()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.position = Spawn.transform.position;
        GetComponent<TrailRenderer>().Clear();
        GetComponent<ParticleSystem>().Emit(15);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(FirstPos, 1);
        Gizmos.DrawWireSphere(SecondPos, 1);
        //Gizmos.DrawWireSphere(transform.position - new Vector3(0, 1f), 0.1f);
    }
}
