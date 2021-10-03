using Tanks;
using UnityEngine;


internal abstract class BaseViewController<TView> : BaseController where TView : BaseView
{
    protected abstract ResourcePath ResourcePath { get; }
    protected TView LoadView(Transform placeForUi = null)
    {
        GameObject prefab = ResourcesLoader.LoadAndInstantiateObject<GameObject>(ResourcePath, placeForUi, false);
        AddGameObject(prefab);

        return prefab.GetComponent<TView>();
    }
}
