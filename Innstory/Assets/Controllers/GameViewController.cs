using UnityEngine;
using System.Collections.Generic;
using System;
using System.Text;
using UnityEngine.UI;

public class GameViewController : MonoBehaviour {

    private Queue<Transform> _cardPlaceholders;

    ///public CardManager CardMgr;
    
    public Transform MissilePrefab;

    public Transform TargetSelector;

    public Transform ConstructionPanelPrefab;
    public Transform GameLogEntryPrefab;
    public Transform GameLogContent;
    public Transform GameLog;
    public Transform DeckSelectDialog;
    public Text DeckShowText;

    // controls which need to be activated/deactivated depending on state
    public Transform ClickForCreditsButton;

    public Transform ClickForCardsButton;
    public Transform EndPhaseButton;

    // gui representation of Game position
    public Transform OpponentNameGUI;
    public Transform OpponentCreditsGUI;
    public Transform OpponentClicksGUI;
    public Transform OpponentCardsInHandGUI;
    public Transform OpponentCardsInDeckGUI;
    public Transform OpponentCardsInDiscardGUI;
    public Transform OpponentConstructionAreaGUI;
    public Transform OpponentShipAreaGUI;
    public Transform OpponentMissileAreaGUI;
    public Transform PlayerNameGUI;
    public Transform PlayerCreditsGUI;
    public Transform PlayerClicksGUI;
    public Transform PlayerCardsInDeckGUI;
    public Transform PlayerCardsInDiscardGUI;
    public Transform PlayerHandGUI;
    public Transform PlayerConstructionAreaGUI;
    public Transform PlayerShipAreaGUI;
    public Transform PlayerMissileAreaGUI;
    public Transform GamePhaseGUI;

    public Transform MissionAreaGUI;
    public Transform CharacterAreaGUI;
    public Transform OpponentMissionAreaGUI;
    public Transform OpponentCharacterAreaGUI;


    public Dictionary<string, Transform> _transformById;

    void Start()
    {
        _transformById = new Dictionary<string, Transform>();
        _cardPlaceholders = new Queue<Transform>();
        CardManager.instance.Init();
        
    }

    internal void EnterTargetingMode()
    {
        // instantiate targeting cursor
        var selector = Instantiate(TargetSelector);
        selector.SetParent(PlayerHandGUI.parent);
    }

    public void AddCharacter(CharacterCard characterCard, bool belongsToPlayer)
    {
        // instantiate
        Transform homeworldTransform = InstantiateCardPrefab(characterCard, belongsToPlayer);

        // position 
        Transform characterArea = (belongsToPlayer ? CharacterAreaGUI : OpponentCharacterAreaGUI);
        homeworldTransform.SetParent(characterArea);
    }

    public void DestroyCardTransform(Card card)
    {
        Transform transform = _transformById[card.ID.ToString()];
        Destroy(transform.gameObject);
    }

    public void MoveToConstructionArea(Card card, bool belongsToPlayer)
    {
        Transform transform = _transformById[card.ID.ToString()];
        Transform constructionArea = (belongsToPlayer ? PlayerConstructionAreaGUI : OpponentConstructionAreaGUI);
        transform.SetParent(constructionArea);
    }

    //public void AddOperation(Operation operation, bool belongsToPlayer)
    //{
    //    // instantiate
    //    Transform operationTransform = InstantiateCardPrefab(operation, belongsToPlayer);

    //    // position 
    //    Transform constructionArea = (belongsToPlayer ? PlayerConstructionAreaGUI : OpponentConstructionAreaGUI);
    //    operationTransform.SetParent(constructionArea);
    //}

    //public void AddShipyard(Shipyard shipyard, bool belongsToPlayer)
    //{        
    //    // instantiate
    //    Transform shipyardTransform = InstantiateCardPrefab(shipyard, belongsToPlayer);

    //    // position 
    //    Transform constructionArea = (belongsToPlayer ? PlayerConstructionAreaGUI : OpponentConstructionAreaGUI);
    //    shipyardTransform.SetParent(constructionArea);        
    //}

