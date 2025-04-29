using UnityEngine;

public class PlayerAimWeapon : MonoBehaviour
{
    [SerializeField] private Transform aimTransform;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 0.5f; // Time between shots
    
    private float lastFireTime;

    private void Update()
    {
        HandleAiming();
        HandleShooting();
    }

    private void HandleAiming()
    {
        Vector3 mousePosition = UtilityFunctions.GetMouseWorldPosition();
        Vector3 aimDirection = (mousePosition - transform.position).normalized;

        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);

        // Tambahkan ini untuk FLIP pistol
        Vector3 localScale = Vector3.one;
        if (angle > 90 || angle < -90)
        {
            localScale.y = -1f; // Balik ke bawah kalau cursor di kiri
        }
        else
        {
            localScale.y = +1f; // Normal kalau cursor di kanan
        }
        aimTransform.localScale = localScale;
    }

    private void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= lastFireTime + fireRate)
        {
            Shoot();
            lastFireTime = Time.time;
        }
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
