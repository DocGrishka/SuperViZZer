using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Mail;
using Limilabs.Mail;
using Limilabs.Client.IMAP;





namespace messengre
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
              
        }

  

       
        private void Button_Click(object sender, RoutedEventArgs e)
        {
        try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("bloodamerika@gmail.com");
                mail.To.Add(sendto.Text);
                mail.Subject = "Test Mail";
                mail.Body = bodymail.Text;
               

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("bloodamerika", "bloodamerika3000");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                MessageBox.Show("Mail Send");
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
       {
            //IMAP ATTEMPT
            try
            {
                // Connect to the Google IMAP server.
                using (AE.Net.Mail.ImapClient Imap = new AE.Net.Mail.ImapClient("imap.gmail.com", "bloodamerika@gmail.com", "bloodamerika3000",AE.Net.Mail.AuthMethods.Login, 993, true))
                {
                    // Select the mailbox you want to read messages from.
                    Imap.SelectMailbox("INBOX");

                    //Displays the count of messages in selected mailbox.
                    label1.Content = Imap.GetMessageCount().ToString();

                    // Get the first 100 messages from selected mailbox. 0 is the first message
                    // MailMessage is a message in your mailbox, so this is an array of 100 messages from you selected mailbox.
                    AE.Net.Mail.MailMessage[] mm = Imap.GetMessages(0, 99);

                    //Loops through selected messages putting the subject in the listbox.
                    foreach (AE.Net.Mail.MailMessage m in mm)
                    {
                        listBox1.Items.Add(m.Subject);
                    }
                }
            }
            catch (Exception exn)
            {
                //Show error message when error occurs.
                
                textBox1.Text = exn.Message;
            }
        }
    }
}
