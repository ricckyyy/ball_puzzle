# sixボールパズル
## 現状のリソース
* ボールの連結処理
* ボール連結による削除処理
* ボールの時間による自動生成処理
* オブジェクトの回転処理

## 変更
### 一言でいうと
* ボールを消しただけ相手にダメージを与える対戦型ゲーム

* イメージ : テトリス、switchの6ボールパズル

### 内容
- 対戦型ゲーム
- ゲーム性
    - 横に9個縦に9個詰めるステージ
    - ボールを6コ繋げる
        - 6個繋げたら消えて相手へダメージ
        - ダメージ量について
            - ピラミッド
                - 6 x 2
            - ヘキサゴン
                - 6 x 4
            - ストレート
                - 18個??
    - アイテムで有利に戦う
        - 個数減らす(回復系)
        - 相手から攻撃を受け付けない(ガード系)
        - 相手へのダメージ量を増やす(攻撃系)
    -  ダメージは蓄積させる
        - 反撃要素あり