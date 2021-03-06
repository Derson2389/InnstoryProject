﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;
using System;
using System.Text;
using UnityEngine.UI;

public class GameClientController : NetworkBehaviour {

    public GameViewController GameViewController;
    public LobbyController LobbyController;

    private NetworkClient _client;
    private Game _game; // local game position
    private List<PlayerAction> _actions;

    public Action<Card> TargetingCallback;

    internal void EnterTargetingMode()
    {
        GameViewController.EnterTargetingMode();
    }

    void Start()
    {
        _actions = new List<PlayerAction>();
    }
    
    public override void OnStartClient()
    {
        base.OnStartClient(); // base implementation is currently empty

        _client = NetworkManager.singleton.client;

        Debug.Log("GameClientController _client=" + _client.ToString());

        _client.RegisterHandler((short)MessageTypes.MessageType.DECK_FIRST, OnDeckFirstMessage);
        _client.RegisterHandler((short)MessageTypes.MessageType.DECK_FRAGMENT, OnDeckFragmentMessage);
        _client.RegisterHandler((short)MessageTypes.MessageType.SETUP_GAME, OnSetupGameMessage);
        _client.RegisterHandler((short)MessageTypes.MessageType.DRAWN_CARD, OnDrawnCardMessage);
        _client.RegisterHandler((short)MessageTypes.MessageType.GAME_LOG, OnGameLogMessage);
        _client.RegisterHandler((short)MessageTypes.MessageType.ACTIONS, OnActionsMessage);
        _client.RegisterHandler((short)MessageTypes.MessageType.S2C_MISSION_SETTLEMENT, OnMissionSettleMentMessage);
    }

    private void OnDeckFirstMessage(NetworkMessage netMsg)
    {
        var msg = netMsg.ReadMessage<MessageTypes.DeckFirstMessage>();

        // set data
        LobbyController.Opponent.DeckData = msg.deckDataFragment;

    }

    private void OnDeckFragmentMessage(NetworkMessage netMsg)
    {
        var msg = netMsg.ReadMessage<MessageTypes.DeckFragmentMessage>();

        // append data
        LobbyController.Opponent.DeckData += msg.deckDataFragment;
    }

    private void OnMissionSettleMentMessage(NetworkMessage netMsg)
    {
        var msg = netMsg.ReadMessage<MessageTypes.DeckEnvCardMessage>();

        msg.deckDataEnvCard = ""/*deckData.Substring(0, Math.Min(deckData.Length, 1024))*/;
        GameViewController.HideDeckSelectDialog();
     
        ////// add to local version of game state 
        //CardType cardTypeName = (CardType)Enum.Parse(typeof(CardType), cardType);
        //Card card = (Card)CardManager.instance.GetCardByType(cardTypeName, int.Parse(cardId));
        //_game.Player.Hand.Add(card);

        //GameViewController.AddCardToHand(card, true);

        // update player state gui
        ///UpdatePlayerStateGUI();

        ///NetworkServer.SendToClient(_game.Player.ConnectionId, (short)MessageTypes.MessageType.S2C_MISSION_SETTLEMENT, msg);
    }

    private void OnActionsMessage(NetworkMessage netMsg)
    {
        var msg = netMsg.ReadMessage<MessageTypes.ActionsMessage>();
        ProcessOpponentActions(msg.actionData);

        if (_game.GamePhase == GamePhase.LOGISTICS_PLANNING)
        {
            // for now, advance straight to combat planning
            _game.StartLogisticsResolutionPhase();
            _game.StartCombatPlanningPhase();
        }
        else if (_game.GamePhase == GamePhase.COMBAT_PLANNING)
        {
            //_game.StartLaserWeaponResolutionPhase();
            //ProcessLaserWeapons();
        }

        GameViewController.UpdateGamePhase(_game.GamePhase);
        GameViewController.EnableDisableControls(_game.GamePhase, false, _game.IsAwaitingOpponent());
    }

    private void ProcessLaserWeapons()
    {

        // apply damage from all laser weapons

        // apply damage from all laser weapons
        ///ProcessLaserWeaponsForPlayer(_game.Player);
        ///ProcessLaserWeaponsForPlayer(_game.Opponent);

        // now that all damage has been applied, remove any dead ships
        DestroyDeadCardsForPlayer(_game.Player);
        DestroyDeadCardsForPlayer(_game.Opponent);
    }

  
    private void DestroyDeadCardsForPlayer(Player player)
    {
        //foreach (Ship ship in player.Ships)
        //{
        //    if (ship.CurrentHealth < 1)
        //    {
        //        ClearAnythingTargeting(ship);
        //        player.Ships.Remove(ship);                
        //        GameViewController.RemoveDeadCard(ship);
        //    }
        //}

        //foreach (Shipyard shipyard in player.Shipyards)
        //{
        //    if (shipyard.CurrentHealth < 1)
        //    {
        //        ClearAnythingTargeting(shipyard);
        //        player.Shipyards.Remove(shipyard);
        //        GameViewController.RemoveDeadCard(shipyard);
        //    }
        //}
    }

