using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float m_Damage;
    public float Damage => m_Damage;

    [SerializeField] private float m_Speed;

    private Vector3 _targetPosition;

    private void Update()
    {
        float step = m_Speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, step);
    }

    public void StartLifecycle(Vector3 startPos, Vector3 targetPos)
    {
        transform.position = startPos;

        SetTarget(targetPos);
    }

    public void EndLifecycle()
    {

    }

    private void SetTarget(Vector3 targetPosition)
    {
        _targetPosition = targetPosition;
    }
}