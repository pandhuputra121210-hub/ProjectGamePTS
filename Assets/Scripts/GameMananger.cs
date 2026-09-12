using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int totalCoin;
    private int coinTerkumpul = 0;
    private int jumlahZombieMati = 0;

    // Penanda status menang
    private bool isMenang = false;

    void Start()
    {
        totalCoin = GameObject.FindGameObjectsWithTag("Coin").Length;
    }

    public void AmbilKoin()
    {
        coinTerkumpul++;
        Debug.Log("Koin diambil! Total: " + coinTerkumpul + " / " + totalCoin);

        if (coinTerkumpul == totalCoin)
        {
            Menang();
        }
    }

    public void SaatZombieMati(Enemy zombie)
    {
        jumlahZombieMati++;
        Debug.Log("GameManager dengar event. Zombie mati: " + jumlahZombieMati + " (" + zombie.name + ")");
    }

    void Menang()
    {
        isMenang = true; // Aktifkan penanda menang
        Debug.Log("KAMU MENANG!");
    }

    void OnGUI()
    {
        // --- 1. UI Skor di Kiri Atas ---
        GUI.skin.label.fontSize = 22;
        GUI.skin.label.alignment = TextAnchor.UpperLeft; // Format teks rata kiri
        
        GUI.Label(new Rect(16, 16, 480, 36), "Koin: " + coinTerkumpul + " / " + totalCoin);
        GUI.Label(new Rect(16, 52, 480, 36), "Zombie mati: " + jumlahZombieMati);

        // --- 2. UI "KAMU MENANG!" di Tengah Layar ---
        if (isMenang)
        {
            GUI.skin.label.fontSize = 48; // Perbesar ukuran teks
            GUI.skin.label.alignment = TextAnchor.MiddleCenter; // Posisi teks rata tengah

            // Membuat posisi kotak UI persis di tengah-tengah layar
            float lebarBox = 500f;
            float tinggiBox = 100f;
            float posX = (Screen.width - lebarBox) / 2f;
            float posY = (Screen.height - tinggiBox) / 2f;

            GUI.Label(new Rect(posX, posY, lebarBox, tinggiBox), "KAMU MENANG!");
        }
    }
}