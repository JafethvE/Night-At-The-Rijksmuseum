using UnityEngine;
using UnityEngine.UI;

namespace NightAtTheRijksmuseum
{
    public class Painting : MonoBehaviour
    {
        [SerializeField]
        private string pantingName;

        public string PantingName
        {
            get
            {
                return pantingName;
            }
        }

        [SerializeField]
        private Color outlineColor;

        public Color OutlineColor
        {
            get
            {
                return outlineColor;
            }

            set
            {
                outlineColor = value;
            }
        }

        [SerializeField]
        private MeshRenderer outline;

        private void Start()
        {
            outline.material.color = outlineColor;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.GetComponent<Enemy>())
            {
                Enemy enemy = other.GetComponent<Enemy>();
                if(enemy.Painting == this)
                {
                    enemy.Die();
                }
            }
        }
    }
}