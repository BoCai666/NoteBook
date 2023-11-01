## API
```c#
// 创建基于Udp的Socket
Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

// 创建基于Tcp的Socket
Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

// 绑定IP地址和端口号
IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
socket.Bind(new IPEndPoint(ipAddress, 12345));

// 开始监听连接，设置最大连接数为10
socket.Listen(10);

// 接受连接(阻塞)
socket.Accept();

// 连接(阻塞)
socket.Connect();

// 发送(阻塞)
socket.Send();

// 接收(阻塞)
socket.Receive();

// 接受连接（异步）
socket.BeginAccept();

// 连接（异步）
socket.BeginConnect();

// 发送（异步）
socket.BeginSend();

// 接收（异步）
socket.BeginReceive();
```
