using UnityEngine;

public class SlashLine : MonoBehaviour
{
    Slash slash;

    private void Start()
    {
        slash = GetComponentInParent<Slash>();
    }

    //Makes sure the line itself is always facing the right direction inside its parent
    private void Update()
    {
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
    }

    //Does different things when hitting something depending on whatever it hit
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TrainingDummy"))
        {
            other.GetComponent<TrainingDummy>().GotHit();
            slash.ResetAttack();
            slash.HasDoneDamage = !slash.IsThrowing;
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyHealth>().Damage(slash.weaponHandler.damage);
            slash.ResetAttack();
            slash.HasDoneDamage = !slash.IsThrowing;
        }
    }
}
