using BuildSoft.OscCore;
using System;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace YourNamespace
{
    class Program
    {
        // Windows API 関数の宣言
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        // ウィンドウを非表示にするための定数
        private const int SW_HIDE = 0;

        static void Main(string[] args)
        {
            // ウィンドウを非表示にする
            IntPtr handle = GetConsoleWindow();
            ShowWindow(handle, SW_HIDE);

            // コマンドライン引数を取得
            string[] commandLineArgs = Environment.GetCommandLineArgs();

            int arg1 = 1;

            if (commandLineArgs.Length >= 2)
            {
                if (int.TryParse(commandLineArgs[1], out int parsedArg))
                {
                    arg1 = parsedArg;
                }
                else
                {
                    Console.WriteLine("Error: Second command-line argument is not a valid integer.");
                    // Handle the error gracefully, perhaps by providing usage instructions or exiting the application
                    return;
                }
            }

            // 引数の値を表示
            Console.WriteLine($"osc: {arg1}");

            // OSC クライアントの作成とメッセージの送信
            OscClient Client = new OscClient("127.0.0.1", 9000);
            Client.Send("/avatar/parameters/VRCEmote", arg1);
            Task.Delay(2000).Wait();
            Client.Send("/avatar/parameters/VRCEmote", 0);
        }
    }
}
