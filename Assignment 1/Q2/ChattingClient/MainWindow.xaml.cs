using ChattingInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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

namespace ChattingClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static IChattingService Server;
        private static DuplexChannelFactory<IChattingService> _channelFactory;
       


        public MainWindow()
        {
            InitializeComponent();
            _channelFactory = new DuplexChannelFactory<IChattingService>(new ClientCallback(),"ChattingServiceEndPoint");
            Server = _channelFactory.CreateChannel();

           


        }


        public void TakeMessage(string message,string userName)
        {
            TextDisplayTextBox.Text += userName + ":" + message + "\n";
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            Server.SendMessageToAll(MessageTextBox.Text, UserNameTextBox.Text);
            string message = MessageTextBox.Text;
            string s1 = String.Empty;
            string s2 = String.Empty;

            bool check = true;

            for (int i = 0; i < message.Length; i++)
            {

                if (check == false)
                {
                    s2 = s2 + message[i];
                }

                if (check == true)
                {
                    if (message[i] != ':')
                    {
                        s1 = s1 + message[i];
                    }
                    else
                        check = false;
                }
            }
                TakeMessage(s1, "You");
                MessageTextBox.Text = "";
            
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            int returnValue = Server.Login(UserNameTextBox.Text);
            if(returnValue==1)
            {
                MessageBox.Show("You are already Logged in");

            }
            else if(returnValue==0)
            {
                MessageBox.Show("You logged in!");
                WelcomeLabel.Content = "Welcome : " + UserNameTextBox.Text + "!";
                UserNameTextBox.IsEnabled = false;
                LoginButton.IsEnabled = false;

            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Server.Logout();
        }

       

        public void friendName(string n)
        {
            Server.getFriendName(n);
        }
    }
}
