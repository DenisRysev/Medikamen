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
    /// Логика взаимодействия для Sklad.xaml
    /// </summary>
    public partial class Sklad : Window
    {
        public MedikamentEntities9 db = new MedikamentEntities9();
        public Сотрудник user;
        List<Медикамент> medik = new List<Медикамент>();
        List<Поставщик> post = new List<Поставщик>();
        List<Склад> sklad = new List<Склад> ();
        List<Склады> skladiki = new List<Склады>();
        ДокументПоставкии док;
        ДокументСписания спи;
        string naimk;
        string naimс;
        int n;
        int c;
        //Заполнение Выпадающих списков
        public Sklad(Сотрудник user)
        {
            InitializeComponent();
            n= 0;
            c = 0;
            this.user = user;
            txt1.Text=user.ФИО;
            txt3.Text=user.Логин;
            Должность dolg = db.Должность.Where(x => x.КодДолжности == user.КодДолжности).FirstOrDefault();
            txt6.Text = dolg.Наименование;
            medik = db.Медикамент.ToList();
            post = db.Поставщик.ToList();
            sklad = db.Склад.ToList();
            skladiki = db.Склады.ToList();
            for (int i = 0; i < skladiki.Count; i++)
            {
                sklads1.Items.Add(skladiki[i].Наименование);
                sklads2.Items.Add(skladiki[i].Наименование);
            }
            for (int i = 0; i < medik.Count; i++)
            {
                med.Items.Add(medik[i].НаименованиеМедикамента);
            }
            for (int i = 0; i < medik.Count; i++)
            {
                med_2.Items.Add(medik[i].НаименованиеМедикамента);
            }
            for (int i = 0; i < post.Count; i++)
            {
                postavchik.Items.Add(post[i].НаименованиеПоставщика);
            }


        }

        //Создание Документа поставки
        private void sozd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(postavchik!=null && n == 0)
                {

                    Поставщик pos = db.Поставщик.Where(x => x.НаименованиеПоставщика == postavchik.Text).FirstOrDefault();
                    
                    DateTime datenow = DateTime.Now;
                    док = new ДокументПоставкии
                    {
                        ДатаПоставки =datenow,
                        КодПоставщика =pos.КодПоставщика,
                        КодСотрудника=user.КодСотрудника
                    };
                    db.ДокументПоставкии.Add(док);
                    db.SaveChanges();
                    MessageBox.Show("Документ создан, добавтье медикаменты поставки");
                   
                }
            }
            catch 
            {

            }
            n++;
        }

        //Создание Поставки
        private void dobav_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (kol != null && med!=null&& n>0)
                {
                    Медикамент medikament = db.Медикамент.Where(x => x.НаименованиеМедикамента == med.Text).FirstOrDefault();
                    Склады skls = db.Склады.Where(x => x.Наименование == sklads2.Text).FirstOrDefault();
                    Склад skl =db.Склад.Where(x =>x.КодМедикамента==medikament.КодМедикамента && x.КодСклада==skls.КодСклада).FirstOrDefault(); 
                    Поставка post = new Поставка
                    {
                        КодДокументаПоставки=док.КодДокументаПоставки,
                        Количество=Convert.ToInt32(kol.Text),
                        КодПозицииСклада=skl.КодПозицииСклада
                    };
                    db.Поставка.Add(post);
                    db.SaveChanges();
                    MessageBox.Show("Медикамент добавлен в поставку");
                }
            }
            catch
            {

            }
        }
        //Переход на форму входа
        private void back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show(); this.Close();
        }
        //Смена вкладок tabcontrol
        private void spisanie_Click(object sender, RoutedEventArgs e)
        {
            perehods.SelectedIndex = 0;
        }

        private void postavka_Click(object sender, RoutedEventArgs e)
        {
            perehods.SelectedIndex = 1;
        }
        //Создание документа списания
        private void sozd_2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (naim_2 != null )
                {
                    DateTime datenow = DateTime.Now;
                     спи = new ДокументСписания
                    {
                         Комментарий=naim_2.Text,
                         КодСотрудника=user.КодСотрудника,
                         ДатаСписания=datenow
                    };
                    db.ДокументСписания.Add(спи);
                    db.SaveChanges();
                    MessageBox.Show("Документ создан, добавтье медикаменты для списания");
                    
                }
            }
            catch
            {

            }
            c++;
        }
        //Создание списка списания
        private void dobav_2_Click(object sender, RoutedEventArgs e)
        {
           
                if (kol_2 != null && sklads1!=null && med_2 != null && c > 0)
                {

                    Медикамент medikament = db.Медикамент.Where(x => x.НаименованиеМедикамента == med_2.Text).FirstOrDefault();
                    Склады skls = db.Склады.Where(x => x.Наименование == sklads1.Text).FirstOrDefault();
                    Поставщик pos = db.Поставщик.Where(x => x.НаименованиеПоставщика == postavchik.Text).FirstOrDefault();
                    Склад sk =db.Склад.Where(x => x.КодМедикамента ==medikament.КодМедикамента&&x.КодСклада==skls.КодСклада).FirstOrDefault();
                    if (Convert.ToInt32(kol_2.Text) <= sk.Количество)
                    {
                        СписокСписания spis = new СписокСписания
                        {
                            КодДокументаСписания=спи.КодДокументаСписания,
                            Количество=Convert.ToInt32(kol_2.Text),
                            КодПозицииСклада=sk.КодПозицииСклада
                        };
                        db.СписокСписания.Add(spis);
                        db.SaveChanges();
                        MessageBox.Show("Медикамент добавлен в список списания");
                    }
                    else
                    {
                        MessageBox.Show("На складе нет данного количества медикаментов, напишите количество медикамента для полного его списания");
                    }
                }
                else
                {
                    MessageBox.Show("Вы заполнили не все поля или не создали документ списания");
                }
            
        }
        //Переход на форму просмотра медикаментов на складе
        private void prosmotr_Click(object sender, RoutedEventArgs e)
        {
            Medikaments medikaments = new Medikaments(user);
            medikaments.Show(); this.Close();
        }
        //Переход на форму добавления медикаментов в список больницы
        private void dobavka_Click(object sender, RoutedEventArgs e)
        {
            dobavochka dobavoch = new dobavochka(user);
            dobavoch.Show(); this.Close();
        }

        //Разрещение на использование только цифр
        private void kol_2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            foreach (char c in e.Text)
            {
                if (char.IsLetter(c))
                {
                    e.Handled = true;
                    return;
                }
            }
        }
        private void kol_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            foreach (char c in e.Text)
            {
                if (char.IsLetter(c))
                {
                    e.Handled = true;
                    return;
                }
            }
        }
    }
}
