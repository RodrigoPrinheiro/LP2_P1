using UnityEngine;
using IMDBDatabase;

public class TypeToggle : MonoBehaviour
{
	[SerializeField] private TitleType _titleType = 0;
	public TitleType TitleType => _titleType;
}
