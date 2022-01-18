using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class MyItemGeneratePos : MyUpdater
{
    [Header("床管理クラス")]
    [SerializeField]
    FloorManager m_floorMana = null;
    //床の縦横の枚数
    Vector2Int m_floorNum = Vector2Int.zero;
    //床の大きさ
    float m_floorSize = 0.0f;

    class GeneratePos
    {
        public Vector2Int pos;
        public bool isUser;

        public GeneratePos(Vector2Int _pos)
        {
            pos = _pos;
            isUser = true;
        }
    }

    //生成可能な場所
    Dictionary<SplitArea, List<GeneratePos>> m_generatablePos = new Dictionary<SplitArea, List<GeneratePos>>();
    //Dictionary<SplitArea, List<Vector2Int>> m_generatablePos = new Dictionary<SplitArea, List<Vector2Int>>();

    int testNum =0;

    enum SplitArea
    {
        LEFT_DOWN,
        LEFT_UP,
        RIGHT_DOWN,
        RIGHT_UP
    }
    //エリアの範囲
    struct Value
    {
        public Vector2Int min;
        public Vector2Int max;

        public Value(Vector2Int _min, Vector2Int _max)
        {
            min = _min;
            max = _max;
        }
    }
    Dictionary<SplitArea, Value> m_areaSplit = new Dictionary<SplitArea, Value>();

    //エリアの重み
    Dictionary<SplitArea, int> m_areaWeights = new Dictionary<SplitArea, int>();
    //計算のための代入用エリア情報
    Dictionary<SplitArea, int> m_subWeights = new Dictionary<SplitArea, int>();
    //基本の重み
    private int m_basicWeight = 5;

    public override void MyFastestInit()
    {
        //床の枚数を取得する
        m_floorNum = m_floorMana.FloorNumber;
        m_floorSize = m_floorMana.FloorSize;

        //床の大きさを取得
        int width = m_floorNum.x;
        int height = m_floorNum.y;
        int widthHalf = width / 2;
        int heightHalf = height / 2;

        //width=6
        //He=6
        //wi/2=3
        //wi/2=3

        //エリアを上下左右に分割
        //左下
        m_areaSplit[SplitArea.LEFT_DOWN] = new Value(new Vector2Int(width - widthHalf, height - heightHalf), new Vector2Int(width - 1, height - 1));
        //右下
        m_areaSplit[SplitArea.RIGHT_DOWN] = new Value(new Vector2Int(0, height - heightHalf), new Vector2Int(width - 1 - widthHalf, height - 1));
        //左上
        m_areaSplit[SplitArea.LEFT_UP] = new Value(new Vector2Int(width - widthHalf, 0), new Vector2Int(width - 1, height - 1 - heightHalf));
        //右上
        m_areaSplit[SplitArea.RIGHT_UP] = new Value(new Vector2Int(0, 0), new Vector2Int(width - 1 - widthHalf, height - 1 - heightHalf));

        //分割したエリアのキー
        var splitKey = m_areaSplit.Keys;
        foreach (var key in splitKey)
        {
            for (int x = m_areaSplit[key].min.x; x <= m_areaSplit[key].max.x; ++x)
            {
                //端のエリアを追加しない
                if (x == 0 || x == width - 1) continue;

                for (int z = m_areaSplit[key].min.y; z <= m_areaSplit[key].max.y; ++z)
                {
                    if (z == 0 || z == height - 1) continue;

                    if (!m_generatablePos.ContainsKey(key))
                    {
                        //List<Vector2Int> pos = new List<Vector2Int>() { new Vector2Int(x, z) };
                        List<GeneratePos> pos = new List<GeneratePos>() { new GeneratePos(new Vector2Int(x, z)) };
                        m_generatablePos.Add(key, pos);
                    }
                    else
                    {
                        m_generatablePos[key].Add(new GeneratePos(new Vector2Int(x, z)));
                    }
                }
            }
        }


        //エリアの重みを初期化
        m_areaWeights.Add(SplitArea.LEFT_DOWN, m_basicWeight);
        m_areaWeights.Add(SplitArea.LEFT_UP, m_basicWeight);
        m_areaWeights.Add(SplitArea.RIGHT_DOWN, m_basicWeight);
        m_areaWeights.Add(SplitArea.RIGHT_UP, m_basicWeight);
        m_subWeights.Add(SplitArea.LEFT_DOWN, m_basicWeight);
        m_subWeights.Add(SplitArea.LEFT_UP, m_basicWeight);
        m_subWeights.Add(SplitArea.RIGHT_DOWN, m_basicWeight);
        m_subWeights.Add(SplitArea.RIGHT_UP, m_basicWeight);
    }

    public Vector3 GetGeneratePos(ref List<Vector2Int> _generated)
    {
        if (m_generatablePos.Count == 0)
        {
            Debug.Log("出現エリアなし");
            return Vector3.zero;
        }

        //エリア決定フラグ
        bool isDecision = false;
        //選択したエリア
        SplitArea area = SplitArea.LEFT_DOWN;

        //重みをコピーする
        var keys = m_areaWeights.Keys;
        foreach(var key in keys)
        {
            m_subWeights[key] = m_areaWeights[key];
        }

        testNum++;
        //DebugFile.Instance.WriteLog("生成位置の取得開始");
        while (!isDecision)
        {
            //エリア選択
            area = WeightProbability.GetWeightRandm(ref m_subWeights);

            if (m_areaWeights.Count == 0)
            {
                Debug.Log("そんなスクリプトのつもりない");
                //UnityEditor.EditorApplication.isPlaying = false;
                return Vector3.zero;
            }
            if (m_subWeights.Count == 0)
            {
                Debug.Log("場所がない");
                //UnityEditor.EditorApplication.isPlaying = false;
                return Vector3.zero;
            }

            //指定エリアに生成可能位置があるか確認する
            //if (m_generatablePos[area].Count <= 0)
            if (!IsOpening(area))
            {
                //選択したエリアを排除する
                m_subWeights.Remove(area);
                continue;
            }
            else
            {
                isDecision = true;
            }
        }
        //DebugFile.Instance.WriteLog("生成位置の取得終了");

        //エリアの重みを変更する
        foreach (SplitArea areaNum in Enum.GetValues(typeof(SplitArea)))
        {
            //選択したエリアの重みを減少
            if (areaNum == area) m_areaWeights[areaNum] -= m_basicWeight;
            //非選択エリアの重みを増加
            else m_areaWeights[areaNum] += m_basicWeight;

            //エリアが選択不可能状態になるのを防ぐ
            if (m_areaWeights[areaNum] <= 0) m_areaWeights[areaNum] = m_basicWeight;
        }

        //選択したエリアから生成可能な位置をリストする
        ///List<int> generateIdx = new List<int>();
        var generateIdxs = m_generatablePos[area].Select((e, i) => new { element = e, index = i }).Where(value => value.element.isUser == true).Select(value => value.index).ToList();
        Debug.Log(testNum + "リストのセレクト終了");

        //選択したエリアからフロアを取得する
        int idx = UnityEngine.Random.Range(0, generateIdxs.Count);
        Vector2Int floor = m_generatablePos[area][generateIdxs[idx]].pos;

        //生成位置を管理クラスに渡す
        _generated.Add(floor);

        //ToDo：変更予定
        //m_generatablePos[area].Remove(floor);
        m_generatablePos[area][generateIdxs[idx]].isUser = false;

        //床の生成位置を取得
        Vector3 createPos = m_floorMana.GetFloorPos(floor);

        //高さを制限
        if (createPos.y < 0.0f) createPos.y = 0.0f;

        //生成位置に幅を持たせる
        Vector2 offset = new Vector2(UnityEngine.Random.Range(-m_floorSize / 2.0f, m_floorSize / 2.0f), 
            UnityEngine.Random.Range(-m_floorSize / 2.0f, m_floorSize / 2.0f));
        createPos.x += offset.x;
        createPos.z += offset.y;

        //DebugFile.Instance.WriteLog("生成位置の返却");
        return createPos;
    }
    
    /// <summary>
    /// 生成可能な空き場所があるか確認する
    /// </summary>
    public bool IsOpenPlace()
    {
        int openPlace = 0;

        var keys = m_generatablePos.Keys;
        foreach(var key in keys)
        {
            for (int idx = 0; idx < m_generatablePos[key].Count; ++idx)
            {
                openPlace += Convert.ToInt32(m_generatablePos[key][idx].isUser);
            }
        }

        if (openPlace > 0) return true;

        return false;
    }

    /// <summary>
    /// 生成可能位置があるか全てのエリアから確認する
    /// </summary>
    public int IsAllOpenPlace()
    {
        int place = 0;

        var keys = m_generatablePos.Keys;
        foreach (var key in keys)
        {
            for (int idx = 0; idx < m_generatablePos[key].Count; ++idx)
            {
                if (m_generatablePos[key][idx].isUser)
                {
                    ++place;
                }
            }
        }

        return place;
    }

    /// <summary>
    /// エリアごとに空きがあるか確認する
    /// </summary>
    private bool IsOpening(SplitArea _area)
    {
        for (int idx = 0; idx < m_generatablePos[_area].Count; ++idx)
        {
            if (m_generatablePos[_area][idx].isUser) return true;
        }

        return false;
    }

    /// <summary>
    /// 非選択エリアを再度選択状態に変更する
    /// </summary>
    /// <param name="_generated"></param>
    public void Reset(Vector2Int _generated)
    {
        //エリア分
        foreach (SplitArea area in Enum.GetValues(typeof(SplitArea)))
        {
            //エリア中のフロア分
            GeneratePos pos = m_generatablePos[area].Find(value => value.pos == _generated);
            if (pos != null)
            {
                pos.isUser = true;
                return;
            }
        }
    }

    //使用しない
    public override void MyUpdate() { }
}
