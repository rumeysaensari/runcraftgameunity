using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player; // Ana karakterin transform'u
    public float speed = 0.5f; // Hareket hızı
    public float rotationSpeed = 10f; // Dönme hızı
    public int damageAmount = 1; // Vereceği hasar miktarı
    private float damageDelay = 1f; // Hasar verme aralığı
    private float lastDamageTime; // Son hasar verme zamanı
    private Animator animator; // Animator bileşeni

    void Start()
    {
        // Player'ı otomatik bul
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
        }

        // Animator bileşenini al
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component missing on the enemy!");
        }

        // Yürüme animasyonunu başlat
        if (animator != null)
        {
            animator.Play("Walk"); // "Walk" animasyonu adı Animator'da ayarlı olmalıdır.
        }
    }

    void Update()
    {
        if (player != null)
        {
            // Hedefe olan yönü hesapla
            Vector3 direction = (player.position - transform.position).normalized;

            // Düşmanın yönelmesini sağla
            Quaternion targetRotation = Quaternion.LookRotation(direction); // Hedef rotasyonu
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime); // Yumuşak döndürme

            // Objeyi hareket ettir
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && Time.time >= lastDamageTime + damageDelay)
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
                lastDamageTime = Time.time;
                Debug.Log("Player took damage! Current health: " + playerHealth.GetCurrentHealth());
            }
        }
    }
}
