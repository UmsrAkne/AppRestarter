using System.Diagnostics;

namespace AppRestarter
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Program
    {
        private static void Main(string[] args)
        {
            // コマンドライン引数のチェック
            if (args.Length != 2)
            {
                Console.WriteLine("使用法: ProcessTerminator <プロセス名> <AppPath> ");
                return;
            }

            var processName = args[0];
            var applicationPath = args[1];

            if (!File.Exists(applicationPath))
            {
                Console.WriteLine($"{applicationPath} が見つかりません。");
                return;
            }

            try
            {
                // 現在起動中のプロセスリストを取得
                var processes = Process.GetProcessesByName(processName);

                if (processes.Length == 0)
                {
                    Console.WriteLine($"プロセス '{processName}' が見つかりませんでした。");
                    return;
                }

                // 指定されたプロセスを終了
                foreach (var process in processes)
                {
                    process.Kill();
                    process.WaitForExit(); // プロセスが終了するのを待つ
                    Console.WriteLine($"プロセス '{processName}' (ID: {process.Id}) を終了しました。");
                }

                ExecuteProcess(applicationPath);

                // 自身を終了
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"プロセス '{processName}' を終了できませんでした: {ex.Message}");
            }
        }

        private static void ExecuteProcess(string path)
        {
            try
            {
                // プロセスを開始するための ProcessStartInfo オブジェクトを作成
                var processStartInfo = new ProcessStartInfo
                {
                    FileName = path,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                };

                // プロセスを開始
                using var process = Process.Start(processStartInfo);
                if (process == null)
                {
                    return;
                }

                // 出力を読み取る
                var output = process.StandardOutput.ReadToEnd();
                var error = process.StandardError.ReadToEnd();

                // プロセスの終了を待つ
                process.WaitForExit();

                // 出力を表示
                Console.WriteLine("Output:");
                Console.WriteLine(output);

                // エラー出力を表示
                if (!string.IsNullOrEmpty(error))
                {
                    Console.WriteLine("Error:");
                    Console.WriteLine(error);
                }

                // 終了コードを取得
                var exitCode = process.ExitCode;
                Console.WriteLine($"Exit Code: {exitCode}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"エラーが発生しました: {ex.Message}");
            }
        }
    }
}