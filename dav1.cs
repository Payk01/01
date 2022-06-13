<Window x:Name="AutorizeWindows" x:Class="ekzamTest.windowAutorization"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:ekzamTest"
        mc:Ignorable="d"
        Title="windowAutorization" Height="450" Width="800" WindowStartupLocation="CenterScreen" WindowState="Normal" ResizeMode="NoResize" Icon="/images/icon.ico">
    <Grid>
        <TextBox x:Name="textUsername" HorizontalContentAlignment="Center" HorizontalAlignment="Center" Margin="0,101,0,0" TextWrapping="Wrap" FontSize="18" Text="" VerticalAlignment="Top" Width="260" Height="53"/>
        <PasswordBox x:Name="textPasswordUser" FontSize="18" HorizontalContentAlignment="Center" HorizontalAlignment="Center" VerticalAlignment="Top" Width="260" Height="44" Margin="0,198,0,0" FontFamily="Comic Sans MS"/>
        <Label Content="Авторизация" HorizontalAlignment="Center" Margin="0,10,0,0" VerticalAlignment="Top" Height="45" Width="152" FontFamily="Comic Sans MS" FontSize="22"/>
        <Label Content="Логин" HorizontalContentAlignment="Center" HorizontalAlignment="Center" Margin="0,62,0,0" VerticalAlignment="Top" Height="34" Width="136" FontFamily="Comic Sans MS" FontSize="20"/>
        <Label Content="Пароль" HorizontalContentAlignment="Center" HorizontalAlignment="Center" Margin="0,163,0,0" VerticalAlignment="Top" Height="35" Width="136" FontFamily="Comic Sans MS" FontSize="20"/>
        <Button x:Name="btnAuthorization" Content="Войти" FontSize="18" HorizontalAlignment="Center" Margin="0,359,0,0" VerticalAlignment="Top" Height="38" Width="124" FontFamily="Comic Sans MS" Background="#FF498C51" Click="btnAuthorization_Click"/>
        <Button x:Name="btnGuestLogin" Content="Гость " FontSize="18" HorizontalAlignment="Left" Margin="696,10,0,0" VerticalAlignment="Top" Height="34" Width="94" FontFamily="Comic Sans MS" Background="#FF76E383" Click="btnGuestLogin_Click"/>
        <Image HorizontalAlignment="Left" Height="210" Margin="10,62,0,0" VerticalAlignment="Top" Width="214" Source="/images/logo.png"/>
        <Grid x:Name="gridCaptch" Margin="265,252,220,75">
            <Label Content="Капча" HorizontalContentAlignment="Center" HorizontalAlignment="Left" VerticalAlignment="Top" Height="35" Width="81" FontFamily="Comic Sans MS" FontSize="20"/>
            <Image HorizontalAlignment="Left" Height="30" Margin="135,2,0,0" VerticalAlignment="Top" Width="124" Source="/images/Безымянный.png"/>
            <Label x:Name="labelCaapth" Content="" HorizontalContentAlignment="Center" HorizontalAlignment="Left" Margin="135,0,0,0" VerticalAlignment="Top" Height="35" Width="127" FontFamily="Comic Sans MS" FontSize="20"/>
            <TextBox x:Name="textCaptch" HorizontalContentAlignment="Center" HorizontalAlignment="Left" Margin="0,48,0,0" TextWrapping="Wrap" FontSize="18" Text="" VerticalAlignment="Top" Width="228" Height="34"/>
            <Button x:Name="btnCaptchReboot" Content="" FontSize="18" HorizontalAlignment="Left" Margin="233,48,0,0" VerticalAlignment="Top" Height="34" Width="49" FontFamily="Comic Sans MS" Click="btnCaptchReboot_Click">
                <Button.Background>
                    <ImageBrush ImageSource="/images/logo.png"/>
                </Button.Background>
            </Button>
        </Grid>
        <Label x:Name="labelTimeBlock" HorizontalContentAlignment="Center" VerticalContentAlignment="Center" Content="" Margin="585,119,30,113" FontFamily="Comic Sans MS" FontSize="40"/>
        <Label Content="ООО &quot;Творчество&quot;" HorizontalAlignment="Left" Margin="30,283,0,0" VerticalAlignment="Top" Height="45" Width="170" FontFamily="Comic Sans MS" FontSize="18"/>
        <Label x:Name="labelTextBL" Visibility="Hidden" Content="Блокировка" HorizontalAlignment="Left" Margin="616,326,0,0" VerticalAlignment="Top" Height="45" Width="124" FontFamily="Comic Sans MS" FontSize="20"/>

    </Grid>
</Window>


using ekzamTest.classes;
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
using System.Windows.Threading;

namespace ekzamTest
{
    public partial class windowAutorization : Window
    {
        public windowAutorization()
        {
            InitializeComponent();

            timerLock.Tick += new EventHandler(timerLockAut);
            timerLock.Interval = new TimeSpan(0, 0, 1);
            gridCaptch.Visibility = Visibility.Hidden;
        }

