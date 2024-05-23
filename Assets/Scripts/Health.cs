using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private GameObject player;
    private Text healthText;

    void Start()
    {
        GetCorrespondingPlayer();
        healthText = GetComponent<Text>();
    }

    void Update()
    {
        if (player != null)
        {
            healthText.text = player.GetComponent<PlayerControl>().GetHealth().ToString();
        }
    }

    public void SetHealth(int health)
    {
        healthText.text = health.ToString();
    }

    public void GetCorrespondingPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject p in players)
        {
            if (p.GetComponent<PhotonView>().IsMine)
            {
                player = p;
                break;
            }
        }
    }
}
