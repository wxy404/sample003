using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sample003
{
    class Program
    {
        static int[,] map =
        {
        //   0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 }, // 0
            {1,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 }, // 1
            {1,0,0,0,0,1,1,1,1,1,1,1,0,0,1,1,1,1,1,1 }, // 2
            {1,1,1,0,0,0,0,0,1,1,0,0,0,1,1,1,1,1,1,1 }, // 3
            {1,1,1,1,0,1,1,0,0,0,0,0,0,1,1,1,1,1,1,1 }, // 4
            {1,1,1,1,0,0,1,0,0,0,0,0,0,0,0,0,9,1,1,1 }, // 5
            {1,1,1,1,1,0,1,1,0,1,1,1,1,1,1,1,1,1,1,1 }, // 6
            {1,1,1,1,1,0,5,1,0,0,0,0,1,1,1,1,1,1,1,1 }, // 7
            {1,1,1,1,1,1,1,1,1,0,0,0,1,1,1,1,1,1,1,1 }, // 8
            {1,1,1,1,1,1,1,0,0,0,0,1,1,1,1,1,1,1,1,1 }, // 9
            {1,1,1,1,1,1,1,1,0,0,1,1,1,1,1,1,1,1,1,1 }, // 0
            {1,1,1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,1,1,1 }, // 1
            {1,1,1,1,1,1,1,1,1,0,0,0,9,1,1,1,1,1,1,1 }, // 2
            {1,1,1,1,1,1,1,1,1,1,0,0,1,1,1,1,1,1,1,1 }, // 3
            {1,1,1,1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,1,1 }, // 4
            {1,1,1,1,1,1,1,0,0,0,0,1,1,1,1,1,1,1,1,1 }, // 5
            {1,1,1,1,1,1,1,1,4,1,1,1,1,1,1,1,1,1,1,1 }, // 6
            {1,1,1,1,1,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1 }, // 7
            {1,1,1,5,0,6,1,1,1,1,1,1,1,1,1,1,1,1,1,1 }, // 8
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 }, // 9
        };

        // 地図の要素の配列
        static String[] mapChip =
        {
        //   0    1   2    3    4    5    6   7    8    9
            "  ","■","上","下","門","宝","空","回","出","罠"
        };
        //プレイヤーの座標
        private static int[] pos = new int[2] { 1, 1 };
        private static void initPlayer()
        {
            Locate(pos[0], pos[1]);
            Console.Write("●");
        }
        private static void clearplayer()
        {
            Locate(pos[0], pos[1]);
            Console.Write("  ");
        }
        /// <summary>
        /// 当たり判定用
        /// </summary>
        /// <param name="x">判定するｘ座標</param>
        /// <param name="y">判定するｙ座標</param>
        /// <returns></returns>
        private static bool CheckHitCheck(int x, int y)
        {
            int mapdata = map[y, x];
            // 壁
            if (map[y, x] == 1)
                return false;
            // 道
            else if (map[y, x] == 0)
                return true;
            // 壁や道以外
            return false;
        }

        static void Main(string[] args)
        {
            for(int y = 0; y < 20; y++)
            {
                for(int x = 0; x < 20; x++)
                {
                    Console.Write(mapChip[map[y, x]]);
                    //if (map[y, x] == 1)
                        //Console.Write("■");
                    //else if (map[y, x] == 0)
                        //Console.Write("  ");
                }
                Console.WriteLine("");
            }
            initPlayer();
            // カーソルの非表示
            Console.CursorVisible = false;
            
            
            // 永久ループ
            while(true)
            {
                // キー入力を受け取る（キーが入力されるまで先に進まない）
                ConsoleKeyInfo c = Console.ReadKey(true);
                // 入力されたキーが「エスケープキー」ならばループを抜ける
                if (c.Key == ConsoleKey.Escape)
                    break;
                UpdatePlayer(c);
                TregCheck(c);
            }
            /*
            // 乱数の初期化
            Random random = new Random();
            // 10回ループする
            for (int i = 0; i < 10; i++)
            {
                // ｘ、ｙの座標をランダムに取得
                // ０～３９
                var x = random.Next() % 40;
                // ０～２９
                var y = random.Next() % 30;
                Locate(x, y);
                Console.Write("★");
            }
            */
        }

        // 取得したキーの数
        private static int KeyCount = 0;
        /// <summary>
        /// 宝箱チェック
        /// </summary>
        /// <param name="c">キー入力</param>
        private static void TregCheck(ConsoleKeyInfo c)
        {
            // SPACEバーを押したら宝箱の確認
            // 自分の位置の周囲4方向を確認し、
            // 宝箱ならばキーをとる
            if(c.Key == ConsoleKey.Spacebar)
            {
                // 上を確認
                TChk(pos[0]    , pos[1] - 1);
                // 下を確認
                TChk(pos[0]    , pos[1] + 1);
                // 左を確認
                TChk(pos[0] - 1, pos[1]    );
                // 右を確認
                TChk(pos[0] + 1, pos[1]    );
            }
        }

        private static void TChk(int v1, int v2)
        {
            
        }

        /// <summary>
        /// キャラクターの移動操作
        /// </summary>
        /// <param name="c">キー入力</param>
        static private void UpdatePlayer(ConsoleKeyInfo c)
        {
            // キャラクターが移動するので今いる場所のキャラクターを消す
            clearplayer();
            // 入力されたキーが「左矢印」ならば左移動処理を実行
            if (c.Key == ConsoleKey.LeftArrow)
            {
                // 移動先が移動可能かどうかの判定
                if (CheckHitCheck(pos[0] - 1, pos[1]))
                    // 移動が可能なので現在のx座標を‐1する
                    pos[0] -= 1;
            }
            // 入力されたキーが「右矢印」ならば右移動処理を実行
            if (c.Key == ConsoleKey.RightArrow)
            {
                // 移動先が移動可能かどうかの判定
                if (CheckHitCheck(pos[0] + 1, pos[1]))
                    // 移動が可能なので現在のx座標を+1する
                    pos[0] += 1;
            }
            // 入力されたキーが「上矢印」ならば上移動処理を実行
            if (c.Key == ConsoleKey.UpArrow)
            {
                // 移動先が移動可能かどうかの判定
                if (CheckHitCheck(pos[0], pos[1] - 1))
                    // 移動が可能なので現在のy座標を‐1する
                    pos[1] -= 1;
            }
            // 入力されたキーが「下矢印」ならば下移動処理を実行    
            if (c.Key == ConsoleKey.DownArrow)
            {
                // 移動先が移動可能かどうかの判定
                if (CheckHitCheck(pos[0], pos[1] + 1))
                    // 移動が可能なので現在のy座標を+1する
                    pos[1] += 1;
            }
            //  新しい座標にキャラクターを表示する    
            initPlayer();
        }

        /// <summary>
        /// カーソルを指定の座標に移動させる
        /// </summary>
        /// <param name="x">X座標</param>
        /// <param name="y">Y座標</param>
        static void Locate(int x, int y)
        {
            Console.CursorLeft = x * 2;
            Console.CursorTop = y;
        }
    }
}
