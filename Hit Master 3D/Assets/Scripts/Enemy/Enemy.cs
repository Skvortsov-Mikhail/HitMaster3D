using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider))]
public class Enemy : MonoBehaviour
{
    public event Action<float> HPUpdated;
    public event Action EnemyDied;

    [SerializeField] private float m_MaxHP;
    public float MaxHP => m_MaxHP;

    private float _currentHP;

    [Header("Sounds")]
    [SerializeField] private AudioSource m_TakeDamageSound;
    [SerializeField] private AudioSource m_DieSound;

    private LevelController _levelController;
    private StagesContainer _stagesContainer;

    private Stage _parentStage;

    private Animator _animator;
    private Collider _collider;

    [Inject]
    public void Construct(LevelController levelController, StagesContainer stagesContainer)
    {
        _levelController = levelController;
        _stagesContainer = stagesContainer;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider>();
        _currentHP = m_MaxHP;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Bullet bullet))
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

        m_TakeDamageSound.Play();

        CheckDeath();
    }

    private void CheckDeath()
    {
        if (_currentHP <= 0)
        {
            _animator.enabled = false;
            _collider.enabled = false;

            m_DieSound.Play();

            _parentStage.RemoveEnemy();

            EnemyDied?.Invoke();
        }
    }
}