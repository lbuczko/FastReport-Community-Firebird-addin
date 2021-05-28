Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Common
Imports FastReport.Data.ConnectionEditors
Imports FirebirdSql.Data.FirebirdClient

Namespace FastReport.Data
	
	Public Class FirebirdDataConnection
		Inherits DataConnectionBase
		
		Private Sub GetDBObjectNames(list As List(Of String))
			Dim dataTable As DataTable = Nothing
			Dim dataTableViews As DataTable = Nothing
			Dim text As String = ""
			Dim connection As DbConnection = MyBase.GetConnection()
			Try
				MyBase.OpenConnection(connection)
				
				Dim cs As New FbConnectionStringBuilder(MyBase.ConnectionString)
				dataTableViews = connection.GetSchema("Views", New String() {Nothing, Nothing, Nothing}) '(name)
				dataTable = connection.GetSchema("Tables", New String() {Nothing, Nothing, Nothing, "TABLE"}) '(name)
				text = cs.Database
			Finally
				MyBase.DisposeConnection(connection)
			End Try
						
			For Each obj As Object In dataTable.Rows
				Dim dataRow As DataRow = CType(obj, DataRow)
				list.Add(dataRow("TABLE_NAME").ToString())
			Next
			
			For Each obj As Object In dataTableViews.Rows
				Dim dataRow As DataRow = CType(obj, DataRow)	
				list.Add(dataRow("VIEW_NAME").ToString())
			Next
			
			
		End Sub

		' OK
		Public Overrides Function GetTableNames() As String()
			Dim list As List(Of String) = New List(Of String)()
			Me.GetDBObjectNames(list)
			Return list.ToArray()
		End Function

		'OK
		Public Overrides Function QuoteIdentifier(value As String, connection As DbConnection) As String
			Return "" + value + ""
		End Function

		'OK
		Protected Overrides Function GetConnectionStringWithLoginInfo(userName As String, password As String) As String
			Return New FbConnectionStringBuilder(MyBase.ConnectionString) With { .UserID = userName, .Password = password }.ToString()
		End Function

		'OK
		Public Overrides Function GetConnectionType() As Type
			Return GetType(FbConnection)
		End Function

		'OK
		Public Overrides Function GetAdapter(selectCommand As String, connection As DbConnection, parameters As CommandParameterCollection) As DbDataAdapter
			Dim fbDataAdapter As FbDataAdapter = New FbDataAdapter(selectCommand, TryCast(connection, FbConnection))
			For Each obj As Object In parameters
				Dim commandParameter As CommandParameter = CType(obj, CommandParameter)
				fbDataAdapter.SelectCommand.Parameters.Add(commandParameter.Name, CType(commandParameter.DataType, FbDbType), commandParameter.Size).Value = commandParameter.Value
			Next
			Return fbDataAdapter
		End Function
		
		'OK
		Public Overrides Function GetParameterType() As Type
			Return GetType(FbDbType)
		End Function

		'OK
		Public Overrides Function GetConnectionId() As String	
			Dim cs As New FbConnectionStringBuilder(MyBase.ConnectionString)
			Dim str As String = ""
			Try
				str = (If(cs.DataSource, String.Empty))
			Catch
			End Try
			Return "Firebird: " + str
		End Function

		' OK
		Public Overrides Function GetEditor() As ConnectionEditorBase
			Return New FirebirdConnectionEditor()
		End Function
	End Class
End Namespace
