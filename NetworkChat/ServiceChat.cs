using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace NetworkChat
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "ServiceChat" в коде и файле конфигурации.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServiceChat : IServiceChat
    {
        List<ServerUser> users = new List<ServerUser>();
        int nextID = 1;

        public int Connect(string userName)
        {
            ServerUser user = new ServerUser()
            {
                UserID = nextID,
                UserName = userName,
                operationContext = OperationContext.Current
            };
            nextID++;
            SendMessage($"{user.UserName} подключился к чату!", 0);
            users.Add(user);
            return user.UserID;
        }

        public void Disconnect(int clientID)
        {
            ServerUser user = users.SingleOrDefault(u => u.UserID == clientID);
            if (user != null)
            {
                users.Remove(user);
                SendMessage($"{user.UserName} отключился от чата!", 0);
            }
        }

        public void SendMessage(string message, int userID)
        {
            foreach (ServerUser user in users)
            {
                string answer = DateTime.Now.ToShortTimeString();
                ServerUser currentUser = users.SingleOrDefault(u => u.UserID == userID);
                if (currentUser != null)
                {
                    answer += $": {currentUser.UserName} ";
                }
                answer += message;

                user.operationContext.GetCallbackChannel<IServerChatCallback>().MessageCallback(answer);
            }
        }
    }
}
