using UnityEngine;
using System.Collections.Generic;
using Miniville;

namespace Miniville
{
    public class GameObjectAligner : MonoBehaviour
    {
        // Champs publics configurables depuis l'inspecteur dans Unity
        public List<GameObject> Objects = new List<GameObject>(); // Dictionnaire des cartes et leurs GameObjects
        public Vector3 rotationOffset;
        public float spacingX = 1.5f; // Espacement horizontal entre les cartes
        public float spacingZ = 1.5f; // Espacement horizontal entre les cartes
        public bool invertX;
        public bool invertZ;
        [Min(1)] public int maxPerRow = 5; // Nombre maximum de cartes par ligne

        protected void AlignGameObjectsHorizontally()
        {
            if (Objects == null || Objects.Count == 0)
            {
                //Debug.LogWarning("Le dictionnaire de cartes est vide ou nul.");
                return;
            }

            int cardCount = Objects.Count;

            Vector3 startPosition = transform.position; // Position initiale
            Quaternion orientation = transform.rotation; // Orientation du GameObject
            int count = 0;

            foreach (var obj in Objects)
            {
                if (obj == null)
                    continue;

                // Calcul de la position cible relative à l'orientation du GameObject
                Vector3 localOffset = new Vector3(
                    (count % maxPerRow) * spacingX * (invertX ? -1 : 1), // Décalage sur l'axe X, inversé si invertX est true
                    0, // Aucun changement sur l'axe Y
                    (count / maxPerRow) * spacingZ * (invertZ ? 1 : -1)  // Décalage sur l'axe Z, direction inversée par défaut
                );

                Vector3 targetPosition = startPosition + orientation * localOffset; // Applique l'orientation au décalage

                obj.transform.position = targetPosition; // Positionne le GameObject

                obj.transform.rotation = GetRotation();

                count++;
            }
        }

        protected Quaternion GetRotation() => transform.rotation * Quaternion.Euler(rotationOffset);

        private void OnValidate()
        {
            AlignGameObjectsHorizontally();
        }
    }
}
