using UnityEngine;
using UnityEngine.UI;

public class Puzzle : MonoBehaviour
{
    [SerializeField] private Button againBtn;
    [SerializeField] private Button exitBtn;
    [SerializeField] private GameObject panel;
    [SerializeField] private Text scoreTxt;
    [SerializeField] private Image[] pieces;
    [SerializeField] private Sprite[] corrects;

    private int selected = -1;
    private int score = 0;

    private void Awake()
    {
        againBtn.onClick.AddListener(Restart);
        exitBtn.onClick.AddListener(Exit);

        foreach (var piece in pieces)
        {
            int index = System.Array.IndexOf(pieces, piece);
            piece.GetComponent<Button>().onClick.AddListener(() => Select(index));
        }
    }

    private void Start()
    {
        Shuffle();
    }
     
    private void Restart()
    {
        panel.SetActive(true);
        Shuffle();
    }
    
    private void Exit()
    {
        panel.SetActive(false);
    }

    private void Select(int index)
    {
        if (selected == -1)
        {
            selected = index;
        }
        else
        {
            Swap(selected, index);
            selected = -1;

            Check();
        }
    }

    private void Swap(int a, int b)
    {
        Sprite temp = pieces[a].sprite;
        pieces[a].sprite = pieces[b].sprite;
        pieces[b].sprite = temp;
    }

    private void Shuffle()
    {
        for (int i = 0; i < pieces.Length; i++)
        {
            int random = Random.Range(i, pieces.Length);
            Swap(i, random);
        }
    }

    private void Check()
    {
        for (int i = 0; i < pieces.Length; i++)
        {
            if (pieces[i].sprite != corrects[i])
                return;
        }

        Restart();
        score++;
        scoreTxt.text = score.ToString();

        Debug.Log("done");
    }
}