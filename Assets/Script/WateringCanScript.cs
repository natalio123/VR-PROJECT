using UnityEngine;

public class WateringCanScript : MonoBehaviour
{
    [Header("Referensi")]
    public Animator canAnimator;       // Drag Animator Kaleng ke sini
    public ParticleSystem waterParticle; // Drag objek WaterStream ke sini
    public Transform mainCamera;       // Drag Main Camera

    [Header("Setting")]
    public float jarakSiram = 3.0f;    // Jarak maksimal bisa menyiram
    public float jedaSiram = 1.0f;     // Berapa detik sekali skor bertambah?
    private float timerSiram = 0f;

    void Start()
    {
        if (mainCamera == null) mainCamera = Camera.main.transform;
        
        // Matikan air saat mulai
        var emission = waterParticle.emission;
        emission.enabled = false;
    }

    void Update()
    {
        // 1. Tembakkan Raycast dari tengah layar
        Ray ray = new Ray(mainCamera.position, mainCamera.forward);
        RaycastHit hit;

        // Cek apakah kita melihat sesuatu?
        if (Physics.Raycast(ray, out hit, jarakSiram))
        {
            // Cek apakah yang kita lihat punya script 'FlowerPlant'?
            FlowerPlant bunga = hit.collider.GetComponent<FlowerPlant>();
            
            // ATAU cek apakah kita melihat pot/collider anaknya
            if (bunga == null)
            {
                bunga = hit.collider.GetComponentInParent<FlowerPlant>();
            }

            if (bunga != null)
            {
                // -- KITA SEDANG MELIHAT BUNGA --
                MulaiMenyiram(bunga);
                return; // Keluar dari update biar gak jalanin 'BerhentiMenyiram'
            }
        }

        // Kalau raycast tidak kena bunga, hentikan semuanya
        BerhentiMenyiram();
    }

    void MulaiMenyiram(FlowerPlant bungaTarget)
    {
        // 1. Play Animasi Miring
        canAnimator.SetBool("isPouring", true);

        // 2. Nyalakan Partikel Air
        var emission = waterParticle.emission;
        emission.enabled = true;

        // 3. Logika Siram (Pakai Timer biar gak spam skor 60x per detik)
        timerSiram -= Time.deltaTime;
        if (timerSiram <= 0)
        {
            // Panggil fungsi Siram di script Bunga
            bungaTarget.Siram();
            
            // Reset timer (misal tiap 1 detik nambah level/skor)
            timerSiram = jedaSiram; 
        }
    }

    void BerhentiMenyiram()
    {
        // 1. Kembali Tegak
        canAnimator.SetBool("isPouring", false);

        // 2. Matikan Air
        var emission = waterParticle.emission;
        emission.enabled = false;

        // Reset timer biar pas lihat bunga lagi langsung nyiram instan
        timerSiram = 0f; 
    }
}