using UnityEngine;
using Zenject;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float m_Damage;
    public float Damage => m_Damage;

    [SerializeField] private float m_Speed;

    [SerializeField] private float m_LifeTime;

    private float _timer;

    private BulletsPool _bulletsPool;
    [Inject]
    public void Construct(BulletsPool bulletsPool)
    {
        _bulletsPool = bulletsPool;
    }

    private void OnTriggerEnter(Collider other)
    {
        ReleaseBulletToPool();
    }

    private void Update()
    {
        MoveBullet();

        UpdateTimer();
    }

    public void SetNewValues(Vector3 startPos, Vector3 targetPosition)
    {
        transform.position = startPos;
        transform.LookAt(targetPosition);
    }

    private void ReleaseBulletToPool()
    {
        _timer = 0f;
        _bulletsPool.Pool.Release(this);
    }

    private void MoveBullet()
    {
        float step = m_Speed * Time.deltaTime;
        transform.Translate(Vector3.forward * step);
    }

    private void UpdateTimer()
    {
        _timer += Time.deltaTime;

        if (_timer >= m_LifeTime)
        {
            ReleaseBulletToPool();
        }
    }
}