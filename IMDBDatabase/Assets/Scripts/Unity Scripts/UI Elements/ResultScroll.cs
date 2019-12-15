using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IMDBDatabase;

public class ResultScroll : MonoBehaviour
{
	[SerializeField] private Transform _contentTransform;
	[SerializeField] private GameObject _rowPrefab;
	[SerializeField] private Button _exitButton;

	private void Start()
	{
		_exitButton.onClick.AddListener(DestroySelf);
	}

	public void Initialize(IReadable[] readables)
	{
		foreach(IReadable r in readables)
		{
			Instantiate(_rowPrefab, _contentTransform).
				GetComponent<ContentRow>().Initialize(r);
		}
	}

	private void DestroySelf()
	{
		Destroy(gameObject);
	}
}
