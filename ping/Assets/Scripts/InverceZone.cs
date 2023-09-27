using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InverceZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private static SerializedProperty GetChildProperty(SerializedProperty parent, string name)
    {
        SerializedProperty child = parent.Copy();
        child.Next(true);
        do
        {
            if (child.name == name) return child;
        }
        while (child.Next(false));
        return null;
    }

    public static bool AxisDefined(string axisName)
    {
        SerializedObject serializedObject = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0]);
        //SerializedProperty scriptingDefineSymbolsProperty = serializedObject.FindProperty("scriptingDefineSymbols");
        //SerializedProperty platformProperty = scriptingDefineSymbolsProperty.GetArrayElementAtIndex(0);
        //platformProperty.FindPropertyRelative("first").stringValue = "Standalone";
        //platformProperty.FindPropertyRelative("second").stringValue = "put your defines here";


        SerializedProperty axesProperty = serializedObject.FindProperty("m_Axes");

        axesProperty.Next(true);
        axesProperty.Next(true);
        while (axesProperty.Next(false))
        {

            axesProperty.Next(true);
            //SerializedProperty axis = axesProperty;
            //axis.Next(true);
            //Debug.Log(axesProperty.FindPropertyRelative("m_Name"));
            if (GetChildProperty(axesProperty, "m_Name").stringValue == axisName)
            {
                axesProperty.FindPropertyRelative("invert").boolValue = true;
                //GetChildProperty(axis, "invert").boolValue = true;
                serializedObject.ApplyModifiedProperties();
                return true;
            }
        }
        return false;
    }
    public enum AxisType
    {
        KeyOrMouseButton = 0,
        MouseMovement = 1,
        JoystickAxis = 2
    };

    public class InputAxis
    {
        public string name;
        public string descriptiveName;
        public string descriptiveNegativeName;
        public string negativeButton;
        public string positiveButton;
        public string altNegativeButton;
        public string altPositiveButton;

        public float gravity;
        public float dead;
        public float sensitivity;

        public bool snap = false;
        public bool invert = false;

        public AxisType type;

        public int axis;
        public int joyNum;
    }

    public static void EditAxis(InputAxis axis)
    {
        if (AxisDefined(axis.name))
        {
            SerializedObject serializedObject = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0]);
            SerializedProperty axesProperty = serializedObject.FindProperty(axis.name);
            axesProperty.arraySize++;
            serializedObject.ApplyModifiedProperties();
            SerializedProperty axisProperty = axesProperty.GetArrayElementAtIndex(axesProperty.arraySize - 1);
            GetChildProperty(axisProperty, "invert").boolValue = axis.invert;
            serializedObject.ApplyModifiedProperties();
        } 
    }
}
