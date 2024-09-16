using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IStateSpace
{
    /// <summary>
    /// ステートマシン
    /// </summary>
    public class StateMachineBase<T,B> where T : System.Enum where B : class
    {
        //現在の状態
        public T currenrState { get; private set; }

        protected Dictionary<T, IState<T,B>> stateDic = new Dictionary<T, IState<T,B>>();
      
        //状態の追加
        public void AddState(T _stateType, IState<T,B> _state,B _parentClass)
        {
            //既に追加済み
            if (stateDic.ContainsKey(_stateType)) return;

            //自身を親として登録
            _state.SetParent(_parentClass);

            stateDic.Add(_stateType, _state);
        }

        /// <summary>
        /// ステートの更新
        /// </summary>
        public void UpdateState()
        {
            if (!stateDic.ContainsKey(currenrState)) return;

            //更新後、遷移処理を行う
            ChangeState(stateDic[currenrState].Update());
        }

        /// <summary>
        /// 物理更新処理
        /// </summary>
        public void FixedUpdateState()
        {
            if (!stateDic.ContainsKey(currenrState)) return;

            stateDic[currenrState].FiexdUpdate();
        }

        /// <summary>
        /// 遷移処理
        /// </summary>
        /// <param name="_type"> 次の遷移処理 </param>
        /// 自己遷移はしないモノとする
        public void ChangeState(T _next)
        {
            //状態を所持しているか確認
            if (!stateDic.ContainsKey(_next)) return;
            //自己遷移はしない
            if (stateDic[currenrState] == stateDic[_next]) return;

            //現在の状態の退場処理
            if (stateDic.ContainsKey(currenrState)) stateDic[currenrState].Exit();

            //状態変更
            currenrState = _next;

            //次の状態の入場処理
            stateDic[currenrState].Entry();
        }

        public void InitState(T _first)
        {
            if(!stateDic.ContainsKey(_first))
            {
                Debug.Log("StateMachine：存在しないキーでスタートしている");
                return;
            }

            //初期状態に変更
            currenrState = _first;

            //入場処理開始
            stateDic[currenrState].Entry();
        }
    }

    /// <summary>
    /// ステートクラスのインターフェイス
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class IState<T,B> where T : System.Enum where B :class
    {
        //親のマシーンから操作対象のコンポーネントを取得する
        protected B board = null;

        //入場処理
        public abstract void Entry();

        /// <summary>
        /// 物理更新処理
        /// </summary>
        public virtual void FiexdUpdate() { return; }

        /// <summary>
        /// 更新処理
        /// </summary>
        /// <returns> 次の状態 </returns>
        public abstract T Update();
        
        // 退場処理
        public abstract void Exit();

        public void SetParent(B _parent) { board = _parent; }
    }

}



