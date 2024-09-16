// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "UI/Default"
{
    Properties
    {
        // テクスチャが一致した場合にバッチングが効くようにするため、
        // テクスチャは Material から直接ではなく MaterialPropertyBlock 経由で設定する
        // （なお、UI シェーダーでは基本的に MaterialPropertyBlock を使うことはできない）
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}

        // 色
        _Color ("Tint", Color) = (1,1,1,1)
        
        // ステンシル比較関数
        // UnityEngine.Rendering.CompareFunction で定義されている
        // https://docs.unity3d.com/ja/current/ScriptReference/Rendering.CompareFunction.html
        // 8 は Always であり、常にステンシルテストが成功する
        _StencilComp ("Stencil Comparison", Float) = 8

        // ステンシルテストの基準値（0 ～ 255）
        _Stencil ("Stencil ID", Float) = 0

        // ステンシルテスト成功時の挙動
        // UnityEngine.Rendering.StencilOp で定義されている
        // https://docs.unity3d.com/ja/current/ScriptReference/Rendering.StencilOp.html
        // 0 は Keep であり、変更を行わない
        _StencilOp ("Stencil Operation", Float) = 0

        // ステンシルテストを行った後にバッファに基準値を書き込むビットを指定するマスク
        // 0xFF なので基準値もバッファの内容もそのまま比較する
        _StencilWriteMask ("Stencil Write Mask", Float) = 255

        // ステンシルテストを行う前に基準値とバッファの内容の両方にかける論理和マスク
        // 0xFF なので基準値もバッファの内容もそのまま比較する
        _StencilReadMask ("Stencil Read Mask", Float) = 255

        // 描画を反映しないカラーチャンネルの設定
        // UnityEngine.Rendering.ColorWriteMask で定義されている
        // https://docs.unity3d.com/ja/current/ScriptReference/Rendering.ColorWriteMask.html
        // 15 は　All であり、全てのチャンネル(A/B/G/R)を出力する
        _ColorMask ("Color Mask", Float) = 15

        // UNITY_UI_ALPHACLIP を define するかどうか
        // 0 なら define しない
        // Mask を使わないのであれば必要ない
        [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
    }

    SubShader
    {
        // タグを使っていつ/どのようにレンダリングするかを指定する
        // https://docs.unity3d.com/ja/current/Manual/SL-SubShaderTags.html
        Tags
        {
            // UI 用なので RenderQueue は Transparent
            "Queue"="Transparent"

            // Projector コンポーネントの影響を受けない
            "IgnoreProjector"="True"

            // シェーダの分類。RenderQueue とは別
            // Shader Replacement を使わないなら必要ないが一応書いておく
            "RenderType"="Transparent"

            // Inspector の下のマテリアルビューの表示方式
            // デフォルトは Sphere（球体）だが Plane（2D）または Skybox（スカイボックス）が選べる
            "PreviewType"="Plane"

            // このシェーダーが Sprite 用かつアトラス化された場合には動作しないことを明示したい場合には False にする
            // 基本的には True で良い
            "CanUseSpriteAtlas"="True"
        }

        // プロパティで指定されたステンシルの設定値を実際に設定する
        // https://docs.unity3d.com/ja/current/Manual/SL-Stencil.html
        Stencil
        {
            // ステンシルテストの基準値
            Ref [_Stencil]

            // 比較関数
            Comp [_StencilComp]

            // ステンシルテスト成功時の挙動
            Pass [_StencilOp]

            // バッファ読み込み時ビットマスク
            ReadMask [_StencilReadMask]

            // バッファ書き込み時ビットマスク
            WriteMask [_StencilWriteMask]
        }

        // https://docs.unity3d.com/ja/current/Manual/SL-CullAndDepth.html
        // UI なのでカリング不要
        Cull Off

        // レガシーな固定機能ライティング（非推奨）
        // https://docs.unity3d.com/ja/current/Manual/SL-Material.html
        // 現在では（UI に限らず）基本的には Off で良い
        Lighting Off

        // 深度バッファへの書き込み
        // https://docs.unity3d.com/ja/current/Manual/SL-CullAndDepth.html
        // Transparent なので ZWrite は不要
        ZWrite Off

        // 深度テストの方法
        // https://docs.unity3d.com/ja/current/Manual/SL-CullAndDepth.html
        // Canvas が Overlay なら Always（常に描画）
        // それ以外なら LEqual（描画済みオブジェクトとの距離が距離が等しいまたはより近い場合に描画）
        ZTest [unity_GUIZTestMode]

        // https://docs.unity3d.com/ja/current/Manual/SL-Blend.html
        // Unity 2020.1 からピクセルブレンドは乗算済み透明（Premultiplied transparency）になった
	// https://issuetracker.unity3d.com/issues/transparent-ui-gameobject-ignores-opaque-ui-gameobject-when-using-rendertexture
        // 以前のブレンドにしたいなら Blend SrcAlpha OneMinusSrcAlpha にしてフラグメントシェーダーの乗算済み透明の処理を消す
	Blend One OneMinusSrcAlpha

        // 描画を反映しないカラーチャンネルの設定はプロパティで設定した値を使う
        ColorMask [_ColorMask]

        Pass
        {
            // UsePass で使う名前
            // https://docs.unity3d.com/ja/current/Manual/SL-Name.html
            Name "Default"

        // Cg/HLSL 開始
        CGPROGRAM
            // HLSL スニペット
            // https://docs.unity3d.com/ja/2018.4/Manual/SL-ShaderPrograms.html

            // 頂点シェーダーの関数名を指定
            #pragma vertex vert

            // フラグメントシェーダーの関数名を指定
            #pragma fragment frag
            
            // ターゲットレベルは全プラットフォーム向け
            // https://docs.unity3d.com/ja/current/Manual/SL-ShaderCompileTargets.html
            #pragma target 2.0

            // インクルードファイルの指定
            // インクルードファイルの場所は (Unity のインストール先)/Editor/Data/CGIncludes
            #include "UnityCG.cginc"
            #include "UnityUI.cginc"

            // Mask や RectMask2D が有効かどうかでクリッピング機能の有無を切り替えるためのシェーダーバリアント
            // UNITY_UI_CLIP_RECT キーワードはグローバルではなく、このシェーダーのみ対象で OK なのでローカル
            #pragma multi_compile_local _ UNITY_UI_CLIP_RECT

            // アルファ値でクリッピングする機能無しと有りの 2 種類のシェーダーバリアントを用意
            // UNITY_UI_ALPHACLIP キーワードはグローバルではなく、このシェーダーのみ対象で OK なのでローカル
            #pragma multi_compile_local _ UNITY_UI_ALPHACLIP

            // メッシュの頂点データの定義
            struct appdata_t
            {
                // 位置
                float4 vertex   : POSITION;

                // 頂点カラー
                float4 color    : COLOR;

                // 1 番目の UV 座標
                float2 texcoord : TEXCOORD0;

                // インスタンシングが有効な場合に
                //    uint instanceID : SV_InstanceID
                // という定義が付け加えられる。
                // 詳細は UnityInstancing.cginc を参照のこと
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            // 頂点シェーダーからフラグメントシェーダーに渡すデータ
            struct v2f
            {
                // 頂点のクリップ座標
                // システムが使う（GPU がラスタライズに使う）値なので SV (System Value) が付く
                float4 vertex   : SV_POSITION;

                // 色
                fixed4 color    : COLOR;

                // 1 番目の UV 座標
                float2 texcoord  : TEXCOORD0;

                // 2 番目の UV 座標に頂点のワールド空間での位置を格納して渡す
                float4 worldPosition : TEXCOORD1;

                // 3 番目の UV 座標にマスクのデータを格納して渡す
                half4  mask : TEXCOORD2;

                // VR 用。シングルパスで両目のレンダリングを可能にする。
                UNITY_VERTEX_OUTPUT_STEREO
            };

            // テクスチャデータを参照するためにはテクスチャサンプラ型の値をプロパティ経由で受け取る
            sampler2D _MainTex;

            // 色
            fixed4 _Color;

            // UI 用に Unity によって自動的に設定される。
            // 使用するテククチャが Alpha8 型なら (1,1,1,0) 、それ以外なら (0,0,0,0) になる
            fixed4 _TextureSampleAdd;

            // MaskableGraphic.SetClipRect() 等から CanvasRenderer.EnableRectClipping() で設定
            float4 _ClipRect;

            // テクスチャ変数名 に _ST を追加すると Tiling と Offset の値が入ってくる
            // x, y は Tiling 値の x, y で、z, w は Offset 値の z, w が入れられる
            float4 _MainTex_ST;

            // Unity 2020.1 から導入された Soft Mask（端をぼかす機能）の範囲
            // MaskableGraphic.SetClipSoftness() 等から CanvasRenderer.clippingSoftness で設定
            float _MaskSoftnessX;
            float _MaskSoftnessY;

            // 頂点シェーダー
            // appdata_t を受け取って v2f を返す
            v2f vert(appdata_t v)
            {
                // フラグメントシェーダーに渡す変数
                v2f OUT;

                // VR 用の目の情報と、GPU インスタンシングのためのインスタンシングごとの座標を反映させる
                // UnityInstancing.cginc を参照のこと
                UNITY_SETUP_INSTANCE_ID(v);

                // VR 用のテクスチャ配列の目を GPU に伝える
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);

                // オブジェクト空間の頂点の座標をカメラのクリップ空間に変換する
                // UnityShaderUtilities.cginc より
                // mul(UNITY_MATRIX_VP, float4(mul(unity_ObjectToWorld, float4(inPos, 1.0)).xyz, 1.0));
                //   UNITY_MATRIX_VP : 現在のビュー * プロジェクション行列
                //   unity_ObjectToWorld : 現在のモデル行列
                //   https://docs.unity3d.com/ja/2018.4/Manual/SL-UnityShaderVariables.html
                // 実態としては mul(UNITY_MATRIX_MVP, v.vertex) と等しい
                float4 vPosition = UnityObjectToClipPos(v.vertex);

                // 2 番目の UV 座標に頂点のワールド空間を渡す
                OUT.worldPosition = v.vertex;

                // 変換した頂点のクリップ座標を渡す
                OUT.vertex = vPosition;

                // w はカメラからの距離
                float2 pixelSize = vPosition.w;
		
                // _ScreenParams : 現在のスクリーン（レンダーターゲット）サイズ
                // UNITY_MATRIX_P : 現在のプロジェクション行列
                // 1 ピクセルに相当する大きさを求める
                pixelSize /= float2(1, 1) * abs(mul((float2x2)UNITY_MATRIX_P, _ScreenParams.xy));

                // 精度を落として Mask テクスチャの UV を作成
                float4 clampedRect = clamp(_ClipRect, -2e10, 2e10);
                float2 maskUV = (v.vertex.xy - clampedRect.xy) / (clampedRect.zw - clampedRect.xy);

                // マスクされた部分切り取ってテクスチャの UV を作成
                OUT.texcoord = float4(v.texcoord.x, v.texcoord.y, maskUV.x, maskUV.y);

                // ソフトマスクを考慮したクリッピング用のデータ
                OUT.mask = half4(v.vertex.xy * 2 - clampedRect.xy - clampedRect.zw, 
                0.25 / (0.25 * half2(_MaskSoftnessX, _MaskSoftnessY) + abs(pixelSize.xy)));

                // 頂点カラーにプロパティのカラーを乗算
                OUT.color = v.color * _Color;

                return OUT;
            }

            // フラグメントシェーダー
            fixed4 frag(v2f IN) : SV_Target
            {
                // テクスチャから色のサンプリング
                half4 color = (tex2D(_MainTex, IN.texcoord) + _TextureSampleAdd) * IN.color;

                #ifdef UNITY_UI_CLIP_RECT
                // ソフトマスクを考慮したクリッピング
                half2 m = saturate((_ClipRect.zw - _ClipRect.xy - abs(IN.mask.xy)) * IN.mask.zw);
                color.a *= m.x * m.y;
                #endif

                #ifdef UNITY_UI_ALPHACLIP
                // アルファが 0.001 以下なら（＝ほぼ透明なら）ピクセルを破棄する
                // エッジが汚い場合は数字を増やしてもいいかもしれない
                clip (color.a - 0.001);
                #endif

                // 乗算済み透明（Premultiplied transparency）なので RGB に Alpha を乗算しておく
                color.rgb *= color.a;

                return color;
            }
        // Cg/HLSL 終了
        ENDCG
        }
    }
}
