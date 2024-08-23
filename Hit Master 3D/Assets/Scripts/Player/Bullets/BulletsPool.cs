using UnityEngine;
using UnityEngine.Pool;
using Zenject;

public class BulletsPool : MonoBehaviour
{
    [SerializeField] private Bullet m_BulletPrefab;

    private ObjectPool<Bullet> _pool;
    public ObjectPool<Bullet> Pool => _pool;

    private DiContainer _diContainer;

    [Inject]
    public void Construct(DiContainer diContainer)
    {
        _diContainer = diContainer;
    }

    private void Start()
    {
        _pool = new ObjectPool<Bullet>(createFunc: () => _diContainer.InstantiatePrefabForComponent<Bullet>(m_BulletPrefab),
            actionOnGet: (obj) => obj.gameObject.SetActive(true),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: false,
            defaultCapacity: 10,
            maxSize: 10);
    }
}