        classAutorization classLogin = new classAutorization();
       
        private void btnCaptchReboot_Click(object sender, RoutedEventArgs e)
        {
            classLogin.generateCaptcha(this);
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            
        }

        public DispatcherTimer timerLock = new DispatcherTimer();
        int intrerv = 11;
        public void timerLockAut(object sender, EventArgs e)
        {
            labelTextBL.Visibility = Visibility.Visible;
            btnAuthorization.Visibility = Visibility.Hidden;
            intrerv--;
            if (intrerv == 0)
            {
                timerLock.Stop();
                intrerv = 11;
                labelTimeBlock.Content = "";
                labelTimeBlock.Visibility = Visibility.Hidden;
                btnAuthorization.Visibility = Visibility.Visible;
                btnAuthorization.IsEnabled = true;
            }
            labelTimeBlock.Content = Convert.ToString(intrerv); // Отображение секунд
        }

        private void btnGuestLogin_Click(object sender, RoutedEventArgs e)
        {
            windowGlavMenu windowGlavMenu = new windowGlavMenu();
            windowGlavMenu.gridCheckLogin.Visibility = Visibility.Hidden;
            windowGlavMenu.Show();
            this.Close();
        }

        private void btnAuthorization_Click(object sender, RoutedEventArgs e)
        {
            classLogin.Autorize(this);
        }
    }
}


Класс логин

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Threading;

namespace ekzamTest.classes
{
    internal class classAutorization
    {
        public void Autorize(windowAutorization windowAutorization)
        {
            windowGlavMenu windowGlavMenu = new windowGlavMenu();
            DataTable Select(string selectSQL)
            {
                DataTable dataTable = new DataTable("dataBase");
                SqlConnection sqlConnection = new SqlConnection("server=DELOWERPC;Trusted_Connection=Yes;DataBase=Trade;");
                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = selectSQL;
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(dataTable);
                return dataTable;
            }

            if (windowAutorization.textUsername.Text != "" && windowAutorization.textPasswordUser.Password != "")
            {
                if (windowAutorization.textCaptch.Text.ToString() == windowAutorization.labelCaapth.Content.ToString() || windowAutorization.textCaptch.Text.ToString() != "")
                {
                    string log;
                    int index = 0;
                    DataTable dt_users = Select("SELECT * FROM [dbo].[User] WHERE [UserPassword] = '" + windowAutorization.textPasswordUser.Password + "' AND [UserLogin] = '" + windowAutorization.textUsername.Text + "'");
                    if (dt_users.Rows.Count > 0)
                    {
                        dt_users = Select("SELECT * FROM [dbo].[User]");
                        for (int i = 0; i < dt_users.Rows.Count; i++)
                        {
                            log = dt_users.Rows[i][4].ToString();
                            if (log == windowAutorization.textUsername.Text.ToString())
                            {
                                index = i;
                            }

                        }

                        if (Convert.ToInt32(dt_users.Rows[index][6]) == 1)
                        {
                            windowGlavMenu.gridCheckLogin.Visibility = Visibility.Visible;
                            windowAutorization.Close();
                        }
                        else
                        {
                            windowGlavMenu.gridCheckLogin.Visibility = Visibility.Hidden;
                            windowAutorization.Close();
                        }
                        windowGlavMenu.labelName.Content = dt_users.Rows[index][2].ToString();
                        windowGlavMenu.labelSername.Content = dt_users.Rows[index][1].ToString();
                        windowGlavMenu.labelOtchestv.Content = dt_users.Rows[index][3].ToString();
                        windowGlavMenu.Show();
                        windowAutorization.Close();
                    }
                    else
                    {
                        windowAutorization.gridCaptch.Visibility = Visibility.Visible;
                        MessageBox.Show("Логин или пароль не верные");
                        generateCaptcha(windowAutorization);
                    }
                }
                else
                {
                    MessageBox.Show("Блокировка 10 секнуд");
                    windowAutorization.btnAuthorization.IsEnabled = false;
                    windowAutorization.timerLock.Start();
                }
            }
            else
            {
                windowAutorization.gridCaptch.Visibility = Visibility.Visible;
                MessageBox.Show("Логин или пароль не введены");
                windowAutorization.textUsername.Clear();
                windowAutorization.textPasswordUser.Clear();
                generateCaptcha(windowAutorization);
            }
        }

        public void generateCaptcha(windowAutorization autorizeWindow)
        {
            char[] chars = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789".ToCharArray();
            string randomString = "";
            Random ran = new Random();
            for (int i = 0; i < 5; i++)
            {
                randomString += chars[ran.Next(0, chars.Length)];
            }
            autorizeWindow.labelCaapth.Content = randomString;
        }

    }
}


