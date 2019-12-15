using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IMDBDatabase;

public class DatabaseHolder : MonoBehaviour
{
	public Database Database { get => db; }

	private Database db = null;

    // Start is called before the first frame update
    void Start()
    {
		Database db = new Database();
	}
}
