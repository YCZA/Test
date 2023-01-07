using UnityEngine;
using UnityEngine.EventSystems;

public class GameObjectTest : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        gameObject.GetComponent<MeshRenderer>().material.color = new Color32((byte)Random.Range(0, 256), (byte)Random.Range(0, 256), (byte)Random.Range(0, 256), (byte)Random.Range(255, 256));
    }
    private void OnMouseDown()
    {
        //gameObject.GetComponent<MeshRenderer>().material.color = new Color32((byte)Random.Range(0, 256), (byte)Random.Range(0, 256), (byte)Random.Range(0, 256), (byte)Random.Range(255, 256));
    }
}
