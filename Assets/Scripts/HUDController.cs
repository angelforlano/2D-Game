using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HUDController : MonoBehaviour
{
    public Image hpBar;
    public Text coinsText;

    public Image keyImage01;
    public Image keyImage02;
    public Image keyImage03;
    
    // Singleton....
    public static HUDController Instance;

    void Awake()
    {
        Instance = this;
    }
    // End Singleton region

    public void UpdateHud(Player _playerInstance)
    {
        hpBar.fillAmount =  _playerInstance.HpPercent;
        coinsText.text = _playerInstance.coins.ToString();

        if (_playerInstance.keys == 1)
        {
            keyImage01.gameObject.SetActive(true);
        }

        if (_playerInstance.keys == 2)
        {
            keyImage02.gameObject.SetActive(true);
        }

        if (_playerInstance.keys == 3)
        {
            keyImage03.gameObject.SetActive(true);
        }
    }
}