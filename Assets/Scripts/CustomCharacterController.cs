using DG.Tweening;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class CustomCharacterController : MonoBehaviour
{
    [Header("Characters")]
    [SerializeField]
    private GameObject[] characters;

    [Space(10)]
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private ThirdPersonController _personController;
    [SerializeField]
    private string[] attackAnimations;
    public TrailRenderer swordRenderer;

    public bool isAttacking = false;

    public float playerHealth = 100;
    public Image playerHealthBarImage;

    public bool isPlayerDead = false;
    private static readonly string _deathAnim = "Death";

    public Vector3 playerInitPostion;

    public ParticleSystem playerHitEffect;

    [SerializeField] private AudioSource playerFootStepSound;

    [SerializeField] private float moveDistance = 0.35f;

    public static CustomCharacterController Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            return;
        }
        else
        {
            Destroy(this.gameObject);
        }


        
    }

    // Start is called before the first frame update
    void Start()
    {
        swordRenderer.emitting = false;
        _animator = GetComponent<Animator>();
        _personController = GetComponent<ThirdPersonController>();

        playerHealthBarImage = UIManager.Instance.healthBarFillImage;

        playerInitPostion = transform.position;

        var currentSelectedPlayer = PlayerPrefs.GetInt(PlayerPrefsHelper.SelectedPlayer, 0);
        characters[currentSelectedPlayer].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerDead) return;
        if (Level.Instance == null) return;
        if (Level.Instance.isLevelCompleted) return;

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.V))
        {
            Dodge();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }

#endif
    }

    public void Attack()
    {
        if (isPlayerDead) return;
        if (Level.Instance == null) return;
        if (Level.Instance.isLevelCompleted) return;


        swordRenderer.emitting = true;
        _personController.SprintSpeed = 2;
        isAttacking = true;
        var randomAttackAnim = Random.Range(0, attackAnimations.Length);
        _animator.Play(attackAnimations[randomAttackAnim]);

        if (SFXManager.Instance != null)
            SFXManager.Instance.PlaySwordSwingSound();

        // Move the player in the direction they are facing
        Vector3 moveDirection = transform.forward; // Get the forward direction of the player

        // Calculate the new position
        Vector3 newPosition = transform.position + moveDirection * moveDistance;

        // Move the player to the new position
        _personController.gameObject.transform.DOMove(new Vector3(newPosition.x, newPosition.y, newPosition.z), 0.25f);


    }

    public void Dodge()
    {
        if (isPlayerDead) return;
        if (Level.Instance == null) return;
        if (Level.Instance.isLevelCompleted) return;

        _animator.Play("Roll");
        ResetSprint();
    }


    //this is being called in both attack animations
    public void ToggleSpriteSpeedBack()
    {
        swordRenderer.emitting = false;
        isAttacking = false;
        ResetSprint();
    }

    private void ResetSprint()
    {
        _personController.SprintSpeed = 5.335f;
    }

    public void OnPlayerDeathEvent()
    {
        PlayDeathAnimation();
        Debug.Log("LEVEL FAILED");
        isPlayerDead = true;
        this.GetComponent<ThirdPersonController>().enabled = false;

        Level.Instance.OnLevelFailedEvent();
    }

    public void PlayDeathAnimation()
    {
        _animator.Play(_deathAnim);
    }

    public void OnPlayerReviveEvent()
    {
        isPlayerDead = false;
        Level.Instance.isLevelCompleted = false;
        Level.Instance.isLevelFailed = false;

        _animator.Rebind();

        this.gameObject.transform.position = playerInitPostion;

        //increase player's health back to 80
        playerHealth = 100f;

        //set the player health bar to 0.8 too
        playerHealthBarImage.DOFillAmount(0.8f, 0.5f);

        _personController.enabled = true;
    }

    public void PlayPlayerFootStepSound()
    {
        if (SFXManager.Instance)
        {
            if(SFXManager.Instance.isSoundEnabled)
                playerFootStepSound.Play();
        }
        else
        {
            playerFootStepSound.Play();
        }
    }
}
