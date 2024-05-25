using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BallWeapon : Weapon
{
    [SerializeField] Ball ball;
    [SerializeField] GameObject player;
    [SerializeField] GameObject spawnPoint;
    [SerializeField] AudioClip shotSound;

    bool canShoot;
    float timeUntilShoot;

    SFXController sfx;

    private void Start()
    {
        sfx = FindObjectOfType<SFXController>();

        canShoot = true;
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
    public void ShootBallPlayer()
    {
        ShootBall(true, false, null);
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
}
