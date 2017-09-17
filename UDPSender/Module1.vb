Imports System.Net.Sockets
Imports System.Text
Module Module1

    Sub Main()
        Dim UDPClient As New UdpClient()
        UDPClient.Client.SetSocketOption(SocketOptionLevel.Socket,
           SocketOptionName.ReuseAddress, True)
        UDPClient.Connect("localhost", 14321)
        Dim strMessage = "419008|10/05/2017 06:30:57|33.6967766284943|120.805366516113|0.929268360137939|-61.625|13.75|11.25"
        Dim bytSent As Byte() =
               Encoding.ASCII.GetBytes(strMessage)

        UDPClient.Send(bytSent, bytSent.Length)
    End Sub

End Module
