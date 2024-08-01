using UnityEngine;
using Zenject;

public class StagesContainerInstaller : MonoInstaller
{
    [SerializeField] private StagesContainer m_StagesContainer;

    public override void InstallBindings()
    {
        BindStagesContainer();
    }

    private void BindStagesContainer()
    {
        Container.Bind<StagesContainer>().FromInstance(m_StagesContainer).AsSingle();
    }
}