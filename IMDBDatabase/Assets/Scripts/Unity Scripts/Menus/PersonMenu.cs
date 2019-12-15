using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PersonMenu : MonoBehaviour
{
	[Header("Menus")]
	[SerializeField] private GameObject _mainMenu = null;

	[Header("Input fields")]
	[SerializeField] private TMP_InputField _nameInputField = null;

	[Header("Buttons")]
	[SerializeField] private Button _backButton = null;
	[SerializeField] private Button _searchButton = null;

	private string _name;

	private void Start()
	{
		name = "";
		_backButton.onClick.AddListener(OnBackClick);
		_searchButton.onClick.AddListener(OnSearchClick);
	}

	private void OnBackClick()
	{
		ToggleMainMenu();
		DisableMenu();
	}

	private void OnSearchClick()
	{
		GetName();
	}

	private void ToggleMainMenu()
	{
		_mainMenu.SetActive(true);
	}

	private void DisableMenu()
	{
		gameObject.SetActive(false);
	}

	private void GetName()
	{
		_name = _nameInputField.text;
	}
}
