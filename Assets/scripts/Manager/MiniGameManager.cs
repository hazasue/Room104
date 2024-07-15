using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : Singleton<MiniGameManager>
{
    private Dictionary<string, MiniGame> miniGames;
    
    // Start is called before the first frame update
    public override void Awake()
    {
        base.Awake();
        init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void init()
    {
        miniGames = new Dictionary<string, MiniGame>();
    }

    public void ActivateGame(string name)
    {
        MiniGame miniGame;
        if (!miniGames.TryGetValue(name, out miniGame))
        {
            Debug.Log($"There's no any mini game named \'{name}\'");
            return;
        }

        miniGame.gameObject.SetActive(false);
        miniGame.Activate(0f); // argument: safety
    }

    public void GetCurrentMiniGameState(MiniGame miniGame, bool state)
    {
        miniGame.gameObject.SetActive(false);
        
        if (state)
        { 
            //clear();
        } 
        else
        {
            //failed();
        }
    }
}