    private void ClearAnythingTargeting(IDamageable target)
    {
        ClearAnythingTargetingForPlayer(target, _game.Player);
        ClearAnythingTargetingForPlayer(target, _game.Opponent);
    }

    private void ClearAnythingTargetingForPlayer(IDamageable target, Player player)
    {
        //foreach (Ship ship in player.Ships)
        //{
        //    for(int i = 0; i < ship.Weapons.Count; i++)
        //    {
        //        Weapon weapon = ship.Weapons[i];
        //        if (weapon.Target == target)
        //        {
        //            weapon.ClearTarget();
        //            GameViewController.ClearWeaponTarget(ship, i);
        //        }
        //    }
        //}
    }

    private void ProcessLaserWeaponsForPlayer(Player player)
    {
        // run through each weapon for each of the players ships and apply damage to the target if its a laser
        //foreach (Ship ship in player.Ships)
        //{
        //    foreach (Weapon weapon in ship.Weapons)
        //    {
        //        if (weapon.WeaponType == WeaponType.LASER // is a laser
        //            && weapon.Target != null              // has a target
        //            && !(weapon.Target is Homeworld))     // target is not a Homeworld (these resolve later)
        //        {
        //            weapon.Target.DealDamage(weapon.Damage);
        //            weapon.ClearTarget();
        //        }
        //    }
        //}
    }

    private void ProcessOpponentActions(string actionData)
    {
        // TODO - process these actions slowly, need to think about how to do this
        string[] data = actionData.Split('#');
        for (int i = 0; i < data.Length; i++)
        {
            ProcessOpponentAction(data[i]);

            // innefficient as sometimes unnecessary, but simple way to ensure this is up to date
            UpdateOpponentStateGUI();
            ///UpdatePlayerStateGUI();
        }
    }

    private void ProcessOpponentAction(string action)
    {
        Debug.Log(string.Format("Processing action {0}", action));
        string[] actionData = action.Split('|');
        ActionType actionType = (ActionType)Enum.Parse(typeof(ActionType), actionData[0]);
        switch (actionType)
        {
            case ActionType.CLICK_FOR_CARD:
                ProcessOpponentClickForCardAction();
                break;
            case ActionType.CLICK_FOR_CREDIT:
                ProcessOpponentClickForCreditAction();
                break;
            default:
                Debug.LogError("Unknown action type [" + actionType + "]");
                break;
        }
    }

    private void ProcessOpponentWeaponTargetAction(string shipId, int weaponIndex, string targetId)
    {
        // find ship
        ///Ship ship = (Ship)FindCardIn(shipId, _game.Opponent.Ships);

        // TODO - error if ship not active

        // TODO - error if ship does not have enough weapons (invalid index)

        // find target
        IDamageable target = null;
        if (_game.Player.Deck.Faction.Character.ID.ToString() == targetId)
        {
            target = _game.Player.Deck.Faction.Character;
        }
        else if (target == null)
        {
           /// target = FindCardIn(targetId, _game.Player.Ships); 
        }

        if (target == null)
        {
            ///target = FindCardIn(targetId, _game.Player.Shipyards);
        }

        //// TODO - error if target is not valid

        //// if we get here, all ok, set target
        //ship.Weapons[weaponIndex].SetTarget(target);
        //// need to find the handler on the weapon prefab and set the target
        //GameViewController.SetWeaponTarget(ship, weaponIndex, target);

    }

  
    private void ProcessOpponentClickForCreditAction()
    {
        _game.Opponent.ChangeClicks(-1);
        _game.Opponent.ChangeCredits(1);
        GameViewController.AddGameLogMessage(string.Format("<b>{0}</b> clicks for a credit", _game.Opponent.Name));
    }

    private void ProcessOpponentClickForCardAction()
    {
        _game.Opponent.ChangeClicks(-1);
        _game.Opponent.DrawFromDeck();        
        GameViewController.AddGameLogMessage(string.Format("<b>{0}</b> clicks for a card", _game.Opponent.Name));
    }

    private T FindCardIn<T>(string cardId, List<T> findIn) where T : Card
    {
        return findIn.Find(t => t.ID.ToString() == cardId);
    }

