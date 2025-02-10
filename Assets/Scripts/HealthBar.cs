using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    private RectTransform healthBarTransform;
    private float offsetX = 50f; // Ajuste este valor para mover a barra para a direita

    PlayerControl player;

    void Start()
    {
        player = Object.FindFirstObjectByType<PlayerControl>();
        healthBarTransform = GetComponent<RectTransform>();

        // Move a barra de vida para a direita no início do jogo
        Vector3 newPosition = healthBarTransform.anchoredPosition;
        newPosition.x += offsetX;
        healthBarTransform.anchoredPosition = newPosition;
    }

    void Update()
    {
        slider.value = player.health;

        if (player.health <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
