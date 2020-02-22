using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour//<Monobehaviour is the base class which every Unity script derives
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
    public Text healthtext;//Shows health number
    //teleportation floats
    private float entervelocity, exitvelocity;
    private Rigidbody2D enterRigidbody;
    bool playsong = false;
    bool Isdead
    {
        get
        {
            return currentHealth <= 0;//when health is less than or equal to zero, Isdead is true
        }
    }
    Camera cam;
    public GameObject CompleteLevelUI;
    public void Complete()
    {
        CompleteLevelUI.SetActive(true);
    }
    //end the game
    public float restartDelay = 1f;
    // Start Function {{1 Unity Engine}}
    void Start()//<Start> is called on the frame when the script is enabled before the Update methods are called
    {
        //player health
        currentHealth = maxHealth;
        SetMaxHealth(maxHealth);
        //here is the offset to change player position for the camera
        offset = transform.position - player.transform.position;
        //transform{{6 Unity Engine}} Inherited Members
        //This is the variable to call the Rigidbody2D component
        rgbd = GetComponent<Rigidbody2D>();
        cam = GetComponent<Camera>();
        animator = GetComponent<Animator>();
    }
    // Update Function {{2 Unity Engine}}
    void Update()//Called Every Frame if Monobehaviour is enabled.
    {
        if (!Isdead)
        {
            //Jump and movement
            if (Input.GetButtonDown("Jump"))//if you press space, you jump
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpVelocity;//jumps the player
                TakeDamage(jumphurt);
                FindObjectOfType<Audiomanager>().Play("jump");
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
                if (rgbd.velocity.x < 20)
                {
                    rgbd.velocity += Vector2.right * Physics2D.gravity.x * (fallmulti - 100) * Time.deltaTime;//tried to slow down character from moving too fast
                    rgbd.velocity += Vector2.left * Physics2D.gravity.x * (fallmulti - 100) * Time.deltaTime;
                }
            }
            if(playsong == true)
            {
                FindObjectOfType<Audiomanager>().Play("movement");
            }
        }
        else
        {
            Invoke("Restart", restartDelay);
        }
        FindObjectOfType<Audiomanager>().Play("theme");
        healthtext.text = currentHealth.ToString();
    }
    //   {{3 Unity Engine}}
    //<OnTriggerEnter2D> Send method body when another object enters a trigger collider 2D physics only
    private void OnTriggerEnter2D(Collider2D collision)//when touched by an enemy mob
    {
       if (collision.transform.tag == "enemy")
        {
            currentHealth = currentHealth - enemydamage;//take damage
            SetHealth(currentHealth);
        }
        if (Isdead)
        {//we can change the animation and audio if the player is killed by an enemy (later)
            animator.SetTrigger("death");//play death animation
            FindObjectOfType<Audiomanager>().Play("deathsound");//play death sound
        }
    }

    public void TakeDamage(int damage)//takes health away from player
    {
        currentHealth -= damage;//subtracts damage from health
        SetHealth(currentHealth);//updates currenthealth

        if (Isdead)//if player Isdead bool is true
        {
            animator.SetTrigger("death");//play death animation
            FindObjectOfType<Audiomanager>().Play("deathsound");//play death sound
            //FindObjectofType <static method{{{{}}}}
            //Returns the first active loaded object of Type type
        }
    }
    //Fixed Update Function {{4 Unity Engine}}
    private void FixedUpdate()//Frame-rate independent MonoBehaviour for physic calculations
    {
        if (!Isdead)
        {
            //Player will move using the "A" and "D" key to move left and right>>>>>>>>
            horizontalMove = Input.GetAxisRaw("Horizontal") * speed;// moves character left and right
            Vector2 movement = new Vector2(horizontalMove, 0);
            rgbd.AddForce(movement * speed);//adds force to the rigidbody of the player, showing movement
        }
    }
    //{{5 Unity Engine}} Late Update Fucntion
    private void LateUpdate()//Called every frame if the Behaviour is enabled
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
        //gradient.Evaluate Engine.UI
    }
    void Jump()
    {
        if (currentHealth >= 0)//will prevent a jump animation when player dies
        {
            animator.SetTrigger("Jump");
            //SetTrigger From Animator
            //Engine.UI
        }
        //play a jump animation
    }
    void Restart ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);//Resets the current level on death
    }
    void Isplaying()
    {
        if (Input.GetButtonDown("Horizontal"))
        {
            playsong = true;
        }
        else
        {
            playsong = false;
        }
    }
}
