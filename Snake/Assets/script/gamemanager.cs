using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gamemanager : MonoBehaviour
{
	public void Restart()
	{
		SceneManager.LoadScene("snake");
	}
}
