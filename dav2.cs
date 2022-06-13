<Window x:Class="ekzamTest.classes.windowGlavMenu"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:ekzamTest.classes"    
        mc:Ignorable="d"
        Title="windowGlavMenu" Height="738" Width="1288" MinHeight="500" MinWidth="800" ResizeMode="CanResize" WindowStartupLocation="CenterScreen">
    <Grid>
        <Grid.RowDefinitions>
            <RowDefinition/>
        </Grid.RowDefinitions>
        <Grid x:Name="gridCheckLogin" Background="#FF76E383" HorizontalAlignment="Center" Width="676" Height="89" VerticalAlignment="Top" Margin="0,633,0,0" >
            <Button x:Name="btnAddItems" Content="Добавить" HorizontalAlignment="Left" Margin="10,0,0,0" VerticalAlignment="Center" Height="56" Width="196" FontSize="22" FontFamily="Comic Sans MS" Background="#FF498C51" Click="btnAddItems_Click"/>
            <Button x:Name="btnDeleteItems" Content="Удалить" HorizontalAlignment="Center" VerticalAlignment="Center" Height="56" Width="196" FontSize="22" FontFamily="Comic Sans MS" Background="#FF498C51" Click="btnDeleteItems_Click"/>
            <Button Content="Изменить" HorizontalAlignment="Left" Margin="470,0,0,0" VerticalAlignment="Center" Height="56" Width="196" FontFamily="Comic Sans MS" FontSize="22" Background="#FF498C51"/>
        </Grid>
        <Label x:Name="labelName" HorizontalContentAlignment="Center" Content="" Margin="0,8,358,0" VerticalAlignment="Top" Height="45" FontFamily="Comic Sans MS" FontSize="20" HorizontalAlignment="Right" Width="136"/>
        <Label x:Name="labelSername" HorizontalContentAlignment="Center" Content="" HorizontalAlignment="Right" Margin="0,8,193,0" VerticalAlignment="Top" Height="45" Width="136" FontFamily="Comic Sans MS" FontSize="20"/>
        <Label x:Name="labelOtchestv" HorizontalContentAlignment="Center" Content="Гость" HorizontalAlignment="Right" Margin="0,10,32,0" VerticalAlignment="Top" Height="45" Width="136" FontFamily="Comic Sans MS" FontSize="20"/>
        <Button x:Name="btnBack" Content="Выйти" FontSize="22" VerticalContentAlignment="Center" Click="btnBack_Click" Margin="10,12,0,0" Background="#FF498C51" Height="43" VerticalAlignment="Top" HorizontalAlignment="Left" Width="114"/>
        <ListView x:Name="listTovarCatalog" Visibility="Visible" IsSynchronizedWithCurrentItem="True" ScrollViewer.CanContentScroll="True" ScrollViewer.VerticalScrollBarVisibility="Disabled" ScrollViewer.HorizontalScrollBarVisibility="Disabled"  HorizontalAlignment="Left" Height="550" VerticalAlignment="Bottom" Width="1260" Background="Transparent" BorderBrush="Transparent" Foreground="White" FontSize="18" Margin="10,0,0,94" RenderTransformOrigin="0.5,0.5">
            <ListView.View>
                <GridView>
                    <GridViewColumn Header="Фото" Width="230">
                        <GridViewColumn.CellTemplate>
                            <DataTemplate>
                                <StackPanel>
                                    <Image Width="220" Source="{Binding productPhoto}"/>
                                </StackPanel>
                            </DataTemplate>
                        </GridViewColumn.CellTemplate>
                    </GridViewColumn>
                    <GridViewColumn Header="Наименования товара" Width="590">
                        <GridViewColumn.CellTemplate>
                            <DataTemplate>
                                <StackPanel>
                                    <TextBlock Width="580" TextWrapping="Wrap" Text="{Binding productName}"/>
                                    <TextBlock Width="580" TextWrapping="Wrap" Text="{Binding productDescription}"/>
                                    <TextBlock Width="580" TextWrapping="Wrap" Text="{Binding productManufacturer}"/>
                                    <TextBlock Width="580" TextWrapping="Wrap" Text="{Binding productCost}"/>
                                    <TextBlock Width="580" TextWrapping="Wrap" Text="{Binding productArticleNumber}"/>
                                </StackPanel>
                            </DataTemplate>
                        </GridViewColumn.CellTemplate>
                    </GridViewColumn>
                    <GridViewColumn Header="Наличие" Width="190">
                        <GridViewColumn.CellTemplate>
                            <DataTemplate>
                                <TextBlock Width="185" TextWrapping="Wrap" Text="{Binding productQuantityInStock}"/>
                            </DataTemplate>
                        </GridViewColumn.CellTemplate>
                    </GridViewColumn>
                </GridView>
            </ListView.View>
        </ListView>
    </Grid>
