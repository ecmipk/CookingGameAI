using UnityEngine;
using UnityEngine.SceneManagement;

public class ReplayButton : MonoBehaviour
{
    void OnMouseDown()
    {
        Debug.Log("🔄 Replay butonuna tıklandı! Oyun yeniden başlıyor...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
