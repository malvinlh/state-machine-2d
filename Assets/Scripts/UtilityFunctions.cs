using UnityEngine;

// Kumpulan fungsi statis (bisa dipanggil tanpa buat instance)
// Digunakan untuk konversi posisi mouse dari layar ke dunia
public class UtilityFunctions : MonoBehaviour
{
    // Fungsi utama: ambil posisi mouse di dunia, dengan z = 0
    public static Vector3 GetMouseWorldPosition() 
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f; // Karena game 2D, kita abaikan sumbu Z
        return vec;
    }

    // Overload tanpa parameter kamera
    public static Vector3 GetMouseWorldPositionWithZ() 
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }

    // Overload pakai kamera spesifik
    public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera) 
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    }

    // Fungsi utama: mengubah posisi screen (Input.mousePosition) jadi posisi dunia (WorldPosition)
    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera) 
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
}
