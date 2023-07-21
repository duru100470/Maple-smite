using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerModel _playerModel;
    [SerializeField]
    private KeyInputSender _keyInputSender;
    [SerializeField]
    private TreeController _treeController;
    private bool _canAttack = true;

    public void Init(PlayerModel playerModel)
    {
        _playerModel = playerModel;

        _keyInputSender.OnKeyPressed += PressKey;
    }

    private void PressKey(KeyType keyType)
    {
        switch (keyType)
        {
            case KeyType.Attack:
                if (!_canAttack) break;
                _treeController.GetDamage(_playerModel.Damage);
                StartCoroutine(DelayAttack());
                break;
        }
    }

    private IEnumerator DelayAttack()
    {
        _canAttack = false;
        yield return new WaitForSeconds(_playerModel.AttackSpeed);
        _canAttack = true;
    }
}