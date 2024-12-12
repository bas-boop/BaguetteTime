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

        /// <summary>
        /// Create a runtime material based on a target material.
        /// </summary>
        public static Material CreateMaterialCopy(Material targetMat)
        {
            if (targetMat == null)
            {
                Debug.LogError("Target material is null!");
                return null;
            }

            Material newMaterial = new (targetMat)
            {
                name = GetRandomName(targetMat.name)
            };

            return newMaterial;
        }
        
#if UNITY_EDITOR
        public static (Material, string) CreateAndReturnMaterialCopy(Material targetMat)
            => (CreateMaterial(targetMat), _lastCreatedMaterialPath);

        public static void DeleteMaterial() => AssetDatabase.DeleteAsset(_lastCreatedMaterialPath);

        public static void DeleteMaterial(string path) => AssetDatabase.DeleteAsset(path);

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
    
        private static Material CreateMaterial(Material targetMat)
        {
            _lastCreatedMaterialPath = MATERIAL_RUNTIME_DIRECTORY + GetRandomName(targetMat.name) + MAT;
            
            AssetDatabase.CopyAsset(AssetDatabase.GetAssetPath(targetMat), _lastCreatedMaterialPath);
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            
            return AssetDatabase.LoadAssetAtPath<Material>(_lastCreatedMaterialPath);
        }
#endif

        private static string GetRandomName() => $"{Random.Range(0, 99)}-{Random.Range(0, 99)}";

        /// <summary>
        /// Generates a random name for the material.
        /// </summary>
        private static string GetRandomName(string baseName) 
            => $"{baseName}_{Random.Range(0, 99)}_{Random.Range(0, 99)}";
    }
}
