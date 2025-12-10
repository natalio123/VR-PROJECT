using UnityEngine;

public class FlowerPlant : MonoBehaviour
{
    [Header("Daftar Model Bunga")]
    // Ini adalah 'laci' untuk menyimpan 4 model bunga kamu (Layu, Tumbuh, Kuncup, Mekar)
    public GameObject[] flowerStages; 
    
    // Variabel untuk melacak tahap keberapa sekarang (0 = Layu, 3 = Mekar)
    private int currentStageIndex = 0;

    void Start()
    {
        // Saat game mulai, pastikan tampilannya benar (hanya yang layu yang muncul)
        UpdateFlowerVisuals();
    }

    // Fungsi ini akan dipanggil oleh "Air" nanti (Tugas Jenn di Hari 5)
    public void Siram()
    {
        // Cek apakah bunga belum mekar maksimal? (Index 3 adalah batas akhir)
        if (currentStageIndex < flowerStages.Length - 1)
        {
            // Naikkan level bunga
            currentStageIndex++; 
            
            // Perbarui tampilan model 3D
            UpdateFlowerVisuals();

            // PANGGIL SCRIPT NATALIO (GameManager)
            // Fungsinya: Tambah skor setiap kali bunga tumbuh
            // Kita pakai 'try-catch' atau pengecekan null biar tidak error kalau script Natalio belum ada
            GameManager gm = FindObjectOfType<GameManager>();
            if (gm != null)
            {
                gm.AddScore(); // Pastikan di script Natalio fungsi AddScore() bersifat 'public'
            }
            else
            {
                Debug.Log("Bunga Tumbuh! (GameManager belum ditemukan, skor tidak bertambah)");
            }
        }
    }

    // Fungsi untuk mengatur mana yang muncul/hilang
    void UpdateFlowerVisuals()
    {
        for (int i = 0; i < flowerStages.Length; i++)
        {
            if (i == currentStageIndex)
            {
                flowerStages[i].SetActive(true); // Munculkan tahap ini
            }
            else
            {
                flowerStages[i].SetActive(false); // Sembunyikan tahap lain
            }
        }
    }
    
    // --- FITUR TESTING (HAPUS NANTI) ---
    // Gunakan Spasi untuk menyiram bunga secara manual buat ngetes
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Siram();
        }
    }
}
