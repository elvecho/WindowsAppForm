Imports System
Imports System.IO
Imports System.Collections.Generic
Imports Microsoft.VisualBasic
Imports System.Text.RegularExpressions
Imports System.Xml.Linq
Imports System.Data
Imports System.Collections.Specialized
Imports System.Xml.XPath
Imports WindowsAppForm.VLLRulesInterface
Imports Microsoft.Win32
Imports System.ServiceModel.Security
Imports System.Security.Cryptography.X509Certificates



Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles INVIO.Click
        ConnectToWSDL()



    End Sub

    Protected Sub ConnectToWSDL()
        Try
            Dim szFuncName As String = "ConnectToWSDL"
            Dim vllRequestWithCredential As String = GetVLLCredentials()

            Dim szCustomerID As String = TesseraCliente.Text.Trim()

            Dim binding As New System.ServiceModel.BasicHttpBinding
            binding.Name = My.Settings.BindingName.ToString()

            Dim endPointAddress As New System.ServiceModel.EndpointAddress(My.Settings.EndPointAddress)

            Try

                If NuovoIndirizzoIP.Text <> "" Then

                    Dim newEndPointAddress As New System.ServiceModel.EndpointAddress(NuovoIndirizzoIP.Text)
                    endPointAddress = newEndPointAddress
                Else
                    endPointAddress = endPointAddress
                End If
            Catch ex As Exception
                Log.AppendText(vbCrLf + ex.ToString + "  formato indirizzo ip non valido" + vbCrLf)
                Exit Sub
            End Try
            System.Net.ServicePointManager.ServerCertificateValidationCallback =
