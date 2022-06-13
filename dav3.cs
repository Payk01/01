<Window x:Class="tvorchestvo.windowAddItems"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:tvorchestvo.classes"
        mc:Ignorable="d"
        Title="windowAddItems" Height="546.117" Width="549.879" WindowStartupLocation="CenterScreen">
    <Grid>
        <TextBox x:Name="textName" HorizontalAlignment="Left" HorizontalContentAlignment="Center" VerticalContentAlignment="Center" Height="44" Margin="10,100,0,0" TextWrapping="Wrap" VerticalAlignment="Top" Width="199" FontSize="15" Background="#FF76E383"/>
        <Label Content="Наименование" HorizontalAlignment="Left" Margin="40,66,0,0" VerticalAlignment="Top" FontFamily="Comic Sans MS" FontSize="17" Width="132"/>
        <Label x:Name="labelTitle" Content="Добавление" HorizontalAlignment="Left" Margin="214,5,0,0" VerticalAlignment="Top" FontFamily="Comic Sans MS" FontSize="20"/>
        <TextBox x:Name="textDescription" HorizontalAlignment="Left" HorizontalContentAlignment="Center" VerticalContentAlignment="Center" Height="111" Margin="12,183,0,0" TextWrapping="Wrap" VerticalAlignment="Top" Width="199" FontSize="15" Background="#FF76E383"/>
        <Label Content="Описание" HorizontalContentAlignment="Center" HorizontalAlignment="Left" Margin="58,149,0,0" VerticalAlignment="Top" FontFamily="Comic Sans MS" FontSize="17" Width="98"/>
        <TextBox x:Name="textManufacture" HorizontalAlignment="Left" HorizontalContentAlignment="Center" VerticalContentAlignment="Center" Height="44" Margin="10,333,0,0" TextWrapping="Wrap" VerticalAlignment="Top" Width="199" FontSize="15" Background="#FF76E383"/>
        <Label Content="Производитель" HorizontalAlignment="Left" Margin="40,299,0,0" VerticalAlignment="Top" FontFamily="Comic Sans MS" FontSize="17" Width="144"/>
        <TextBox x:Name="textCost" HorizontalAlignment="Left" HorizontalContentAlignment="Center" VerticalContentAlignment="Center" Height="34" Margin="346,343,0,0" TextWrapping="Wrap" VerticalAlignment="Top" Width="105" FontSize="15" Background="#FF76E383"/>
        <Label Content="Цена" HorizontalContentAlignment="Center" HorizontalAlignment="Left" Margin="286,343,0,0" VerticalAlignment="Top" FontFamily="Comic Sans MS" FontSize="17" Width="54"/>
        <Label Content="руб" HorizontalContentAlignment="Left" VerticalContentAlignment="Top" HorizontalAlignment="Left" Margin="451,347,0,0" VerticalAlignment="Top" FontFamily="Comic Sans MS" FontSize="13" Width="30" Height="30"/>
        <Label Content="Фото" HorizontalAlignment="Left" Margin="365,76,0,0" VerticalAlignment="Top" FontFamily="Comic Sans MS" FontSize="17" Width="48"/>
        <TextBox x:Name="textNalich" HorizontalAlignment="Left" HorizontalContentAlignment="Center" VerticalContentAlignment="Center" Height="34" Margin="347,299,0,0" TextWrapping="Wrap" VerticalAlignment="Top" Width="105" FontSize="15" Background="#FF76E383"/>
        <Label Content="Наличие" HorizontalContentAlignment="Center" HorizontalAlignment="Left" Margin="258,299,0,0" VerticalAlignment="Top" FontFamily="Comic Sans MS" FontSize="17" Width="84"/>
        <Label Content="шт" HorizontalContentAlignment="Left" VerticalContentAlignment="Top" HorizontalAlignment="Left" Margin="452,303,0,0" VerticalAlignment="Top" FontFamily="Comic Sans MS" FontSize="13" Width="30" Height="30"/>
        <TextBox x:Name="textId" HorizontalAlignment="Left" HorizontalContentAlignment="Center" VerticalContentAlignment="Center" Height="44" Margin="10,429,0,0" TextWrapping="Wrap" VerticalAlignment="Top" Width="199" FontSize="15" Background="#FF76E383"/>
        <Label Content="ID" HorizontalContentAlignment="Center" HorizontalAlignment="Left" Margin="40,395,0,0" VerticalAlignment="Top" FontFamily="Comic Sans MS" FontSize="17" Width="144"/>
        <Button x:Name="btnAddItems" Content="Добавить" HorizontalAlignment="Left" Margin="396,471,0,0" VerticalAlignment="Top" Width="137" Height="36" FontFamily="Comic Sans MS" FontSize="18" Background="#FF498C51" Click="btnAddItems_Click"/>
        <TextBox x:Name="textPicAddres" HorizontalAlignment="Left" HorizontalContentAlignment="Center" VerticalContentAlignment="Center" Height="52" Margin="267,115,0,0" TextWrapping="Wrap" VerticalAlignment="Top" Width="257" FontSize="15" Background="#FF76E383"/>
        <Label Content="Путь к изображению" VerticalContentAlignment="Top" HorizontalAlignment="Left" Margin="341,167,0,0" VerticalAlignment="Top" FontFamily="Comic Sans MS" FontSize="10" Width="110"/>
    </Grid>
</Window>


using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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

namespace tvorchestvo
{
    public partial class windowAddItems : Window
    {
        public windowAddItems()
        {
            InitializeComponent();
        }
        private void btnAddItems_Click(object sender, RoutedEventArgs e)
        {
            if (btnAddItems.Content.ToString() == "Изменить")
            {
                classes.classChangeItems classChangeItems = new classes.classChangeItems();
                classChangeItems.changeItems(this);
            }

            else
            {
                classes.classAddItems classAddItems = new classes.classAddItems();
                try /// Проверка на ошибки запуска
                {
                    classAddItems.additems(this);
                    this.Close();
                }
                catch (Exception)
                {

                    MessageBox.Show("Ошибка, неверный тип данных или пустые поля");
                }
            }
            
        }
    }
}
