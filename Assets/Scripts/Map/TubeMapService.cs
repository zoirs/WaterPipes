using UnityEngine;
using Zenject;

public class TubeMapService : IInitializable{
    
    [Inject] private GameSettingsInstaller.GameSetting setting;

    int[,] map = new int[,] {
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    };

    TextMesh[,] debugTextArray = new TextMesh[16, 16];

    public int Wight => 9;
    public int Hight => 12;

    private void InitDebug() {
        for (int i = 0; i < 9; i++) {
            for (int j = 0; j < 12; j++) {
                debugTextArray[i, j] = CreateWorldText(map[i, j].ToString(), null,
                    new Vector3(i, j, 0) + new Vector3(0.4f, 0.4f, 0), 15, Color.white, TextAnchor.MiddleCenter);
            }
        }
    }

    public void Busy(PointController[] list) {
        foreach (PointController pointController in list) {
            Vector3Int p = pointController.GetVector();
            Debug.Log(p);
            map[p.x, p.y] = -1;
        }

        printDebug();
    }

    public void Free(PointController[] list) {
        foreach (PointController pointController in list) {
            Vector3Int p = pointController.GetVector();
            map[p.x, p.y] = 0;
        }

        printDebug();
    }
    
    public bool Available(PointController[] list) {
        foreach (PointController pointController in list) {
            Vector3Int p = pointController.GetVector();
            if (p.x < 0 || p.x >= Wight || p.y < 0 || p.y >= Hight) {
                return false;
            }
        }
        return true;
    }

    public bool Check(PointController[] list) {
        foreach (PointController pointController in list) {
            Vector3Int p = pointController.GetVector();
            if (p.x < 0 || p.x >= Wight || p.y < 0 || p.y >= Hight) {
                return false;
            }
            if (map[p.x, p.y] != 0) {
                return false;
            }
        }

        return true;
    }

    private void printDebug() {
        if (!setting.isDebug) {
            return;
        }

        for (int i = 0; i < Wight; i++) {
            for (int j = 0; j < Hight; j++) {
                // CreateWorldText(map[i, j].ToString(), null,
                // new Vector3(i, j, 0) + new Vector3(0.4f, 0.4f, 0), 15, Color.white, TextAnchor.MiddleCenter);
                debugTextArray[i, j].text = map[i, j].ToString();
            }
        }

        // TextMesh[,] debugTextArray = new TextMesh[width, height];
        // gridArray = new int[width, height];
        //
        // debugTextArray[eventArgs.x, eventArgs.y].text = gridArray[eventArgs.x, eventArgs.y].ToString();
        // debugTextArray[x, y] = CreateWorldText(gridArray[x, y].ToString(), null,
        //     GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * .5f, 30, Color.white, TextAnchor.MiddleCenter);
    }


    // Create Text in the World
    public static TextMesh CreateWorldText(string text, Transform parent = null,
        Vector3 localPosition = default(Vector3), int fontSize = 40, Color? color = null,
        TextAnchor textAnchor = TextAnchor.UpperLeft, TextAlignment textAlignment = TextAlignment.Left,
        int sortingOrder = 5000) {
        if (color == null) color = Color.white;
        return CreateWorldText(parent, text, localPosition, fontSize, (Color) color, textAnchor, textAlignment,
            sortingOrder);
    }

    // Create Text in the World
    public static TextMesh CreateWorldText(Transform parent, string text, Vector3 localPosition, int fontSize,
        Color color, TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder) {
        GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor = textAnchor;
        textMesh.alignment = textAlignment;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
        return textMesh;
    }

    public void Initialize() {
        if (setting.isDebug) {
            InitDebug();
        }
    }
}