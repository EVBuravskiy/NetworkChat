using System.ServiceModel;

namespace NetworkChat
{
    // ПРИМЕЧАНИЕ. Можно использовать команду "Переименовать" в меню "Рефакторинг", чтобы изменить имя интерфейса "IServiceChat" в коде и файле конфигурации.
    //Интерфейс работы сервера
    [ServiceContract(CallbackContract = typeof(IServerChatCallback))] 		//Параметр CallbackContract
    public interface IServiceChat
    {
        [OperationContract]
        int Connect(string userName);

        [OperationContract]
        void Disconnect(int clientID);

        [OperationContract(IsOneWay = true)]
        void SendMessage(string message, int userID);
    }

    //Интерфейс работы на стороне клиента
    public interface IServerChatCallback
    {
        [OperationContract(IsOneWay = true)]
        void MessageCallback(string message);
    }

}
