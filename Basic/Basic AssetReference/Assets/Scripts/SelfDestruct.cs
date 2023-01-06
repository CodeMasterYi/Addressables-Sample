using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Scripting;

public class SelfDestruct : MonoBehaviour
{
	[SerializeField]
	private float lifetime = 2f;

	private void Start()
	{
		Invoke(nameof(Release), lifetime);
	}

	[Preserve]
	private void Release()
	{
		if (!Addressables.ReleaseInstance(gameObject))
			Destroy(gameObject);
	}
}
