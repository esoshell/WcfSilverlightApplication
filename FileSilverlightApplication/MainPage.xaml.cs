using FileSilverlightApplication.FileServiceReference;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace FileSilverlightApplication
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            label.Content = string.Empty;

            FileServiceClient client = new FileServiceClient();
            DoWorkRequest request = new DoWorkRequest();
            request.x = 2;
            client.DoWorkAsync(request);

            Upload(client);
            label.Content = "Success !";
        }

        private void Upload(FileServiceClient client)
        {
            OpenFileDialog filedialog = new OpenFileDialog();
            if (filedialog.ShowDialog() == true)
            {
                var localFile = filedialog.File;
                using (Stream fileStream = localFile.OpenRead())
                {
                    byte[] buffer = new byte[16 * 1024];
                    using (MemoryStream ms = new MemoryStream())
                    {
                        int read;
                        while ((read = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            ms.Write(buffer, 0, read);
                        }

                        var data = ms.ToArray();

                        UploadFileRequest request = new UploadFileRequest();
                        request.FileContents = data;

                        using (new OperationContextScope(client.InnerChannel))
                        {
                            // Add a SOAP Header to an outgoing request 
                            MessageHeader entrepreneurHeader = MessageHeader.CreateHeader("IdEntrepreneur", "http://tempuri.org", Guid.NewGuid());
                            OperationContext.Current.OutgoingMessageHeaders.Add(entrepreneurHeader);

                            MessageHeader headerFileName = MessageHeader.CreateHeader("FileName", "http://tempuri.org", localFile.Name);
                            OperationContext.Current.OutgoingMessageHeaders.Add(headerFileName);

                            HttpRequestMessageProperty requestMessage = new HttpRequestMessageProperty();
                            requestMessage.Headers["FileName"] = localFile.Name;
                            OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = requestMessage;

                            client.UploadFileAsync(request);
                        }

                        //UploadStream data = new UploadStream();
                        //data.stream = ms.ToArray();
                        //client.UploadStreamAsync(data);
                    }
                }
            }
        }
    }
}
