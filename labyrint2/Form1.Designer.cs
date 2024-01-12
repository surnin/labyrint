using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;




namespace labyrint2
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        /// 

        private List<Panel> stoppers = new List<Panel>();

        private Panel gamer = null;
        private Panel exit = null;

        private void PressedDown(object sender, KeyEventArgs e)
        {
            if (this.gamer == null)
                return;
            int left = this.gamer.Left;
            int top = this.gamer.Top;
            int[] old = new int[] { left, top };
            int step = 10;
            if (e.KeyCode == Keys.Up)
            {
                top -= step;
                if (top < 0)
                    top = 0;
            }
            if (e.KeyCode == Keys.Left)
            {
                left -= step;
                if (left < 0)
                    left = 0;
            }
            if (e.KeyCode == Keys.Down)
            {
                top += step;
                if (top + this.gamer.Height >= this.ClientSize.Height)
                    top = this.ClientSize.Height - this.gamer.Height;
            }
            if (e.KeyCode == Keys.Right)
            {
                left += step;
                if (left + this.gamer.Width >= this.ClientSize.Width)
                    left = this.ClientSize.Width - this.gamer.Width;
            }
            if (old[0] == left && old[1] == top)
                return;
            this.gamer.Top = top;
            this.gamer.Left = left;
            if (this.exit != null && this.exit.Bounds == this.gamer.Bounds)
            {
                string message = "Повторить игру? :)";
                string title = "Ура! Вы победили";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show(message, title, buttons);
                if (result == DialogResult.Yes)
                {
                    this.StartGame();
                } else
                {
                    this.Close();
                }
                return;
            }
            foreach (Panel stopper in this.stoppers)
            {
                if (stopper.Bounds.IntersectsWith(this.gamer.Bounds))
                {
                    this.gamer.Top = old[1];
                    this.gamer.Left = old[0];
                    return;
                }
            }
        }

        private Panel CreateSprite(int left, int top, int size, string src, Color bg)
        {
            Panel sprite = new Panel();
            sprite.Left = left;
            sprite.Top = top;
            sprite.Height = size;
            sprite.Width = size;
            sprite.BackgroundImage = System.Drawing.Image.FromFile("Z:\\projects\\visual-studio-app\\labyrint2\\labyrint2\\img\\" + src);
            sprite.BackColor = bg;
            return sprite;
        }
        
        private void StartGame()
        {
            int size = 30;
            this.Controls.Clear();
            this.stoppers.Clear();
            int[,] field =
            {
                {2, 1, 0, 1, 0, 0, 0, 0, 1, 0},
                {0, 1, 0, 0, 0, 1, 1, 0, 0, 0},
                {0, 1, 0, 1, 0, 1, 0, 1, 1, 1},
                {0, 0, 0, 1, 0, 0, 0, 0, 0, 0},
                {1, 1, 1, 1, 0, 1, 1, 1, 1, 0},
                {0, 0, 0, 1, 0, 0, 0, 0, 0, 0},
                {0, 1, 0, 1, 0, 1, 0, 1, 1, 0},
                {0, 1, 1, 1, 0, 1, 0, 0, 1, 0},
                {0, 0, 0, 1, 0, 1, 1, 0, 1, 0},
                {1, 1, 0, 0, 0, 1, 0, 0, 1, 3}
            };
            int rows = field.GetUpperBound(0) + 1;    // количество строк
            int columns = field.Length / rows;        // количество столбцов
                                                      // или так
                                                      // int columns = numbers.GetUpperBound(1) + 1;
            this.ClientSize = new System.Drawing.Size(columns * size, rows * size);
            Panel sprite = null;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    int value = field[i, j];
                    
                    switch (value)
                    {
                        case 1:
                            sprite = this.CreateSprite(j * size, i * size, size, "kirpich.png", System.Drawing.Color.Chocolate);
                            this.stoppers.Add(sprite);
                            break;
                        case 2:
                            sprite = this.CreateSprite(j * size, i * size, size, "mario.png", System.Drawing.Color.Blue);
                            this.gamer = sprite;
                            break;
                        case 3:
                            sprite = this.CreateSprite(j * size, i * size, size, "finish.png", System.Drawing.Color.Yellow);
                            this.exit = sprite;
                            break;
                        default:
                            sprite = this.CreateSprite(j * size, i * size, size, "flowers.png", System.Drawing.Color.Green);
                            break;
                    }
                    this.Controls.Add(sprite);
                }
            }
        }
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PressedDown);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Text = "Labirint";
            this.StartGame();
        }
        #endregion
    }
}

