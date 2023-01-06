using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class FilteredReferences : MonoBehaviour
{
    [Serializable]
    public class AssetReferenceMaterial : AssetReferenceT<Material>
    {
        public AssetReferenceMaterial(string guid) : base(guid) { }
    }

    [SerializeField]
    private AssetReferenceGameObject leftObject;
    [SerializeField]
    private AssetReferenceGameObject rightObject;
    [SerializeField]
    private AssetReferenceMaterial spawnMaterial;
    [SerializeField]
    private AssetReferenceMaterial midMaterial;
    [SerializeField]
    private AssetReferenceMaterial lateMaterial;

    [SerializeField]
    private Vector3 leftPosition;
    [SerializeField]
    private Vector3 rightPosition;

    private MeshRenderer _leftMeshRender;
    private MeshRenderer _rightMeshRender;

    private void Start()
    {
        leftObject.LoadAssetAsync();
        rightObject.LoadAssetAsync();
        spawnMaterial.LoadAssetAsync();
        midMaterial.LoadAssetAsync();
        lateMaterial.LoadAssetAsync();
    }

    private int _frameCounter = 0;

    //Note that we never actually wait for the loads to complete.  We just check if they are done (if the asset exists)
    //before proceeding.  This is often not going to be the best practice, but has some benefits in certain scenarios.
    private void FixedUpdate()
    {
        _frameCounter++;
        if (_frameCounter == 20)
        {
            if (leftObject.Asset != null)
            {
                var leftGo = Instantiate(leftObject.Asset, leftPosition, Quaternion.identity) as GameObject;
                _leftMeshRender = leftGo.GetComponent<MeshRenderer>();
            }

            if (rightObject.Asset != null)
            {
                var rightGo = Instantiate(rightObject.Asset, rightPosition, Quaternion.identity) as GameObject;
                _rightMeshRender = rightGo.GetComponent<MeshRenderer>();
            }

            if (spawnMaterial.Asset != null && _leftMeshRender != null && _rightMeshRender != null)
            {
                _leftMeshRender.material = spawnMaterial.Asset as Material;
                _rightMeshRender.material = spawnMaterial.Asset as Material;
            }
        }

        if (_frameCounter == 40)
        {
            if (midMaterial.Asset != null && _leftMeshRender != null && _rightMeshRender != null)
            {
                _leftMeshRender.material = midMaterial.Asset as Material;
                _rightMeshRender.material = midMaterial.Asset as Material;
            }
        }

        if (_frameCounter == 60)
        {
            _frameCounter = 0;
            if (lateMaterial.Asset != null && _leftMeshRender != null && _rightMeshRender != null)
            {
                _leftMeshRender.material = lateMaterial.Asset as Material;
                _rightMeshRender.material = lateMaterial.Asset as Material;
            }
        }
    }

    private void OnDisable()
    {
        //note that this may be dangerous, as we are releasing the asset without knowing if the instances still exist.
        // sometimes that's fine, sometimes not.
        leftObject.ReleaseAsset();
        rightObject.ReleaseAsset();
        spawnMaterial.ReleaseAsset();
        midMaterial.ReleaseAsset();
        lateMaterial.ReleaseAsset();
    }
}
