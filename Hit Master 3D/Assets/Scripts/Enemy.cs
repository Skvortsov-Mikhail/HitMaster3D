using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float m_MaxHP;

    private float _currentHP;

    private Stage _parentStage;

    private void Start()
    {
        _currentHP = m_MaxHP;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        var bullet = other.GetComponent<Bullet>();

        if (bullet != null)
        {
            ApplyDamage(bullet.Damage);

            bullet.EndLifecycle();
        }
    }

    public void SetParentStage(Stage stage)
    {
        _parentStage = stage;
    }

    private void ApplyDamage(float damage)
    {
        _currentHP = Mathf.Clamp(_currentHP - damage, 0, m_MaxHP);

        CheckDeath();
    }

    private void CheckDeath()
    {
        if (_currentHP <= 0)
        {
            _parentStage.RemoveEnemy();
        }
    }
}