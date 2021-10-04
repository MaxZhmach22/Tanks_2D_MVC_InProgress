using System.Collections;
using System.Collections.Generic;
using Tanks;
using UnityEngine;
using UnityEngine.UI;

public class FireButtonScript : MonoBehaviour
{
    private Button _fireButton;
    [SerializeField] private Player _player;
    void Start()
    {
        _fireButton = GetComponent<Button>();
        _fireButton.onClick.AddListener(_player.Fire);
    }

    private void OnDestroy()
    {
        _fireButton.onClick.RemoveListener(_player.Fire);
    }
}
