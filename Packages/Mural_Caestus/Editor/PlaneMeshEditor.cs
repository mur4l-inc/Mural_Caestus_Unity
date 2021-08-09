using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace Mural.Caestus
{
    [CustomEditor(typeof(PlaneMesh))]
    public class PlaneMeshEditor : Editor
    {
        SerializedProperty _widthSegments;
        SerializedProperty _heightSegments;
        SerializedProperty _width;
        SerializedProperty _height;
        SerializedProperty _renderType;

        void OnEnable()
        {
            _widthSegments = serializedObject.FindProperty("_widthSegments");
            _heightSegments = serializedObject.FindProperty("_heightSegments");
            _width = serializedObject.FindProperty("_width");
            _height = serializedObject.FindProperty("_height");
            _renderType = serializedObject.FindProperty("_renderType");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(_widthSegments);
            EditorGUILayout.PropertyField(_heightSegments);
            EditorGUILayout.PropertyField(_width);
            EditorGUILayout.PropertyField(_height);
            EditorGUILayout.PropertyField(_renderType);
            var rebuild = EditorGUI.EndChangeCheck();

            serializedObject.ApplyModifiedProperties();

            if (rebuild)
            {
                foreach(var t in targets)
                {
                    ((PlaneMesh)t).RebuildMesh();
                }
            }
        }

        [MenuItem("Assets/Create/Mural/Caestus/Plane Mesh")]
        public static void CreatePlaneMeshAsset()
        {
            var path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (string.IsNullOrEmpty(path))
            {
                path = "Assets";
            }
            else if (Path.GetExtension(path) != "")
            {
                path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
            }
            var assetPathName = AssetDatabase.GenerateUniqueAssetPath(path + "/Plane.asset");

            var asset = ScriptableObject.CreateInstance<PlaneMesh>();
            AssetDatabase.CreateAsset(asset, assetPathName);
            AssetDatabase.AddObjectToAsset(asset.sharedMesh, asset);

            asset.RebuildMesh();

            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;
        }
    }
}

