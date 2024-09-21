using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.U2D;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;

public class LoadSprites : MonoBehaviour {
    [SerializeField] private List<SpriteRenderer> _spriteRenderers;
    [SerializeField] private AssetLabelReference _assetLabelReference;
    private SpriteAtlas _spriteAtlas;
    private void Start()
    {
        foreach (Transform child in transform)
        {
            SpriteRenderer sr = child.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                _spriteRenderers.Add(sr);
            }
        }
    }

    public void Load() 
    {
        Addressables.LoadAssetAsync<SpriteAtlas>(_assetLabelReference).Completed += LoadSpiteAtlas;
    }
    private void LoadSpiteAtlas(AsyncOperationHandle<SpriteAtlas> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            _spriteAtlas = handle.Result;

            for (int i = 0; i < _spriteRenderers.Count; i++)
            {
                if (i < _spriteAtlas.spriteCount)
                {
                    Sprite sprite = _spriteAtlas.GetSprite($"Image {i+1}");
                    if (sprite != null)
                    {
                        _spriteRenderers[i].sprite = sprite;
                    }
                    else
                    {
                        Debug.LogError($"Sprite {i+1} not found");
                    }
                }
            }
        }
        else
        {
            Debug.LogError("Failed to load Sprite Atlas");
        }
    }
}
