using swantiez.unity.tools.utils;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace swantiez.unity.tools.editor
{
    public class UtilsForEditor
    {
        private UtilsForEditor() { }

        public static List<string> GetAvailableBuiltScenesNames()
        {
            List<string> scenes = new List<string>();
            foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
            {
                if (scene.enabled)
                {
                    string name = scene.path.Substring(scene.path.LastIndexOf('/') + 1);
                    name = name.Substring(0, name.Length - 6);
                    //Debug.Log("Scene name: " + name + " - path: " + scene.path);
                    scenes.Add(name);
                }
            }
            return scenes;
        }

        public static List<string> GetLayersNames()
        {
            List<string> layers = new List<string>();
            for(int i = 0; i < 32; ++i)
            {
                string layerName = InternalEditorUtility.GetLayerName(i);
                if (layerName != "") layers.Add(layerName);
            }
            return layers;
        }

        public static string[] GetAvailableSortingLayersNames()
        {
            Type internalEditorUtilityType = typeof(InternalEditorUtility);
            PropertyInfo sortingLayersProperty = internalEditorUtilityType.GetProperty("sortingLayerNames", BindingFlags.Static | BindingFlags.NonPublic);
            return (string[]) sortingLayersProperty.GetValue(null, new object[0]);
        }

        public static string ShowArrayParamInInspector(string arrayName, string[] arrayContent, string parameterLabel, string scriptLabel)
        {
            if (arrayName == null) arrayName = "";
            int arrayIndex = Array.IndexOf(arrayContent, arrayName);
            arrayIndex = EditorGUILayout.Popup(parameterLabel, arrayIndex, arrayContent);
            if (arrayIndex != -1)
            {
                arrayName = arrayContent[arrayIndex];
            }
            else if (arrayName != "")
            {
                Debug.LogError("Invalid " + parameterLabel + " name in " + scriptLabel + ": " + arrayName);
            }
            return arrayName;
        }

        public static string ShowListParamInInspector(string listName, List<string> listContent, string parameterLabel, string scriptLabel)
        {
            return ShowArrayParamInInspector(listName, listContent.ToArray(), parameterLabel, scriptLabel);
        }

        public static string ShowScenesParamInInspector(string sceneName, string parameterLabel, string scriptLabel)
        {
            List<string> sceneNames = GetAvailableBuiltScenesNames();
            return ShowListParamInInspector(sceneName, sceneNames, parameterLabel, scriptLabel);
        }

        public static string ShowLayersParamInInspector(string layerName, string parameterLabel, string scriptLabel)
        {
            List<string> layersNames = GetLayersNames();
            return ShowListParamInInspector(layerName, layersNames, parameterLabel, scriptLabel);
        }

        public static Material ShowMaterialParamInInspector(Material material, string matLabel)
        {
            return (Material)EditorGUILayout.ObjectField(matLabel, material, typeof(Material), true);
        }

        public static void CompleteArrayWithChildren(UnityEngine.Object target, ref Transform[] transformArray)
        {
            if ((transformArray == null) || (transformArray.Length != 1)) return;
            if ((transformArray[0] == null) || (transformArray[0].childCount == 0)) return;
            if (GUILayout.Button("Replace object with its children"))
            {
                Transform parentTransform = transformArray[0];
                int nbObjects = parentTransform.childCount;
                int index = 0;
                transformArray = new Transform[nbObjects];
                foreach (Transform childTransform in parentTransform)
                {
                    transformArray[index++] = childTransform;
                }
                EditorUtility.SetDirty(target);
            }
        }

        public static void CompleteArrayWithChildren(UnityEngine.Object target, ref GameObject[] objArray)
        {
            if ((objArray == null) || (objArray.Length != 1)) return;
            if ((objArray[0] == null) || (objArray[0].transform.childCount == 0)) return;
            if (GUILayout.Button("Replace object with its children"))
            {
                Transform parentTransform = objArray[0].transform;
                int nbObjects = parentTransform.childCount;
                int index = 0;
                objArray = new GameObject[nbObjects];
                foreach (Transform childTransform in parentTransform)
                {
                    objArray[index++] = childTransform.gameObject;
                }
                EditorUtility.SetDirty(target);
            }
        }

        public static float ShowRadiusInScene(UnityEngine.Object target, float radiusValue, Color radiusColor, Vector3 radiusOrigin)
        {
            Handles.color = radiusColor;
            radiusValue = Handles.RadiusHandle(Quaternion.identity, radiusOrigin, radiusValue);
            if (GUI.changed) EditorUtility.SetDirty(target);
            return radiusValue;
        }

        public static T[] ShowArrayAsList<T>(UnityEngine.Object target, T[] array, string label, ref T newValueParam, Func<T,T> addNewField, T emptyValue)
        {
            EditorGUILayout.LabelField(label);

            for (int i = 0; (array != null) && (i < array.Length); i++)
            {
                GUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("- " + array[i]);
                if (GUILayout.Button("-"))
                {
                    array = array.Remove(array[i]);
                    EditorUtility.SetDirty(target);
                }
                GUILayout.EndHorizontal();
            }

            GUILayout.BeginHorizontal();
            newValueParam = addNewField(newValueParam);
            if (GUILayout.Button("+"))
            {
                if (array != null)
                {
                    array = array.AddElements(newValueParam);
                }
                else
                {
                    array = new T[] { newValueParam };
                }
                newValueParam = emptyValue;
                EditorUtility.SetDirty(target);
            }
            GUILayout.EndHorizontal();

            return array;
        }
    }
}