# 3DGmaeExamination
後期の審査会に提出する3Dゲーム 

# タイトル 
Magic And Dungeons

# 操作方法
- WASD又は左スティックで移動
- マウス又は右スティックで視点操作
- 右クリック又はLBボタン長押しでFPSに切り替わりエイム
- エイム中に左クリック又はRBボタンで魔法を発射

# 自分で作ったところ 
- エイムした時のFPSの切り替えと魔法陣(UI)の展開
- 魔法の弾の処理(今回はTransformでやってしまったが、次回からはRigidbodyでやる)
- 魔法の弾をオブジェクトップールで管理
- 動く床の制御(Playerと親子関係を結んでも滑り落ちていたのを、乗っている時だけ床のVelocityをPlayerに代入することで解決した。原因は、Playerが動いている状態から入力を辞めた時にピタッと止まれる処理)
- マップ作り(AssetStorから持ってきたPrefabを組み合わせて作成)
- タイトルのオブジェクト(AssetStorから持ってきたPrefabを組み合わせて作成)
- タイトルの背景を動かす処理(空が動いているようにみえるが、実際はオブジェクトが回っている)

# これからやりたいこと 
- ストーリーSceneの作成
- タイトルからTutorialSceneへの移行
- タイトルの音やアニメーション（ダンジョンっぽい音楽と風の音、風に揺らぐ草みたいな）
- クリアした時の演出作成
- チュートリアル（timelineを使用して操作説明など）
- アイテムのシステム作成
- アイテムの種類追加
- アイテムによるマップギミック