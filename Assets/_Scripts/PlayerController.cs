using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Experimental.Rendering.Universal;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private TextMeshProUGUI infoTextbox;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private GameObject haloLight;

    private Light2D light2d;
    private Rigidbody2D rb2D;
    private ParasiteCollider parasite;
    private Attributes attribute;
    private bool facingRight = true;
    private bool grounded = true;

    private float TargetVision;

    void Start()
    {
        light2d = haloLight.GetComponent<Light2D>();
        InvokeRepeating("takeDamage", 0.5f, 0.3f);
        GetPlayerBody();
    }

    private void Update()
    {
        haloLight.transform.position = player.transform.position;
        if (parasite != null)
        {
            if( player.gameObject.GetComponent<ParasiteCollider>().CollidedToEnemy)
            {
                player.gameObject.GetComponent<ParasiteCollider>().CollidedToEnemy = false;
                player = player.gameObject.GetComponent<ParasiteCollider>().CollidedEnemy;
                if (player.gameObject.GetComponent<EnemyAI>() != null)
                    Destroy(player.gameObject.GetComponent<EnemyAI>());
                GetPlayerBody();
            }
        }
    }

    /// <summary>
    /// For controls and pysics updates
    /// </summary>
    void FixedUpdate()
    {
        //Controls
        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), 0);
        rb2D.velocity = new Vector2(attribute.speed * direction.x, rb2D.velocity.y);

        light2d.pointLightOuterRadius = TargetVision;

        if (direction.x < 0 && facingRight == true)
        {
            player.transform.Rotate(new Vector3(0, 180, 0));
            facingRight = false;
        }
        else if (direction.x > 0 && facingRight == false)
        {
            player.transform.Rotate(new Vector3(0, 180, 0));
            facingRight = true;
        }

        grounded = Physics2D.OverlapCircle(groundCheck.position, 0.08f, groundLayer);

        if (Input.GetKey(KeyCode.Z) && grounded)
            rb2D.velocity = new Vector2(rb2D.velocity.x, attribute.jumpSpeed);
        //rb2D.AddForce(new Vector2(0, attribute.jumpSpeed), ForceMode2D.Impulse);

        if (rb2D.velocity.y < 1)
            rb2D.gravityScale = 3;
        else
            rb2D.gravityScale = 1;


        if (Input.GetKey(KeyCode.X) && player.tag == "Enemy")
            {
                ReturnToParasite();
            }
    }

    /// <summary>
    /// refreshes the players attributes to the new target. ie. changing rb2D to new target rb2D
    /// </summary>
    private void GetPlayerBody()
    {
        groundCheck = player.transform.GetChild(0);
        rb2D = player.GetComponent<Rigidbody2D>();
        attribute = player.GetComponent<Attributes>();

        TargetVision = attribute.vision;

        facingRight = player.transform.rotation.eulerAngles.y == 180 ? false : true;

        if (player.gameObject.GetComponent<ParasiteCollider>() != null)
        {
            parasite = player.gameObject.GetComponent<ParasiteCollider>();
            parasite.returnMe();
        }
        else
        {
            parasite = null;
        }

        
        UpdateText();
    }

    /// <summary>
    /// returns the target back to the parasite
    /// </summary>
    private void ReturnToParasite()
    {
        //Debug.Log("we running");
        player = GameObject.FindGameObjectWithTag("Player");
        GetPlayerBody();
    }

    private void UpdateText()
    {
        infoTextbox.SetText(
            "<#" + ColorUtility.ToHtmlStringRGB(attribute.textColor) + ">"
            + "<size=48><u><b><i>" + attribute.targetName + "</size=48></u></b></i></color>"
            + "<br>" +
            attribute.description);
    }

    void takeDamage()
    {
        if(TargetVision > .01f)
            TargetVision -= 0.5f;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(groundCheck.position, 0.08f);
    }
}