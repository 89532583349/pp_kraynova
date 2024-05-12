using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Runtime;
using System.Windows;

namespace pp_kraynova;

public partial class hzchtoeto : Window
{
    public hzchtoeto()
    {
        InitializeComponent();
        string fullTable = "SELECT* FROM оборудование";
        ShowTable(fullTable);
        CmbFill();
    }

    private List<oborudovanie> obs;
    private string connStr = "server=localhost;database=pp_kraynova;port=3306;User Id=root;password=Mama.2003";
    private MySqlConnection conn;

    public void ShowTable(string sql)
    {
        obs = new List<oborudovanie>();
        conn = new MySqlConnection(connStr);
        conn.Open();
        MySqlCommand command = new MySqlCommand(sql, conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var Tech = new oborudovanie()
            {
                id = reader.GetInt32("ID"),
                Nazvanie = reader.GetString("Название"),
                Model = reader.GetString("Модель"),
                God = reader.GetInt32("Год_производства"),
            };
            obs.Add(Tech);
        }

        conn.Close();
        DataGrid.ItemsSource = obs;
    }

    private void Search(object? sender, TextChangedEventArgs e)
    {
        var client = obs;
        client = client.Where(x => x.Nazvanie.Contains(SearchFIO.Text)).ToList();
        DataGrid.ItemsSource = client;
    }

    private void SotFioCmb(object? sender, SelectionChangedEventArgs e)
    {
        var cmb = (ComboBox)sender;
        var Sotrud = cmb.SelectedItem as oborudovanie;
        var filterSot = obs
            .Where(x => x.Model == Sotrud.Model)
            .ToList();
        DataGrid.ItemsSource = filterSot;
    }

    public void CmbFill()
    {
        obs = new List<oborudovanie>();
        conn = new MySqlConnection(connStr);
        conn.Open();
        MySqlCommand command = new MySqlCommand("SELECT * FROM оборудование", conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var Tech = new oborudovanie()
            {
                id = reader.GetInt32("ID"),
                Nazvanie = reader.GetString("Название"),
                Model = reader.GetString("Модель"), 
                God = reader.GetInt32("Год_производства"),
            };
            obs.Add(Tech);
        }

        conn.Close();
        var sotcmb = this.Find<ComboBox>(name: "CmbSot");
        sotcmb.ItemsSource = obs.DistinctBy(x => x.Model);
    }

    private void DeleteData(object? sender, RoutedEventArgs e)
    {
        try
        {
            oborudovanie currentZaem = DataGrid.SelectedItem as oborudovanie;
            if (currentZaem == null)
            {
                return;
            }

            conn = new MySqlConnection(connStr);
            conn.Open();
            string sql = "DELETE FROM оборудование WHERE ID = " + currentZaem.id;
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            obs.Remove(currentZaem);
            ShowTable("SELECT * FROM оборудование");
        }
        catch (Exception ex)


        {
            Console.WriteLine(ex.Message);
        }
    }
    
    private void Reset_OnClick(object? sender, RoutedEventArgs e)
    {
        string fullTable = "SELECT * FROM оборудование";
        ShowTable(fullTable);
        SearchFIO.Text = string.Empty;
    }

    private void AddData(object? sender, RoutedEventArgs e)
    {
        oborudovanie newZaem = new oborudovanie();
        postavte3 add = new postavte3 (newZaem, obs);
        add.Show();
        this.Hide();
    }

    private void Edit(object? sender, RoutedEventArgs e)
    {
        oborudovanie currentZaem = DataGrid.SelectedItem as oborudovanie;
        if (currentZaem == null)
            return;
        postavte3 edit = new postavte3 (currentZaem, obs);
        edit.Show();
        this.Hide();
    }
}
    
