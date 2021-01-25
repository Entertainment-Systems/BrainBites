using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Rendering.Universal;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private GameObject visionLight;

    private Attributes attribute;
    private Rigidbody2D rb2D;
    private Light2D light2d;

    [System.NonSerialized]
    public float viewRadius;

    public float viewAngle;
    public LayerMask playerMask;
    public LayerMask obstacleMask;

    private enum EnemyType { blind, badLegs, spin, speen, noMovement}
    [SerializeField] private EnemyType enemyType;

    private void Awake()
    {
        attribute = GetComponent<Attributes>();
        rb2D = GetComponent<Rigidbody2D>();
        light2d = visionLight.GetComponent<Light2D>();

    }
    private void Start()
    {
        viewRadius = attribute.vision;
        light2d.pointLightOuterRadius = viewRadius;
        light2d.pointLightInnerAngle = viewAngle;
        light2d.pointLightOuterAngle = viewAngle*2;
        light2d.color = attribute.textColor;


        switch(enemyType)
        {
            case EnemyType.blind:
                StartCoroutine(blindMovement(attribute.repeatRate));
                break;
            case EnemyType.badLegs:
                StartCoroutine(BadLegsMovement(attribute.repeatRate));
                break;
            case EnemyType.spin:
                StartCoroutine(SpinMovement(attribute.repeatRate));
                break;
            case EnemyType.speen:
                StartCoroutine(SpeenMovement(attribute.repeatRate));
                break;
            default:
                break;
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees -= transform.eulerAngles.z;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), 0);
    }

    void FindVisibleTarget()
    {
        Collider2D targetsinViewRadius = Physics2D.OverlapCircle(transform.position, viewRadius, playerMask);

        if (targetsinViewRadius != null)
        {
            Transform target = targetsinViewRadius.transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;

            if (Vector2.Angle(transform.right, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics2D.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    Destroy(targetsinViewRadius);
                    killPlayer();

                }
            }
        }
    }
 
    void FixedUpdate()
    {
        FindVisibleTarget();

    }

    private IEnumerator blindMovement(float waitTime)
    {
        Vector2 direction = new Vector2(0, 0);
        while (true)
        {
            int random = Random.Range(0, 2);
            if(random == 0) 
            {
                direction = new Vector2(-1, 0);
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
            else
            {
                direction = new Vector2(1, 0);
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }

            rb2D.velocity = new Vector2(direction.x, rb2D.velocity.y);

            yield return new WaitForSeconds(waitTime);
        }
    }

    private IEnumerator BadLegsMovement(float waitTime)
    {
        while (true)
        {
            transform.Rotate(new Vector3(0, 180, 0));

            yield return new WaitForSeconds(waitTime);
        }
    }

    private IEnumerator SpinMovement(float spinSpeed)

    {
        while (true)
        {
            visionLight.transform.Rotate(new Vector3(0, 0, 10));
            yield return new WaitForSeconds(spinSpeed);
        }
    }

    private IEnumerator SpeenMovement(float spinSpeed)

    {
        while (true)
        {
            transform.Rotate(new Vector3(0, 0, 10));
            yield return new WaitForSeconds(spinSpeed);
        }
    }

    public void killPlayer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("I see you motherfucker");
    }
}
