using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NightAtTheRijksmuseum
{
    public class Character : MonoBehaviour
    {
        [SerializeField]
        protected CharacterController controller;

        [SerializeField]
        protected float movementSpeed;
    }
}
