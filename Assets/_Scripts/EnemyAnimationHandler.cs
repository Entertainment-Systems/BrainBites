using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationHandler : MonoBehaviour
{
    Animator m_Anim;
    Rigidbody2D m_RB;
    float m_Veloctity;

    public bool freeze = false;
    void Start()
    {
        m_Anim = GetComponent<Animator>();
        m_RB = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(freeze)
            m_RB.bodyType = RigidbodyType2D.Static;
        else
            m_RB.bodyType = RigidbodyType2D.Dynamic;

        m_Veloctity = Mathf.Abs(m_RB.velocity.x);
        m_Anim.SetFloat("velocity", m_Veloctity);
    }

    public IEnumerator takeover()
    {
        m_RB.bodyType = RigidbodyType2D.Static;
        m_Anim.SetBool("takeover", true);
        yield return new WaitForEndOfFrame();
        //m_RB.bodyType = RigidbodyType2D.Dynamic;
        m_Anim.SetBool("takeover", false);
    }

    public void die() => m_Anim.SetBool("die", true);
}
