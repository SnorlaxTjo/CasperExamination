using UnityEngine;

public class Slash : Weapon
{
    [Header("SlashAttack")]
    [SerializeField] float timeToDoAttack;
    [SerializeField] float distanceToDoAttack;
    [SerializeField] AudioClip slashSfx;

    [Header("ThrowAttack")]
    [SerializeField] float timeToThrow;
    [SerializeField] float distanceToThrow;
    [SerializeField] AudioClip throwSfx;

    [Space]
    [SerializeField] GameObject slashObject;
    [SerializeField] GameObject player;

    float timeAttacked;
    float timeThrowed;
    float timeReloaded;
    bool isAttacking;
    bool canAttack;
    bool hasDoneDamage;
    bool isThrowing;
    float knifeSidePos;

    SFXController sfx;

    public bool HasDoneDamage { get { return hasDoneDamage; } set { hasDoneDamage = value; } }
    public bool IsThrowing { get { return isThrowing; } }

    private void Start()
    {
        sfx = FindObjectOfType<SFXController>();
    }

    private void Update()
    {
        PerformAttack();
        ReloadAttack();       
    }

    //Starts the attack
    void PerformAttack()
    {
        //The regular slash attack, going from left to right
        if (isAttacking)
        {
            SetSlashCollider(true);

            knifeSidePos += (distanceToDoAttack / timeToDoAttack) * Time.deltaTime;
            transform.localPosition = new Vector3(knifeSidePos, transform.localPosition.y, transform.localPosition.z);
            timeAttacked += Time.deltaTime;

            if (timeAttacked >= timeToDoAttack)
            {
                timeReloaded = 0;
                isAttacking = false;
                canAttack = false;
            }
        }
        //If you slash again quickly after hitting something with the slash, the knife gets thrown away to a land far far away instead
        else if (isThrowing)
        {
            SetSlashCollider(true);
            transform.position += new Vector3(transform.forward.x * (distanceToThrow / timeToDoAttack * Time.deltaTime), 0, transform.forward.z * (distanceToThrow / timeToDoAttack * Time.deltaTime));
            timeThrowed += Time.deltaTime;
            hasDoneDamage = false;

            if (timeThrowed >= timeToThrow)
            {
                SetSlashCollider(false);
                timeReloaded = 0;
                isThrowing = false;
                canAttack = false;
            }
        }
        //Just sets the knife back to its original position if an attack is not happening
        else
        {
            SetSlashCollider(false);
            transform.parent = player.transform;
            transform.localPosition = new Vector3(-distanceToDoAttack / 2, 0, 1);
            transform.localEulerAngles = Vector3.zero;
            timeAttacked = 0;
            timeThrowed = 0;
            knifeSidePos = -distanceToDoAttack / 2;
        }
    }

    //Having to wait before slashing again
    void ReloadAttack()
    {
        if (!canAttack)
        {
            timeReloaded += Time.deltaTime;

            if (timeReloaded >= weaponHandler.reloadTime)
            {
                canAttack = true;
                hasDoneDamage = false;
            }
        }
        else if (!isAttacking || isThrowing)
        {
            timeReloaded = 0;
        }
    }

    //Starts the attack
    public void StartAttack()
    {
        if (canAttack)
        {
            isAttacking = true;
            sfx.PlaySound(slashSfx);
        }
        else if (hasDoneDamage)
        {
            isThrowing = true;
            transform.localPosition = new Vector3(0, 0, 1);
            transform.parent = null;
            sfx.PlaySound(throwSfx);
        }
    }

    //Makes the slashing object have a collider and mesh renderer active
    void SetSlashCollider(bool enabled)
    {
        slashObject.SetActive(enabled);
    }

    //Sets the attack to be fully done whenever it hits something
    public void ResetAttack()
    {
        if (isAttacking)
        {
            timeAttacked = timeToDoAttack + 1;
        }
        else if (isThrowing)
        {
            timeThrowed = timeToThrow + 1;
        }
    }
}
