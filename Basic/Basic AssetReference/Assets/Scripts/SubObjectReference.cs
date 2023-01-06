using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class SubObjectReference : MonoBehaviour
{
    [SerializeField]
    private AssetReference sheetReference;
    [SerializeField]
    private AssetReference sheetSubReference;
    [SerializeField]
    private List<SpriteRenderer> spritesToChange;

    [SerializeField]
    private Button loadMainButton;
    [SerializeField]
    private Button loadSubButton;

    public void LoadMainAsset()
    {
        loadMainButton.interactable = false;
        sheetReference.LoadAssetAsync<IList<Sprite>>().Completed += AssetDone;
    }

    public void LoadSubAsset()
    {
        loadSubButton.interactable = false;
        sheetSubReference.LoadAssetAsync<Sprite>().Completed += SubAssetDone;
    }

    private void AssetDone(AsyncOperationHandle<IList<Sprite>> op)
    {
        if (op.Result == null)
        {
            Debug.LogError("no sheets here.");
            return;
        }
        spritesToChange[0].sprite = op.Result[1];
        loadMainButton.interactable = false;
    }

    private void SubAssetDone(AsyncOperationHandle<Sprite> op)
    {
        if (op.Result == null)
        {
            Debug.LogError("no sprite in sheet here.");
            return;
        }
        spritesToChange[1].sprite = op.Result;
        loadSubButton.interactable = false;
    }

    private void Start()
    {
        Addressables.InitializeAsync();
    }
}
