using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro; 

public class GameManager : MonoBehaviour
{
    [Header("Pengaturan Game")]
    public float waktuTotal = 420f; 
    public int targetSkorMenang = 100; // Karena skor sekarang puluhan/ratusan, targetnya kita naikkan

    [Header("UI")]
    public TMP_Text textWaktu; 
    public TMP_Text textSkor;  

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

        sisaWaktu -= Time.deltaTime;

        if (sisaWaktu <= 0)
        {
            sisaWaktu = 0;
            AkhiriGame();
        }

        UpdateUI();
    }

    // --- PERUBAHAN PENTING ---
    // Fungsi sekarang menerima angka 'nilai'. Jadi bisa nambah +1, +3, atau +10.
    public void TambahSkor(int nilai)
    {
        skorSaatIni += nilai;
        UpdateUI();
    }

    void UpdateUI()
    {
        int menit = Mathf.FloorToInt(sisaWaktu / 60);
        int detik = Mathf.FloorToInt(sisaWaktu % 60);

        if (textWaktu != null)
            textWaktu.text = string.Format("{0:00}:{1:00}", menit, detik);

        // --- UI HANYA ANGKA ---
        // Tidak ada lagi "/6". Hanya angka skor mentah.
        if (textSkor != null)
            textSkor.text = skorSaatIni.ToString();
    }

    void AkhiriGame()
    {
        gameSelesai = true;
        PlayerPrefs.SetInt("SkorAkhir", skorSaatIni);
        PlayerPrefs.Save();

        // Menang jika skor mencapai target angka (bukan jumlah bunga lagi)
        if (skorSaatIni >= targetSkorMenang)
        {
            SceneManager.LoadScene("Scene_Ending_Lulus");
        }
        else
        {
            SceneManager.LoadScene("Scene_Ending_Gagal");
        }
    }
}