</Window>


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

namespace ekzamTest.classes
{
    public partial class windowGlavMenu : Window
    {
        public windowGlavMenu()
        {
            InitializeComponent();
            classLoadCatalog classCatalog = new classLoadCatalog();
            classCatalog.tovarList(this);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            windowAutorization autorizeWindow = new windowAutorization();
            autorizeWindow.Show();
            this.Close();
        }

        private void btnAddItems_Click(object sender, RoutedEventArgs e)
        {
            windowItemsAdd windowAdd = new windowItemsAdd();
            windowAdd.Show();
        }

        private void btnDeleteItems_Click(object sender, RoutedEventArgs e)
        {
            classItemsDelete classDelete = new classItemsDelete();
            classDelete.itemsDelete(this);
        }
    }
}

класс рентс

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ekzamTest.classes
{
    internal class rentsLoadClass
    {
        public string productPhoto { get; set; }
        public string productDescription { get; set; }
        public string productQuantityInStock { get; set; }
        public string productArticleNumber { get; set; }
        public string productCost { get; set; }
        public string productName { get; set; }
        public string productManufacturer { get; set; }
        
        
    }
}


Загрузка списка товаров

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace ekzamTest.classes
{
    internal class classLoadCatalog
    {
        public void tovarList(windowGlavMenu windowGlavMenu)
        {

            DataTable Select(string selectSQL)
            {
                DataTable dataTable = new DataTable("dataBase");
                SqlConnection sqlConnection = new SqlConnection("server=DELOWERPC\\SQLEXPRESS;Trusted_Connection=Yes;DataBase=Trade;");
                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = selectSQL;
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(dataTable);
                return dataTable;
            }

            DataTable dt_itemsLoad = Select("SELECT * FROM [dbo].[Product]"); 
            for (int i = 0; i < dt_itemsLoad.Rows.Count; i++)
            {
                rentsLoadClass rentsLoad = new rentsLoadClass();
                {
                    rentsLoad.productArticleNumber = dt_itemsLoad.Rows[i][0].ToString();
                    rentsLoad.productPhoto = dt_itemsLoad.Rows[i][4].ToString();
                    rentsLoad.productName = dt_itemsLoad.Rows[i][1].ToString();
                    rentsLoad.productDescription = dt_itemsLoad.Rows[i][2].ToString();
                    rentsLoad.productCost = dt_itemsLoad.Rows[i][6].ToString();
                    rentsLoad.productQuantityInStock = dt_itemsLoad.Rows[i][9].ToString();
                }
                if (Convert.ToInt32(dt_itemsLoad.Rows[i][5]) == 1)
                {
                    rentsLoad.productManufacturer = "АртМир";
                }
                else if (Convert.ToInt32(dt_itemsLoad.Rows[i][5]) == 2)
                {
                    rentsLoad.productManufacturer = "Волшебная мастерская";
                }
                else if (Convert.ToInt32(dt_itemsLoad.Rows[i][5]) == 3)
                {
                    rentsLoad.productManufacturer = "ОригамиПлюс";
                }
                else
                {
                    rentsLoad.productManufacturer = "ФлюидАрт";
                }
                windowGlavMenu.listTovarCatalog.Items.Add(rentsLoad);
            }
            
        }
    }
}

