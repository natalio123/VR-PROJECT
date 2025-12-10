using UnityEngine;
using UnityEngine.UI;

public class VRGazeInteractor : MonoBehaviour
{
    [Header("Gaze Settings")]
    public float gazeDuration = 2f;      // pemain harus menatap selama 2 detik
    private float gazeTimer = 0f;

    public float rayDistance = 10f;      // jarak raycast

    [Header("Reticle UI")]
    public Image reticleFill;            // set Image type: Filled

    private GameObject currentTarget;


    void Update()
    {
        DoGazeInteraction();
    }


    void DoGazeInteraction()
    {
        // Membuat ray dari tengah kamera (mata pemain VR)
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // Debug agar kamu bisa lihat ray di Scene View
        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.yellow);

        // Jika raycast kena sesuatu
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            // Apakah objek tersebut bertag "Flower"?
            if (hit.collider.CompareTag("Flower"))
            {
                // Jika berpindah bunga → reset timer
                if (currentTarget != hit.collider.gameObject)
                {
                    ResetGaze();
                    currentTarget = hit.collider.gameObject;
                }

                // Tambah timer selama gaze aktif
                gazeTimer += Time.deltaTime;

                // Update progress reticle
                if (reticleFill != null)
                    reticleFill.fillAmount = gazeTimer / gazeDuration;

                // Jika timer penuh → siram
                if (gazeTimer >= gazeDuration)
                {
                    FlowerPlant flower = currentTarget.GetComponent<FlowerPlant>();
                    if (flower != null)
                        flower.Siram();  // Ini akan otomatis menambah skor ke GameManager

                    ResetGaze();  // Reset setelah menyiram
                }

                return; // pastikan tidak turun ke ResetGaze()
            }
        }

        // Jika tidak melihat bunga → reset gaze
        ResetGaze();
    }


    void ResetGaze()
    {
        gazeTimer = 0f;

        if (reticleFill != null)
            reticleFill.fillAmount = 0f;

        currentTarget = null;
    }
}