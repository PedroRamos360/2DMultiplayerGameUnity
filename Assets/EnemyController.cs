using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class EnemyController : MonoBehaviour
{
    private GameObject[] players;
    private GameObject targetPlayer;
    private bool reachedPlayer = false;

    void Update()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        targetPlayer = GetClosestPlayer();
        MoveTowardsPlayer();
    }

    GameObject GetClosestPlayer()
    {
        float minDistance = Mathf.Infinity;
        GameObject closestPlayer = null;
        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestPlayer = player;
            }
        }
        return closestPlayer;
    }

    void MoveTowardsPlayer()
    {
        if (targetPlayer != null && !reachedPlayer)
            transform.position = Vector3.MoveTowards(transform.position, targetPlayer.transform.position, 0.01f);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            reachedPlayer = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            reachedPlayer = false;
        }
    }
}
