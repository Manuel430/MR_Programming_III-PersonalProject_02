using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MR_HunterSpawnScript : MonoBehaviour
{
    [SerializeField] GameObject _Hunter;

    public void HunterSpawn()
    {
        Instantiate(_Hunter,transform.position,Quaternion.identity);
    }
}
