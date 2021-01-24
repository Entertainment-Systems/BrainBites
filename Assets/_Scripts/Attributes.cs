using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attributes : MonoBehaviour
{
    [Header("Physics")]
    public float speed;
    public float maxSpeed;
    public float jumpHeight;

    [Header("")]
    public float health;
    [Range(0, 20)]
    public float vision;

    [Header("")]
    public Color32 textColor;
    public string targetName;
    [TextArea]
    public string description;
    
}
