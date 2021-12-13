using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
/// <summary>
/// 
/// </summary>
[ExecuteInEditMode]
public class ClickSpawn : MonoBehaviour
{
    [SerializeField]
    private LayerMask groundLayer;

    public GameObject floor;
    public GameObject wall;
    public GameObject gate;

    private void OnEnable()
    {
        if (!Application.isEditor)
        {
            Destroy(this);
        }
        SceneView.onSceneGUIDelegate += OnScene;
    }

    void OnScene(SceneView scene)
    {
        if(Controller.currentBrush == 0)
        {
            return;
        }

        Event e = Event.current;

        if ((e.type == EventType.MouseDown || e.type == EventType.MouseDrag) && e.button == 0)
        {
            Debug.Log(Controller.currentBrush);

            Vector3 mousePos = e.mousePosition;
            float ppp = EditorGUIUtility.pixelsPerPoint;
            mousePos.y = scene.camera.pixelHeight - mousePos.y * ppp;
            mousePos.x *= ppp;

            Ray ray = scene.camera.ScreenPointToRay(mousePos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
            {
                Vector3 spawnPosition = new Vector3(Mathf.Floor(hit.point.x+0.5f), 0, Mathf.Floor(hit.point.z+0.5f));

                GameObject spawnObject = Controller.currentBrush == 1 ? floor : Controller.currentBrush == 2 ? wall : gate;
                GameObject go = GameObject.Instantiate(spawnObject, spawnPosition, Quaternion.Euler(0,0,0));
                Debug.Log("Instantiated Floor " + hit.point);
            }
            e.Use();
        }
    }
}
#endif