using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [SerializeField]
    private Text _ammoText;
    [SerializeField]
    private Text _messageText;
    [SerializeField]
    private GameObject _imageCoin;
    public bool infoTextIsEnabled = false;

    public void UpdateAmmo(int ammoCount)
    {
        HideMessageText();
        if (ammoCount == -1)
        {
            _ammoText.text = "Reloading...";
        }
        else
        {
            if (ammoCount == 0)
            {
                ShowMessageText("Press 'R' to reload!");                
            }
            _ammoText.text = "Ammo : " + ammoCount;
        }
    }

    public void ShowMessageText(string message) {
        _messageText.text = message;
        _messageText.enabled = true;
        infoTextIsEnabled = _messageText.enabled;
    }
    public void HideMessageText()
    {
        _messageText.text = "";
        _messageText.enabled = false;
        infoTextIsEnabled = _messageText.enabled;
    }

    public void CollectedCoin(bool isEnabled)
    {
        _imageCoin.SetActive(isEnabled);
    }
}
