using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	[Header("Menus")]
	[SerializeField] private GameObject _titleMenu = null;
	[SerializeField] private GameObject _peopleMenu = null;

	[Header("Buttons")]
	[SerializeField] private Button _titleSearchButton = null;
	[SerializeField] private Button _peopleSearchButton = null;
	[SerializeField] private Button _exitButton = null;

    // Start is called before the first frame update
    void Start()
    {
        _titleSearchButton.onClick.AddListener(OnTitleSearchClick);

		_peopleSearchButton.onClick.AddListener(OnPeopleSearchClick);

		_exitButton.onClick.AddListener(Application.Quit);
	}

	private void OnTitleSearchClick()
	{
		DisableSelf();
		ToggleTitleMenu();
	}

	private void OnPeopleSearchClick()
	{
		DisableSelf();
		TogglePeopleMenu();
	}

	private void DisableSelf()
	{
		gameObject.SetActive(false);
	}

	private void ToggleTitleMenu()
	{
		_titleMenu.SetActive(true);

	}

	private void TogglePeopleMenu()
	{
		_peopleMenu.SetActive(true);
	}
}
