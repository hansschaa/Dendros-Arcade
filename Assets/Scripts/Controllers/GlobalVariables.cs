﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables 
{
	public static int _currentLevel = 0;
	public static int _currentLifes = 3;

	public static int _iMaxMatrix = 11;
	public static int _jMaxMatrix = 14;

	public static float _widthTile = 0.16f;

	public static int _totallyStages;

	public static bool _gameComplete = false;
	public static bool _iconStagesAdded= false;
	public static bool _yellowBonus = false;
	public static bool _purpleBonus = false;
	public static bool _changeDirection = false;

	public static int _xPosEnemy = 8;
	public static int _yPosEnemy = 2;

	public static int _xPosPlayer = 0;
	public static int _yPosPlayer = 0;

	public static float _playerVelocity = 0.5f;
	public static List<int> _walkablesTiles = new List<int>();
	public static bool _followPlayer = true;


	//Keys
	public static KeyCode _enter = KeyCode.Return;
	public static KeyCode _left = KeyCode.A;
	public static KeyCode _right = KeyCode.D;
	public static KeyCode _down = KeyCode.S;
	public static KeyCode _up = KeyCode.W;
}
