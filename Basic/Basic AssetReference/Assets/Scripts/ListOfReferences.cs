using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ListOfReferences : MonoBehaviour
{
	[SerializeField]
	private List<AssetReference> shapes;

	private bool _isReady = false;
	private int _toLoadCount;

	private int _currentIndex = 0;

	private void Start ()
	{
		_toLoadCount = shapes.Count;
		foreach (var shape in shapes)
		{
			shape.LoadAssetAsync<GameObject>().Completed += OnShapeLoaded;
		}
	}

	private void OnShapeLoaded(AsyncOperationHandle<GameObject> obj)
	{
		_toLoadCount--;
		if (_toLoadCount <= 0)
			_isReady = true;
	}

	public void SpawnAThing()
	{
		if (_isReady && shapes[_currentIndex].Asset != null)
		{
			for(var count = 0; count <= _currentIndex; count++)
				Instantiate(shapes[_currentIndex].Asset);
			_currentIndex++;
			if (_currentIndex >= shapes.Count)
				_currentIndex = 0;
		}
	}

	private void OnDestroy()
	{
		foreach (var shape in shapes)
		{
			shape.ReleaseAsset();
		}
	}
}
