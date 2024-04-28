using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using _2._Simple_Socket_Client;

namespace _3._WPF_Client;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void btnSend_OnClick(object sender, RoutedEventArgs e)
    {
        var ip = IPAddress.Parse(txtServerIP.Text);
        var port = int.Parse(txtServerPort.Text);

        try
        {
            TCPClient client = new(new IPEndPoint(ip, port));
            client.Send(txtMessage.Text);
            var response = client.Recieve();
            MessageBox.Show(response, "Відповідь", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message, "Помилка");
        }
        
    }
}