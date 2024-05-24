using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Laser : Weapon
{
    [Header("Cameras")]
    [SerializeField] Camera mainCamera;
    [SerializeField] CinemachineFreeLook freeLookCamera;
    [SerializeField] Camera focusCamera;
    [SerializeField] Canvas targetCanvas;

    [Header("Variables")]
    [SerializeField] GameObject player;
    [SerializeField] GameObject cube;
    [SerializeField] float range;

    bool isInFocusMode;

    PlayerAttacks attacks;

    public bool IsInFocusMode { get { return isInFocusMode; } }

    private void Start()
    {
        attacks = FindObjectOfType<PlayerAttacks>();
    }

    private void Update()
    {
        if (attacks.CurrentWeapon != (int)AttackTypes.laser) { return; }

        if (Input.GetMouseButtonDown(1))
        {
            SetFocusMode(true);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            SetFocusMode(false);
        }
    }

    void SetFocusMode(bool focusMode)
    {
        mainCamera.enabled = !focusMode;
        freeLookCamera.enabled = !focusMode;

        focusCamera.enabled = focusMode;
        targetCanvas.enabled = focusMode;

        isInFocusMode = focusMode;
    }

    public void ShootLaser()
    {
        if (!isInFocusMode) { return; }

        Ray laserRay = new Ray(focusCamera.transform.position, focusCamera.transform.forward);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(laserRay, out hit, range, hitLayers))
        {
            HandleEntityHit(hit);
        }
    }

    void HandleEntityHit(RaycastHit hit)
    {
        if (hit.collider.gameObject.TryGetComponent(out TrainingDummy dummy))
        {
            dummy.GotHit();
        }
        else if (hit.collider.gameObject.TryGetComponent(out Ball ball))
        {
            ball.SpeedUp();
        }
    }
}
