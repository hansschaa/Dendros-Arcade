using LitJson;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class JsonController : MonoBehaviour
{
	[Header("Views")]
	public GameObject _mainSceneCanvas;
	public GameObject _gameObjectsMainScene;
	public GameObject _loadingSceneCanvas;
	private string jsonString;
	public static JsonData jsonDataStages;
	public static JsonData jsonDataConfig;
	public string _stageUrl;


	void Start()
	{
		jsonDataStages = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/GameData.txt").Trim());
		loadGlobalVariablesFromJson();
		this._mainSceneCanvas.SetActive(true);
		this._gameObjectsMainScene.SetActive(true);
		this.GetComponent<AnimationController>().playAnimations(GameState.States.MAINSCENE);
		this._loadingSceneCanvas.SetActive(false);      
	}

    private void loadGlobalVariablesFromJson()
    {
        GlobalVariables._totallyStages = jsonDataStages["Stages"].Count;
    }
}
