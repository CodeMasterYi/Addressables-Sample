using UnityEngine;
using UnityEngine.AddressableAssets;

public class BasicReference : MonoBehaviour
{
	[SerializeField]
	private AssetReference baseCube;

	public void SpawnThing()
	{
		baseCube.InstantiateAsync();
	}
}
