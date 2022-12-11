using System;
using UnityEngine;
using ZigZagClone.Level;

namespace ZigZagClone.Controllers
{
    public class CubeController : MonoBehaviour
    {
        private Rigidbody rig;

        private void Awake()
        {
            rig = GetComponent<Rigidbody>();
        }


        protected virtual void OnCollisionExit(Collision other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Invoke(nameof(DropCube), 0.75f - other.gameObject.GetComponent<PlayerController>().CurrentSpeed / 10f);
            }
        }

        private void DropCube()
        {
            rig.useGravity = true;
            rig.isKinematic = false;
            LevelCreator.Instance.RecycleCube(this, rig);
        }
    }
}