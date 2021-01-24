using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(EnemyAI)) ]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        EnemyAI enemyAI = (EnemyAI)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(enemyAI.transform.position, Vector3.forward, Vector2.right, 360, enemyAI.viewRadius);
        Vector3 viewAngleA = enemyAI.DirFromAngle(-enemyAI.viewAngle / 2, false);
        Vector3 viewAngleB = enemyAI.DirFromAngle(enemyAI.viewAngle / 2, false);

        Handles.DrawLine(enemyAI.transform.position, enemyAI.transform.position + viewAngleA * enemyAI.viewRadius);
        Handles.DrawLine(enemyAI.transform.position, enemyAI.transform.position + viewAngleB * enemyAI.viewRadius);
    }
}
