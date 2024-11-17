using UnityEngine;

namespace Miniville
{
    public class PileVisual : MonoBehaviour
    {
        public Cards Card;

        public void Init(Cards card)
        {
            Card = card;
        }

        public void OnClick()
        {
            Debug.Log("Click Pile");
            CardBuySelection.instance.BuyCard(Card);
        }
    }
}
