using System.Collections;
using System.Xml.Serialization;
using UnityEngine;

namespace Miniville
{
    public class CardVisual : MonoBehaviour
    {
        public Cards Card
        {
            get => _card;
            set
            {
                _card = value;
                Renderer.sprite = _card.Visual;
            }
        }
        private Cards _card;

        public SpriteRenderer Renderer;
    } 
}
