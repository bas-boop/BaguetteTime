using Framework.Extensions;
using UnityEngine;

namespace Framework.Gameplay.HeldItemSystem
{
    public sealed class TestTaker : ItemTaker
    {
        public override void TakeAction()
        {
            Vector3 a = transform.position;
            a.AddY(0.5f);
            p_takenItem.transform.parent = transform;
            p_takenItem.transform.position = a;
        }
    }
}