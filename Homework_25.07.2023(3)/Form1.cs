using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Homework_25._07._2023_3_
{
    public partial class Form1 : Form
    {
     
        PictureBox pictureBox1 = new PictureBox();
        Pen myPen;
        int[] points = new int[5];


        public Form1()
        {
            InitializeComponent();

            numericUpDown1.ValueChanged += numericUpDown1_ValueChanged;
            pictureBox1.Paint += pictureBox1_Paint;
            // расширенное окно для выбора цвета
            colorDialog1.FullOpen = true;
            // установка начального цвета для colorDialog
            colorDialog1.Color = this.BackColor;
            //цвет пера по умолчанию
            myPen = new Pen(Color.Aqua);
            
        }

       

        private void butadd_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            pictureBox1.Size = new Size(300, 300);
            pictureBox1.Location = new Point(20, 0);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            Controls.Add(pictureBox1);
            openFileDialog1.ShowDialog();
            pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);//картинка по выбору пользователя
            

        }

        private void butSave_Click(object sender, EventArgs e)
        {

            //сохранение картинки
            Bitmap bmpSave = new Bitmap(pictureBox1.DisplayRectangle.Width, pictureBox1.DisplayRectangle.Height);
            pictureBox1.DrawToBitmap(bmpSave, pictureBox1.DisplayRectangle);
            saveFileDialog1.FileName = "myPicture";
            saveFileDialog1.DefaultExt = "bmp";//Расширение имени файла по умолчанию
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.CreatePrompt = true;//если указан не существующий файл, то будет отображаться сообщение о его создании
            saveFileDialog1.OverwritePrompt = false; //если указан существующий файл, то будет отображаться сообщение о том, что файл не будет перезаписан
            saveFileDialog1.InitialDirectory = @"D:/";//устанавливает каталог, который отображается при первом вызове окна
            saveFileDialog1.Title = "Сохраните свой файл здесь";
            saveFileDialog1.Filter = "Image files (*.bmp)|*.bmp|All files (*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                bmpSave.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
                MessageBox.Show("Файл сохранен");
            }
        }

        private void buttonPaint_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            points[1] = Convert.ToInt32(textBox1.Text);
            points[2] = Convert.ToInt32(textBox2.Text);
            points[3] = Convert.ToInt32(textBox3.Text);
            points[4] = Convert.ToInt32(textBox4.Text);
            pictureBox1.Refresh();
        }

        private void butColor_Click(object sender, EventArgs e)//не работает
        {
            if (colorDialog1.ShowDialog() == DialogResult.Cancel) //Если окно закрылось с OK, то меняем цвет для пера 
                return;
            myPen.Color = colorDialog1.Color; //меняем цвет для пер

        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)//меняем толщину пера
        {

            myPen.Width = (float)numericUpDown1.Value;
       
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            //Рисуем линию
         
            e.Graphics.DrawLine(myPen, points[1], points[2], points[3], points[4]);

        }





    }
}
