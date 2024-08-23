using UnityEngine;
using UnityEngine.UI;

public class UI_EnemyHP : MonoBehaviour
{
    [SerializeField] private Canvas m_HPBarCanvas;

    [SerializeField] private GameObject m_targetRotate;
    [SerializeField] private Image m_HPBar;

    private Camera _mainCamera;

    private Enemy _enemy;

    private void Awake()
    {
        _enemy = transform.root.GetComponent<Enemy>();
    }

    private void Start()
    {
        m_HPBar.fillAmount = 1;

        _mainCamera = Camera.main;

        _enemy.HPUpdated += OnHPUpdated;
        _enemy.EnemyDied += OnEnemyDied;
    }

    private void OnDestroy()
    {
        _enemy.HPUpdated -= OnHPUpdated;
        _enemy.EnemyDied -= OnEnemyDied;
    }

    private void Update()
    {
        RotateHPBarToCamera();
    }

    private void RotateHPBarToCamera()
    {
        Vector3 toTarget = _mainCamera.transform.position - transform.position;

        m_targetRotate.transform.rotation = Quaternion.LookRotation(-toTarget);
    }

    private void OnHPUpdated(float currentHP)
    {
        m_HPBar.fillAmount = currentHP / _enemy.MaxHP;

        m_HPBar.color = Color.Lerp(Color.red, Color.green, m_HPBar.fillAmount);
    }

    private void OnEnemyDied()
    {
        m_HPBarCanvas.gameObject.SetActive(false);
    }
}