using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// RIgidbodyでの移動先がステージ外か確認する
/// </summary>
public static class IsEdgeRigid
{
    //床の管理クラス
    static FloorManager m_floorManager = null;

	/// <summary>
	/// Rigidbodyのvelosityのn秒後の移動先がステージ内か確認する
	/// </summary>
	/// <param name="_rigid"> 呼び出し元 </param>
	/// <param name="_startPos"> 移動開始位置 </param>
	/// <param name="_nTime"> n秒後の位置で判定する </param>
	/// <param name="_offset"> 端からどれぐらい離れた距離から判定するか </param>
	/// <returns> trueならステージ内 </returns>
	public static bool InsideStage(this Rigidbody _rigid, Vector3 _startPos, float _nTime = 1.0f, float _offset = 1.0f)
	{
		//床管理クラスを取得
		if (!m_floorManager) m_floorManager = GameObject.FindGameObjectWithTag("FloorManager").GetComponent<FloorManager>();
		//床がシーンにない場合などは、処理をしない
		if (!m_floorManager) return false;

		//重力の具合を計算
		var halfGravityX = Physics.gravity.x * 0.5f;
		var halfGravityZ = Physics.gravity.z * 0.5f;
		//移動量を計算
		var moveX = _rigid.velocity.x * Time.fixedDeltaTime + _nTime + halfGravityX + Mathf.Pow(_nTime, 2.0f);
		var moveZ = _rigid.velocity.z * Time.fixedDeltaTime + _nTime + halfGravityZ + Mathf.Pow(_nTime, 2.0f);
		//移動量の符号を適応
		if (_rigid.velocity.x < 0.0f) moveX = -moveX;
		if (_rigid.velocity.z < 0.0f) moveZ = -moveZ;
		//移動先を計算
		Vector3 movePos = _startPos + new Vector3(moveX, 0.0f, moveZ);

		//ステージ内か確認
		bool inside = true;

		if (movePos.x <= (m_floorManager.StageMinEdge.x + _offset) || movePos.x >= (m_floorManager.StageMaxEdge.x - _offset))
			inside = false;

		if (movePos.z <= (m_floorManager.StageMinEdge.y + _offset) || (movePos.z >= m_floorManager.StageMaxEdge.y - _offset))
			inside = false;

		//領域外の場合
		if(!inside)
        {
			_rigid.velocity = Vector3.zero;
        }

		return inside;
	}
	
}