    public void hostMissionCard(MissionCard _card, bool belongsToPlayer)
    {
        Transform missionTrans;
        if (belongsToPlayer)
        {
            missionTrans = _transformById[_card.ID.ToString()];
        }
        else
        {
            missionTrans = CardManager.instance.CreateCardPrefab(_card, _card.prefabPath, false);
        }


    }


    //public void HostShip(Ship ship, Shipyard shipyard, bool belongsToPlayer)
    //{
    //    Transform shipTransform;
    //    if (belongsToPlayer)
    //    {
    //        // will already be in hand, just find it
    //        shipTransform = _transformById[ship.CardId];
    //    }
    //    else
    //    {
    //        // will not exist yet, need to instantiate
    //        shipTransform = InstantiateCardPrefab(ship, false);
    //    }
    //    Transform shipyardTransform = FindCardTransformById(shipyard.CardId);
        
    //    // add "construction remaining" overlay
    //    var constructionPanelPrefab = Instantiate(ConstructionPanelPrefab);
    //    constructionPanelPrefab.name = "ConstructionPanel";
    //    var constructionRemaining = (Text)constructionPanelPrefab.Find("ConstructionRemaining").GetComponent(typeof(Text));
    //    constructionRemaining.text = ship.ConstructionRemaining.ToString();

    //    if (belongsToPlayer == false)
    //    {
    //        // deactivate the "advance" button
    //        Button button = constructionPanelPrefab.Find("AdvanceConstructionButton").GetComponent<Button>();
    //        button.transform.localScale = new Vector3();
    //        button.interactable = false;            
    //    }

    //    constructionPanelPrefab.SetParent(shipTransform);
    //    constructionPanelPrefab.localPosition = new Vector3(0, 0, 0);
        
    //    // position the ship on the shipyard
    //    shipTransform.SetParent(shipyardTransform);
    //    shipTransform.localPosition = new Vector3(15, 15, 0);
    //}

  
    public void UpdateGamePhase(GamePhase gamePhase)
    {
        SetText(GamePhaseGUI, gamePhase.ToString().Replace('_', ' '));
    }

    private void SetText(Transform guiElement, string value)
    {
        // TODO - check this is actually a Text element?
        guiElement.GetComponent<Text>().text = value;
    }

    public void SetPlayerName(string name)
    {
        SetText(PlayerNameGUI, name);
    }

    public void SetOpponentName(string name)
    {
        SetText(OpponentNameGUI, name);
    }

    public void SetDeckDialogText(string txt)
    {
        DeckShowText.text = txt;
    }

    public void HideDeckSelectDialog()
    {
        DeckSelectDialog.gameObject.SetActive(false);
    }

    public void EnableDisableControls(GamePhase gamePhase, bool clicksRemain, bool waitingForOpponent)
    {
        //Debug.Log("EnableDisableControls");
        ((Button)ClickForCreditsButton.GetComponent(typeof(Button))).interactable = clicksRemain;
        ((Button)ClickForCardsButton.GetComponent(typeof(Button))).interactable = clicksRemain;

        // some controls only available when in logistics planning and clicks remain
        bool planningClicksRemain = gamePhase == GamePhase.LOGISTICS_PLANNING && clicksRemain;

        ((Button)EndPhaseButton.GetComponent(typeof(Button))).interactable = waitingForOpponent == false &&
            (
                gamePhase == GamePhase.COMBAT_PLANNING
            || (gamePhase == GamePhase.LOGISTICS_PLANNING && clicksRemain == false)
            );

        // handle "advance construction" buttons
        //var constructionHandlers = FindObjectsOfType<AdvanceConstructionHandler>();
        //foreach (AdvanceConstructionHandler handler in constructionHandlers)
        //{
        //    (handler.GetComponent<Button>()).interactable = planningClicksRemain ||
        //        (gamePhase == GamePhase.LOGISTICS_PLANNING && handler.IsComplete());
        //}

        //// handle weapon buttons
        //var weaponHandlers = FindObjectsOfType<WeaponHandler>();
        //foreach (WeaponHandler handler in weaponHandlers)
        //{
        //    (handler.GetComponent<Button>()).interactable = (handler.BelongsToPlayer) &&
        //        (gamePhase == GamePhase.COMBAT_PLANNING);
        //}
    }