    private void OnGameLogMessage(NetworkMessage netMsg)
    {
        var msg = netMsg.ReadMessage<MessageTypes.GameLogMessage>();

        GameViewController.AddGameLogMessage(msg.message);
    }   
       
    private void EnableDisableControls()
    {
        GameViewController.EnableDisableControls(_game.GamePhase, _game.Player.Clicks > 0, _game.IsAwaitingOpponent());     
    }

    private void OnSetupGameMessage(NetworkMessage netMsg)
    {
        Debug.Log("Game starting");
        //var msg = netMsg.ReadMessage<MessageTypes.SetupGameMessage>();
        Deck deck = new Deck(LobbyController.Opponent.DeckData);
        
        LobbyController.Opponent.SetDeck(deck);
        _game = new Game(0, LobbyController.LocalPlayer);
        _game.AddOpponent(LobbyController.Opponent);
        _game.Setup();

        _game.Player.DrawStartingHand();
        _game.Opponent.DrawStartingHand();
        SetupInitialGameView();
        
        GameViewController.HideDeckSelectDialog();
        WriteGameTurnToLog();
        GameViewController.EnableDisableControls(_game.GamePhase, true, _game.IsAwaitingOpponent());
    }

    private void SetupInitialGameView()
    {
        GameViewController.SetPlayerName(_game.Player.Name);
        GameViewController.SetOpponentName(_game.Opponent.Name);

        ///UpdatePlayerStateGUI();
        UpdateOpponentStateGUI();

        GameViewController.AddCharacter(_game.Player.Deck.Faction.Character, true);
        GameViewController.AddCharacter(_game.Opponent.Deck.Faction.Character, false);

        //foreach (Shipyard shipyard in _game.Player)
        //{
        //    GameViewController.AddShipyard(shipyard, true);
        //}

        //foreach (Shipyard shipyard in _game.Opponent.Shipyards)
        //{
        //    GameViewController.AddShipyard(shipyard, false);
        //}

        GameViewController.UpdateGamePhase(_game.GamePhase);
    }

    private void WriteGameTurnToLog()
    {
        GameViewController.AddGameLogMessage(string.Format("<b>Turn {0}</b>", _game.GameTurn));
    }

    private void OnDrawnCardMessage(NetworkMessage netMsg)
    {
        var msg = netMsg.ReadMessage<MessageTypes.DrawnCardMessage>();
        string cardId = msg.cardId;
        string cardType = msg.CardType;
        string cardName = msg.CardCodename;
        Debug.Log(String.Format("Card drawn: {0}({1})", cardType, cardId));

        //// add to local version of game state 
        CardType cardTypeName = (CardType)Enum.Parse(typeof(CardType), cardType);
        Card card = (Card)CardManager.instance.GetCardByType(cardTypeName, int.Parse(cardId));
        _game.Player.Hand.Add(card);

        GameViewController.AddCardToHand(card, true);

        // update player state gui
        ///UpdatePlayerStateGUI();
    }

    private void UpdatePlayerStateGUI()
    {
        GameViewController.UpdatePlayerStateGUI(_game.Player);
    }

    private void UpdateOpponentStateGUI()
    {
        GameViewController.UpdateOpponentStateGUI(_game.Opponent);
    }
    
    public void PlayerReady()
    {
        // this is where we would set the deck to be the one selected in the deck selection dialog
        LobbyController.LocalPlayer.SetDeck(new Deck());

        Debug.Log("Sending deck fragment");
        string deckData = LobbyController.LocalPlayer.Deck.ToString(false);
        MessageTypes.DeckFirstMessage msgFirst = new MessageTypes.DeckFirstMessage();
        msgFirst.deckDataFragment = deckData.Substring(0, Math.Min(deckData.Length, 1024));
        NetworkManager.singleton.client.Send((short)MessageTypes.MessageType.DECK_FIRST, msgFirst);

        for (int i = 1; i * 1024 < deckData.Length; i++)
        {
            Debug.Log("Sending deck fragment");
            MessageTypes.DeckFragmentMessage msgFrag = new MessageTypes.DeckFragmentMessage();
            msgFrag.deckDataFragment = deckData.Substring(i * 1024, Math.Min((deckData.Length - i * 1024), 1024));
            NetworkManager.singleton.client.Send((short)MessageTypes.MessageType.DECK_FRAGMENT, msgFrag);
        }

        Debug.Log("Sending ready message");
        MessageTypes.PlayerReadyMessage msg = new MessageTypes.PlayerReadyMessage();
        NetworkManager.singleton.client.Send((short)MessageTypes.MessageType.PLAYER_READY, msg);

        GameViewController.SetDeckDialogText("等待对方准备。。。");
        GameViewController.HideDeckSelectDialog();

        //create mission card
        for (int i = 0; i < LobbyController.LocalPlayer.Deck.Faction.MissionCards.Count; i++)
        {
            Debug.Log("Adding fake card to hand");
            var missionCard = LobbyController.LocalPlayer.Deck.Faction.MissionCards[i];
            GameViewController.AddMissionCardToHand(missionCard);
        }
    }

