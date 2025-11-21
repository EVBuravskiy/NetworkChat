using System.ServiceModel;

namespace NetworkChat
{
    public class ServerUser
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public OperationContext operationContext { get; set; }
    }

}
