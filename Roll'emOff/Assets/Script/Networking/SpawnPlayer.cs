using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] private float spawnPosMinx;
    [SerializeField] private float spawnPosMaxx;
    [SerializeField] private float spawnPosMinz;
    [SerializeField] private float spawnPosMaxz;
    [SerializeField] private GameObject spawnObj;
    void Start()
    {
        Vector3 randomPosition = new Vector3(Random.Range(spawnPosMinx, spawnPosMaxx), 0.08f,Random.Range(spawnPosMinz, spawnPosMaxz));
        PhotonNetwork.Instantiate(spawnObj.name, randomPosition, Quaternion.identity);
    }



}
