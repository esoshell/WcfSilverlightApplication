using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace FileSilverlightApplication.Web.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IFileService" in both code and config file together.
    [ServiceContract]
    public interface IFileService
    {
        [OperationContract]
        int DoWork(int x);

        [OperationContract]
        void UploadFile(UploadFileRequest request);

        [OperationContract(IsOneWay = true)]
        void UploadStream(Stream stream);
    }
}
