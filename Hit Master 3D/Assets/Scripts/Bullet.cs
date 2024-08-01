using UnityEngine;
using Zenject;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float m_Damage;
    public float Damage => m_Damage;

    [SerializeField] private float m_Speed;

    private BulletsPool _bulletsPool;
    [Inject]
    public void Construct(BulletsPool bulletsPool)
    {
        _bulletsPool = bulletsPool;
    }

    private void OnTriggerEnter(Collider other)
    {
        _bulletsPool.Pool.Release(this);
    }

    private void Update()
    {
        float step = m_Speed * Time.deltaTime;
        transform.Translate(Vector3.forward * step);
    }

    public void SetDirection(Vector3 startPos, Vector3 targetPosition)
    {
        transform.position = startPos;
        transform.LookAt(targetPosition);
    }
}