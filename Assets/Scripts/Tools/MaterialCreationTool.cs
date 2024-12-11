using UnityEditor;
using UnityEngine;

namespace Tools
{
    public static class MaterialCreationTool
    {
        private const string MATERIAL_RUNTIME_DIRECTORY = "Assets/Art/Materials/Runtime/";
        private const string MATERIAL_DIRECTORY = "Assets/Art/Materials/";
        private const string MAT = ".mat";
        
        private static string _lastCreatedMaterialPath;

        public static void CreateMaterialCopy(Material targetMat) 
            => CreateMaterial(targetMat);

        public static (Material, string) CreateAndReturnMaterialCopy(Material targetMat)
            => (CreateMaterial(targetMat), _lastCreatedMaterialPath);

        public static void DeleteMaterial() => AssetDatabase.DeleteAsset(_lastCreatedMaterialPath);

        public static void DeleteMaterial(string path) => AssetDatabase.DeleteAsset(path);

#if UNITY_EDITOR
        [MenuItem("Tools/Materials/Delete All Runtime Materials")]
        private static void DeleteAllRuntimeMaterials()
        {
            string[] materialPaths = AssetDatabase.FindAssets("t:Material",
                new[] { MATERIAL_RUNTIME_DIRECTORY });
            
            foreach (string guid in materialPaths)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                
                if (path.StartsWith(MATERIAL_RUNTIME_DIRECTORY)) 
                    AssetDatabase.DeleteAsset(path);
            }
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            
            Debug.Log($"All runtime materials in {MATERIAL_RUNTIME_DIRECTORY} have been deleted.");
        }

        [MenuItem("Tools/Materials/Create Material")]
        private static void CreateMaterial()
        {
            Material newMaterial = new (Shader.Find("Universal Render Pipeline/Lit"));
            const string NEW_MAT_NAME = "NewMaterial";
            _lastCreatedMaterialPath = MATERIAL_DIRECTORY + NEW_MAT_NAME + MAT;
            
            AssetDatabase.CreateAsset(newMaterial, _lastCreatedMaterialPath);
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            
            Debug.Log($"Material created at {MATERIAL_DIRECTORY}");
        }
#endif
    
        private static Material CreateMaterial(Material targetMat)
        {
            _lastCreatedMaterialPath = MATERIAL_RUNTIME_DIRECTORY + GetRandomName(targetMat.name) + MAT;
            
            AssetDatabase.CopyAsset(AssetDatabase.GetAssetPath(targetMat), _lastCreatedMaterialPath);
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            
            return AssetDatabase.LoadAssetAtPath<Material>(_lastCreatedMaterialPath);
        }

        private static string GetRandomName() => $"{Random.Range(0, 99)}-{Random.Range(0, 99)}";

        private static string GetRandomName(string baseName) => $"{baseName}{Random.Range(0, 99)}-{Random.Range(0, 99)}";
    }
}