Изменение эллементов

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using System.Data.SqlClient;

namespace ekzamTest.classes
{
    internal class classItemsEdit
    {
        DataTable Select(string selectSQL)
        {
            DataTable dataTable = new DataTable("dataBase");
            SqlConnection sqlConnection = new SqlConnection("server=PC\\SQLEXPRESS01;Trusted_Connection=Yes;DataBase=Trade;");
            sqlConnection.Open();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = selectSQL;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }

        public int index = 0;
        public string idSearch;
        public void showItems(windowGlavMenu windowMenu)
        {
            dynamic itemSelectList = windowMenu.listTovarCatalog.SelectedItem;
            var id = itemSelectList.productArticleNumber;
            windowItemsAdd windowAddItems = new windowItemsAdd();
            windowAddItems.btnAddItems.Content = "Изменить";
            windowAddItems.labelTitle.Content = "Редактирование";
            windowAddItems.Show();
            string log;

            DataTable dt_search = Select("SELECT * FROM [dbo].[Product] WHERE [productArticleNumber] = '" + id + "'");
            if (dt_search.Rows.Count > 0)
            {
                dt_search = Select("SELECT * FROM [dbo].[Product]");
                for (int i = 0; i < dt_search.Rows.Count; i++)
                {
                    log = dt_search.Rows[i][0].ToString();
                    if (log == id)
                    {
                        index = i;
                    }

                }

                rentsLoadClass arend = new rentsLoadClass();
                {
                    idSearch = dt_search.Rows[index][0].ToString();
                    windowAddItems.textproductArticleNumberTovar.Text = dt_search.Rows[index][0].ToString();
                    windowAddItems.textImage.Text = dt_search.Rows[index][4].ToString();
                    windowAddItems.textNameTovar.Text = dt_search.Rows[index][1].ToString();
                    windowAddItems.textDescriptionTovar.Text = dt_search.Rows[index][2].ToString();
                    windowAddItems.textCostTovar.Text = dt_search.Rows[index][6].ToString();
                    windowAddItems.textCountTovar.Text = dt_search.Rows[index][9].ToString();
                }
                if (Convert.ToInt32(dt_search.Rows[index][5]) == 1)
                {
                    windowAddItems.textManufactureTovar.Text = "АртМир";
                }
                else if (Convert.ToInt32(dt_search.Rows[index][5]) == 2)
                {
                    windowAddItems.textManufactureTovar.Text = "Волшебная мастерская";
                }
                else if (Convert.ToInt32(dt_search.Rows[index][5]) == 3)
                {
                    windowAddItems.textManufactureTovar.Text = "ОригамиПлюс";
                }
                else
                {
                    windowAddItems.textManufactureTovar.Text = "ФлюидАрт";
                }

            }
        }

        public void changeItems(windowItemsAdd windowAddItems)
        {
            MessageBoxResult result = MessageBox.Show("Подтверждаете изменение?", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                int replace = 0;
                DataTable dt_search = Select("SELECT * FROM [dbo].[Product] WHERE [productArticleNumber] = '" + idSearch + "'");
                if (windowAddItems.textManufactureTovar.Text.ToString() == "АртМир")
                {
                    replace = 1;
                }
                else if (windowAddItems.textManufactureTovar.Text == "Волшебная мастерская")
                {
                    replace = 2;
                }
                else if (windowAddItems.textManufactureTovar.Text == "ОригамиПлюс")
                {
                    replace = 3;
                }
                else
                {
                    replace = 4;
                }
                DataTable dataTable = new DataTable("dataBase");
                SqlConnection sqlConnection = new SqlConnection("server=PC\\SQLEXPRESS01;Trusted_Connection=Yes;DataBase=Trade;");
                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = "Insert into [Product] (ProductArticleNumber,ProductName,ProductDescription,ProductPhoto,ProductManufacturer,ProductCost,ProductQuantityInStock, ProductStatus)values(@productNumberTovar,@productNameTovar,@productDescTovar,@productPhotoTovar,@productManufacureTovar,@productCostTovar,@productQualStTovar, @prouctStatTovar)";
                sqlCommand.Parameters.AddWithValue("@productNumberTovar", windowAddItems.textproductArticleNumberTovar.Text);
                sqlCommand.Parameters.AddWithValue("@productNameTovar", windowAddItems.textNameTovar.Text);
                sqlCommand.Parameters.AddWithValue("@productDescTovar", windowAddItems.textDescriptionTovar.Text);
                sqlCommand.Parameters.AddWithValue("@productPhotoTovar", windowAddItems.textImage.Text);
                sqlCommand.Parameters.AddWithValue("@productManufacureTovar", replace.ToString());
                sqlCommand.Parameters.AddWithValue("@productcostTovar", Convert.ToDecimal(windowAddItems.textCostTovar.Text));
                sqlCommand.Parameters.AddWithValue("@productQualStTovar", windowAddItems.textCountTovar.Text);
                sqlCommand.Parameters.AddWithValue("@productStatTovar", 1);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(dataTable);
                sqlConnection.Close();
            }
            windowAddItems.Close();
        }
    }
}


