using UnityEngine;

public class FlowerPlant : MonoBehaviour
{
    [Header("Daftar Model Bunga")]
    // Urutan: [0]Layu, [1]Tumbuh, [2]Kuncup, [3]Mekar
    public GameObject[] flowerStages; 
    
    [Header("Pengaturan Waktu")]
    public float durasiLayu = 10.0f; 
    private float timerMenujuLayu;

    private int currentStageIndex = 0;

    void Start()
    {
        timerMenujuLayu = durasiLayu;
        UpdateFlowerVisuals();
    }

    void Update()
    {
        // --- SIKLUS ABADI ---
        // Bunga akan SELALU menghitung mundur untuk layu, 
        // BAHKAN SAAT SUDAH MEKAR (Stage terakhir).
        // Ini supaya pemain bisa menyiramnya lagi nanti untuk dapat poin lagi.
        if (currentStageIndex > 0) 
        {
            timerMenujuLayu -= Time.deltaTime;

            if (timerMenujuLayu <= 0)
            {
                MenjadiLayu(); 
            }
        }
        
        // Fitur Test Spasi
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Siram();
        }
    }

    public void Siram()
    {
        // Cek apakah bunga belum mekar maksimal?
        if (currentStageIndex < flowerStages.Length - 1)
        {
            // 1. Naikkan level
            currentStageIndex++; 
            
            // 2. Reset Timer
            timerMenujuLayu = durasiLayu;

            // 3. Update Visual
            UpdateFlowerVisuals();

            // 4. --- TAMBAH SKOR +3 SETIAP FASE ---
            GameManager gm = FindFirstObjectByType<GameManager>();
            if (gm != null)
            {
                // Kita kirim angka 3 ke GameManager
                gm.TambahSkor(3); 
                Debug.Log("Bunga Tumbuh! +3 Poin");
            }
        }
    }

    void MenjadiLayu()
    {
        if (currentStageIndex > 0)
        {
            currentStageIndex--; // Turun satu level
            timerMenujuLayu = durasiLayu; // Reset timer
            UpdateFlowerVisuals();
            Debug.Log("Bunga layu turun satu level.");
        }
    }

    void UpdateFlowerVisuals()
    {
        for (int i = 0; i < flowerStages.Length; i++)
        {
            if (i == currentStageIndex)
                flowerStages[i].SetActive(true);
            else
                flowerStages[i].SetActive(false);
        }
    }
}