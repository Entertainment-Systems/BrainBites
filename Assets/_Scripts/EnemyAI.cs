using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    Rigidbody2D rb2D;
    public float viewRadius;
    public float viewAngle;
    public LayerMask playerMask;
    public LayerMask obstacleMask;
    public float  meshResolution;

    private enum Enemy { attack, wander, patrol, lookAround }

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
                    Debug.Log("I see you motherfucker");
                    Debug.DrawRay(transform.position, dirToTarget, Color.green);
                }
            }
        }
    }

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        FindVisibleTarget();


    }
}