Класс удаления

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Windows;

namespace ekzamTest.classes
{
    internal class classItemsDelete
    {
        public void itemsDelete(windowGlavMenu windowGlavMenu)
        {
            MessageBoxResult result = MessageBox.Show("Подтверждаете?","Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                dynamic itemSelectList = windowGlavMenu.listTovarCatalog.SelectedItem;
                var idItemsSel = itemSelectList.productArticleNumber;

                DataTable dataTable = new DataTable("dataBase");
                SqlConnection sqlConnection = new SqlConnection("server=DELOWERPC;Trusted_Connection=Yes;DataBase=Trade;");
                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = "DELETE FROM [OrderProduct] WHERE [productArticleNumber] = @id";
                sqlCommand.CommandText = "DELETE FROM [Product] WHERE [productArticleNumber] = @id";
                sqlCommand.Parameters.AddWithValue("@id", idItemsSel);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(dataTable);
            }
        }
    }
}


Класс добавления

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Windows;

namespace ekzamTest.classes
{
    internal class classItemsAdd
    {
        public void additems(windowItemsAdd windowItemsAdd)
        {
            MessageBoxResult result = MessageBox.Show("Подтверждаете действие?", "Подтвердждение", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                DataTable dataTable = new DataTable("dataBase");
                SqlConnection sqlConnection = new SqlConnection("server=PC\\SQLEXPRESS01;Trusted_Connection=Yes;DataBase=Trade;");
                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = "Insert into [Product] (ProductArticleNumber,ProductName,ProductDescription,ProductPhoto,ProductManufacturer,ProductCost,ProductQuantityInStock, ProductStatus)values(@productNumberTovar,@productNameTovar,@productDescTovar,@productPhotoTovar,@productManufacureTovar,@productCostTovar,@productQualStTovar, @prouctStatTovar)";
                sqlCommand.Parameters.AddWithValue("@productNumberTovar", windowItemsAdd.textproductArticleNumberTovar.Text);
                sqlCommand.Parameters.AddWithValue("@productNameTovar", windowItemsAdd.textNameTovar.Text);
                sqlCommand.Parameters.AddWithValue("@productDescTovar", windowItemsAdd.textDescriptionTovar.Text);
                sqlCommand.Parameters.AddWithValue("@productPhotoTovar", windowItemsAdd.textImage.Text);
                sqlCommand.Parameters.AddWithValue("@productManufacureTovar", windowItemsAdd.textManufactureTovar.Text);
                sqlCommand.Parameters.AddWithValue("@productcostTovar", windowItemsAdd.textCostTovar.Text);
                sqlCommand.Parameters.AddWithValue("@productQualStTovar", windowItemsAdd.textCountTovar.Text);
                sqlCommand.Parameters.AddWithValue("@productStatTovar", 1.ToString());
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(dataTable);
                sqlConnection.Close();
            }
        }
    }
}
