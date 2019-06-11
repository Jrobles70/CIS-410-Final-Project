using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelOpener : MonoBehaviour
{
	public GameObject PanelToBeOpened;
	public GameObject [] PanelsToBeClosed;
   
  	public void OpenPanel()
   	{
   		PanelToBeOpened.SetActive (true);
   		foreach (GameObject Panel in PanelsToBeClosed) {
   			Panel.SetActive (false);
   		}
   	}
}
