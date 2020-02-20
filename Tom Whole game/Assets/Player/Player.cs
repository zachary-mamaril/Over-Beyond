using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int enemydamage= 100;
    public GameObject target;
    public Collision collision = new Collision();
    //Animator component
    public Animator animator;

    //Damage taken per jump// rule, dont jump too often, or you lose
    public int jumphurt=10;

    //Health Bar UI and player health
    public Slider slider;
    public int maxHealth = 100;
    public int currentHealth;
    //different colors at different points of health
    public Gradient gradient;
    public Image fill;

    //Camera variables>>>>>
    public GameObject player; //Store a reference to player    ///unity engine
    private Vector3 offset; //Store the offset distance between player and camera

    //Float Variables for movement>>>>>>>>
    float horizontalMove = 0f; //store float for horizontal movement
    public float speed = 5f; //set a public float <speed> to tweek in project
    public float fallmulti=2.5f; //set a public float <fallmulti> tp change the fall speed
    public float lowjumpmult = 2f;//Set a public float to change the lowjump speed
    Rigidbody2D rgbd;//variable to store the RigidBody2D
    [Range(1, 10)]//This will create a usable slide bar to update the jump velocity
    public float jumpVelocity;//Store the jump velocity

    public Text healthtext;

    //teleportation floats
    private float entervelocity, exitvelocity;
    private Rigidbody2D enterRigidbody;

    bool Isdead
    {
        get
        {
            return currentHealth <= 0;
        }
    }
    Camera cam;

    //end the game
    public float restartDelay = 1f;


    // Start is called before the first frame update
    void Start()
    {

        //player health
        currentHealth = maxHealth;
        SetMaxHealth(maxHealth);

        //here is the offset to change player position for the camera
        offset = transform.position - player.transform.position;

        //This is the variable to call the Rigidbody2D component
        rgbd = GetComponent<Rigidbody2D>();
        cam = GetComponent<Camera>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!Isdead)
        {

            //Jump and movement
            if (Input.GetButtonDown("Jump"))//if you press space, you jump
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpVelocity;//jumps the player
                TakeDamage(jumphurt);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();//we now create a jumping function
            }
            if (rgbd.velocity.y < 0)//this will prevent the player from floating!
            {
                rgbd.velocity += Vector2.up * Physics2D.gravity.y * (fallmulti - 1) * Time.deltaTime;//method to bring the player back down from a jump
            }

            if (rgbd.velocity.x < 10)
            {
                rgbd.velocity += Vector2.right * Physics2D.gravity.x * (fallmulti - 1) * Time.deltaTime;//tried to slow down character from moving too fast
                rgbd.velocity += Vector2.left * Physics2D.gravity.x * (fallmulti - 1) * Time.deltaTime;
            }
            horizontalMove = Input.GetAxisRaw("Horizontal") * speed;//moves character left and right

            Debug.Log(currentHealth);
        }
        else
        {
            Invoke("Restart", restartDelay);
        }

        healthtext.text = currentHealth.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.transform.tag == "enemy")
        {
            currentHealth -= enemydamage;
            SetHealth(currentHealth);
        }
        if (Isdead)
        {
            animator.SetTrigger("death");
        }
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        SetHealth(currentHealth);

        if (Isdead)
        {
            animator.SetTrigger("death");
        }
    }

    private void FixedUpdate()
    {
        if (!Isdead)
        {
            //Player will move using the "A" and "D" key to move left and right>>>>>>>>
            horizontalMove = Input.GetAxisRaw("Horizontal") * speed;// moves character left and right
            Vector2 movement = new Vector2(horizontalMove, 0);
            rgbd.AddForce(movement * speed);//adds force to the rigidbody of the player, showing movement
        }
    }
    private void LateUpdate()
    {
        transform.position = player.transform.position + offset;//allows the camera to follow player
    }

    public void SetHealth(int health)
    {
        slider.value = health;//changes the health of the player on the health bar
        fill.color = gradient.Evaluate(slider.normalizedValue);//changes the color of the healh bar, depending on health percentage
    }
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health; //sets max health to the maxhealth bar
        slider.value = health;
        fill.color = gradient.Evaluate(1f); //changes the color of the heatlh bar to its max health color
    }

    void Jump()
    {
        if (currentHealth > 0)
        {
            animator.SetTrigger("Jump");
        }
        //play a jump animation
    }


    void Restart ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