    private void ChangeClicks(int change)
    {
        _game.Player.ChangeClicks(change);

        EnableDisableControls();
        ///UpdatePlayerStateGUI();
    }

    public void ClickToDraw() // not that we don't draw the cards until the end of the phase
    {
        Debug.Log("Clicking for card");
        _actions.Add(new PlayerAction(ActionType.CLICK_FOR_CARD));
        _game.Player.Deck.Draw();
        ChangeClicks(-1);        
        GameViewController.AddGameLogMessage(string.Format("<b>You</b> click for a card"));
        ///GameViewController.AddUnknownCardToHand();        
    }

    public void ClickForCredit()
    {        
        _actions.Add(new PlayerAction(ActionType.CLICK_FOR_CREDIT));
        _game.Player.ChangeCredits(1);
        GameViewController.AddGameLogMessage(string.Format("<b>You</b> click for a creidt"));
        ChangeClicks(-1);
    }

    //public bool TryAdvanceConstruction(Ship ship, Shipyard shipyard)
    //{
    //    // cant advance if already complete
    //    if (ship.ConstructionRemaining < 1)
    //        return false;

    //    // if we get here, go ahead and perform the advancement
    //    ship.AdvanceConstruction(1);
    //    _actions.Add(new AdvanceConstructionAction(ship, shipyard));
    //    ChangeClicks(-1);

    //    GameViewController.AddGameLogMessage(string.Format("<b>You</b> advance construction of {0}", ship.CardName));
    //    return true;
    //}

    //public bool TryPlayShipyard(Shipyard shipyard)
    //{
    //    // card must be in the players hand
    //    if (!_game.Player.Hand.Contains(shipyard))
    //        return false;

    //    // need enough money
    //    if (shipyard.BaseCost > _game.Player.Credits)
    //        return false;

    //    // playing costs a click
    //    if (_game.Player.Clicks < 1)
    //        return false;

    //    // if we get here, go ahead and play it
    //    _actions.Add(new ShipyardAction(shipyard));
    //    _game.Player.Shipyards.Add(shipyard);
    //    _game.Player.ChangeCredits(-shipyard.BaseCost);
    //    ChangeClicks(-1);        

    //    GameViewController.MoveToConstructionArea(shipyard, true);
    //    GameViewController.AddGameLogMessage(string.Format("<b>You</b> play {0}", shipyard.CardName));

    //    return true;
    //}

    //public bool TryDeployShip(Ship ship, Shipyard shipyard)
    //{
    //    if (shipyard.HostedShip != ship)
    //    {
    //        Debug.LogError("Trying to deploy from wrong shipyard");
    //        return false;
    //    }

    //    if (ship.ConstructionRemaining > 0)
    //    {
    //        Debug.LogError("Trying to deploy incomplete ship");
    //        return false;
    //    }

    //    // if we get here, all good
    //    shipyard.ClearHostedCard();
    //    _game.Player.Ships.Add(ship);
    //    _actions.Add(new DeployAction(ship, shipyard));

    //    GameViewController.DeployShip(ship, true);

    //    GameViewController.AddGameLogMessage(string.Format("<b>You</b> deploy {0}", ship.CardName));

    //    return true;
    //}

    public bool TryPlaySkillCard(Card skillCard)
    {

        return true;
    }

    public bool TryHostMissionCard(MissionCard missionCard)
    {
        if (!_game.Player.MissionCardList.Contains(missionCard))
        {
            return false;
        }
        if (!(_game.GamePhase == GamePhase.ChooseMissionCard))
        {
            return false;
        }

        if (_game.Player.setMissioCard != null)
        {
            return false;
        }
        _game.Player.setMissioCard = missionCard;
        _game.Player.Hand.Remove(missionCard);

        GameViewController.hostMissionCard(missionCard, true);


        Debug.Log("Sending Missionc ready message");
        MessageTypes.MissionCardReadyMessage msg = new MessageTypes.MissionCardReadyMessage();
        NetworkManager.singleton.client.Send((short)MessageTypes.MessageType.C2S_MISSION_CARD_READY, msg);

        return true;
    }

