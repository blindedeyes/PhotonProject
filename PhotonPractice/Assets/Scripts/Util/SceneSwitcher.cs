﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneSwitcher : MonoBehaviour {
	public void SwitchScenes(string sceneName){
		SceneManager.LoadScene(sceneName);
	}
	public void SwitchScenesID(int id){
		SceneManager.LoadScene(id);
	}

}
