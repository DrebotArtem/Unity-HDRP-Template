using Zenject;
using System.Collections;

namespace DrebotGS
{
  public class InjectableFeature : Feature
  {
    public InjectableFeature()
        : base()
    {
    }

    public InjectableFeature(string name)
        : base(name)
    { }

    public void IncjectSelfAndChildren(DiContainer container)
    {
      container.Inject(this);
      InjectInChilndren(_cleanupSystems, container);
      InjectInChilndren(_executeSystems, container);
      InjectInChilndren(_initializeSystems, container);
      InjectInChilndren(_tearDownSystems, container);
    }

    private void InjectInChilndren(IEnumerable collection, DiContainer container)
    {
      foreach (var sys in collection)
      {
        var injectableFeature = sys as InjectableFeature;
        if (injectableFeature != null)
        {
          injectableFeature.IncjectSelfAndChildren(container);
        }
        else
        {
          container.Inject(sys);
        }
      }
    }
  }
}