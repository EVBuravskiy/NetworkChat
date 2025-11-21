using System.Windows;
using NetworkClient.ServiceChat;

namespace NetworkClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, ServiceChat.IServiceChatCallback
    {
        bool isConnected = false;
        
        ServiceChat.ServiceChatClient client;

        int userID;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!isConnected) ConnectUser();
            else DisconnectUser();
        }

        void ConnectUser()
        {
            if (!isConnected)
            {
                client = new ServiceChatClient(new System.ServiceModel.InstanceContext(this));
                userID = client.Connect(tbUserName.Text);
                tbUserName.IsEnabled = false;
                btnConnectDisconnect.Content = "Disconnect";
                isConnected = true;
            }
        }

        void DisconnectUser()
        {
            if (isConnected)
            {
                client.Disconnect(userID);
                client = null;
                tbUserName.IsEnabled = true;
                btnConnectDisconnect.Content = "Connect";
                isConnected = false;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DisconnectUser();
        }

        public void MessageCallback(string message)
        {
            lbChat.Items.Add(message);
            lbChat.ScrollIntoView(lbChat.Items[lbChat.Items.Count - 1]);
        }

        private void tbMessage_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                if (client != null)
                {
                    client.SendMessage(tbMessage.Text, userID);
                    tbMessage.Text = "";
                }
            }
        }
    }
}
