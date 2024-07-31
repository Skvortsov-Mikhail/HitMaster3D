using UnityEngine;
using Zenject;

public class EnemiesFactoryInstaller : MonoInstaller
{
    [SerializeField] private EnemiesFactory m_EnemiesFactory;

    public override void InstallBindings()
    {
        BindEnemiesFactory();
    }

    private void BindEnemiesFactory()
    {
        Container.Bind<EnemiesFactory>().FromInstance(m_EnemiesFactory).AsSingle();
    }
}