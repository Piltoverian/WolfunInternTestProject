using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBarUI : MonoBehaviour
{
    [SerializeField] private Image healthImage;
    [SerializeField] private Enemy targetEnemy;
    [SerializeField] private Canvas canvas;

    private static readonly int CurrentHealthProp = Shader.PropertyToID("_CurrentHealth");

    private void Start()
    {
        if (targetEnemy == null)
        {
            targetEnemy = GetComponentInParent<Enemy>();
        }
        
        if (canvas == null)
        {
            canvas = GetComponent<Canvas>();
        }
        
        // Cấu hình Canvas World Space để luôn hướng về Camera (tùy chọn)
        if (canvas != null && canvas.renderMode == RenderMode.WorldSpace)
        {
            canvas.worldCamera = Camera.main;
        }
    }

    private void LateUpdate()
    {
        if (targetEnemy != null && healthImage != null && healthImage.material != null)
        {
            float healthPercent = targetEnemy.GetHealthPercent();
            healthImage.material.SetFloat(CurrentHealthProp, healthPercent);

            // Ẩn thanh máu nếu đầy máu (chưa bị đánh) hoặc chết
            if (canvas != null)
            {
                bool shouldShow = healthPercent < 1f && healthPercent > 0f;
                canvas.enabled = shouldShow;
            }
        }
        
        // Luôn xoay Canvas về phía Camera chính (Billboard effect)
        if (canvas != null && canvas.renderMode == RenderMode.WorldSpace && Camera.main != null)
        {
            transform.rotation = Camera.main.transform.rotation;
        }
    }
}
