using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
	// public Dropdown dropdown;

	Resolution [] resolutions;
	public Dropdown resolutionDropdown;
	public Dropdown qualityDropdown;

	public void SetQuality (int qualityIndex) {
		QualitySettings.SetQualityLevel (qualityIndex);
	}

	public void SetResolution (int resolutionIndex) {
		Resolution res = resolutions[resolutionIndex];
		Screen.SetResolution (res.width, res.height, Screen.fullScreen);
	}

	void Start () {

		//resolution stuff
		resolutions = Screen.resolutions;
		resolutionDropdown.ClearOptions ();
		List <string> resolutionOptions = new List <string>();

		int currentResolutionIndex = 0;
		for (int i = 0; i < resolutions.Length; i++) {
			string option = resolutions[i].width + " x " + resolutions[i].height;
			resolutionOptions.Add (option);

			if (resolutions [i].width == Screen.currentResolution.width && resolutions [i].height == Screen.currentResolution.height) {
				currentResolutionIndex = i;
			}
		}
		resolutionDropdown.AddOptions (resolutionOptions);
		resolutionDropdown.value = currentResolutionIndex;
		resolutionDropdown.RefreshShownValue ();

		//quality stuff
		int qualityLevel = QualitySettings.GetQualityLevel ();
		qualityDropdown.value = qualityLevel;
		qualityDropdown.RefreshShownValue ();
	}
}
