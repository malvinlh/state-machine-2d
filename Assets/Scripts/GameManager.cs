using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

// GameManager bertugas untuk:
// - Mengatur Singleton akses global
// - Spawn player berdasarkan scene yang aktif
// - Menyimpan reference ke player
public class GameManager : MonoBehaviour
{
    // 🔥 Singleton instance yang bisa diakses dari mana saja
    public static GameManager Instance { get; private set; }

    [Header("Player Config")]
    [SerializeField] private GameObject playerPrefab; // Prefab player yang akan di-spawn

    [Header("Spawn Point Per Scene")]
    [SerializeField] private List<SpawnPointByScene> spawnPoints = new List<SpawnPointByScene>();
    // List spawn point yang di-link ke nama scene masing-masing via inspector

    [Header("Global References")]
    [HideInInspector] public GameObject currentPlayer; // Reference player yang aktif sekarang

    private void Awake()
    {
        // Singleton setup
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Hancurkan duplicate GameManager
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Tetap hidup antar pergantian scene
    }

    private void Start()
    {
        SpawnPlayer(); // Spawn player saat scene dimulai
    }

    // Fungsi untuk spawn player ke posisi sesuai scene
    public void SpawnPlayer()
    {
        if (currentPlayer == null) // Hanya spawn kalau player belum ada
        {
            Vector3 spawnPosition = GetSpawnPositionBasedOnScene(); // Ambil posisi spawn sesuai scene aktif
            currentPlayer = Instantiate(playerPrefab, spawnPosition, Quaternion.identity); // Instantiate player
        }
    }

    // Cari posisi spawn berdasarkan nama scene
    private Vector3 GetSpawnPositionBasedOnScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name; // Ambil nama scene aktif

        // Cari di list spawnPoints
        foreach (var spawnData in spawnPoints)
        {
            if (spawnData.sceneName == currentSceneName) // Kalau nama scene cocok
            {
                if (spawnData.spawnPoint != null)
                    return spawnData.spawnPoint.position; // Kembalikan posisi spawn yang ditemukan
                else
                    Debug.LogWarning($"Spawn Point untuk scene {currentSceneName} belum diset!");
            }
        }

        // Kalau tidak ada yang cocok, fallback ke (0,0,0)
        Debug.LogWarning($"Tidak ada spawn point yang cocok untuk scene {currentSceneName}, spawn di (0,0,0).");
        return Vector3.zero;
    }
}

// Class untuk mendefinisikan pasangan SceneName dan SpawnPoint
// Bisa diatur lewat Inspector Unity
[System.Serializable]
public class SpawnPointByScene
{
    public string sceneName;         // Nama scene (harus cocok persis dengan Build Settings)
    public Transform spawnPoint;     // Transform di mana player akan di-spawn
}
