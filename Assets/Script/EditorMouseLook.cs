using UnityEngine;

public class EditorMouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody; // Drag PlayerRig ke sini

    float xRotation = 0f;

    void Start()
    {
        // Cari PlayerRig otomatis (Parent dari kamera)
        if (playerBody == null && transform.parent != null)
        {
            playerBody = transform.parent;
        }
    }

    void Update()
    {
        // Kodingan ini HANYA jalan di Unity Editor (Laptop).
        // Pas di-build ke HP, dia otomatis mati biar gak ganggu Gyroscope HP.
        #if UNITY_EDITOR
        
        // Cek apakah user menahan Klik Kanan?
        if (Input.GetMouseButton(1)) 
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            // Rotasi Atas-Bawah (Kepala/Kamera)
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Batasi biar gak patah leher
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            // Rotasi Kiri-Kanan (Badan/PlayerRig ikut muter)
            if (playerBody != null)
            {
                playerBody.Rotate(Vector3.up * mouseX);
            }
        }
        #endif
    }
}