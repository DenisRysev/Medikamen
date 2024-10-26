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
    /// Логика взаимодействия для Medikaments.xaml
    /// </summary>
    public partial class Medikaments : Window
    {
        public MedikamentEntities9 db = new MedikamentEntities9();
        List<Склад> sk = new List<Склад>();
        public Сотрудник user;
        public class Sklads
        {
            public string medName { get; set; }
            public string name { get; set; }
            public int kol { get; set; }
        }
        //Заполнение ListView
        public Medikaments(Сотрудник user)
        {
            this.user = user;
            InitializeComponent();
            sk =db.Склад.ToList();
            for(int i = 0; i < sk.Count; i++)
            {
                int codesk = Convert.ToInt32(sk[i].КодСклада);
                int codemk = Convert.ToInt32(sk[i].КодМедикамента);
                Склады sklads = db.Склады.Where(x =>x.КодСклада == codesk).FirstOrDefault();
                Медикамент med = db.Медикамент.Where(x => x.КодМедикамента == codemk).FirstOrDefault();
                var Sklads = new Sklads { medName = med.НаименованиеМедикамента, name = sklads.Наименование, kol = Convert.ToInt32(sk[i].Количество) };
                ListV.Items.Add(Sklads);
            }

        }
        //Переход на форму Кладовщика
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Sklad skl = new Sklad(user);
            skl.Show(); this.Close();
        }
    }
}
