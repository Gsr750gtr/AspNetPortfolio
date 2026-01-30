# AspNetPortfolio

## 概要
このプロジェクトはAPS.NETのサンプルです。
バックエンドアプリ（ASP.NET）
デスクトップアプリ（C#／WPF／MVVM）
モバイルアプリ（Android／Kotlin／Jetpack Compose）
バックエンド１つに対し、デスクトップとモバイルアプリの二つのUIからアクセスします。

## 使用技術
- ASP.NET Core Web API
- WPF(C#,Prism)
- Android (Kotlin,Jetpack Compose)

## 主な機能
- 取得（一覧：WPF / 1件指定：Android）
- 追加(WPF)
- 削除(WPF)

## 工夫した点
- Service を DI に登録し、ViewModel に通信処理を直接記述しない構成にした
- ASP.NET Core（API）と WPF（UI）を分離し、クライアント差し替え（WPF / Android）を前提とした構成にした
- DTO を共有プロジェクト（SharedDTOs）として切り出し、API とクライアント間で型を共有できるようにした
- record 型 DTO の JSON バインドで発生した問題を踏まえ、settable なプロパティを持つ DTO に修正した
- Android 側のトースト表示を「該当無し」「通信失敗」の2種類に絞り、必要最低限の情報のみを伝えるようにした

## 補足
学習・ポートフォリオ用途のサンプルです。