    public void AddCardToHand(Card card, bool replacesUnknown)
    {
        // instantiate the card prefab
        Transform cardPrefab = InstantiateCardPrefab(card, true);

        // add it to the players hand area
        cardPrefab.SetParent(PlayerHandGUI);

        if (replacesUnknown)
        {
            // remove the placeholder face down card
            Destroy(_cardPlaceholders.Dequeue().gameObject);
        }
    }

    public void AddMissionCardToHand(Card _card)
    {
        var cardPrefab = CardManager.instance.CreateCardPrefab(_card, _card.prefabPath, true);
        cardPrefab.SetParent(PlayerHandGUI);
        _cardPlaceholders.Enqueue(cardPrefab);
    }

    public void AddUnknownCardToHand()
    {
        var  cardPrefab = CardManager.instance.CreateCardPrefab(null, "Prefab/1", true);
        cardPrefab.SetParent(PlayerHandGUI);
        _cardPlaceholders.Enqueue(cardPrefab);
    }


    public void DeployCard(Card card, bool belongsToPlayer)
    {
        Transform Area = (belongsToPlayer ? PlayerShipAreaGUI : OpponentShipAreaGUI);

        Transform _Transform = FindCardTransformById(card.ID.ToString());        
        _Transform.SetParent(Area);

       RemoveConstructionPanel(card);
    }


    public void RemoveConstructionPanel(Card _card)
    {
        Transform consTransform = FindCardTransformById(_card.ID.ToString());
        Destroy(consTransform.Find("ConstructionPanel").gameObject);
    }

    public void RemoveDeadCard(Card card)
    {
        Transform cardTransform = FindCardTransformById(card.ID.ToString());
        Destroy(cardTransform.gameObject);
    }

    public void AddGameLogMessage(string message)
    {
        var entry = Instantiate(GameLogEntryPrefab);
        Text t = (Text)entry.GetComponent(typeof(Text));
        t.text = message;
        entry.SetParent(GameLogContent);

        var scrollRect = (ScrollRect)GameLog.GetComponent(typeof(ScrollRect));
        scrollRect.verticalNormalizedPosition = 0f;
    }

    private Transform InstantiateCardPrefab(Card card, bool belongsToPlayer)
    {
        Transform cardPrefab = CardManager.instance.CreateCardPrefab(card, card.prefabPath, belongsToPlayer);

        // store in lookup so we can find it by its id
        _transformById[card.ID.ToString()] = cardPrefab;

        return cardPrefab;
    }

    public void UpdateOpponentStateGUI(Player opponent)
    {
        SetText(OpponentClicksGUI, opponent.Clicks.ToString());
        SetText(OpponentCreditsGUI, opponent.Credits.ToString());
        SetText(OpponentCardsInHandGUI, opponent.Hand.Count.ToString());
        SetText(OpponentCardsInDeckGUI, opponent.Deck.GetCount().ToString());
        ///SetText(OpponentCardsInDiscardGUI, opponent.Discard.Count.ToString());
    }

    public void UpdatePlayerStateGUI(Player player)
    {
        SetText(PlayerClicksGUI, player.Clicks.ToString());
        SetText(PlayerCreditsGUI, player.Credits.ToString());
        SetText(PlayerCardsInDeckGUI, player.Deck.GetCount().ToString());
        ///SetText(PlayerCardsInDiscardGUI, player.Discard.Count.ToString());
    }

    private Transform FindCardTransformById(string cardId)
    {
        return _transformById[cardId];
    }

    
}
