using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace Tools
{
    internal class AdressablesAsstetsExample : MonoBehaviour
    {
        [Header("BackGround Image")]
        [SerializeField] private Image _backGround;

        [SerializeField] private Button _addBackground;
        [SerializeField] private Button _removeBackground;
        [SerializeField] private AssetReference _imageAsset;
        private AsyncOperationHandle<Sprite> _handle;

        private async void Start()
        {
            _addBackground.onClick.AddListener(LoadSprite);
            _removeBackground.onClick.AddListener(RemoveImage);
        }

        private async void LoadSprite()
        {
            AsyncOperationHandle<Sprite> _handle = _imageAsset.LoadAssetAsync<Sprite>();
            await _handle.Task;
            if (_handle.Status == AsyncOperationStatus.Succeeded)
            {
                Sprite sprite = _handle.Result;
                _backGround.sprite = sprite;
                Addressables.Release(_handle);
            }
        }

        private void RemoveImage()
        {
            _backGround.sprite = null;
           
        }

        private void OnDestroy()
        {
            _addBackground.onClick.RemoveListener(LoadSprite);
            _removeBackground.onClick.RemoveListener(RemoveImage);
           
        }
 
    }
}