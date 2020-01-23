using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkShop : MonoBehaviour {

    private UIManager _uiManager;
    [SerializeField]
    private AudioClip _winSoundClip;

    private void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player") {
            Player _player = other.GetComponent<Player>();
            if (_player != null && _uiManager != null)
            {
                if (_player.hasWeapon)
                {
                    _uiManager.ShowMessageText("What are you waiting for? Go shoot something!");
                }
                else
                {
                    if (!_player.hasCoin)
                    {
                        _uiManager.ShowMessageText("You need money to buy some 'fish'...");
                    }
                    else
                    {
                        _uiManager.ShowMessageText("Press 'E' to buy a 'fish'!");
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            _player.hasCoin = false;
                            _player.hasWeapon = true;
                            AudioSource.PlayClipAtPoint(_winSoundClip, Camera.main.transform.position, 0.1f);
                        }
                    }
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (_uiManager != null && _uiManager.infoTextIsEnabled)
        {
            _uiManager.HideMessageText();
        }
    }
}
