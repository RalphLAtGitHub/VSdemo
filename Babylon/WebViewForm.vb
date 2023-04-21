Imports Microsoft.Web.WebView2
Imports Microsoft.Web.WebView2.Core

Public Class WebViewForm

    Private ReadOnly m_WebView As WinForms.WebView2

    Public Sub New()

        InitializeComponent()

        m_WebView = New WinForms.WebView2 With {.Dock = DockStyle.Fill}
        Controls.Add(m_WebView)
        InitializeWebView()

    End Sub

    Private Async Sub InitializeWebView()

        AddHandler m_WebView.CoreWebView2InitializationCompleted, AddressOf InitializationCompletedHandler
        AddHandler m_WebView.NavigationCompleted, AddressOf NavigationCompletedHandler

        Dim m_Options As New CoreWebView2EnvironmentOptions With {.AdditionalBrowserArguments = "--allow-file-access-from-files"}
        Dim m_EnvironmentFolder As CoreWebView2Environment = Await CoreWebView2Environment.CreateAsync(userDataFolder:=IO.Path.Combine(IO.Path.GetTempPath, "Babylon"), options:=m_Options)

        Await m_WebView.EnsureCoreWebView2Async(m_EnvironmentFolder)

        m_WebView.Source = New Uri(IO.Path.Combine(Environment.CurrentDirectory, "WebResources\test.html"))
        m_WebView.CoreWebView2.OpenDevToolsWindow()

    End Sub

    Private Sub InitializationCompletedHandler(sender As Object, e As CoreWebView2InitializationCompletedEventArgs)

        If e.IsSuccess Then

            Debug.WriteLine("Init completed - Edge Runtime Version is " + CoreWebView2Environment.GetAvailableBrowserVersionString)

        Else

            MsgBox("WebView Initialization Error - Error Code is " + e.InitializationException.ToString, MsgBoxStyle.Exclamation, "Test")

        End If

    End Sub

    Private Sub NavigationCompletedHandler(sender As Object, e As CoreWebView2NavigationCompletedEventArgs)

        If e.IsSuccess Then

            Debug.WriteLine("Navigation completed - Navigation ID is " + e.NavigationId.ToString)

        Else

            MsgBox("WebView Navigation Error - Error Code is " + e.WebErrorStatus.ToString, MsgBoxStyle.Exclamation, "Test")

        End If

    End Sub

End Class