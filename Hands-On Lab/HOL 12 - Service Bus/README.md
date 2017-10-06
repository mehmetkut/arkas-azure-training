## What will be accomplished
This Llab covers the following steps:

1. Create a Service Bus namespace, using the Azure portal.
2. Create a Service Bus queue, using the Azure portal.
3. Write a console application to send a message.
4. Write a console application to receive the messages sent in the previous step.

## Prerequisites
1. [Visual Studio 2015 or higher](http://www.visualstudio.com). The examples in this tutorial use Visual Studio 2017.
2. An Azure subscription.

> [!NOTE]
> To complete this tutorial, you need an Azure account. You can [activate your MSDN subscriber benefits](https://azure.microsoft.com/pricing/member-offers/msdn-benefits-details/?WT.mc_id=A85619ABF) or [sign up for a free account](https://azure.microsoft.com/pricing/free-trial/?WT.mc_id=A85619ABF).
> 
> 

## 1. Create a namespace using the Azure portal
If you have already created a Service Bus Messaging namespace, jump to the [Create a queue using the Azure portal](#2-create-a-queue-using-the-azure-portal) section.

To begin using Service Bus queues in Azure, you must first create a namespace. A namespace provides a scoping container for addressing Service Bus resources within your application. 

To create a namespace:

1. Log on to the [Azure portal][Azure portal].
2. In the left navigation pane of the portal, click **New**, then click **Enterprise Integration**, and then click **Service Bus**.
3. In the **Create namespace** dialog, enter a namespace name. The system immediately checks to see if the name is available.
4. After making sure the namespace name is available, choose the pricing tier (Basic, Standard, or Premium).
5. In the **Subscription** field, choose an Azure subscription in which to create the namespace.
6. In the **Resource group** field, choose an existing resource group in which the namespace will live, or create a new one.      
7. In **Location**, choose the country or region in which your namespace should be hosted.
   
    ![Create namespace][create-namespace]
8. Click **Create**. The system now creates your namespace and enables it. You might have to wait several minutes as the system provisions resources for your account.

### Obtain the management credentials

1. In the list of namespaces, click the newly created namespace name.
2. In the namespace blade, click **Shared access policies**.
3. In the **Shared access policies** blade, click **RootManageSharedAccessKey**.
   
    ![connection-info][connection-info]
4. In the **Policy: RootManageSharedAccessKey** blade, click the copy button next to **Connection string–primary key**, to copy the connection string to your clipboard for later use. Paste this value into Notepad or some other temporary location.
   
    ![connection-string][connection-string] 

5. Repeat the previous step, copying and pasting the value of **Primary key** to a temporary location for later use.

## 2. Create a queue using the Azure portal
If you have already created a Service Bus queue, jump to the [Send messages to the queue](#3-send-messages-to-the-queue) section.

Please ensure that you have already created a Service Bus namespace, as shown [here][namespace-how-to].

1. Log on to the [Azure portal][azure-portal].
2. In the left navigation pane of the portal, click **Service Bus** (if you don't see **Service Bus**, click **More services**).
3. Click the namespace in which you would like to create the queue. In this case, it is **nstest1**.
   
    ![Create a queue][createqueue1]
4. In the **Service Bus namespace** blade, select **Queues**, then click **Add queue**.
   
    ![Select Queues][createqueue2]
5. Enter the **Queue Name** and leave the other values with their defaults.
   
    ![Select New][createqueue3]
6. At the bottom of the blade, click **Create**.


## 3. Send messages to the queue
To send messages to the queue, we write a C# console application using Visual Studio.

### Create a console application

Launch Visual Studio and create a new **Console app (.NET Framework)** project.

### Add the Service Bus NuGet package
1. Right-click the newly created project and select **Manage NuGet Packages**.
2. Click the **Browse** tab, search for **Microsoft Azure Service Bus**, and then select the **WindowsAzure.ServiceBus** item. Click **Install** to complete the installation, then close this dialog box.
   
![Select a NuGet package][nuget-pkg]

### Write some code to send a message to the queue
1. Add the following `using` statement to the top of the Program.cs file.
   
    ```csharp
    using Microsoft.ServiceBus.Messaging;
    ```
2. Add the following code to the `Main` method. Set the `connectionString` variable to the connection string that you obtained when creating the namespace, and set `queueName` to the queue name that you used when creating the queue.
   
    ```csharp
    var connectionString = "<your connection string>";
    var queueName = "<your queue name>";
   
    var client = QueueClient.CreateFromConnectionString(connectionString, queueName);
    var message = new BrokeredMessage("This is a test message!");

    Console.WriteLine(String.Format("Message body: {0}", message.GetBody<String>()));
    Console.WriteLine(String.Format("Message id: {0}", message.MessageId));

    client.Send(message);

    Console.WriteLine("Message successfully sent! Press ENTER to exit program");
    Console.ReadLine();
    ```
   
    Here is what your Program.cs file should look like.
   
    ```csharp
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.ServiceBus.Messaging;

    namespace qsend
    {
        class Program
        {
            static void Main(string[] args)
            {
                var connectionString = "Endpoint=sb://<your namespace>.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=<your key>";
                var queueName = "<your queue name>";

                var client = QueueClient.CreateFromConnectionString(connectionString, queueName);
                var message = new BrokeredMessage("This is a test message!");

                Console.WriteLine(String.Format("Message body: {0}", message.GetBody<String>()));
                Console.WriteLine(String.Format("Message id: {0}", message.MessageId));

                client.Send(message);

                Console.WriteLine("Message successfully sent! Press ENTER to exit program");
                Console.ReadLine();
            }
        }
    }
    ```
3. Run the program, and check the Azure portal: click the name of your queue in the namespace **Overview** blade. The queue **Essentials** blade is displayed. Notice that the **Active Message Count** value should now be 1. Each time you run the sender application without retrieving the messages, this value increases by 1. Also note that the current size of the queue increments each time the app adds a message to the queue.
   
      ![Message size][queue-message]

## 4. Receive messages from the queue

1. To receive the messages you just sent, create a new console application and add a reference to the Service Bus NuGet package, similar to the previous sender application.
2. Add the following `using` statement to the top of the Program.cs file.
   
    ```csharp
    using Microsoft.ServiceBus.Messaging;
    ```
3. Add the following code to the `Main` method. Set the `connectionString` variable to the connection string that was obtained when creating the namespace, and set `queueName` to the queue name that you used when creating the queue.
   
    ```csharp
    var connectionString = "<your connection string>";
    var queueName = "<your queue name>";
   
    var client = QueueClient.CreateFromConnectionString(connectionString, queueName);
   
    client.OnMessage(message =>
    {
      Console.WriteLine(String.Format("Message body: {0}", message.GetBody<String>()));
      Console.WriteLine(String.Format("Message id: {0}", message.MessageId));
    });
   
    Console.WriteLine("Press ENTER to exit program");
    Console.ReadLine();
    ```
   
    Here is what your Program.cs file should look like:
   
    ```csharp
    using System;
    using Microsoft.ServiceBus.Messaging;
   
    namespace GettingStartedWithQueues
    {
      class Program
      {
        static void Main(string[] args)
        {
          var connectionString = "Endpoint=sb://<your namespace>.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=<your key>";;
          var queueName = "<your queue name>";
   
          var client = QueueClient.CreateFromConnectionString(connectionString, queueName);
   
          client.OnMessage(message =>
          {
            Console.WriteLine(String.Format("Message body: {0}", message.GetBody<String>()));
            Console.WriteLine(String.Format("Message id: {0}", message.MessageId));
          });

          Console.WriteLine("Press ENTER to exit program");   
          Console.ReadLine();
        }
      }
    }
    ```
4. Run the program, and check the portal again. Notice that the **Active Message Count** and **Current** values are now 0.
   
    ![Queue length][queue-message-receive]

Congratulations! You have now created a queue, sent a message, and received a message.


<!--Image references-->

[nuget-pkg]: ./media/service-bus-dotnet-get-started-with-queues/nuget-package.png
[queue-message]: ./media/service-bus-dotnet-get-started-with-queues/queue-message.png
[queue-message-receive]: ./media/service-bus-dotnet-get-started-with-queues/queue-message-receive.png
[createqueue1]: ./media/service-bus-create-queue-portal/create-queue1.png
[createqueue2]: ./media/service-bus-create-queue-portal/create-queue2.png
[createqueue3]: ./media/service-bus-create-queue-portal/create-queue3.png
[namespace-how-to]: ../articles/service-bus-messaging/service-bus-create-namespace-portal.md
[azure-portal]: https://portal.azure.com
[create-namespace]: ./media/service-bus-create-namespace-portal/create-namespace.png
[connection-info]: ./media/service-bus-create-namespace-portal/connection-info.png
[connection-string]: ./media/service-bus-create-namespace-portal/connection-string.png
[Azure portal]: https://portal.azure.com