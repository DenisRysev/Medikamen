using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Medikament
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MedikamentEntities9   db =new MedikamentEntities9();
        public Сотрудник user;
        public MainWindow()
        {
            InitializeComponent();
            Capcha();
        }
        string pwd = "";
        //Генерация капчи
        public void Capcha()
        {
            String allowchar = " ";
            allowchar = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            allowchar += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,y,z";
            allowchar += "1,2,3,4,5,6,7,8,9,0";
            char[] a = { ',' };
            // записываем набор символов в массив
            String[] ar = allowchar.Split(a);
            //переменная в которой будет хранится значение капчи string temp = " "; //переменная, в которую будет записываться рандомный символ из массива Random r = new Random();
            string temp = " ";
            pwd = "";
            Random r = new Random();
            int kol = 6; // количество символов в капче
            for (int i = 0; i < kol; i++)
            {
                // генерируем рандомный символ из массива // мы берем элемент массива и задаем рандомный индекс элемента // обратите внимание, что диапазон рандома начинается с 0 и заканчивается длиной массива символов
                temp = ar[(r.Next(0, ar.Length))];
                pwd += temp;
                //выводим сформированный текст в поле

            }
            label2.Content = pwd;
        }
        //Авторизация
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            user = db.Сотрудник.FirstOrDefault(x => x.Логин == textBox7.Text && x.Пароль == textBox8.Text);
            if (user != null)
            {
                if (textBox9.Text == pwd)
                {

                    if (user.КодДолжности == 1)
                    {
                        MessageBox.Show("Авторизация успешна");
                        Sklad usr = new Sklad(user);
                        usr.Show(); this.Hide();
                    }
                    else if (user.КодДолжности == 2)
                    {
                        MessageBox.Show("Авторизация успешна");
                        admin adm = new admin(user);
                        adm.Show(); this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show("Капча введена неправильно");
                }
            }
            else
            {
                MessageBox.Show("Такого пользователя не существует");
            }
        }
    }
}
