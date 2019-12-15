using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using IMDBDatabase;

public class TitleMenu : MonoBehaviour
{
	[Header("DATABASE HOLDER")]
	[SerializeField] private Database _dataBase = null;

	[Header("Menus")]
	[SerializeField] private GameObject _mainMenu = null;

	[Header("Input fields")]
	[SerializeField] private TMP_InputField _nameInputField = null;
	[SerializeField] private TMP_InputField _startYearInputField = null;
	[SerializeField] private TMP_InputField _endYearInputField = null;

	[Header("Toggles")]
	[SerializeField] private ToggleGroup _typeToggleGroup = null;
	[SerializeField] private ToggleGroup _contentToggleGroup = null;
	[SerializeField] private GameObject _genreTogglesParent = null;

	[Header("Buttons")]
	[SerializeField] private Button _backButton = null;
	[SerializeField] private Button _searchButton = null;

	private string name;
	private int startYear;
	private int endYear;
	private TitleType type;
	private TitleGenre genre;
	private bool content;

	// Start is called before the first frame update
	private void Start()
    {
		name = "";
		startYear = 0;
		endYear = 0;
		type = TitleType.Ignore;
		genre = TitleGenre.Ignore;
		content = false;

		_backButton.onClick.AddListener(OnBackButtonClick);
		_searchButton.onClick.AddListener(OnSearchButtonClick);

		//_typeToggleGroup.ActiveToggles();
    }

	private void OnBackButtonClick()
	{
		ToggleMainMenu();
		DisableMenu();
	}

	private void OnSearchButtonClick()
	{
		GetValuesFromFields();
		_dataBase.AdvancedSearch(name, type, genre, content, (ushort)startYear, (ushort)endYear);
	}

	private	void ToggleMainMenu()
	{
		_mainMenu.SetActive(true);
	}

	private void DisableMenu()
	{
		gameObject.SetActive(false);
	}

	private void GetValuesFromFields()
	{
		name = _nameInputField.text;
		if (_startYearInputField.text != "")
			startYear = Int32.Parse(_startYearInputField.text);
		if (_endYearInputField.text != "")
			endYear = Int32.Parse(_endYearInputField.text);
		type = GetSelectedType();
		genre = GetWantedGenre();
		content = GetSelectedContent();

		Debug.Log(name + " " + startYear + " " + endYear + " " + type + " " + genre + " " + content);
	}

	private	TitleType GetSelectedType()
	{
		foreach (Toggle t in _typeToggleGroup.ActiveToggles())
			if (t.isOn)
				return t.GetComponent<TypeToggle>().TitleType;

		return TitleType.Ignore;
	}

	private TitleGenre GetWantedGenre()
	{
		TitleGenre genre = 0;
		foreach (Transform toggleTrans in _genreTogglesParent.transform)
			if (toggleTrans.GetComponent<Toggle>().isOn)
				genre |= toggleTrans.GetComponent<GenreToggle>().TitleGenre;

		return genre == 0 ? TitleGenre.Ignore : genre;
	}

	private bool GetSelectedContent()
	{
		foreach (Toggle t in _contentToggleGroup.ActiveToggles())
			if (t.isOn)
				return t.GetComponent<ContentToggle>().IsYes;

		return false;
	}
}