using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemManagement
{
    /// <summary>
    /// 通常時のアイテムの出現位置の取得
    /// </summary>
    public class ItemAppearsPosition
    {
        //床管理クラス
        private FloorManager floorManager = null;

        private enum SplitArea
        {
            LEFT_DOWN = 0,
            RIGHT_DOWN,
            LEFT_TOP,
            RIGHT_TOP
        }

        //前回のエリア
        private SplitArea m_lastArea = SplitArea.LEFT_DOWN;
        //エリアの重み
        private Dictionary<SplitArea, int> m_areaWeight = new Dictionary<SplitArea, int>();
        //初期の重み
        private const int m_initWeight = 20;
        //重みの変動値
        private const int m_weightVariation = 10;

        //マップを四分割した際の床の添え字の範囲
        private struct SplitSubscript
        {
            public Vector2Int min;
            public Vector2Int max;
        }

        private SplitSubscript[] m_areaSubScript = new SplitSubscript[4];

        /// <summary>
        /// 生成時の初期化
        /// </summary>
        public ItemAppearsPosition()
        {
            //床管理クラスの取得
            floorManager = GameObject.FindGameObjectWithTag("FloorManager").GetComponent<FloorManager>();

            //床の大きさを取得
            int width = floorManager.GetFloorLength(0);
            int height = floorManager.GetFloorLength(1);
            int widthHalf = width / 2;
            int heightHalf = height / 2;
            //マップを分割
            //左下
            m_areaSubScript[(int)SplitArea.LEFT_DOWN].min = new Vector2Int(width - 1 - widthHalf, height - 1 - heightHalf);
            m_areaSubScript[(int)SplitArea.LEFT_DOWN].max = new Vector2Int(width - 1, height - 1);
            //右下
            m_areaSubScript[(int)SplitArea.RIGHT_DOWN].min = new Vector2Int(0, height - 1 - heightHalf);
            m_areaSubScript[(int)SplitArea.RIGHT_DOWN].max = new Vector2Int(width - 1 - widthHalf, height - 1);
            //左上
            m_areaSubScript[(int)SplitArea.LEFT_TOP].min = new Vector2Int(width - 1 - widthHalf, 0);
            m_areaSubScript[(int)SplitArea.LEFT_TOP].max = new Vector2Int(width - 1, height - 1 - heightHalf);
            //右上
            m_areaSubScript[(int)SplitArea.RIGHT_TOP].min = new Vector2Int(0, 0);
            m_areaSubScript[(int)SplitArea.RIGHT_TOP].max = new Vector2Int(width - 1 - widthHalf, height - 1 - heightHalf);

            //重みの初期化
            m_areaWeight.Add(SplitArea.LEFT_DOWN, m_initWeight + m_weightVariation);
            m_areaWeight.Add(SplitArea.RIGHT_DOWN, m_initWeight - m_weightVariation);
            m_areaWeight.Add(SplitArea.LEFT_TOP, m_initWeight - m_weightVariation);
            m_areaWeight.Add(SplitArea.RIGHT_TOP, m_initWeight - m_weightVariation);
        }

        /// <summary>
        /// 通常のアイテム排出時の位置
        /// </summary>
        public Vector3 GetNormalItemPos()
        {
            //戻り値
            Vector3 rePos = Vector3.zero;

            //キーリスト取得
            var keylist = new List<SplitArea>(m_areaWeight.Keys);

            //前回のエリアから重みを変動させる
            foreach (var key in keylist)
            {
                if (key == m_lastArea) m_areaWeight[key] -= m_weightVariation;
                else m_areaWeight[key] += m_weightVariation;

                //重みが最低値になる場合、初期化する
                if (m_areaWeight[key] <= 0) m_areaWeight[key] = m_initWeight;
            }

            //出現エリアの取得
            m_lastArea = WeightProbability.GetWeightRandm(ref m_areaWeight);
            int areaNum = (int)m_lastArea;

            //欲しい床の番号を取得
            //Random.Range(int型はmax未満、float型はmax以下)
            Vector2Int subscript = new Vector2Int(Random.Range(m_areaSubScript[areaNum].min.x, m_areaSubScript[areaNum].max.x + 1),
                                                  Random.Range(m_areaSubScript[areaNum].min.y, m_areaSubScript[areaNum].max.y + 1));


            //床管理クラスから指定番号の位置を取得
            rePos = floorManager.GetFloorPos(subscript);

            return rePos;
        }


        /// <summary>
        /// プレイヤーの位置を参照にして、アイテムの排出位置を渡す
        /// </summary>
        public Vector3 GetNormalItemPos_ByPlayer(Vector3 _playerPos)
        {
            Vector3 rePos = Vector3.zero;

            //プレイヤーのいる床の添え字番号を取得する
            Vector2Int floorNum = floorManager.GetFloorNumber(_playerPos);

            //アイテム出現位置の候補
            List<Vector2Int> candidateFloor = new List<Vector2Int>();

            int widthLimit = floorManager.GetFloorLength(0) - 1;
            int heightLimit = floorManager.GetFloorLength(1) - 1;

            //プレイヤーの周り(8方向)の添え字を求める
            for (int width = floorNum.x - 1; width < floorNum.x + 2; width++)
            {
                //配列内か確認
                if (width < 0 || width > widthLimit)
                    continue;

                for (int height = floorNum.y - 1; height < floorNum.y + 2; height++)
                {
                    //配列内か確認
                    if (height < 0 || height > heightLimit)
                        continue;

                    //出現位置候補を追加
                    candidateFloor.Add(new Vector2Int(width, height));
                }
            }

            //候補の中からランダムで位置を取得する
            floorNum = candidateFloor[Random.Range(0, candidateFloor.Count)];
            rePos = floorManager.GetFloorPos(floorNum);

            return rePos;
        }
    }

}
