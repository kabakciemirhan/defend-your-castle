using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TurretManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    //kurşun
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;

    [Header("Attribute")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float rotationSpeed = 150f;
    [SerializeField] private float bps = 1f; //bullets per second

    private Transform target;
    private float timeUntilFire;

    private void Update()
    {
        if (target == null)
        {
            FindTarget();
            return;
        }

        RotateTowardsTarget(); //düşmana doğru yönlendirme

        if(CheckTargetIsInRange())
        {
            target = null;
        }
        else
        {
            timeUntilFire += Time.deltaTime;
            if(timeUntilFire >= 1f / bps) //target targetingrange alanının içine girdiyse ateşle ve ateş etmenin üzerinden geçen zamanı sıfırla.
            {
                Shoot();
                timeUntilFire = 0f;
            }
        }
    }

    private void Shoot()
    {
        Debug.Log("shoot");
        GameObject bullet = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity); //bullet spawn
        BulletManager bulletScript = bullet.GetComponent<BulletManager>();
        bulletScript.SetTarget(target); //burada da hedefi belirtiyoruz
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);
        if(hits.Length > 0)
        {
            target = hits[0].transform; 
        }
    }

    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg + 180f;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime); //ucu döndürmenin akıcılığını sağlıyoruz
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan; //targeting range çember rengi
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
}
