using UnityEngine;
using Zenject;

public class EnemiesFactory : MonoBehaviour
{
    [SerializeField] private Enemy m_EnemyPrefab;

    private DiContainer _diContainer;
    [Inject]
    public void Construct(DiContainer container)
    {
        _diContainer = container;
    }

    public void CreateEnemy(Vector3 position, Stage parentStage)
    {
        if (m_EnemyPrefab == null)
        {
            Debug.LogWarning("Enemy Prefab is null");
            return;
        }

        var enemy = _diContainer.InstantiatePrefab(m_EnemyPrefab);

        enemy.transform.position = position;
        enemy.GetComponent<Enemy>().SetParentStage(parentStage);
    }
}