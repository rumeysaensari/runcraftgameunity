using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    public float moveSpeed = 2f; // Hareket hızı
    public float moveDuration = 2f; // Hareket süresi
    public float pauseDuration = 1f; // Durma süresi

    private float timer; // Zamanlayıcı
    private bool isMoving; // Hareket ediyor mu?
    private Vector3 randomDirection; // Rastgele yön

    void Start()
    {
        // İlk değerleri ayarla
        timer = 0f;
        isMoving = false;
    }

    void Update()
    {
        timer -= Time.deltaTime; // Timer'i azalt

        if (timer <= 0f)
        {
            if (isMoving)
            {
                // Hareketten durmaya geçiş
                isMoving = false;
                timer = pauseDuration; // Durma süresi kadar bekle
            }
            else
            {
                // Durmadan harekete geçiş
                isMoving = true;
                timer = moveDuration; // Hareket süresi kadar hareket et

                // Rastgele bir yön seç (X-Z düzleminde)
                float randomAngle = Random.Range(0f, 360f); // 0-360 derece arasında rastgele açı
                randomDirection = new Vector3(Mathf.Cos(randomAngle * Mathf.Deg2Rad), 0f, Mathf.Sin(randomAngle * Mathf.Deg2Rad)).normalized;
            }
        }

        if (isMoving)
        {
            // Düşmanı rastgele seçilen yöne doğru hareket ettir
            transform.position += randomDirection * moveSpeed * Time.deltaTime;
        }
    }
}
