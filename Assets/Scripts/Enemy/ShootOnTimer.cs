using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShootOnTimer : MonoBehaviour
{
    [SerializeField] float startTime;
    [SerializeField] float timeBetweenShots;
    [SerializeField] UnityEvent whatToDoOnTimerZero;
    [SerializeField] LayerMask laserHitLayers;

    float time;

    BallWeapon ballWeapon;
    Ball shotBall;
    PlayerMovement playerMovement;

    public Ball ShotBall { set { shotBall = value; } }

    private void Start()
    {
        ballWeapon = GetComponent<BallWeapon>();
        playerMovement = FindObjectOfType<PlayerMovement>();

        time = startTime;
    }

    //Just makes the enemy do its attack every once in a while
    private void Update()
    {
        time += Time.deltaTime;

        if (time >= timeBetweenShots)
        {
            whatToDoOnTimerZero?.Invoke();
            time -= timeBetweenShots;
        }
    }

    //Makes the enemy shoot a ball with nothing else
    public void ShootBall()
    {
        ballWeapon.ShootBall(false, false, null);
    }


    //Combines a ball and laser to speed up ball
    public void ShootBallAndLaser()
    {
        StartCoroutine(BallAndLaserRoutine());
    }

    //Does the laser shooting after 1 second
    IEnumerator BallAndLaserRoutine()
    {
        ballWeapon.ShootBall(false, true, this);

        yield return new WaitForSeconds(1);

        ShootLaser();
    }

    //Shoots the laser at the ball to speed it up and aim it at the player
    void ShootLaser()
    {
        if (shotBall == null) { return; }

        Ray laserRay = new Ray(transform.position, shotBall.gameObject.transform.position - transform.position);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(laserRay, out hit, 100, laserHitLayers))
        {
            HandleEntityHit(hit, playerMovement.gameObject.transform.position - transform.position);
        }
    }

    void HandleEntityHit(RaycastHit hit, Vector3 direction)
    {
        if (hit.collider.gameObject.TryGetComponent(out Ball ball))
        {
            ball.SpeedUp(direction);
        }
    }
}
