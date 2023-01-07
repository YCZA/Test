using UnityEngine;

public class RaycastTest : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit2D hit2D = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit2D.collider != null)
            {
                hit2D.transform.gameObject.GetComponent<SpriteRenderer>().color = new Color32((byte)Random.Range(0, 256), (byte)Random.Range(0, 256), (byte)Random.Range(0, 256), (byte)Random.Range(255, 256));
            }

            Physics.Raycast(ray, out RaycastHit hit);
            if (hit.collider != null)
            {
                hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = new Color32((byte)Random.Range(0, 256), (byte)Random.Range(0, 256), (byte)Random.Range(0, 256), (byte)Random.Range(255, 256));
            }


        }
    }
}
