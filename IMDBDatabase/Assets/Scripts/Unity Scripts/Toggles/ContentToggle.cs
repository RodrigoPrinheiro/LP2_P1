using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentToggle : MonoBehaviour
{
	[SerializeField] private bool _isYes = false;
	public bool IsYes => _isYes;
}
