using DamageNumbersPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ZombieAI : MonoBehaviour
{
    //public float chaseSpeed = 5f;
    public float attackRange = 1.5f;
    public float attackPrepareTime = 1f;

    private Transform player;
    private bool isPreparingAttack;
    private float prepareTimer;
    private bool isInAttackRange; // Flag to check if the zombie is in attack range

    public Image healthBarImage;

    public float totalHealth = 100f;

    public Slider healthBarSlider;

    [SerializeField]
    private GameObject zombieHitEffect;
    [SerializeField]
    private GameObject zombieDeathEffect;

    public GameObject zombieDisappearEffect;

    public DamageNumber xpDamage;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        isPreparingAttack = false;
        prepareTimer = 0f;
        isInAttackRange = false;

        //set the initial zombie health bar
        healthBarImage.fillAmount = totalHealth / 100f;

        healthBarSlider.maxValue = totalHealth;
        healthBarSlider.value = totalHealth;
    }

    void Update()
    {
        // Check if the player is in range
        if (Vector3.Distance(transform.position, player.position) < attackRange)
        {
            // If not preparing for an attack, start preparing
            if (!isPreparingAttack)
            {
                isPreparingAttack = true;
                prepareTimer = 0f;
                Debug.Log("IS PREPARING ATTACK");

                //stop the enemy when preparing for attack and play preparing for attack animation

            }

            // Increase the prepare timer
            prepareTimer += Time.deltaTime;

            // If the prepare timer reaches the desired time, attack
            if (prepareTimer >= attackPrepareTime)
            {
                Attack();
            }

            // Set the flag indicating that the zombie is in attack range
            isInAttackRange = true;
        }
        else
        {
            // If the player is outside the attack range, reset flags and timers
            isPreparingAttack = false;
            prepareTimer = 0f;
            isInAttackRange = false;

            this.GetComponent<Animator>().SetBool("Attack", false);
            // If the zombie is not in attack range, chase the player
            ChasePlayer();
        }

        UpdateHealth();
    }

    void ChasePlayer()
    {
        if (!isInAttackRange)
        {
            // Calculate direction towards the player
            Vector3 direction = (player.position - transform.position).normalized;

            // Rotate the zombie to look at the player's position
            transform.LookAt(player);

            // Move towards the player
            //transform.Translate(direction * chaseSpeed * Time.deltaTime, Space.World);
        }
    }

    void Attack()
    {
        if (CustomCharacterController.Instance.playerHealth <= 0) return;
        // Perform the attack here (e.g., reduce player health)
        Debug.Log("Zombie attacks!");

        // Reset prepare variables
        isPreparingAttack = false;
        prepareTimer = 0f;

        // Rotate the zombie to look at the player's position
        transform.LookAt(player);

        //Play zombie attack animation
        this.GetComponent<Animator>().SetBool("Attack", true);
    }

    public void DamageToPlayer()
    {
        if (CustomCharacterController.Instance.playerHealth <= 0) return;
        if (isInAttackRange)
        {
            CustomCharacterController.Instance.playerHitEffect.Play();

            Debug.Log("DAMAGE TO PLAYER");
            //subtract 10 health from player
            CustomCharacterController.Instance.playerHealth -= 10f;

            var targetFillAmount = CustomCharacterController.Instance.playerHealth / 100f;
            //Update the health bar image
            CustomCharacterController.Instance.playerHealthBarImage.DOFillAmount(targetFillAmount, 0.25f);

            if(CustomCharacterController.Instance.playerHealth <= 0)
            {
                CustomCharacterController.Instance.OnPlayerDeathEvent();
            }

            if (SFXManager.Instance)
            {
                SFXManager.Instance.OnVibrateEvent();
            }
        }
    }

    public void UpdateHealth()
    {
        healthBarSlider.value = totalHealth;
    }

    public void InstantiateZombieHitEffect()
    {
        var hitEffect = Instantiate(zombieHitEffect, this.gameObject.transform.position, Quaternion.identity);
        Destroy(hitEffect, 2f);
    }
}
