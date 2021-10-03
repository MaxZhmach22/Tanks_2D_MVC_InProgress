using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Tools
{
    internal class AssetBundleViewBase : MonoBehaviour
    {

        private const string UrlAssetBundeSprite = "https://drive.google.com/uc?export=download&id=1dAfIaLcEivxlIveJn7h4Qj_-2noaj9D3";
        [SerializeField] private DataSpriteBundle[] _dataSpriteBundles;

        private AssetBundle _spriteAssetBundle;


        protected IEnumerator DownloadAssetBundle()
        {
            yield return DownloadSpriteAssetBundle();

            if(_spriteAssetBundle == null)
                Debug.LogError($"Sprite bundle missing!");

            SetDownloadAssets();
        }

        private IEnumerator DownloadSpriteAssetBundle()
        {
            var request = UnityWebRequestAssetBundle.GetAssetBundle(UrlAssetBundeSprite);
            yield return request.SendWebRequest();
            while (!request.isDone)
                yield return null;

            StateRequest(request, out _spriteAssetBundle);
        }

        private void StateRequest(UnityWebRequest request, out AssetBundle assetBundle)
        {
            if(request.error == null)
            {
                assetBundle = DownloadHandlerAssetBundle.GetContent(request);
                Debug.Log("Complete");
            }
            else
            {
                assetBundle = null;
                Debug.LogError(request.error);
            }
        }

        private void SetDownloadAssets()
        {
            if (_dataSpriteBundles != null)
            {
                foreach (var sprites in _dataSpriteBundles)
                {
                    sprites.Image.sprite = _spriteAssetBundle.LoadAsset<Sprite>(sprites.NameAssetBundle);
                    sprites.Image.color = new Color(0, 1, 0, 1);
                }
            }
            
        }

    }
}
