using UnityEngine;

public class FlowerPlant : MonoBehaviour
{
    [Header("Daftar Model Bunga")]
    public GameObject[] flowerStages; 
    
    private int currentStageIndex = 0;

    void Start()
    {
        UpdateFlowerVisuals();
    }

    public void Siram()
    {
        // Cek apakah bunga belum mekar maksimal?
        if (currentStageIndex < flowerStages.Length - 1)
        {
            // 1. Naikkan level bunga
            currentStageIndex++; 
            
            // 2. Perbarui tampilan visual
            UpdateFlowerVisuals();

            // 3. LOGIKA SKOR (DIPERBAIKI)
            // Kita hanya nambah skor KALAU bunga sudah sampai tahap terakhir (Mekar)
            // Supaya 1 Bunga = 1 Skor.
            if (currentStageIndex == flowerStages.Length - 1)
            {
                GameManager gm = FindFirstObjectByType<GameManager>(); // Pakai FindFirstObjectByType utk Unity 6
                if (gm != null)
                {
                    // PERBAIKAN NAMA: Dari AddScore() jadi TambahSkor()
                    gm.TambahSkor(); 
                }
                else
                {
                    Debug.Log("Bunga Mekar! (Tapi GameManager tidak ditemukan)");
                }
            }
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
    
    // Fitur Test Spasi (Tetap simpan buat ngetes gampang)
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Siram();
        }
    }
}