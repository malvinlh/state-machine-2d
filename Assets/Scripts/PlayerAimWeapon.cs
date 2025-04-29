using UnityEngine;

// Script ini menangani arah aim senjata berdasarkan mouse
// dan menembakkan peluru jika klik kiri ditekan
public class PlayerAimWeapon : MonoBehaviour
{
    [SerializeField] private Transform aimTransform;     // Objek yang akan diputar mengikuti mouse
    [SerializeField] private GameObject bulletPrefab;    // Prefab peluru
    [SerializeField] private Transform firePoint;        // Titik tempat peluru muncul
    [SerializeField] private float fireRate = 0.5f;      // Waktu minimal antar tembakan (cooldown)

    private float lastFireTime; // Waktu saat terakhir kali menembak

    private void Update()
    {
        HandleAiming();   // Putar aim ke arah mouse
        HandleShooting(); // Tembak jika mouse diklik
    }

    private void HandleAiming()
    {
        // Ambil posisi mouse di dunia (bukan di layar)
        Vector3 mousePosition = UtilityFunctions.GetMouseWorldPosition();

        // Hitung arah dari player ke mouse
        Vector3 aimDirection = (mousePosition - transform.position).normalized;

        // Konversi ke sudut derajat dan putar objek aim
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);

        // Flip sumbu Y kalau arah rotasi melewati kiri (supaya sprite tidak terbalik secara visual)
        Vector3 localScale = Vector3.one;
        if (angle > 90 || angle < -90)
        {
            localScale.y = -1f;
        }
        else
        {
            localScale.y = +1f;
        }
        aimTransform.localScale = localScale;
    }

    private void HandleShooting()
    {
        // Jika klik kiri dan sudah melewati cooldown
        if (Input.GetMouseButtonDown(0) && Time.time >= lastFireTime + fireRate)
        {
            Shoot();                     // Buat peluru
            lastFireTime = Time.time;   // Simpan waktu tembakan terakhir
        }
    }

    private void Shoot()
    {
        // Buat instance peluru di fire point
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
