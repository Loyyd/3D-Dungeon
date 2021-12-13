using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SetUp))]
public class LevelEditorOnScene : Editor
{
    // draw lines between a chosen game object
    // and a selection of added game objects

    public override void OnInspectorGUI()
    {
        Debug.Log("qwer");
        Debug.Log("qwer");
    }

    public void OnSceneGUI()
    {
        Debug.Log("asdfasdcf");

        if (Input.GetMouseButtonDown(0))
        {    //If the mouse button is pressed
            Debug.Log("Mouse was pressed");
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));    //Disable scene view mouse selection
        }

        // get the chosen game object
        DrawLine t = target as DrawLine;

        if (t == null || t.GameObjects == null)
            return;

        // grab the center of the parent
        Vector3 center = t.transform.position;

        // iterate over game objects added to the array...
        for (int i = 0; i < t.GameObjects.Length; i++)
        {
            // ... and draw a line between them
            if (t.GameObjects[i] != null)
                Handles.DrawLine(center, t.GameObjects[i].transform.position);
        }
    }
}
