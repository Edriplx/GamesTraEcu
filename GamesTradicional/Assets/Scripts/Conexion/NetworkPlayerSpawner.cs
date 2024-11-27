using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.XR.CoreUtils;

public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
{
    public Transform hostSpawnPoint;
    public Transform clientSpawnPoint;
    public XROrigin xrOriginHost; // Referencia al XR Origin del host
    public XROrigin xrOriginClient; // Referencia al XR Origin del cliente

    private GameObject spawnedPlayerPrefab;

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        Vector3 spawnPosition;
        Quaternion spawnRotation;

        if (PhotonNetwork.IsMasterClient)
        {
            spawnPosition = hostSpawnPoint.position;
            spawnRotation = hostSpawnPoint.rotation;
            Debug.Log("Spawning host at: " + spawnPosition);

            // Activar XR Origin del host y desactivar el del cliente
            xrOriginHost.gameObject.SetActive(true);
            xrOriginClient.gameObject.SetActive(false);
            MoveXROrigin(xrOriginHost, spawnPosition, spawnRotation);
        }
        else
        {
            spawnPosition = clientSpawnPoint.position;
            spawnRotation = clientSpawnPoint.rotation;
            Debug.Log("Spawning client at: " + spawnPosition);

            // Activar XR Origin del cliente y desactivar el del host
            xrOriginClient.gameObject.SetActive(true);
            xrOriginHost.gameObject.SetActive(false);
            MoveXROrigin(xrOriginClient, spawnPosition, spawnRotation);
        }

        Debug.Log("Attempting to instantiate player at: " + spawnPosition);
        spawnedPlayerPrefab = PhotonNetwork.Instantiate("Network Player", spawnPosition, spawnRotation);
        Debug.Log("Player instantiated at: " + spawnedPlayerPrefab.transform.position);
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.Destroy(spawnedPlayerPrefab);
    }

    private void MoveXROrigin(XROrigin xrOrigin, Vector3 position, Quaternion rotation)
    {
        if (xrOrigin != null)
        {
            xrOrigin.transform.position = position;
            xrOrigin.transform.rotation = rotation;
            Debug.Log("XR Origin moved to: " + position);
        }
        else
        {
            Debug.LogError("XR Origin reference is missing.");
        }
    }
}