Function(se As Object,
cert As System.Security.Cryptography.X509Certificates.X509Certificate,
chain As System.Security.Cryptography.X509Certificates.X509Chain,
sslerror As System.Net.Security.SslPolicyErrors) True

            If endPointAddress.Uri.Scheme.ToLower().Equals("https") Then
                binding.Security.Mode = ServiceModel.BasicHttpSecurityMode.Transport
            Else
                binding.Security.Mode = ServiceModel.BasicHttpSecurityMode.None
            End If


            Dim wsdl As New VLLRulesInterface.VLLRulesInterfaceSoapClient(binding, endPointAddress)


            Dim response As String = String.Empty

            If (My.Settings.CallWSDLAccountBalance) Then

                '[VA-20220705 Start]
                Dim szAccountBalanceRequest As String = String.Empty
                If (Not String.IsNullOrEmpty(vllRequestWithCredential)) Then
                    szAccountBalanceRequest = My.Settings.Request
                    Dim endofReq As Integer = szAccountBalanceRequest.IndexOf(">")
                    szAccountBalanceRequest = szAccountBalanceRequest.Replace(szAccountBalanceRequest.Substring(0, endofReq + 1), "")
                    szAccountBalanceRequest = $"{vllRequestWithCredential}{szAccountBalanceRequest}"
                Else
                    szAccountBalanceRequest = My.Settings.Request
                End If

                szAccountBalanceRequest = szAccountBalanceRequest.Replace("%VERSION%", "2")
                szAccountBalanceRequest = szAccountBalanceRequest.Replace("{0}", szCustomerID)

                '[VA-20220705 End]

                response = StartThread(wsdl, szAccountBalanceRequest, "GetAccountBalance", My.Settings.WSDLTimeout)

                If String.IsNullOrEmpty(response) AndAlso Not String.IsNullOrEmpty(My.Settings.EndPointAddressBackup.Trim) Then
                    response = String.Empty
                    Dim endPointAddressBackup As New System.ServiceModel.EndpointAddress(My.Settings.EndPointAddressBackup)
                    If endPointAddressBackup.Uri.Scheme.ToLower().Equals("https") Then
                        binding.Security.Mode = ServiceModel.BasicHttpSecurityMode.Transport
                    Else
                        binding.Security.Mode = ServiceModel.BasicHttpSecurityMode.None
                    End If
                    Dim wsdlBackup As New VLLRulesInterface.VLLRulesInterfaceSoapClient(binding, endPointAddressBackup)

                    response = StartThread(wsdlBackup, szAccountBalanceRequest, "GetAccountBalance", My.Settings.WSDLTimeout)
                End If
            End If


        Catch ez As Exception
            Log.AppendText(vbCrLf + ez.ToString + vbCrLf)
        End Try
    End Sub

    Dim szThreadRequest As String
    Dim szThreadResponse As String
    Dim szThreadType As String
    Dim ThreadWsdl As VLLRulesInterfaceSoapClient

    Private Function StartThread(wsdl As VLLRulesInterfaceSoapClient, szRequest As String, szType As String, lTimeout As Integer) As String

        'LOG_Error(getLocationString("StartThread"), $"Start thread of type {szType} request:{szRequest}, timeout: {lTimeout.ToString()}")
        Log.AppendText(vbCrLf + "====================================================== Log directory =======================================================" + vbCrLf)

        Log.AppendText(vbCrLf + $"Start thread of type {szType}" + vbCrLf + $"request:{szRequest}," + vbCrLf + $"timeout: {lTimeout.ToString()}" + vbCrLf)
        Dim timeStart As TimeSpan = DateTime.Now.TimeOfDay


        Try

            Dim thread As System.Threading.Thread
            szThreadResponse = String.Empty
            szThreadRequest = szRequest
            szThreadType = szType
            ThreadWsdl = wsdl
            thread = New System.Threading.Thread(AddressOf CallWsdl)
            thread.Start()
            thread.Join(lTimeout)
            thread.Abort()
        Catch ex As Exception
            'LOG_Error(getLocationString("StartThread"), ex)
            Log.AppendText(vbCrLf + ex.ToString + vbCrLf)
        Finally
            StartThread = szThreadResponse
        End Try

        Dim TimeDiff As TimeSpan = DateTime.Now.TimeOfDay.Subtract(timeStart)
        'LOG_Error(getLocationString("StartThread"), $"End thread of type {szType}, response: {szThreadResponse}, time: {TimeDiff}")
        Log.AppendText(vbCrLf + $"End thread of type {szType}," + vbCrLf + $"response: {szThreadResponse}," + vbCrLf + $"time: {TimeDiff}" + vbCrLf + "========================================================================================================================")
    End Function


    Sub CallWsdl()
        Try
            Select Case szThreadType.ToUpper
                Case "GETACCOUNTBALANCE"
                    szThreadResponse = ThreadWsdl.GetAccountBalance(szThreadRequest)
                    Exit Select
                Case "GETTRXHISTO"
                    szThreadResponse = ThreadWsdl.GetTrxHisto(szThreadRequest)
                    Exit Select
                Case "GETINFOSACCOUNT"
                    szThreadResponse = ThreadWsdl.GetInfosAccount(szThreadRequest)
                    Exit Select
                Case "GETCAMPAIGNEXT"
                    szThreadResponse = ThreadWsdl.GetCampaignExt(szThreadRequest)
                    Exit Select
            End Select
        Catch ex As Exception
            'LOG_Error(getLocationString("CallWsdl"), ex)
            Log.AppendText(vbCrLf + ex.ToString + vbCrLf)
        End Try
    End Sub

    Public Function GetAccountBalance(ByVal XmlIn As String) As String
        Dim inValue As VLLRulesInterface.GetAccountBalanceRequest = New VLLRulesInterface.GetAccountBalanceRequest()
        inValue.Body = New VLLRulesInterface.GetAccountBalanceRequestBody()
        inValue.Body.XmlIn = XmlIn
        Dim retVal As VLLRulesInterface.GetAccountBalanceResponse = CType(Me, VLLRulesInterface.VLLRulesInterfaceSoap).GetAccountBalance(inValue)
        Return retVal.Body.GetAccountBalanceResult
    End Function

    Public Shared Function GetVLLCredentials() As String
        Dim vllRequestWithCredential As String = ""
        Dim IsVllCredentialsExist As Boolean

        Try
            Dim vllRequestLogin = My.Settings.vllRequestLogin.Trim()

            Dim vllRequestPassword = My.Settings.vllRequestPassword.Trim()
            IsVllCredentialsExist = IIf(vllRequestLogin <> "" AndAlso vllRequestPassword <> "", True, False)
            If (IsVllCredentialsExist) Then
                vllRequestWithCredential = $"<REQUEST externaldatabase="""" org=""CLL"" password=""{(vllRequestPassword)}"" user=""{vllRequestLogin}"" version=""%VERSION%"">"
            End If
        Catch ex As Exception
            GetVLLCredentials = ""
        End Try

        GetVLLCredentials = IIf(IsVllCredentialsExist, vllRequestWithCredential, "")

    End Function
    Public Function getParam(ByRef who_what As String) As String


        getParam = " "

        Try
            'LOG_FuncStart(getLocationString("getParam"), "ParameterName: " & who_what)
            Log.AppendText(vbCrLf + "ParameterName: " & who_what + vbCrLf)
            who_what = Trim(who_what)

            Dim myResult As ParamType = Params(who_what)

            If myResult.szContents IsNot Nothing Then
                getParam = myResult.szContents
            Else
                getParam = " "
            End If

        Catch ex As Exception
            Try
                'LOG_Error(getLocationString("getParam"), ex)
                Log.AppendText(vbCrLf + ex.ToString + vbCrLf)
            Catch InnerEx As Exception
                'LOG_ErrorInTry(getLocationString("getParam"), InnerEx)
                Log.AppendText(vbCrLf + InnerEx.ToString + vbCrLf)
            End Try
        Finally
            'LOG_FuncExit(getLocationString("getParam"), "Function getParam returns " & getParam.ToString)
            Log.AppendText(vbCrLf + "Function getParam returns " & getParam.ToString + vbCrLf)
        End Try
    End Function


    Public Structure ParamType
        Public szObject As String
        Public szDllName As String
        Public lRetailStoreID As Integer
        Public szWorkstationGroupID As String
        Public szKey As String
        Public szContents As String
    End Structure

    Private m_Params As New Collections.Generic.Dictionary(Of String, ParamType)(System.StringComparer.InvariantCultureIgnoreCase)
    Public ReadOnly Property Params(szKey As String) As ParamType
        Get
            If m_Params.ContainsKey(szKey) = False Then
                Return Nothing
            Else
                Return m_Params(szKey)
            End If
        End Get
    End Property

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles CANCELLA.Click
        TesseraCliente.Clear()
        Log.Clear()
        NuovoIndirizzoIP.Clear()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)
        Dim tessere As New List(Of String)
        tessere.Add(TesseraCliente.Text)





    End Sub
End Class




