using ChattingInterfaces;
using ChattingServer;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ChattingInterfaces
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
    public class ChattingService : IChattingService
    {
        public ConcurrentDictionary<string, ConnectedClient> _connectedClients = new ConcurrentDictionary<string, ConnectedClient>();
        public string friend;
        public string friend1;
        public int Login(string userName)
        {
            foreach (var client in _connectedClients)
            {
                if (client.Key.ToLower() == userName.ToLower())
                {
                    return 1;
                }
            }


            var establishedUserConnection = OperationContext.Current.GetCallbackChannel<IClient>();

            ConnectedClient newClient = new ConnectedClient();
            newClient.connection = establishedUserConnection;
            _connectedClients.TryAdd(userName, newClient);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Client Login:" + userName + "{0} ata{1} ", newClient.UserName, System.DateTime.Now);
            Console.ResetColor();

            return 0;
        }
        public void Logout()
        {
            ConnectedClient client = GetMyClient();
            string s1 = client.UserName;
          
            if(client!=null)
            {
                ConnectedClient removedClient=new ConnectedClient();
                try
                {
                    _connectedClients.TryRemove(client.UserName, out removedClient);
                }
                catch (Exception e)
                {
                  
                }

               

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Client Logoff:"+ friend1 + "{0} ata{1} " , removedClient.UserName,System.DateTime.Now);
                Console.ResetColor();

            }  
        }

        public ConnectedClient GetMyClient()
        {
            var establishedUserConnection = OperationContext.Current.GetCallbackChannel<IClient>();
            ConnectedClient clients=new ConnectedClient();
            foreach (var client in _connectedClients)
            {
                if(client.Value.connection==establishedUserConnection)
                {
                    clients=client.Value;
                    friend1=client.Key.ToLower();
                    break;
                }
                
            }

            return clients;
        }

        public void SendMessageToAll(string message, string userName)
        {
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
                        s1= s1 + message[i];
                    }
                    else
                        check = false;
                }



            }


            foreach (var client in _connectedClients)
            {
                try
                {
                    if (client.Key.ToLower() == s2)
                    {
                        client.Value.connection.GetMessage(s1, userName);
                       
                    }
                }
                    
                    catch(Exception e)
                {

                }
                
            }
        }


        public void getFriendName(string name)
        {
            friend = name;
        }


    }
}