    //public bool TryPlayOperation(Operation operation)
    //{
    //    // card must be in the players hand
    //    if (!_game.Player.Hand.Contains(operation))
    //        return false;
        
    //    // need enough money
    //    if (operation.BaseCost > _game.Player.Credits)
    //        return false;

    //    // playing operations costs a click
    //    if (_game.Player.Clicks < 1)
    //        return false;

    //    // if we get here is all good, play the card
    //    _actions.Add(new OperationAction(operation));

    //    if (operation.OnPlay != null)
    //    {
    //        operation.OnPlay(_game, _game.Player);
    //    }

    //    _game.Player.ChangeCredits(-operation.BaseCost);
    //    _game.Player.Hand.Remove(operation);        
        
    //    if (operation.OperationType == OperationType.ONESHOT)
    //    {
    //        _game.Player.Discard.Add(operation);
    //        GameViewController.DestroyCardTransform(operation);
    //    }
    //    else
    //    {
    //        _game.Player.OngoingOperations.Add(operation);
    //        GameViewController.MoveToConstructionArea(operation, true);
    //    }

    //    ChangeClicks(-1);

    //    GameViewController.AddGameLogMessage(string.Format("<b>You</b> play {0}", operation.CardName));

    //    return true;
    //}

    //public bool TryHost(Ship ship, Shipyard shipyard)
    //{
    //    // ship card must be in the players hand
    //    if (!_game.Player.Hand.Contains(ship))
    //        return false;

    //    // can only host one card
    //    if (shipyard.HostedShip != null)
    //        return false;

    //    // can only host ships up to a certain size
    //    if (ship.Size > shipyard.MaxSize)
    //        return false;

    //    // need enough money
    //    if (ship.BaseCost > _game.Player.Credits)
    //        return false;

    //    // hosting costs a click
    //    if (_game.Player.Clicks < 1)
    //        return false;

    //    // TODO - we could gather the reasons for failure here and do something e.g display a popup? 

    //    // if we get here is all good, host away
    //    _actions.Add(new HostShipAction(ship, shipyard));
    //    ChangeClicks(-1);
    //    _game.Player.ChangeCredits(-ship.BaseCost);
    //    shipyard.HostCard(ship);
    //    ship.StartConstruction();
    //    _game.Player.Hand.Remove(ship);

    //    GameViewController.HostShip(ship, shipyard, true);

    //    UpdatePlayerStateGUI();
    //    EnableDisableControls();

    //    GameViewController.AddGameLogMessage(string.Format("<b>You</b> host {0} on {1}", ship.CardName, shipyard.CardName));

    //    return true;
    //}
    
    //public bool TryTarget(Ship firer, int weaponIndex, Card target)
    //{
    //    // valid targets will be IDamageable
    //    if (target is IDamageable == false)
    //    {
    //        Debug.Log("Target is not damagable");
    //        return false;
    //    }

    //    // TODO - validate that firer belongs to player

    //    // TODO - check validity of index

    //    // TODO - check that target belongs to enemy ? (might be possible to fire on own ships?)

    //    // if we get here, all good, target away
    //    firer.Weapons[weaponIndex].SetTarget((IDamageable)target);
       
    //    // TODO - move this to store all weapon targets when submitted (so we dont have to change/remove when retargetting weapons)
    //    // _actions.Add(new WeaponTargetAction(firer, weaponIndex, (IDamageable)target));
        
    //    return true;
    //}

    public void SubmitActions()
    {        
        MessageTypes.ActionsMessage msg = new MessageTypes.ActionsMessage();

        if (_game.GamePhase == GamePhase.COMBAT_PLANNING)
        {
            // cycle through the weapons and add target actions
            // we defer to now rather than have to handle changing targets
            //foreach(Ship ship in _game.Player.Ships)
            //{
            //    for (int weaponIndex = 0; weaponIndex < ship.Weapons.Count; weaponIndex++)
            //    {
            //        Weapon weapon = ship.Weapons[weaponIndex];
            //        if (weapon.Target != null)
            //        {
            //            _actions.Add(new WeaponTargetAction(ship, weaponIndex, weapon.Target));
            //        }
            //    }
            //}
        }

        // serialize actions
        StringBuilder data = new StringBuilder();
        foreach(PlayerAction action in _actions)
        {
            data.Append(action.ToString() + '#');
        }
        msg.actionData = data.ToString().TrimEnd('#');

        // TODO - we may need to split this into chunks
        NetworkManager.singleton.client.Send((short)MessageTypes.MessageType.ACTIONS, msg);

        _actions.Clear();
        _game.AwaitOpponent();
        EnableDisableControls();
    }
}
            

