using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DropHandler : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public DropZoneType DropZoneType;
    public GameClientController GameClientController;

    public void OnStart()
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {        
    }

    public void OnPointerExit(PointerEventData eventData)
    {        
    }

    public void OnDrop(PointerEventData eventData)
    {
        DragHandler d = eventData.pointerDrag.GetComponent<DragHandler>();
        if (d != null)
        {
            CardBehaviour cl = eventData.pointerDrag.GetComponent<CardBehaviour>();
            if (cl != null)
            {
                if (GameClientController == null)
                    GameClientController = FindObjectOfType<GameClientController>();

                Card card = cl.Card;
                Debug.Log(card.Name + " was dropped on " + gameObject.name);

                if (card.cardType == CardType.MissionCard && DropZoneType == DropZoneType.MissionCardZone)
                {
                    Card missionCard = (Card)GetComponent<CardBehaviour>().Card;

                    if (GameClientController.TryHostMissionCard((MissionCard)missionCard))
                    {
                        d.returnToParent = false;
                    }
                }    
                
                if (card.cardType == CardType.ItemCard && DropZoneType == DropZoneType.ItemCardZone)
                {
                    //if (GameClientController.TryPlayShipyard((Shipyard)card))
                    //{
                    //    d.returnToParent = false;
                    //}
                    
                }    
                
                if (card.cardType == CardType.SkillCard)
                {
                    //if (GameClientController.TryPlayOperation((Operation)card))
                    //{
                    //    d.returnToParent = false;
                    //}
                }        
            }
        }
    }
}
