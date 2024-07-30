using UnityEngine;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{
    //[SerializeField] private AnyProjectClass m_AnyProjectClassObject;

    public override void InstallBindings()
    {/*
        Container.
            Bind<AnyProjectClass>()
            .FromComponentInNewPrefab(m_AnyProjectClassObject)
            .AsSingle();*/
    }
}