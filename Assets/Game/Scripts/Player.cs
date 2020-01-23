using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private CharacterController _controller;
    [SerializeField]
    private GameObject _muzzleFlash;
    [SerializeField]
    private GameObject _hitMarkerPrefab;
    [SerializeField]
    private float _speed = 3.5f;
    private float _gravity = 9.81f;
    [SerializeField]
    private int currentAmmo;
    private int maxAmmo = 50;
    private bool _isReloading = false;
    private UIManager _uiManager;
    [SerializeField]
    public bool hasCoin;
    [SerializeField]
    public bool hasWeapon;
    [SerializeField]
    private GameObject _weaponPrefab;
    // Use this for initialization
    void Start () {
        _controller = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        currentAmmo = maxAmmo;
        _weaponPrefab.SetActive(false);
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }
	
	// Update is called once per frame
	void Update () {
        _uiManager.CollectedCoin(hasCoin);

        if (hasWeapon) {
            _weaponPrefab.SetActive(true);
            ActivateWeaponControls();
        }
        
        if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        
        CalculateMovement();
        
    }

    private void ActivateWeaponControls()
    {
        if (Input.GetMouseButton(0) && currentAmmo > 0)
        {
            Shoot();
        }
        else
        {
            _muzzleFlash.SetActive(false);
        }
        if (Input.GetKey(KeyCode.R) && _isReloading == false)
        {
            _isReloading = true;
            _uiManager.UpdateAmmo(-1);
            StartCoroutine("Reload");
        }
    }

    void CalculateMovement() {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
        Vector3 velocity = direction * _speed;
        velocity.y -= _gravity;

        //to sync my Local values with the Global values
        velocity = transform.transform.TransformDirection(velocity);
        _controller.Move(velocity * Time.deltaTime);
    }

    void Shoot() {
        currentAmmo--;
        _uiManager.UpdateAmmo(currentAmmo);
        _muzzleFlash.SetActive(true);


        Ray origin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        RaycastHit hitInfo;
        if (Physics.Raycast(origin, out hitInfo))
        {
            
            Debug.Log("Raycast hit " + hitInfo.transform.name);
            Instantiate(_hitMarkerPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));

            //check if we hit the crate
            Destructable crate = hitInfo.transform.GetComponent<Destructable>();
            if (crate != null)
            {
                crate.DestroyCrate();
            }
        }
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(1.5f);
        currentAmmo = maxAmmo;
        _uiManager.UpdateAmmo(currentAmmo);
        _isReloading = false;
    }
}
