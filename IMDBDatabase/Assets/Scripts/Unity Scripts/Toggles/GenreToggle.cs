using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IMDBDatabase;

public class GenreToggle : MonoBehaviour
{
	[SerializeField] private TitleGenre _titleGenre = 0;
	public TitleGenre TitleGenre => _titleGenre;
}
