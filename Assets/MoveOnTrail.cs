
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;

public class MoveOnTrail : MonoBehaviour
{
    Knot NextTarget;
    Knot PreviousTarget;
    public float PositionPercent;
    public Knot[] ShortPath;
    public Knot[] LongPath;
    private int NextIndex;
    private int PreviousIndex;
    private Knot[] Path;
    bool ChangePath;
    void Start()
    {
        Path = ShortPath;
        NextIndex = 1;
        PreviousIndex = NextIndex - 1;
        NextTarget = Path[NextIndex];
        PreviousTarget = Path[PreviousIndex];
        PositionPercent = 0.1f;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, NextTarget.transform.position) < 0.01f)
        {
            if (ChangePath && NextIndex == 1)
            {
                if (Path == ShortPath)
                {
                    Path = LongPath;
                }
                else
                {
                    Path = ShortPath;
                }
                ChangePath = false;
            }
            NextIndex++;
            if (NextIndex == Path.Length)
                NextIndex = 0;

            PreviousIndex = NextIndex - 1;
            if (PreviousIndex == -1)
                PreviousIndex = Path.Length - 1;

            PositionPercent = 0f;

            NextTarget = Path[NextIndex];
            PreviousTarget = Path[PreviousIndex];
        }
        PositionPercent += 0.1f;

        Vector3 a = Vector3.MoveTowards(PreviousTarget.transform.position, NextTarget.Anchor.position, PositionPercent);
        Vector3 b = Vector3.MoveTowards(NextTarget.Anchor.position, NextTarget.transform.position, PositionPercent);
        transform.position = Vector3.MoveTowards(a, b, PositionPercent);
        Vector3 NextPosition = Vector3.MoveTowards(a, b, PositionPercent * 0.1f);

        transform.LookAt(NextPosition);
    }

    public void SetChangePath()
    {
        ChangePath = true;
    }
}
