﻿using IMDBDatabase;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ContentRow : MonoBehaviour
{
	[Header("Row Elements")]
	[SerializeField] private TextMeshProUGUI _nameTxt = null;
	[SerializeField] private TextMeshProUGUI _typeTxt = null;
	[SerializeField] private Button _selfButton = null;

	[Header("Detailed Elements")]
	[SerializeField] private GameObject _detailedWindow = null;
	[SerializeField] private TextMeshProUGUI _detailedNameTxt = null;
	[SerializeField] private TextMeshProUGUI _detailedTypeTxt = null;
	[SerializeField] private TextMeshProUGUI _detailedAdultTxt = null;
	[SerializeField] private TextMeshProUGUI _detailedScoreTxt = null;
	[SerializeField] private TextMeshProUGUI _detailedVotesTxt = null;
	[SerializeField] private TextMeshProUGUI _detailedStartYearTxt = null;
	[SerializeField] private TextMeshProUGUI _detailedEndYearTxt = null;
	[SerializeField] private TextMeshProUGUI _detailedGenresTxt = null;
	[SerializeField] private Button _seePeopleButton = null;
	[SerializeField] private Button _seeEpisodesButton = null;
	[SerializeField] private Button _exitDetailedButton = null;

	private IReadable _targetResult;

	private void Start()
	{
		_selfButton.onClick.AddListener(ShowDetailedScreen);
		_detailedWindow.SetActive(false);
	}

	public void Initialize(IReadable targetResult)
	{
		_targetResult = targetResult;
		UpdateBasicInfo();
	}

	private void UpdateBasicInfo()
	{
		string[] info = _targetResult.GetBasicInfo().Split('\t');

		_nameTxt.text = info[0];
		_typeTxt.text = info[1];
	}

	private void ShowDetailedScreen()
	{
		UpdateDetailedInfo();
		_detailedWindow.SetActive(true);
	}

	private void UpdateDetailedInfo()
	{
		string[] detInfo = _targetResult.GetDetailedInfo().Split('\t');

		_detailedNameTxt.text = detInfo[0];

		if (_targetResult is Person)
		{
			_detailedStartYearTxt.text = detInfo[1];
			_detailedEndYearTxt.text = detInfo[2];
			_detailedGenresTxt.text = detInfo[3];

			_detailedTypeTxt.text = "...";
			_detailedAdultTxt.text = "...";
			_detailedScoreTxt.text = "...";
			_detailedVotesTxt.text = "...";
		}
		else
		{
			_detailedTypeTxt.text = detInfo[1];
			_detailedAdultTxt.text = detInfo[2];
			_detailedScoreTxt.text = detInfo[3];
			_detailedVotesTxt.text = detInfo[4];
			_detailedStartYearTxt.text = detInfo[5];
			_detailedEndYearTxt.text = detInfo[6];
		}

	}
}
