using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BallWeapon : Weapon
{
    [SerializeField] Ball ball;
    [SerializeField] GameObject player;

    public void ShootBall()
    {
        Ball projectile = Instantiate(ball);
        projectile.Init(player.transform.position,
            player.transform.forward.normalized + player.transform.position);
    }
}
