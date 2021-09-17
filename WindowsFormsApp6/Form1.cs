using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp6
{
    public partial class Form1 : Form
    {
        private string fileName;
        private System.Timers.Timer timer = null;
        private int n = 10;
        

        public Form1()
        {
            // フォームを初期化する
            InitializeComponent();
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "テキストファイル(*.txt)|*.txt|すべてのファイル(*.*)|*.*";
            dialog.Title = "ファイルを保存する";
            if (dialog.ShowDialog() == DialogResult.OK)
                File.WriteAllText(dialog.FileName, textBox1.Text);
        }
        

        private void 元に戻すToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Undo();
        }

        private void 読み込みToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "テキストファイル(*.txt)|*.txt|すべてのファイル(*.*)|*.*";
            dialog.Title = "ファイルを読み込む";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                fileName = dialog.FileName;
                textBox1.Text = FileIO.ReadFromFile(fileName);
            }

        }

        private void 開始ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (timer == null)
            {
                // 1秒(1000ミリ秒)間隔のタイマー
                timer = new System.Timers.Timer(1000);

                timer.Elapsed += (sender2, e2) =>
                {
                    this.Invoke(new Action(UpdateText));
                };

                // タイマーの起動
                timer.Start();
            }
        }

        private void 終了ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // タイマーを多重起動させないようにする
            if (timer != null)
            {
                timer.Dispose();
                timer = null;
            }

            // textBox1の内容を元のテキストに戻す
            textBox1.Text = FileIO.ReadFromFile(fileName);
        }

        private void UpdateText()
        {
            var lines = FileIO.ReadLines(fileName);
            string buf = string.Empty;

            foreach (var line in lines.Skip(lines.Count - n).Take(n))
            {
                buf += line + "\r\n";
            }

            textBox1.Text = buf;
        }

        private void nを設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string inputText = Interaction.InputBox(
                "tail -n の引数を入れてください", "-n", n.ToString(), 200, 100);

            n = int.Parse(inputText);
        }

        private void 文字数カウントToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 文字数カウントのmessageboxを表示
            MessageBox.Show("文字数: " + textBox1.TextLength,
                "文字数カウント",
                MessageBoxButtons.OK);
        }

        private void フォントToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FontDialogクラスのインスタンスを作成
            FontDialog fd = new FontDialog();

            //初期のフォントを設定
            fd.Font = textBox1.Font;
            //初期の色を設定
            fd.Color = textBox1.ForeColor;
            //ユーザーが選択できるポイントサイズの最大値を設定する
            fd.MaxSize = 72;
            fd.MinSize = 4;
            //存在しないフォントやスタイルをユーザーが選択すると
            //エラーメッセージを表示する
            fd.FontMustExist = true;
            //横書きフォントだけを表示する
            fd.AllowVerticalFonts = false;
            //色を選択できるようにする
            fd.ShowColor = true;
            //取り消し線、下線、テキストの色などのオプションを指定可能にする
            //デフォルトがTrueのため必要はない
            fd.ShowEffects = true;
            //固定ピッチフォント以外も表示する
            //デフォルトがFalseのため必要はない
            fd.FixedPitchOnly = false;
            //ベクタ フォントを選択できるようにする
            //デフォルトがTrueのため必要はない
            fd.AllowVectorFonts = true;

            //ダイアログを表示する
            if (fd.ShowDialog() != DialogResult.Cancel)
            {
                //TextBox1のフォントと色を変える
                textBox1.Font = fd.Font;
                textBox1.ForeColor = fd.Color;
            }
        }

        private void 右端で折り返すToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // お約束
            ToolStripMenuItem item = (ToolStripMenuItem)sender;

            // チェック状態を反転させる
            textBox1.WordWrap = !item.Checked;
            item.Checked = !item.Checked;
        }

        private void 終了ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
