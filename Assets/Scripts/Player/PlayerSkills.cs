using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    [Serializable]
    public struct SwordStates
    {
        public string anim;
        public float attackRange;
    }

    [Serializable]
    public struct States 
    {
        public GameObject disablePart;
        public GameObject enablePart;
        public bool enableShoot;
        public bool enableMina;
        public bool enableDash;
        public int damageDruzok;
        public int damagePirozok;
        public int damageStrel;
        public int damageSword;
        public float raduisAttack;
        public float speedAttack;
        public string animSword;
    }

    public States[] AllState;

    [SerializeField] private Animator swordAnimator;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject minaPrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private Transform anchorPoint;

    [Space(5)]
    [SerializeField] private float forceBullet;
    [SerializeField] private float forceDash;
    [SerializeField] private float attackPushDuration;
    [SerializeField] private float attackPushDistance;

    [Space(5)]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float shootCooldown;
    [SerializeField] private float minaCooldown;
    [SerializeField] private float dashCooldown;

    [SerializeField] private float attackDamage;
    [SerializeField] private float shootDamage;

    [Space(5)]
    [SerializeField] private SwordStates[] swordStates;
    public string AnimSword = "fullAttack";

    public SpriteRenderer attackDetector;

    [Space(5)]

    private Rigidbody2D rb;

    private bool canShoot = true;
    private bool canMina = true;
    private bool canDash = true;

    private float attackCd;
    private float shootCd;
    private float minaCd;
    private float dashCd;
    private int currentSwordState;
    private int currentStateParts;
    private MeleeDetector meleeDetector;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        meleeDetector = GetComponentInChildren<MeleeDetector>();
    }

    private void OnEnable()
    {
        BattleManager.OnChange += OnChangeParts;
    }

    private void OnDisable()
    {
        BattleManager.OnChange -= OnChangeParts;
    }

    private void Update()
    {
        Melee();
        Ranged();
        Dash();
        MinaSpawn();
    }

    private void OnChangeParts()
    {
        if(AllState.Length >= currentStateParts)
        {
            if(AllState[currentStateParts].enablePart)
            {
                AllState[currentStateParts].enablePart.SetActive(true);
            }

            if(AllState[currentStateParts].disablePart)
            {
                AllState[currentStateParts].disablePart.SetActive(false);
            }

            canDash = AllState[currentStateParts].enableDash;
            canMina = AllState[currentStateParts].enableMina;
            canShoot = AllState[currentStateParts].enableShoot;

            BattleManager.DruzokDamage += AllState[currentStateParts].damageDruzok;
            BattleManager.PirozokDamage += AllState[currentStateParts].damagePirozok;
            BattleManager.StrelDamage += AllState[currentStateParts].damageStrel;

            attackCooldown = AllState[currentStateParts].speedAttack;
            meleeDetector.raduisCast = AllState[currentStateParts].raduisAttack;
            attackDamage = AllState[currentStateParts].damageSword;
            AnimSword = AllState[currentStateParts].animSword;            
        }
        if(currentStateParts == 10)
        {
            
        }

        currentStateParts++;
    }

    private void Melee()
    {
        if(attackCd <= 0f)
        {
            if(Input.GetMouseButton(0))
            {
                attackDetector.flipY = !attackDetector.flipY;

                AudioManager.instance.PlayAttack();
                swordAnimator.SetTrigger(AnimSword);
                
                if(meleeDetector.EnemiesObjects.Length != 0)
                {
                    foreach (var enemy in meleeDetector.EnemiesObjects)
                    {
                        if(enemy.TryGetComponent(out IDamageble damagable))
                        {
                            damagable.TakeDamage(attackDamage);
                        }
                    }
                }            

                attackCd = attackCooldown;
            }
        }

        else
        {
            attackCd -= Time.deltaTime;
        }
    }
    
    private void Dash()
    {
        if(!canDash)  return;

        if(dashCd <= 0f)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(PushEnemy());

                dashCd = dashCooldown;
            }
        }    
        else
        {
            dashCd -= Time.deltaTime;
        }
    }
    
    public IEnumerator PushEnemy()
    {
        Vector2 originalPosition = transform.position;
        Vector2 pushDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        float elapsedTime = 0f;

        while (elapsedTime < attackPushDuration)
        {
            float t = elapsedTime / attackPushDuration;
            transform.position = Vector2.Lerp(originalPosition, originalPosition + pushDirection * attackPushDistance, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private void MinaSpawn()
    {
        if(!canMina)  return;
        
        if(minaCd <= 0f)
        {
            if(Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Mouse2))
            {
                Instantiate(minaPrefab, transform.position, Quaternion.identity, null);

                minaCd = minaCooldown;
            }
        }
        else
        {
            minaCd -= Time.deltaTime;
        }
    }

    private void Ranged()
    {
        if(!canShoot)  return;

        if(shootCd <= 0f)
        {
            if(Input.GetMouseButton(1))
            {
                GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation, null);

                bullet.GetComponent<Bullet>().Init(forceBullet, shootDamage);

                shootCd = shootCooldown;
            }
        }

        if(shootCd >= 0f)
        {
            shootCd -= Time.deltaTime;
        }
    }   
}
