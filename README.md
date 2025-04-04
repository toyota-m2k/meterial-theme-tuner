﻿# meterial-theme-tuner

最近、Androidアプリを Material Design 2 から、3 に移行しました。
可愛らしくて楽しげな雰囲気にしたいと思って、
[Material Design Builder](https://material-foundation.github.io/material-theme-builder/) を使って、
桜をイメージして、ピンク(primary)と緑(tertiary)を基調とした配色を作ってみました。

<img src="https://github.com/user-attachments/assets/ca87f586-b837-4d42-bad5-826a33829f48" width="200"/>

出来上がった配色がこちら。。。

<img src="https://github.com/user-attachments/assets/9d3f4e29-3589-4a37-803e-3f22ab3e6ef3" width="300"/>

うーん、桜というより、ココア＆抹茶。
基調色をビビッドな色に変えたり、いろいろ試行錯誤したが、思ったような配色になりません。
それが Material3 だよ、って言われるんだと思いますが、最初に指定する色はヒントで、それを元に、カラーコントラストが規定値になるよう調整されるので、例えば、Lightモードで明るい色のPrimaryカラーを指定することはできないようです。

Material3 がそういうものだとわかっても、「桜テーマ」が作りたい！！
そこで Material Design Builder で作ったデータを加工してみようと思い、エクスポートして colors.xml をエディタで開いて軽く絶望。。。

設定する色数は、基本47色で、それぞれ Light/Darkモード用に2種類、さらに、それぞれの NormalContrast/MiddleContrast/HighContrast の3種類、
つまり、47 x 2 x 3 = 282色も RGB文字列で指定する。。。テキストエディタごときでは無理。

そこで作ったのが、本アプリ、ThemeTuner です。
Material Design Builder が出力した配色を微調整するためのアプリ、を目指します。

今できること
- Material Design Builder が出力する zip ファイルを読み込む。
- 読み込んだ colors.xml から、Light/Dark, Normal/MiddleContrast/HighContrast の各配色セットを整理
- 各配色セットを選んで Primary/Secondary/Tertiary/Surface の各配色をビジュアルに表示
- 各配色毎に色を変更(BG/FGボタン）

Todo:
- 作成したデータの保存（...そう、今はただのビューアーw）
- Undo/Redo
- セルの選択
- セルの色のコピー&ペースト
- 各セルの色の入れ替え（primaryとprimaryContainerを入れ替える、とか)
- Dark(or Light)モードのどちらかを使わない設定（Light/Nightのどちらかは結構いい感じの場合がある）

Future:

ゆくゆくは、単体で利用可能な Color Builder にしたい。
