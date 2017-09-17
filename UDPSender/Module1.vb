Imports System.Net.Sockets
Imports System.Text
Module Module1

    Sub Main()
        ' string with fields from Trader_Profile_Psychophysiological_Session_Data separated with |
        ' fields could be missed, i.e. in this example fields
        '   Session_Component_ID, Sub_Component_ID, Sub_Component_Protocol_ID, ... are missed
        Dim strMessage = "419008|10/05/2017 06:30:57|33.6967766284943|120.805366516113|0.929268360137939|-61.625|13.75|11.25"
        Console.WriteLine("Example string to send:")
        Console.WriteLine(strMessage)
        Console.WriteLine()
        Console.WriteLine("Write your own string to send:")
        Console.WriteLine()

        While True
            strMessage = Console.ReadLine()

            Dim UDPClient As New UdpClient()
            UDPClient.Connect("localhost", 14321)

            Dim bytSent As Byte() = Encoding.ASCII.GetBytes(strMessage)

            UDPClient.Send(bytSent, bytSent.Length)
        End While

    End Sub

End Module
