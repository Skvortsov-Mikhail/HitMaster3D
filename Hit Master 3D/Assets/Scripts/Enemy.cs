using System;
using UnityEngine;
using Zenject;

public class Enemy : MonoBehaviour
{
    public event Action<float> HPUpdated;

    [SerializeField] private float m_MaxHP;
    public float MaxHP => m_MaxHP;

    private float _currentHP;

    private LevelController _levelController;

    private StagesContainer _stagesContainer;
    private Stage _parentStage;

    private Animator _animator;

    [Inject]
    public void Construct(LevelController levelController, StagesContainer stagesContainer)
    {
        _levelController = levelController;
        _stagesContainer = stagesContainer;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _currentHP = m_MaxHP;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        var bullet = other.GetComponent<Bullet>();

        if (bullet != null)
        {
            ApplyDamage(bullet.Damage);
        }
    }

    public void SetParentStage(Stage stage)
    {
        _parentStage = stage;
    }

    private void ApplyDamage(float damage)
    {
        if (_levelController.CanHitEnemiesOnOtherStages == false && _stagesContainer.GetCurrentStage() != _parentStage) return;

        _currentHP = Mathf.Clamp(_currentHP - damage, 0, m_MaxHP);

        HPUpdated?.Invoke(_currentHP);

        CheckDeath();
    }

    private void CheckDeath()
    {
        if (_currentHP <= 0)
        {
            _animator.enabled = false;

            GetComponent<Collider>().enabled = false;
            GetComponent<UI_EnemyHP>().HideHPBar();

            _parentStage.RemoveEnemy();
        }
    }
}