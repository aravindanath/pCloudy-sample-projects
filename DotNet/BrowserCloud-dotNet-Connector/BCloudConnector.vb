﻿

Public Class BCloudConnector

    Private _browserCloudUrl As String = Nothing

    Public Sub New(Optional browserCloudUrl As String = "https://prod-browsercloud-in.pcloudy.com")
        _browserCloudUrl = browserCloudUrl
    End Sub

    Public Function authenticateUser(pCloudyEndpoint As String, userEmail As String, pCloudyAccessKey As String) As String
        Dim url = String.Format("{0}/api/authenticateWithBrowserCloud.php", _browserCloudUrl)
        Dim jsonData = <json>
                               {"pCloudyEndpoint": "@pCloudyEndpoint",
                                "userEmail": "@userEmail",
                                "pCloudyAccessKey": "@pCloudyAccessKey"}
                           </json>.Value.Trim

        jsonData = jsonData.Replace("@pCloudyEndpoint", pCloudyEndpoint)
        jsonData = jsonData.Replace("@userEmail", userEmail)
        jsonData = jsonData.Replace("@pCloudyAccessKey", pCloudyAccessKey)

        Dim p = callService(Of AuthenticateResponseDTO)(url, jsonData)
        If p.result.error IsNot Nothing Then Throw New UnauthorizedAccessException(p.result.error)

        Return p.result.browserCloudAuthToken
    End Function


    Public Function authenticateUser(userEmail As String, browserCloudAccessKey As String) As String
        Dim url = String.Format("{0}/api/authenticateClient.php", _browserCloudUrl)
        Dim jsonData = <json>
                               {"clientName": "@clientName",
                                "apiKey": "@apiKey"}
                           </json>.Value.Trim

        jsonData = jsonData.Replace("@clientName", userEmail)
        jsonData = jsonData.Replace("@apiKey", browserCloudAccessKey)

        Dim p = callService(Of AuthenticateResponseDTO)(url, jsonData)
        If p.result.error IsNot Nothing Then Throw New UnauthorizedAccessException(p.result.error)

        Return p.result.browserCloudAuthToken
    End Function


    Public Function getAvailableBrowsers(browserCloudAuthToken As String) As AvailableBrowsersResponse.AvailableBrowsersResponseResult
        Dim url = String.Format("{0}/api/getAvailableBrowsers.php", _browserCloudUrl)
        Dim jsonData = <json>
                               {"browserCloudAuthToken": "@browserCloudAuthToken"}
                           </json>.Value.Trim


        jsonData = jsonData.Replace("@browserCloudAuthToken", browserCloudAuthToken)

        Dim p = callService(Of AvailableBrowsersResponse)(url, jsonData)
        If p.result.error IsNot Nothing Then Throw New BrowserCloudError(p.result.error)

        Return p.result
    End Function

    Public Enum SessionType_ENUM
        manual
        automation
        opkey
    End Enum

    Public Function bookBrowser(browserCloudAuthToken As String, instance_id As String, systemOS As String, browserDetails As String, userEmail As String, session_type As SessionType_ENUM, session_name As String) As BookBrowserResponse.BookBrowserResponseResult
        'isset($POST_JSON['browserCloudAuthToken']) && isset($POST_JSON['instance_id']) && isset($POST_JSON['systemOS']) && isset($POST_JSON['browserDetails']) && isset($POST_JSON['userEmail']) && isset($POST_JSON['session_type']) && isset($POST_JSON['session_name']))
        Dim url = String.Format("{0}/api/getAvailableBrowsers.php", _browserCloudUrl)
        Dim jsonData = <json>
                               {"browserCloudAuthToken": "@browserCloudAuthToken",
                                "instance_id": "@instance_id",
                                "systemOS": "@systemOS",
                                "browserDetails": "@browserDetails",
                                "userEmail": "@userEmail",
                                "session_type": "@session_type",
                                "session_name": "@session_name"}
                           </json>.Value.Trim


        jsonData = jsonData.Replace("@browserCloudAuthToken", browserCloudAuthToken)
        jsonData = jsonData.Replace("@instance_id", instance_id)
        jsonData = jsonData.Replace("@systemOS", systemOS)
        jsonData = jsonData.Replace("@browserDetails", browserDetails)
        jsonData = jsonData.Replace("@userEmail", userEmail)
        jsonData = jsonData.Replace("@session_type", session_type.ToString)
        jsonData = jsonData.Replace("@session_name", session_name)


        Dim p = callService(Of BookBrowserResponse)(url, jsonData)
        If p.result.error IsNot Nothing Then Throw New BrowserCloudError(p.result.error)

        Return p.result
    End Function

End Class