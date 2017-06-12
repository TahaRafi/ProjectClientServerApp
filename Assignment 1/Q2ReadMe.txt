ReadMe Q2:

1)Make interfaces for both client and server.
2)ip set to localhost and port set to 9000 in App.config file both client and server.
3)Using BindingConfiguration technique, defined service model externally in App.config.
4)DuplexChannelFatory managed Clients.


*****ChattingClient**********
-ClientCallback.cs manage multiple client and its function decalare in interface class IClient.cs
-MainWindow.xml they provide a wpf interface 

******ChattingServer*********
-Program.cs opens a port(9000 which is defined in App.config) and waiting for the client.
-ConnectedClient manage username and their connection.
-ChattingService manages  the Login,Logout,SendMessageToClient ,Maintain and add the clients in  "ConcurrentDictionary _connectedClients" data structure.


In this q2 we set App.Config through WCF Configuration.
 

