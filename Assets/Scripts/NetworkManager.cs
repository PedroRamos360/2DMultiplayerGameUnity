using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [PunRPC]
    public void DestroyNetworkObject(int viewID)
    {
        PhotonView photonView = PhotonView.Find(viewID);
        if (photonView != null && photonView.IsMine)
        {
            PhotonNetwork.Destroy(photonView.gameObject);
        }
    }
}
