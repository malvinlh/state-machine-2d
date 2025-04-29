using UnityEngine;

// Script ini membuat peluru bergerak menuju arah mouse saat ditembak
public class Bullet : MonoBehaviour
{
    public float speed = 20f;         // Kecepatan peluru
    public int bulletDamage = 10;     // Damage yang diberikan peluru
    private Vector3 target;           // Titik tujuan peluru (posisi mouse saat tembakan terjadi)

    void Start()
    {
        // Simpan posisi mouse saat peluru dibuat
        target = UtilityFunctions.GetMouseWorldPosition();
    }

    void Update()
    {
        // Gerakkan peluru menuju target secara konstan
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Jika peluru hampir sampai target, hancurkan
        if (Vector2.Distance(transform.position, target) < 0.1f)
        {
            DestroyBullet();
        }
    }

    private void DestroyBullet()
    {
        Destroy(gameObject); // Hancurkan objek peluru dari scene
    }
}
