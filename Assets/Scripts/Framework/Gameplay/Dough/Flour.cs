using UnityEngine;

using Framework.Gameplay.HeldItemSystem;
using Tools;

namespace Framework.Gameplay.Dough
{
    public sealed class Flour : HeldItem
    {
        [SerializeField] private MeshRenderer flowerMesh;
        [SerializeField] private Color badFlowerColor =  Color.magenta; 

        public void MakeFlowerBad()
        {
            Material a = MaterialCreationTool.CreateMaterialCopy(flowerMesh.material);
            a.color = badFlowerColor;
            flowerMesh.material = a;
        }
    }
}