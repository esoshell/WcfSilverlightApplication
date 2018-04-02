using System;
using System.IO;
using System.ServiceModel;

namespace FileSilverlightApplication.Web.Services
{
    [MessageContract]
    public class UploadFileRequest
    {
        [MessageHeader(MustUnderstand = true, Namespace = "http://tempuri.org")]
        public string FileName;

        [MessageHeader(MustUnderstand = true, Namespace = "http://tempuri.org")]
        public Guid IdEntrepreneur;

        [MessageBodyMember(Order = 1)]
        public Stream FileContents;
    }
}