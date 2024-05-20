using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Laser : Weapon
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject target;
    [SerializeField] GameObject cube;
    [SerializeField] float range;

    PlayerAttacks attacks;

    private void Start()
    {
        attacks = FindObjectOfType<PlayerAttacks>();
    }

    private void Update()
    {
        ChangeTargetPosition();
    }

    void ChangeTargetPosition()
    {
        if (attacks.CurrentWeapon != (int)AttackTypes.laser) { target.SetActive(false); return; }

        Ray laserRay = new Ray(player.transform.position, player.transform.forward);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(laserRay, out hit, range, hitLayers))
        {
            target.SetActive(true);
            target.transform.position = hit.point;
        }
        else
        {
            target.SetActive(false);
        }
    }

    public void ShootLaser()
    {
        Ray laserRay = new Ray(player.transform.position, player.transform.forward);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(laserRay, out hit, range, hitLayers))
        {
            GameObject newCube = Instantiate(cube);
            newCube.transform.position = hit.point;
        }
    }
}
