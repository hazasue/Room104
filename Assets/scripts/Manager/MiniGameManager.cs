using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MiniGameManager : Singleton<MiniGameManager>
{
    private Dictionary<string, MiniGame> miniGames;

    public Image clearScreen;

    public MiniGame doorLock;
    public MiniGame doorInvade;
    public MiniGame windowInvade;
    public MiniGame hiddenCatch;
    
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

        miniGames.Add("door lock", doorLock);
        miniGames.Add("door invade", doorInvade);
        miniGames.Add("window invade", windowInvade);
        miniGames.Add("hidden catch", hiddenCatch);

        ActivateGame("hidden catch");
    }

    public void ActivateGame(string name)
    {
        MiniGame miniGame;
        if (!miniGames.TryGetValue(name, out miniGame))
        {
            Debug.Log($"There's no any mini game named \'{name}\'");
            return;
        }

        miniGame.gameObject.SetActive(true);
        miniGame.Activate(0f); // argument: safety
    }

    public void GetCurrentMiniGameState(MiniGame miniGame, bool state)
    {
        miniGame.gameObject.SetActive(false);
        
        if (state)
        {
            //clear();
            StartCoroutine(activateClearScreen("img_minigame_success", 0.5f));
        } 
        else
        {
            //failed();
            StartCoroutine(activateClearScreen("img_minigame_failed", 0.5f));
        }
    }

    private IEnumerator activateClearScreen(string screenName, float duration)
    {
        clearScreen.gameObject.SetActive(true);

        clearScreen.sprite = Resources.Load<Sprite>($"Sprites/MiniGame/{screenName}");

        yield return new WaitForSeconds(duration);
        
        clearScreen.gameObject.SetActive(false);
    }
}
