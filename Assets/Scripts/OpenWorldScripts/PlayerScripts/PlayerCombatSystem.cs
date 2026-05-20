using System;
using System.Collections;
using UnityEngine;

public class PlayerCombatSystem : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootPoint;

    public float shootForсe = 10f;
    
    private bool canAttack = true;

    private void Update()
    {
        if (canAttack)
        {
            Attack();
            canAttack = false;
        }
    }

    private void Attack()
    {
        

        GameObject newBullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody rb = newBullet.GetComponent<Rigidbody>();

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.linearVelocity = shootPoint.forward * shootForсe;

        Debug.Log("Attack");

        StartCoroutine(AttackCoolDown());

        Destroy(newBullet, 3f);
    }

    private IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(2f);

        canAttack = true;
    }
}
