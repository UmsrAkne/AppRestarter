﻿## README.md

# AppRestarter

AppRestarter は、指定されたプロセスを終了し、その後指定されたアプリケーションを起動するコンソールアプリケーションです。このアプリケーションは、プロセス名とアプリケーションのパスをコマンドライン引数として受け取ります。

## 使用方法

    AppRestarter <プロセス名> <AppPath>

    <プロセス名>: 終了させたいプロセスの名前。
    <AppPath>: 起動させたいアプリケーションのパス。

以下のコマンドは、notepad プロセスを終了し、指定されたパスのアプリケーションを起動します。

    AppRestarter notepad "C:\Path\To\YourApp.exe"

## エラーハンドリング

- コマンドライン引数が正しく指定されていない場合、エラーメッセージを表示します。
- 指定されたアプリケーションのパスが存在しない場合、エラーメッセージを表示します。
- プロセスの終了やアプリケーションの起動に失敗した場合、例外をキャッチしてエラーメッセージを表示します。