using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
    public Transform playerCamera;
    public float smoothSpeed = 0.1f; // Suavização da câmera
    public GameObject player;
    private SpriteRenderer spriteRenderer;
    private float targetZ; // Armazena a posição desejada no eixo Z
    private Vector3 cameraOffset;

    void Start()
    {
        player = GameObject.Find("Player");
        if (player == null)
        {
            Debug.LogError("Player não encontrado na cena!");
            return;
        }

        spriteRenderer = player.GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer não encontrado no Player!");
            return;
        }

        // 🔹 Ajustando a câmera para ficar mais próxima do jogador
        cameraOffset = new Vector3(0f, 1f, -3f); // Z aproximado para mais proximidade
        targetZ = cameraOffset.z; // Mantém a profundidade ajustada
    }

    void LateUpdate()
    {
        if (player == null) return;

        // Mantém a câmera fixa acima da cabeça do jogador
        Vector3 desiredPosition = player.transform.position + cameraOffset;

        // Suaviza a transição da câmera sem tremedeira
        transform.position = Vector3.Slerp(transform.position, desiredPosition, smoothSpeed);

        // Garante que o eixo Z permaneça constante
        transform.position = new Vector3(transform.position.x, transform.position.y, targetZ);
    }

    void FixedUpdate()
    {
        if (spriteRenderer == null) return;

        // Calcula o offset alvo com base na direção do personagem
        Vector3 targetOffset = spriteRenderer.flipX
            ? new Vector3(-2f, 1.1f, targetZ) // Ajustado para uma visão mais próxima
            : new Vector3(2f, 1.1f, targetZ); // Ajustado para uma visão mais próxima

        cameraOffset = Vector3.Lerp(cameraOffset, targetOffset, smoothSpeed);

        // Define a posição desejada da câmera
        Vector3 desiredPosition = player.transform.position + cameraOffset;

        // Ajusta a altura da câmera para manter a visão do jogador centralizada
        desiredPosition.y = player.transform.position.y + 1.1f; // Reduzido para mais proximidade

        // Aplica a posição suavizada
        transform.position = Vector3.Slerp(transform.position, desiredPosition, smoothSpeed);

        // Mantém a câmera sempre próxima do jogador
        transform.position = new Vector3(transform.position.x, transform.position.y, targetZ);
    }
}
