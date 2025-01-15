using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Collectible collectible;
    public Text textPart;
    public Text fullText;
    public int numberPart = 3;

    public void Update()
    {
        if (collectible.player.part1 = true)
        {
            numberPart--;
            textPart.text = numberPart.ToString();
        }
        if (collectible.player.part2 = true)
        {
            numberPart--;
            textPart.text = numberPart.ToString();
        }
        if (collectible.player.part3 = true)
        {
            numberPart--;
            textPart.text = numberPart.ToString();
        }

        if (numberPart == 0)
        {
            textPart.text = "0";
        }
    }
}
