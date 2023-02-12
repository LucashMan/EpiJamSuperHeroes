using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class ResItem
{
    public int horizontal;
    public int vertical;
}
public class SettingsScreen : MonoBehaviour
{
    public Toggle FullscreenTog;
    public Toggle vsyncTog;
    private int selectedResolution;
    public List<ResItem> resolution = new List<ResItem>();
    public TMP_Text resolutionLabel;

    // Start is called before the first frame update
    void Start()
    {
        bool foundRes = false;

        FullscreenTog.isOn = Screen.fullScreen;
        if (QualitySettings.vSyncCount == 0)
            vsyncTog.isOn = false;
        else
            vsyncTog.isOn = true;
        for (int i = 0; i < resolution.Count; i++) {
            if (Screen.width == resolution[i].horizontal && Screen.height == resolution[i].vertical) {
                foundRes = true;
                selectedResolution = i;
                UpdateResLabel();
            }
        }
        if (!foundRes) {
            ResItem newRes = new ResItem();
            newRes.horizontal = Screen.width;
            newRes.vertical = Screen.height;
            resolution.Add(newRes);
            selectedResolution = resolution.Count - 1;
            UpdateResLabel();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateResLabel()
    {
        resolutionLabel.text = resolution[selectedResolution].horizontal.ToString() + "X" + resolution[selectedResolution].vertical.ToString();
    }

    public void ResLeft()
    {
        selectedResolution--;
        if (selectedResolution < 0)
            selectedResolution = resolution.Count - 1;
        UpdateResLabel();
    }

    public void ResRight()
    {
        selectedResolution++;
        if (selectedResolution > resolution.Count - 1)
            selectedResolution = 0;
        UpdateResLabel();
    }


    public void ApplyGraphics()
    {
        //Screen.fullScreen = FullscreenTog.isOn;
        if (vsyncTog.isOn) {
            QualitySettings.vSyncCount = 1;
        } else {
            QualitySettings.vSyncCount = 0;
        }
        Screen.SetResolution(resolution[selectedResolution].horizontal, resolution[selectedResolution].vertical, FullscreenTog.isOn);
    }

}
