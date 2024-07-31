using UnityEngine;

public class EnemiesFactory : MonoBehaviour
{
    [SerializeField] private Enemy m_EnemyPrefab;

    public void CreateEnemy(Vector3 position, Stage parentStage)
    {
        if (m_EnemyPrefab == null)
        {
            Debug.LogWarning("Enemy Prefab is null");
            return;
        }

        var enemy = Instantiate(m_EnemyPrefab);

        enemy.transform.position = position;
        enemy.SetParentStage(parentStage);
    }
}