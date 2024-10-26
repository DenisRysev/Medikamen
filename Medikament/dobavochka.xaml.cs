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
    /// Логика взаимодействия для dobavochka.xaml
    /// </summary>
    public partial class dobavochka : Window
    {
        public MedikamentEntities9 db = new MedikamentEntities9();
        List<Склад> sk = new List<Склад>();
        Медикамент med;
        List<ВидМедикамента> vid =new List<ВидМедикамента>();
        List<Измерение> ism = new List<Измерение>();
        public Сотрудник user;
        //заполнение выпадающих списков
        public dobavochka(Сотрудник user)
        {
            InitializeComponent();

            this.user = user;
            vid = db.ВидМедикамента.ToList();
            ism = db.Измерение.ToList();
            for (int i = 0; i < vid.Count; i++)
            {
                vidmed.Items.Add(vid[i].Наименование);
            }
            for (int i = 0; i < ism.Count; i++)
            {
                ismmed.Items.Add(ism[i].Наименование);
            }
        }

        //Добавление нового медикамента в список больницы
        private void dobavka_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Медикамент mm = db.Медикамент.Where(x => x.НаименованиеМедикамента == naim.Text).FirstOrDefault();
                if(mm !=null)
                {
                    MessageBox.Show("Данный препарат уже существует, в списках");
                }
                else
                {
                    if (naim != null && vidmed != null && ismmed != null && kol != null)
                    {
                        ВидМедикамента vm = db.ВидМедикамента.Where(x => x.Наименование == vidmed.Text).FirstOrDefault();
                        Измерение im = db.Измерение.Where(x => x.Наименование == ismmed.Text).FirstOrDefault();
                        //добавление медикамента в список
                        Медикамент newmed = new Медикамент
                        {
                            НаименованиеМедикамента = naim.Text,
                            КодВидаМедикамента = vm.КодВидаМедикамента,
                            КодИзмерения = im.КодИзмерения,
                            КоличествоВУпаковке = Convert.ToInt32(kol.Text),
                            СрокГодности = srok.Text,
                        };
                        db.Медикамент.Add(newmed);
                        db.SaveChanges();
                        // Создание места для медикамента на складах
                        for(int i = 1; i < 4; i++)
                        {
                            Склад skk = new Склад
                            {
                                КодМедикамента=newmed.КодМедикамента,
                                Количество=0,
                                КодСклада=i
                            };
                            db.Склад.Add(skk);
                            db.SaveChanges();
                        }
                        MessageBox.Show("Новый препарат добавлен");
                    }
                }
               
            }
            catch
            { }
        }
        //Разрешение написания только цифр
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
        //Переход на форму кладовщика
        private void back_Click(object sender, RoutedEventArgs e)
        {
            Sklad sklad = new Sklad(user);
            sklad.Show(); this.Close();
        }
    }
}
