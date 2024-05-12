    using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MySql.Data.MySqlClient;
using System;

namespace pp_kraynova;

public partial class postavte3 : Window
{
    private List<oborudovanie> Zaem;
    private oborudovanie CurrentZaem;
    public postavte3(oborudovanie currentZaem, List<oborudovanie> obs)
    {
        InitializeComponent();
        CurrentZaem = currentZaem;
        this.DataContext = currentZaem;
        Zaem = obs;
    }
    private MySqlConnection conn;
    private string connStr = "server=localhost;database=pp_kraynova;port=3306;User Id=root;password=Mama.2003";
    private void Save_OnClick(object? sender, RoutedEventArgs e)
    {
        var obs = Zaem.FirstOrDefault(x => x.id == CurrentZaem.id);
        if (obs == null)
        {
            try
            {
                conn = new MySqlConnection(connStr);
                conn.Open();
                string add = "INSERT INTO оборудование(ID, Название, Модель, Год_производства) VALUES (" + Convert.ToInt32(id.Text) + ", '" + Obor.Text + "', " + Mod.Text + ", " + Convert.ToInt32(Proiz.Text) + ");";
                MySqlCommand cmd = new MySqlCommand(add, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error" + exception);
            }
        }
        else
        {
            try
            {
                conn = new MySqlConnection(connStr);
                conn.Open();
                string upd = "UPDATE оборудование SET Название = '" + Obor.Text + "', Модель = '" +  Mod.Text + "', Год_производства = " + Convert.ToInt32(Proiz.Text) + " WHERE id = " + Convert.ToInt32(id.Text) + ";";
                MySqlCommand cmd = new MySqlCommand(upd, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception exception)
            {
                Console.Write("Error" + exception);
            }
        }
    }

    private void GoBack(object? sender, RoutedEventArgs e)
    {
        hzchtoeto back = new hzchtoeto();
        this.Close();
        back.Show(); 
    }
}