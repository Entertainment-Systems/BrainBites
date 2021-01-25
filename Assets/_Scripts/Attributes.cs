using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attributes : MonoBehaviour
{
    [Header("Physics")]
    public float speed;
    public float maxSpeed;
    public float jumpSpeed;

    [Range(0, 10)]
    public float vision;
    [Range(0, 3)]
    public float repeatRate = 2f;

    [Header("How Quickly Death approaches")]
    [Range(0, 1)]
    public float DeathSpeed =.1f;

    [Header("Characteristics")]
    public Color32 textColor;
    public string targetName;
    [TextArea]
    public string description;
    
}
