using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    [SerializeField]
    private AudioClip _pickUpSound;
    UIManager _uiManager;

    private void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Player _player = other.GetComponent<Player>();
            if (_uiManager != null && _player != null)
            {
                _uiManager.ShowMessageText("Press 'E' to collect the coin!");

                if (Input.GetKeyDown(KeyCode.E))
                {
                    _player.hasCoin = true;
                    _uiManager.HideMessageText();

                    AudioSource.PlayClipAtPoint(_pickUpSound, Camera.main.transform.position, 0.1f);
                    Destroy(this.gameObject);
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
