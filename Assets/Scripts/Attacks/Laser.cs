using Cinemachine;
using UnityEngine;

public class Laser : Weapon
{
    [Header("Cameras")]
    [SerializeField] Camera mainCamera;
    [SerializeField] CinemachineFreeLook freeLookCamera;
    [SerializeField] Camera focusCamera;
    [SerializeField] Canvas targetCanvas;

    [Header("Variables")]
    [SerializeField] GameObject player;
    [SerializeField] float range;
    [SerializeField] AudioClip laserSound;

    bool isInFocusMode;
    bool canShoot;
    float timeLeftUntilUnlockShoot;

    PlayerAttacks attacks;
    SFXController sfx;

    public bool IsInFocusMode { get { return isInFocusMode; } }

    private void Start()
    {
        attacks = FindObjectOfType<PlayerAttacks>();
        sfx = FindObjectOfType<SFXController>();

        canShoot = true;
    }

    private void Update()
    {
        if (attacks.CurrentWeapon != (int)AttackTypes.laser) { SetFocusMode(false); return; }

        //Sets into focus-mode when right-click is being held down
        //(Focus mode is a first-person mode with a cross-hair)
        if (Input.GetMouseButtonDown(1))
        {
            SetFocusMode(true);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            SetFocusMode(false);
        }

        //Delay on shooting, can't do that immediatley
        if (!canShoot)
        {
            timeLeftUntilUnlockShoot += Time.deltaTime;

            if (timeLeftUntilUnlockShoot >= weaponHandler.reloadTime)
            {
                canShoot = true;
            }
        }
        else
        {
            timeLeftUntilUnlockShoot = 0;
        }
    }

    //Sets the player in or out of focus mode
    void SetFocusMode(bool focusMode)
    {
        mainCamera.enabled = !focusMode;
        freeLookCamera.enabled = !focusMode;

        focusCamera.enabled = focusMode;
        targetCanvas.enabled = focusMode;

        isInFocusMode = focusMode;
    }

    //Shoots the laser and sees if something gets hit
    public void ShootLaser()
    {
        if (!isInFocusMode || !canShoot) { return; }

        Ray laserRay = new Ray(focusCamera.transform.position, focusCamera.transform.forward);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(laserRay, out hit, range, hitLayers))
        {
            HandleEntityHit(hit);
        }

        canShoot = false;

        sfx.PlaySound(laserSound);
    }

    //Does whatever should be done if the laser sucessfully hit something and depening on what it hit it does different things
    void HandleEntityHit(RaycastHit hit)
    {
        if (hit.collider.gameObject.TryGetComponent(out TrainingDummy dummy))
        {
            dummy.GotHit();
        }
        else if (hit.collider.gameObject.TryGetComponent(out Ball ball))
        {
            ball.SpeedUp(focusCamera.transform.forward);
        }
        else if (hit.collider.gameObject.TryGetComponent(out EnemyHealth enemy))
        {
            enemy.Damage(weaponHandler.damage);
        }
        else if (hit.collider.gameObject.TryGetComponent(out EnemyButton enemyButton))
        {
            enemyButton.Hit();
        }
    }
}
