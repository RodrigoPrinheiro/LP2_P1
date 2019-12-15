using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IMDBDatabase;

public class DatabaseHolder : MonoBehaviour
{
	[SerializeField] private GameObject _scrollPrefab;

	private Database _db;

	void Start()
    {
		_db = new Database();
	}

	public void AdvancedTitleSearch(string name, TitleType type, TitleGenre genre, 
		bool content, ushort startYear, ushort endYear)
	{
		IReadable[] results = 
			_db.AdvancedSearch(name, type, genre, content, startYear, endYear);

		Instantiate(_scrollPrefab).GetComponent<ResultScroll>().Initialize(results);
	}

	public void PersonSearch(string name)
	{
		
	}
}
