using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashLine : MonoBehaviour
{
    Slash slash;

    private void Start()
    {
        slash = GetComponentInParent<Slash>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TrainingDummy"))
        {
            other.GetComponent<TrainingDummy>().GotHit();
            slash.ResetAttack();
        }
    }
}
