using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int bulletDamage = 10;
    private Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        target = UtilityFunctions.GetMouseWorldPosition();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if(Vector2.Distance(transform.position, target) < 0.1f)
        {
            DestroyBullet();
        }
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
