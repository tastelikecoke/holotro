using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject hoverObject;

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverObject.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        hoverObject.SetActive(false);
    }
}
