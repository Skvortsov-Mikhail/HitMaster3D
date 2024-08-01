using UnityEngine;
using Zenject;

public class BulletsPoolInstaller : MonoInstaller
{
    [SerializeField] private BulletsPool m_BulletsPool;

    public override void InstallBindings()
    {
        BindBulletsPool();
    }

    private void BindBulletsPool()
    {
        Container.Bind<BulletsPool>().FromInstance(m_BulletsPool).AsSingle();
    }
}