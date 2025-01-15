using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Collectible collectible;
    public Text textPart;
    public Text fullText;
    private bool part1Good;
    private bool part2Good;
    private bool part3Good;
    public int numberPart = 3;

    public void Update()
    {
        if (collectible.player.part1 && !part1Good)
        {
            numberPart--;
            part1Good = true;
            textPart.text = numberPart.ToString();
        }
        if (collectible.player.part2 && !part2Good)
        {
            numberPart--;
            part2Good = true;
            textPart.text = numberPart.ToString();
        }
        if (collectible.player.part3 && !part3Good)
        {
            numberPart--;
            part3Good = true;
            textPart.text = numberPart.ToString();
        }

        if (numberPart == 0)
        {
            textPart.text = "0";
        }
    }
}
