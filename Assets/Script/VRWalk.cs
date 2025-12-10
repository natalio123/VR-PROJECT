using UnityEngine;

// Wajib ada CharacterController biar script ini jalan
[RequireComponent(typeof(CharacterController))]
public class VRWalk : MonoBehaviour
{
    [Header("Pengaturan Jalan")]
    public float kecepatanJalan = 3.0f;
    public float sudutNunduk = 30.0f; // Berapa derajat nunduk baru jalan?
    
    [Header("Referensi")]
    public Transform kepalaKamera; // Drag Main Camera ke sini

    private CharacterController controller;

    void Start()
    {
        // Ambil komponen CharacterController dari diri sendiri (PlayerRig)
        controller = GetComponent<CharacterController>();
        
        // Kalau lupa masukin kamera, otomatis cari Main Camera
        if (kepalaKamera == null)
        {
            kepalaKamera = Camera.main.transform;
        }
    }

    void Update()
    {
        // 1. Cek Sudut Leher (Sumbu X)
        // Di Unity, nunduk itu sudut X-nya positif (0 sampai 90)
        float sudutX = kepalaKamera.eulerAngles.x;

        // 2. Logika Deteksi Nunduk
        // Kita batasi nunduk antara 'sudutNunduk' (misal 30) sampai 90 derajat (nunduk total)
        bool sedangNunduk = (sudutX >= sudutNunduk && sudutX < 90.0f);

        if (sedangNunduk)
        {
            GerakMaju();
        }
        else
        {
            // Opsi: Bisa tambah logika berhenti atau diam
        }
    }

    void GerakMaju()
    {
        // Cari arah depan sesuai pandangan kamera
        Vector3 arahDepan = kepalaKamera.forward;

        // PENTING: Matikan sumbu Y (Atas/Bawah) supaya pemain tidak terbang ke langit saat nengok ke atas
        // atau masuk ke tanah saat nunduk. Kita mau jalan mendatar.
        arahDepan.y = 0;
        arahDepan.Normalize(); // Standarkan panjang vektor

        // Gerakkan Character Controller
        // SimpleMove otomatis menangani Gravitasi (jatuh kalau ada tangga)
        controller.SimpleMove(arahDepan * kecepatanJalan);
    }
}