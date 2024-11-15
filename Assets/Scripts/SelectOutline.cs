using UnityEngine;
using UnityEngine.UIElements;

public class SelectOutline : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Cards"))
                {
                    var outline = hit.collider.gameObject.GetComponent<Outline>();

                    if (outline == null)
                    {
                        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Cards"))
                        {
                            var outline2 = gameObject.GetComponent<Outline>();
                            if (gameObject != null)
                            {
                                Destroy(outline2);
                            }
                        }
                        outline = hit.collider.gameObject.AddComponent<Outline>();

                        outline.OutlineMode = Outline.Mode.OutlineAll;
                        outline.OutlineColor = Color.red;
                        outline.OutlineWidth = 10f;
                    }
                }
            }
        }
    }
}
