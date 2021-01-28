using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private TextMeshProUGUI infoTextbox;
    [SerializeField] private LayerMask groundLayer;
    private Transform groundCheck;
    [SerializeField] private LevelManager lm;
    [SerializeField] private GameObject haloLight;
    [SerializeField] private Transform lifeBarScalePoint;
    [SerializeField] private float healthLossRate;

    private Light2D light2d;
    private Rigidbody2D rb2D;
    private ParasiteCollider parasite;
    private Attributes attribute;
    private float health;
    private bool facingRight = true;
    private bool grounded = true;
    private bool isEnemy = false;

    private float TargetVision;

    void Start()
    {
        light2d = haloLight.GetComponent<Light2D>();
        GetPlayerBody();
        InvokeRepeating("takeDamage", 0.5f, attribute.DeathSpeed);
        
    }

    private void Update()
    {
        if(haloLight !=null && player !=null) 
            haloLight.transform.position = player.transform.position;
        if (parasite != null)
        {
            if( player.gameObject.GetComponent<ParasiteCollider>().CollidedToEnemy)
            {
                player.gameObject.GetComponent<ParasiteCollider>().CollidedToEnemy = false;
                player = player.gameObject.GetComponent<ParasiteCollider>().CollidedEnemy;
                
                if (player.gameObject.GetComponent<EnemyAI>() != null)
                {
                    Destroy(player.gameObject.GetComponent<EnemyAI>());
                    Destroy(player.gameObject.GetComponentInChildren<Light2D>());
                }
                    
                GetPlayerBody();
            }
        }

        if (Input.GetKeyDown(KeyCode.Z) && grounded)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, attribute.jumpSpeed);
            health -= 25;
        }

        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// For controls and pysics updates
    /// </summary>
    void FixedUpdate()
    {
        //Controls
        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), 0);
        if (rb2D != null)
        {
            rb2D.velocity = new Vector2(attribute.speed * direction.x, rb2D.velocity.y);

            light2d.pointLightOuterRadius = TargetVision;

            if (direction.x < 0)
                player.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            else if(direction.x > 0)
                player.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

            if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)) && Mathf.Abs(rb2D.velocity.x) > 0.1)
                health -= healthLossRate * Time.deltaTime;

            grounded = Physics2D.OverlapCircle(groundCheck.position, 0.08f, groundLayer);


            if (rb2D.velocity.y < 1)
                rb2D.gravityScale = 3;
            else
                rb2D.gravityScale = 1;

            lifeBarScalePoint.localScale = new Vector3(health / 10, 1, 1);

        if ((Input.GetKey(KeyCode.X) && player.tag == "Enemy") || health <= 0 && isEnemy)
            {
                ReturnToParasite();
            }
        }
    }

    /// <summary>
    /// refreshes the players attributes to the new target. ie. changing rb2D to new target rb2D
    /// </summary>
    private void GetPlayerBody()
    {
        if(player.name != "Parasite") { 
            isEnemy = true;
            health = 100;
        }
        else { isEnemy = false; }
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

        InvokeRepeating("takeDamage", 0.5f, attribute.DeathSpeed);

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
        lm.enemiesLeft--;
        if (lm.enemiesLeft == 0)
            lm.unlockExit();
    }

    private void UpdateText()
    {
        infoTextbox.SetText(
            "<#" + ColorUtility.ToHtmlStringRGB(attribute.textColor) + ">"
            + "<size=38><u><b><i>" + attribute.targetName + "</size=38></u></b></i></color>"
            + "<br>" +
            attribute.description);
    }

    void takeDamage()
    {
        if(TargetVision > .6f)
            TargetVision -= 0.1f;
    }

/*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(groundCheck.position, 0.08f);
    }
*/
}