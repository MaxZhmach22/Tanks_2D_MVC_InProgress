using System;
using Tanks;
using UnityEngine;
using UnityEngine.UI;

namespace Tools
{
    internal class NextPrefabButtonExample : MonoBehaviour
    {
        private readonly string _path = "Prefabs/AssetBundleExample/addresableAssetExample";
        private AdressablesAsstetsExample _view;
        private readonly MapGeneratingCotroller _mapGeneratingController;
        

        [SerializeField] private Button _buttonNext;
        [SerializeField] private GameObject _currencyPrefab;
        [SerializeField] private Transform _placeForUi;
        private void Start()
        {
            _buttonNext.onClick.AddListener(LoadNextPrefab);
        }

        private void OnDestroy()
        {
            _buttonNext.onClick.RemoveListener(LoadNextPrefab);
        }
        private void LoadNextPrefab()
        {
            _view = LoadView();
            _currencyPrefab.gameObject.SetActive(false);
        }

        private AdressablesAsstetsExample LoadView()
        {
            AdressablesAsstetsExample prefab = Resources.Load<AdressablesAsstetsExample>(_path);
            GameObject.Instantiate(prefab, _placeForUi);
            return prefab;
        }
    }
}
