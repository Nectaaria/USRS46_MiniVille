using Miniville;
using UnityEngine;

public class MainScenePlay : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject canva;
    public Game game;
    void Start()
    {
        //call le canva de séléction de la difficulté
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseModeCanvaFast()
    {
        game.gameTypeChoice = 1;
        canva.SetActive(false);
        
        
    }
    public void CloseModeCanvaCasual()
    {
        game.gameTypeChoice = 2;
        canva.SetActive(false);
    }
    public void CloseModeCanvaSerious()
    {
        game.gameTypeChoice = 3;
        canva.SetActive(false);
    }
    public void CloseModeCanvaHard()
    {
        game.isExpert = true;
    }
}
