using Unity.VisualScripting;
using UnityEditor.XR;
using UnityEngine;

public class BallWeapon : Weapon
{
    [SerializeField] Ball ball;
    [SerializeField] GameObject player;
    [SerializeField] GameObject spawnPoint;
    [SerializeField] AudioClip shotSound;
    [SerializeField] float multipleBallAngles;
    [SerializeField] GameObject[] rotationObjects;

    bool canShoot;
    float timeUntilShoot;

    SFXController sfx;

    private void Start()
    {
        sfx = FindObjectOfType<SFXController>();

        canShoot = true;
        if (rotationObjects.Length >= 2)
        {
            rotationObjects[0].transform.localEulerAngles -= new Vector3(0, multipleBallAngles, 0);
            rotationObjects[1].transform.localEulerAngles += new Vector3(0, multipleBallAngles, 0);
        }       
    }

    //Gives the delay when you shoot a ball so you can't shoot another one immediately
    private void Update()
    {
        if (!canShoot)
        {
            timeUntilShoot += Time.deltaTime;

            if (timeUntilShoot >= weaponHandler.reloadTime)
            {
                timeUntilShoot = 0;
                canShoot = true;
            }
        }
    }

    //A function that allows you to shoot the ball as player
    public void ShootBallPlayer(int amount)
    {
        switch (amount)
        {
            case 1:
                ShootBall(true, false, null);
                break;
            case 3:
                Shoot3Balls(true);
                break;
            default:
                break;
        }    
    }

    //Shoots the ball, and sets spawnpoint and direction depending on if it's the player or something else firing
    public void ShootBall(bool isPlayer, bool shouldGiveInstanceToShooter, ShootOnTimer toGiveInstanceTo)
    {
        if (isPlayer && canShoot)
        {
            Ball projectile = Instantiate(ball);
            projectile.Init(player.transform.position,
                player.transform.forward.normalized + player.transform.position);
            projectile.Damage = weaponHandler.damage;
            canShoot = false;

            sfx.PlaySound(shotSound);
        }
        else if (!isPlayer)
        {
            if (player == null) { player = FindObjectOfType<PlayerMovement>().gameObject; }

            Ball projectile = Instantiate(ball);
            projectile.Init(spawnPoint.transform.position, player.transform.position);
            projectile.Damage = weaponHandler.damage;

            if (shouldGiveInstanceToShooter)
            {
                toGiveInstanceTo.ShotBall = projectile;
            }

            sfx.PlaySound(shotSound);
        }
    }

    public void Shoot3Balls(bool isPlayer)
    {
        if (isPlayer && canShoot)
        {
            Ball projectileForward = Instantiate(ball);
            Ball projectileLeft = Instantiate(ball);
            Ball projectileRight = Instantiate(ball);

            Vector3 travelDirectionForward = player.transform.forward.normalized + player.transform.position;
            Vector3 travelDirectionLeft = rotationObjects[0].transform.forward.normalized + player.transform.position;
            Vector3 travelDirectionRight = rotationObjects[1].transform.forward.normalized + player.transform.position;

            projectileForward.Init(player.transform.position, travelDirectionForward);
            projectileLeft.Init(player.transform.position, travelDirectionLeft);
            projectileRight.Init(player.transform.position, travelDirectionRight);

            projectileForward.Damage = weaponHandler.damage;
            projectileLeft.Damage = weaponHandler.damage;
            projectileRight.Damage = weaponHandler.damage;

            canShoot = false;

            sfx.PlaySound(shotSound);
        }
        else if (!isPlayer)
        {
            if (player == null) { player = FindObjectOfType<PlayerMovement>().gameObject; }

            Ball projectileForward = Instantiate(ball);
            Ball projectileLeft = Instantiate(ball);
            Ball projectileRight = Instantiate(ball);  

            Vector3 direction = Vector3.RotateTowards(rotationObjects[2].transform.forward, spawnPoint.transform.position - player.transform.position, 360, 0);

            rotationObjects[2].transform.rotation = Quaternion.LookRotation(direction);
            rotationObjects[2].transform.localEulerAngles += new Vector3(0, 180, 0);
            rotationObjects[2].transform.localEulerAngles = new Vector3(0, rotationObjects[2].transform.localEulerAngles.y, 0);

            Vector3 travelDirectionLeft = rotationObjects[0].transform.forward.normalized + spawnPoint.transform.position;
            Vector3 travelDirectionRight = rotationObjects[1].transform.forward.normalized + spawnPoint.transform.position;

            projectileForward.Init(spawnPoint.transform.position, player.transform.position);
            projectileLeft.Init(spawnPoint.transform.position, travelDirectionLeft);
            projectileRight.Init(spawnPoint.transform.position, travelDirectionRight);

            projectileForward.Damage = weaponHandler.damage;
            projectileLeft.Damage = weaponHandler.damage;
            projectileRight.Damage = weaponHandler.damage;
        }
    }
}
