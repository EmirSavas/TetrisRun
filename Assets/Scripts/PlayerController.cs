using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    List<GameObject> piecesPos = new List<GameObject>();
    GameObject bestTarget = null;
    float newRot = 0f;
    private Pieces btPiece;
    
    public GameObject winPanel;
    public GameObject losePanel;

    public SwipeController swipe;
    
    GameObject GetClosestPiece (List<GameObject> pieces)
    {
        
        float closestPiece = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        
        foreach(GameObject potentialTarget in pieces)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float newDis = directionToTarget.sqrMagnitude;
            if(newDis < closestPiece)
            {
                closestPiece = newDis;
                bestTarget = potentialTarget;
            }
        }
        return bestTarget;
    }
    
    private void Start()
    {
        Time.timeScale = 1f;
        
        GameObject[] pieces = GameObject.FindGameObjectsWithTag("Piece");

        for (int i = 0; i < pieces.Length; i++)
        {
            piecesPos.Add(pieces[i]);
        }
    }

    private void FixedUpdate()
    {
        gameObject.transform.Translate(Vector3.forward * 3f *Time.deltaTime, Space.World);

        
    }

    private void Update()
    {

        GetClosestPiece(piecesPos);

        if (Vector3.Distance(gameObject.transform.position,bestTarget.transform.position) < 4.5f)
        {
            if (bestTarget.GetComponent<Pieces>().isLanded == false)
            {
                Dead();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            swipe.releasePoint = Input.mousePosition;
            btPiece = bestTarget.GetComponent<Pieces>();
            
            if (swipe.isDraging)
            {
                float dist = Vector3.Distance(swipe.firstPoint, swipe.releasePoint);
                

                if (Mathf.Abs(dist) > 100 && bestTarget.GetComponent<Pieces>().isLanded == false)
                {
                    bestTarget.transform.position = new Vector3(bestTarget.transform.position.x, 0, bestTarget.transform.position.z);
                    btPiece.isLanded = true;
                    piecesPos.Remove(bestTarget);
                    GetClosestPiece(piecesPos);
                    swipe.Reset();
                }
                
                else if (bestTarget.GetComponent<Pieces>().isLanded == false)
                {
                    GetClosestPiece(piecesPos);
                    bestTarget.transform.Rotate(newRot - 90,bestTarget.transform.rotation.y,bestTarget.transform.rotation.z);
                }
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Piece"))
        {
            Dead();
        }
        
        else if (other.gameObject.CompareTag("GameEnd"))
        {
            winPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    private void Dead()
    {
        losePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Replay()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
