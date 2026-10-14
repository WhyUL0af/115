
using UnityEngine;
using UnityEngine.UI;

public class game : MonoBehaviour
{
    [SerializeField] private Button againBtn;
    [SerializeField] private Button exitBtn;
    [SerializeField] private GameObject panel;
    [SerializeField] private Text scoreTxt;
    [SerializeField] private Image[] pieces;

    private int score = 0;
    private int emptyIndex = 8;

    private Sprite[] originalSprites;
    private const int size = 3;

    private void Awake()
    {
        againBtn.onClick.AddListener(Restart);
        exitBtn.onClick.AddListener(Exit);

        originalSprites = new Sprite[pieces.Length];

        for (int i = 0; i < pieces.Length; i++)
        {
            int index = i;

            originalSprites[i] = pieces[i].sprite;
            global.po[i] = i;

            pieces[i].GetComponent<Button>()
                .onClick.AddListener(() => Select(index));
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
        if (!CanMove(index))
            return;

        Swap(index, emptyIndex);
        emptyIndex = index;

        UpdateEmpty();
        Check();
    }

    private bool CanMove(int index)
    {
        int row = index / size;
        int col = index % size;

        int emptyRow = emptyIndex / size;
        int emptyCol = emptyIndex % size;

        return Mathf.Abs(row - emptyRow) +
               Mathf.Abs(col - emptyCol) == 1;
    }

    private void Swap(int a, int b)
    {
        Sprite temp = pieces[a].sprite;
        pieces[a].sprite = pieces[b].sprite;
        pieces[b].sprite = temp;

        int tempPo = global.po[a];
        global.po[a] = global.po[b];
        global.po[b] = tempPo;
    }

    private void UpdateEmpty()
    {
        for (int i = 0; i < pieces.Length; i++)
        {
            pieces[i].enabled = (i != emptyIndex);
        }
    }

    private void Shuffle()
    {
        for (int i = 0; i < pieces.Length; i++)
        {
            pieces[i].sprite = originalSprites[i];
            global.po[i] = i;
        }

        emptyIndex = 8;

        int previousEmpty = -1;

        for (int i = 0; i < 100; i++)
        {
            int[] neighbors = new int[4];
            int count = 0;

            int row = emptyIndex / size;
            int col = emptyIndex % size;

            if (row > 0)
                neighbors[count++] = emptyIndex - size;

            if (row < size - 1)
                neighbors[count++] = emptyIndex + size;

            if (col > 0)
                neighbors[count++] = emptyIndex - 1;

            if (col < size - 1)
                neighbors[count++] = emptyIndex + 1;

            int next;

            do
            {
                next = neighbors[Random.Range(0, count)];
            }
            while (count > 1 && next == previousEmpty);

            int oldEmpty = emptyIndex;

            Swap(next, emptyIndex);
            emptyIndex = next;

            previousEmpty = oldEmpty;
        }

        UpdateEmpty();

        // 避免洗牌後剛好已完成
        if (IsSolved())
        {
            Shuffle();
        }
    }

    private bool IsSolved()
    {
        for (int i = 0; i < pieces.Length; i++)
        {
            if (global.po[i] != i)
                return false;
        }

        return true;
    }

    private void Check()
    {
        if (!IsSolved())
            return;

        score++;
        scoreTxt.text = score.ToString();

        Debug.Log("done");

        Restart();
    }
}
