using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Выбор адреса текстового файла
            openFileDialog1.ShowDialog();
            textBox1.Text = openFileDialog1.FileName;

        }

        private void button2_Click(object sender, EventArgs e)//Анализ текстового файла
        {
            //Проверка расширения .txt
            string str = textBox1.Text; //Адрес файла
            string rassh = Path.GetExtension(str);//Определение расширения файла

            //Проверка на объем
            FileInfo file = new FileInfo(str);//Создание экземпляра класса FileInfo
            long size = file.Length;//Определение объема файла в байтах

            //Проверка: расширение .txt или нет
            if (rassh != ".txt")
            {
                //Вывод предупреждения об ошибке
                MessageBox.Show("Вы не выбрали файл с расширением .txt", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            //Проверка: объем файла > 2 Гб
            else if(size > 2*Math.Pow(10,9))
            {
                //Вывод предупреждения об ошибке
                MessageBox.Show("Размер файла превышает 2 Гб", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                string inputLines;
                //Считывание текстового файла в переменную inputLines, параметр Encoding.Default определяет поддержку кодировок win-1251 и UTF-8
                inputLines = File.ReadAllText(str, Encoding.Default);

                //Создание строкового массива с разделителями
                var words = inputLines.Split(' ', ',').Select(word => word.ToLower());

                //Создание экземпляра класса Dictionary
                var dict = new Dictionary<string, int>();

                //Определение встречаемости слов
                foreach (var word in words)
                {
                    if (!string.IsNullOrEmpty(word))
                    {
                        if (!dict.ContainsKey(word))
                        {
                            dict[word] = 0;
                        }
                        dict[word]++;
                    }
                }

                //Поиск слова с максимальным индексом
                string maxword = "";
                int max = 0;
                foreach (var word in dict.Keys)
                {
                    if (dict[word]>max)
                    {
                        max = dict[word];
                        maxword = word;
                    }

                }
                //Вывод результата на форму
                labelResult.Text = "В данном тексте слово <" + maxword + "> встречается " + dict[maxword] + " раз";
            }
        }
        }
 }

