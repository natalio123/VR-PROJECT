using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Wajib untuk pindah scene

public class GameManager : MonoBehaviour
{
    [Header("Pengaturan Game")]
    public float waktuTotal = 420f; // 7 Menit (dalam detik)
    public int targetBunga = 6;     // Jumlah total bunga di level

    [Header("UI")]
    public Text textWaktu; // Drag Text UI untuk Timer
    public Text textSkor;  // Drag Text UI untuk Skor

    private float sisaWaktu;
    private int skorSaatIni = 0;
    private bool gameSelesai = false;

    void Start()
    {
        sisaWaktu = waktuTotal;
        UpdateUI();
    }

    void Update()
    {
        if (gameSelesai) return;

        // Hitung mundur waktu
        sisaWaktu -= Time.deltaTime;

        if (sisaWaktu <= 0)
        {
            sisaWaktu = 0;
            AkhiriGame();
        }

        UpdateUI();
    }

    public void TambahSkor()
    {
        skorSaatIni++;
        UpdateUI();
    }

    void UpdateUI()
    {
        // Format waktu jadi Menit:Detik (Contoh 06:30)
        int menit = Mathf.FloorToInt(sisaWaktu / 60);
        int detik = Mathf.FloorToInt(sisaWaktu % 60);

        if (textWaktu != null)
            textWaktu.text = string.Format("{0:00}:{1:00}", menit, detik);

        if (textSkor != null)
            textSkor.text = skorSaatIni + "/" + targetBunga;
    }

    void AkhiriGame()
    {
        gameSelesai = true;

        // --- TAMBAHAN BARU: SIMPAN SKOR ---
        // Kita titip nilai skor ke memori HP dengan kata kunci "SkorAkhir"
        PlayerPrefs.SetInt("SkorAkhir", skorSaatIni);
        PlayerPrefs.Save();
        // ----------------------------------

        float persentase = (float)skorSaatIni / targetBunga * 100f;

        if (persentase >= 50f)
        {
            SceneManager.LoadScene("Scene_Ending_Lulus");
        }
        else
        {
            SceneManager.LoadScene("Scene_Ending_Gagal");
        }
    }
}