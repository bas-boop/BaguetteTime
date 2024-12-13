using UnityEngine;

namespace Framework.Gameplay.HeldItemSystem
{
    public class TestHeldItem : HeldItem
    {
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.gameObject.GetComponent<ItemHolder>().HoldItem(this);
                GetComponent<Collider>().enabled = false;
            }
        }
    }
}