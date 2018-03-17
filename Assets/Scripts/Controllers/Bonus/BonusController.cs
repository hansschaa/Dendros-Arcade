﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusController : MonoBehaviour 
{
	Coroutine _changeParametersCoroutine;
	Coroutine _bonusRespawnCoroutine;
	SearchManager _searchManager;
	public GameObject _shape;
	public List<Vector2> _portalPath;
	public SoundController _soundController;

	[Header("Reimi")]
	public Text _reimiText;

	[Header("Bonus GameObjects")]
	public Transform _lightBonus;
	public Transform _velocityBonus;
	public Transform _coordinationBonus;
	public GameObject _bonusPrefab;
	public GameObject _lightBonusInstance;
	public GameObject _velocityBonusInstance;
	public GameObject _coordinationBonusInstance;
	public GameObject _teleportBonusInstance;
	public GameObject _portalBonusInstance;
	public Transform _portalGroupGameObject;

	public GameObject[] _bonusList;
	public Material _material;

	[Header("Parameters")]
	public float _decreaseValue;
	public float _initializeChangeParametersDelay;
	public float _respawnDelay;
	private float _playerVelocity; 
	[HideInInspector]
	public Vector2 _bar;

	public int _idActiveChildLight;
	public int _idActiveChildVelocity;
	public int _idActiveChildCoordination;

    internal void resetStats()
    {

		if(this._lightBonus.gameObject.activeInHierarchy)
		{
			for(int i = 0 ; i < 3;i++)
				this._lightBonus.transform.GetChild(i).gameObject.GetComponent<RectTransform>().sizeDelta = this._bar;

			_material.color = new Color32(255,255,255,255);
			this._idActiveChildLight = 0;
		}
			

		if(this._velocityBonus.gameObject.activeInHierarchy)
		{
			for(int i = 0 ; i < 3;i++)
				this._velocityBonus.transform.GetChild(i).gameObject.GetComponent<RectTransform>().sizeDelta = this._bar;

			this._playerVelocity = GlobalVariables._playerVelocity;
			GameObject.Find("KaiPlayer(Clone)").gameObject.GetComponent<PlayerBehaviour>().speed = GlobalVariables._playerVelocity;
			this._idActiveChildVelocity = 0;

		}
		
		if(this._coordinationBonus.gameObject.activeInHierarchy)
		{
			GlobalVariables._changeDirection = false;
			for(int i = 0 ; i < 3;i++)
				this._coordinationBonus.transform.GetChild(i).gameObject.GetComponent<RectTransform>().sizeDelta = this._bar;
			
			this._idActiveChildCoordination = 0;
		}
        
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
	{
		this._portalPath = new List<Vector2>();
		this._searchManager = new SearchManager();
		this._decreaseValue = this._lightBonus.GetChild(0).gameObject.GetComponent<RectTransform>().sizeDelta.x/10;
		this._bar = this._lightBonus.GetChild(0).gameObject.GetComponent<RectTransform>().sizeDelta;
		this._playerVelocity = GlobalVariables._playerVelocity;
		this._material.color = new Color32(255,255,255,255);
	}

    private IEnumerator changeParametersBonus()
    {
		while(true)
		{
			
			if(this._lightBonus.gameObject.activeInHierarchy && this._idActiveChildLight!=3)
			{
				if(this._lightBonus.transform.GetChild(this._idActiveChildLight).gameObject.GetComponent<RectTransform>().sizeDelta.x - this._decreaseValue>=0)
				{
					this._lightBonus.transform.GetChild(this._idActiveChildLight).gameObject.GetComponent<RectTransform>().sizeDelta -= new Vector2(this._decreaseValue,0);
				}

				else
				{
					
					this._soundController.playSound(2);
					switch(this._idActiveChildLight)
					{
						case 2:
						_material.color = new Color32(20,20,20,255);
						_reimiText.text = "KAI estás por perder tu visión ¡Busca el bonus VERDE!";
						break;
						case 1:
						_material.color = new Color32(80,80,80,255);
						_reimiText.text = "KAI estás por perder tu visión ¡Busca el bonus VERDE!";
						break;
						case 0:
						_material.color = new Color32(172,172,172,255);
						_reimiText.text = "KAI estás por perder tu visión ¡Busca el bonus VERDE!";
						break;
					}

					this._idActiveChildLight++;
				}
			}

			if(this._velocityBonus.gameObject.activeInHierarchy && this._idActiveChildVelocity!=3)
			{
				if(this._velocityBonus.transform.GetChild(this._idActiveChildVelocity).gameObject.GetComponent<RectTransform>().sizeDelta.x - this._decreaseValue>=0)
				{
					this._velocityBonus.transform.GetChild(this._idActiveChildVelocity).gameObject.GetComponent<RectTransform>().sizeDelta -= new Vector2(this._decreaseValue,0);
				}

				else
				{
					this._soundController.playSound(2);
					switch(this._idActiveChildLight)
					{
						case 2:
						GameObject.Find("KaiPlayer(Clone)").gameObject.GetComponent<PlayerBehaviour>().speed = GlobalVariables._playerVelocity-0.6f;
						_reimiText.text = "KAI vas a perder tu movimiento ¡Busca el bonus NARANJO!";
						break;
						case 1:
						GameObject.Find("KaiPlayer(Clone)").gameObject.GetComponent<PlayerBehaviour>().speed = GlobalVariables._playerVelocity-0.4f;
						_reimiText.text = "KAI vas a perder tu movimiento ¡Busca el bonus NARANJO!";
						break;
						case 0:
						
						GameObject.Find("KaiPlayer(Clone)").gameObject.GetComponent<PlayerBehaviour>().speed = GlobalVariables._playerVelocity-0.2f;
						_reimiText.text = "KAI vas a perder tu movimiento ¡Busca el bonus NARANJO!";
						break;
					}
					

					this._idActiveChildVelocity++;
				}
			}

			if(this._coordinationBonus.gameObject.activeInHierarchy && this._idActiveChildCoordination!=3)
			{
				if(this._coordinationBonus.transform.GetChild(this._idActiveChildCoordination).gameObject.GetComponent<RectTransform>().sizeDelta.x - this._decreaseValue>=0)
				{
					this._coordinationBonus.transform.GetChild(this._idActiveChildCoordination).gameObject.GetComponent<RectTransform>().sizeDelta -= new Vector2(this._decreaseValue,0);
				}

				else
				{
					this._soundController.playSound(2);
					
					this._idActiveChildCoordination++;
					if(this._idActiveChildCoordination == 3)
					{
						GlobalVariables._changeDirection = true;
					}

					else if(this._idActiveChildCoordination == 1 || this._idActiveChildCoordination == 2)
					{
						_reimiText.text = "KAI vas a perder tu Coordinación ¡Busca el bonus GRIS!";
					}
				}
			}

			yield return new WaitForSeconds(_initializeChangeParametersDelay);

		}
		
    }

	internal void stopCoroutine()
    {
		StopCoroutine(_changeParametersCoroutine);
		//Light Childs
		for(int i = 0 ; i < 3;i++)
			this._lightBonus.transform.GetChild(i).gameObject.GetComponent<RectTransform>().sizeDelta = this._bar;

		for(int i = 0 ; i < 3;i++)
			this._velocityBonus.transform.GetChild(i).gameObject.GetComponent<RectTransform>().sizeDelta = this._bar;
		
		for(int i = 0 ; i < 3;i++)
			this._coordinationBonus.transform.GetChild(i).gameObject.GetComponent<RectTransform>().sizeDelta = this._bar;
		
		this._idActiveChildLight = 0;
		this._idActiveChildVelocity = 0;
		this._idActiveChildCoordination = 0;
		_material.color = new Color32(255,255,255,255);
    }

	internal void initializeCorroutines()
    {
		this._changeParametersCoroutine = StartCoroutine(changeParametersBonus());
        this._bonusRespawnCoroutine = StartCoroutine(bonusRespawn());
    }

    private IEnumerator bonusRespawn()
    {
		yield return new WaitForSeconds(_respawnDelay);
		while(true)
		{
			if(this._lightBonus.gameObject.activeInHierarchy)
			{
				createBonus(BonusTypes.Types.LIGHT);
				print("Creando bonus de luz");
			}
			
			if(this._velocityBonus.gameObject.activeInHierarchy)
			{
				createBonus(BonusTypes.Types.VELOCITY);
				print("Creando bonus de velocidad");
			}

			if(this._coordinationBonus.gameObject.activeInHierarchy)
			{
				createBonus(BonusTypes.Types.COORDINATION);		
			}

			if(GlobalVariables._yellowBonus)
			{
				createBonus(BonusTypes.Types.TELEPORT);
			}

			if(GlobalVariables._purpleBonus)
			{
				createBonus(BonusTypes.Types.PORTAL);
			}

			yield return new WaitForSeconds(_respawnDelay);

		}
    }

    private void createBonus(BonusTypes.Types type)
	{
		Vector2 position = searchForPosition();
		position.x = (position.x *  GlobalVariables._widthTile) - MapGeneratorController._offsetMap.x;
		position.y = (position.y *  -GlobalVariables._widthTile) - MapGeneratorController._offsetMap.y;
		
		
		
		if(type == BonusTypes.Types.LIGHT && _lightBonusInstance == null)
		{
			_lightBonusInstance = Instantiate(_bonusList[0],position, Quaternion.identity, this.gameObject.GetComponent<ViewController>()._bonusGroup.transform) as GameObject;
			_lightBonusInstance.GetComponent<Bonus>()._position = position;
		}

		else if(type == BonusTypes.Types.VELOCITY && _velocityBonusInstance == null)
		{
	
			_velocityBonusInstance = Instantiate(_bonusList[1], position, Quaternion.identity, this.gameObject.GetComponent<ViewController>()._bonusGroup.transform) as GameObject;
			_velocityBonusInstance.GetComponent<Bonus>()._position = position;
		}

		else if(type == BonusTypes.Types.COORDINATION && _coordinationBonusInstance == null)
		{
			_coordinationBonusInstance = Instantiate(_bonusList[2],position, Quaternion.identity, this.gameObject.GetComponent<ViewController>()._bonusGroup.transform) as GameObject;
			_coordinationBonusInstance.GetComponent<Bonus>()._position = position;
		}

		else if(type == BonusTypes.Types.TELEPORT && _teleportBonusInstance == null)
		{
			_reimiText.text = "KAI puedes escapar ¡Busca el bonus AMARILLO!";
			_teleportBonusInstance = Instantiate(_bonusList[3], position, Quaternion.identity, this.gameObject.GetComponent<ViewController>()._bonusGroup.transform) as GameObject;
			_teleportBonusInstance.GetComponent<Bonus>()._position = position;
		}

		else if(type == BonusTypes.Types.PORTAL && _portalBonusInstance == null && this.GetComponent<ViewController>()._portalInstance == null)
		{
			_reimiText.text = "KAI resuelve la salida ¡Busca el bonus MORADO!";
			_portalBonusInstance = Instantiate(_bonusList[4], position, Quaternion.identity, this.gameObject.GetComponent<ViewController>()._bonusGroup) as GameObject;
			_portalBonusInstance.GetComponent<Bonus>()._position = position;
		}
		
    }

    private Vector2 searchForPosition()
    {
		int iReset=0;
		int jReset=0;
		do
		{
			iReset = UnityEngine.Random.Range(0, GlobalVariables._iMaxMatrix); 
			jReset = UnityEngine.Random.Range(0, GlobalVariables._jMaxMatrix); 
		} while(ViewController._currentGameModel._map[iReset,jReset] == -1);

		return new Vector2(jReset,iReset);
    }

    public void resetBonusInstances()
	{
		_lightBonusInstance = null;
		_velocityBonusInstance = null;
		_coordinationBonusInstance = null;
		_teleportBonusInstance = null;
		_portalBonusInstance = null;
		this._playerVelocity = GlobalVariables._playerVelocity;
	}

	public void bonusCollision(BonusTypes.Types type, Vector2 position)
	{
		if(type == BonusTypes.Types.LIGHT)
		{
			this._material.color = new Color32(255,255,255,255);
			
		for(int i = 0 ; i < 3;i++)
			this._lightBonus.transform.GetChild(i).gameObject.GetComponent<RectTransform>().sizeDelta = this._bar;

			this._idActiveChildLight = 0;
		}

		else if(type == BonusTypes.Types.VELOCITY)
		{
			
			
		for(int i = 0 ; i < 3;i++)
			this._velocityBonus.transform.GetChild(i).gameObject.GetComponent<RectTransform>().sizeDelta = this._bar;

			this._playerVelocity = GlobalVariables._playerVelocity;
			GameObject.Find("KaiPlayer(Clone)").gameObject.GetComponent<PlayerBehaviour>().speed = GlobalVariables._playerVelocity;

			this._idActiveChildVelocity = 0;
			// this._velocityBonus.sizeDelta = _bar;
		}

		else if(type == BonusTypes.Types.COORDINATION)
		{
			for(int i = 0 ; i < 3;i++)
			this._coordinationBonus.transform.GetChild(i).gameObject.GetComponent<RectTransform>().sizeDelta = this._bar;

			GlobalVariables._changeDirection= false;

			this._idActiveChildCoordination = 0;
			// this._coordinationBonus.sizeDelta = _bar;
		}

		else if(type == BonusTypes.Types.TELEPORT)
		{
			this.GetComponent<ViewController>().createPlayer();
		}

		else if(type == BonusTypes.Types.PORTAL)
		{
			
			this._portalPath.Clear();
			GetComponent<MechanicController>().stopPortalCoroutine();
			GetComponent<MechanicController>().putPortal();
			Vector2 portalPosition = GetComponent<ViewController>()._portalInstance.GetComponent<Bonus>()._position;
			this._portalPath = _searchManager.encontrarCamino(position, portalPosition);
			imprimir();
			drawShapePath();
		}

	}

    private void drawShapePath()
    {
        for(int i = 0 ; i < this._portalPath.Count ; i++)
			Destroy(Instantiate(_shape,new Vector2(_portalPath[i].x * GlobalVariables._widthTile,_portalPath[i].y*-GlobalVariables._widthTile) - MapGeneratorController._offsetMap, Quaternion.identity,this._portalGroupGameObject) as GameObject,10f);
    }

	public void imprimir()
	{
		for(int i = 0 ; i < _portalPath.Count;i++)
		{
			print(_portalPath[i]);
		}
	}
}