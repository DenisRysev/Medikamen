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
using System.Windows.Shapes;

namespace Medikament
{
    /// <summary>
    /// Логика взаимодействия для admin.xaml
    /// </summary>
    public partial class admin : Window
    {
        public MedikamentEntities9 db = new MedikamentEntities9();
        public Сотрудник user;
        List<Должность> dol = new List<Должность> ();
        List<Сотрудник> sotrudniki = new List<Сотрудник>();
        string fam, tel, potch, log, pass, pass2;
        //Заполнение выпадающего списка
        public admin(Сотрудник user)
        {
            InitializeComponent();
            this.user = user;
            txt1.Text = user.ФИО;
            txt3.Text = user.Логин;
            Должность dolg = db.Должность.Where(x => x.КодДолжности == user.КодДолжности).FirstOrDefault();
            txt6.Text = dolg.Наименование;
            sotrudniki = db.Сотрудник.ToList();
            dol = db.Должность.ToList();
            for (int i = 0; i < dol.Count; i++)
            {
                Combo.Items.Add(dol[i].Наименование);
            }

        }
        //Переход на форму входа
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow man = new MainWindow();
            man.Show(); this.Close();
        }

        //Смена вкладок tabcontrol
        private void dobavka_Click(object sender, RoutedEventArgs e)
        {
            tb1.SelectedIndex = 1;
        }
        //Заполнение ListView
        private void prosmotr_Click(object sender, RoutedEventArgs e)
        {
            ListV.Items.Clear();
            ListV.ItemsSource = sotrudniki.ToList();
        }
        //Смена вкладок tabcontrol
        private void prosmotr1_Click(object sender, RoutedEventArgs e)
        {
            tb1.SelectedIndex = 0;
        }
        //Очистка 
        private void ochist_Click(object sender, RoutedEventArgs e)
        {
            textBox1.Text = "";
            textBox1.Clear();
            textBox4.Text = "";
            textBox4.Clear();
            textBox5.Text = "";
            textBox5.Clear();
            textBox6.Text = "";
            textBox6.Clear();
        }

        //Разрешение ввода только цифр в количестве 11 символов
        private void textBox2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (char.IsLetter(c))
                {
                    e.Handled = true;
                    return;
                }
            }
            int maxLength = 11; // Максимальное количество цифр, которое можно ввести

            if (textBox2 != null && textBox2.Text.Length >= maxLength)
            {
                e.Handled = true; // Отменить ввод, если достигнуто максимальное количество символов
            }
            else if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true; // Отменить ввод, если введенный символ не является цифрой
            }
        }
        //Разрешение написания только букв
        private void textBox1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (char.IsDigit(c))
                {
                    e.Handled = true;
                    return;
                }
            }
        }

        //Регистрация
        private void registr_Click(object sender, RoutedEventArgs e)
        {
            fam = textBox1.Text;
            tel = textBox2.Text;
            log = textBox4.Text;
            pass = textBox5.Text;
            pass2 = textBox6.Text;
            if (textBox1.Text != "" && textBox2.Text != ""  && textBox4.Text != "")
            {
                if (pass == pass2)
                {
                    Сотрудник sot =db.Сотрудник.Where(x => x.Логин == log).FirstOrDefault();
                    if (sot!=null)
                    {
                        MessageBox.Show("Такой пользователь уже существует");
                    }
                    else
                    {
                        Должность dolgo = db.Должность.Where(x => x.Наименование == Combo.Text).FirstOrDefault();
                        Сотрудник sotka = new Сотрудник
                        {
                            ФИО = fam,
                            КодДолжности = dolgo.КодДолжности,
                            НомерТелефона = tel,
                            Логин = log,
                            Пароль = pass
                        };
                        db.Сотрудник.Add(sotka);
                        db.SaveChanges();
                        MessageBox.Show("Новый сотрудник добавлен в списки работников");

                    }
                }
                else
                {
                    MessageBox.Show("пароли не совпадают!");
                }
            } 
            else
            {
                MessageBox.Show("Не все данные введены!");
            }
        }
    }
    
}
