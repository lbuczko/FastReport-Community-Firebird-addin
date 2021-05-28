Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports FastReport.Controls
Imports FastReport.Data.ConnectionEditors
Imports FastReport.Forms
Imports FastReport.Utils
Imports FirebirdSql.Data.FirebirdClient

Namespace FastReport.Data
 
	Public Class FirebirdConnectionEditor
		Inherits ConnectionEditorBase

		Private Sub btnAdvanced_Click(sender As Object, e As EventArgs)
			Using advancedConnectionPropertiesForm As AdvancedConnectionPropertiesForm = New AdvancedConnectionPropertiesForm()
				Dim advancedProperties As New FbConnectionStringBuilder(MyBase.ConnectionString)
				advancedConnectionPropertiesForm.AdvancedProperties = advancedProperties
				If advancedConnectionPropertiesForm.ShowDialog() = DialogResult.OK Then
					MyBase.ConnectionString = advancedConnectionPropertiesForm.AdvancedProperties.ToString()
				End If
			End Using
		End Sub

		' OK
		Private Sub Localize()
			Dim myRes As MyRes = New MyRes("ConnectionEditors,Odbc")
			'Me.lblDataSource.Text = myRes.[Get]("DataSource")
			myRes = New MyRes("ConnectionEditors,Common")
			Me.gbDatabase.Text = myRes.[Get]("Database")
			Me.btnAdvanced.Text = Res.[Get]("Buttons,Advanced")
			
			Me.gbServer.Text = myRes.[Get]("ServerLogon")
			Me.lblServer.Text = myRes.[Get]("Server")
			Me.lblUserName.Text = myRes.[Get]("UserName")
			Me.lblPassword.Text = myRes.[Get]("Password")
			Me.gbDatabase.Text = myRes.[Get]("Database")
			Me.lblDatabase.Text = myRes.[Get]("DatabaseName")
			
		End Sub

		'OK
		Protected Overrides Function GetConnectionString() As String
			Dim cs As New FbConnectionStringBuilder
			cs.Dialect = 3
			cs.ServerType = FbServerType.Default
			cs.Role = "PUBLIC"
			cs.Charset = "WIN1250"
			cs.Pooling = False 
			cs.DataSource = Me.tbServer.Text
			cs.UserID = Me.tbUserName.Text
			cs.Password = Me.tbPassword.Text
			cs.Database = Me.tbDatabase.Text 
			Return cs.Tostring
		End Function

		'OK
		Protected Overrides Sub SetConnectionString(value As String)
			Me.FConnectionString = value
			Dim cs As New FbConnectionStringBuilder(value)
			
			On Error Resume next
			Me.tbPassword.Text = cs.password
			Me.tbUserName.Text = cs.UserID
			Me.tbServer.Text = cs.DataSource
			Me.tbDatabase.Text = cs.Database
		End Sub

		'OK
		Public Sub New()
			Me.InitializeComponent()
			Me.Localize()
		End Sub


		' OK
		Protected Overrides Sub Dispose(disposing As Boolean)
			If disposing AndAlso Me.components IsNot Nothing Then
				Me.components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		' OK
		Private Sub InitializeComponent()
			Me.btnAdvanced = New Button()
			Me.gbDatabase = New GroupBox()
			Me.lblDatabase = New Label()
			Me.tbPassword = New TextBox()
			Me.tbUserName = New TextBox()
			Me.lblPassword = New Label()
			Me.lblUserName = New Label()
			Me.label1 = New LabelLine()
			Me.gbServer = New GroupBox()
			Me.lblServer = New Label()
			Me.tbServer = New TextBox()
			Me.tbDatabase = New TextBox()
			Me.gbDatabase.SuspendLayout()
			Me.gbServer.SuspendLayout()
			MyBase.SuspendLayout()
			Me.btnAdvanced.Anchor = (AnchorStyles.Top Or AnchorStyles.Right)
			Me.btnAdvanced.AutoSize = True
			Me.btnAdvanced.Location = New Point(252, 220)
			Me.btnAdvanced.Name = "btnAdvanced"
			Me.btnAdvanced.Size = New Size(77, 23)
			Me.btnAdvanced.TabIndex = 0
			Me.btnAdvanced.Text = "Advanced..."
			Me.btnAdvanced.UseVisualStyleBackColor = True
			AddHandler Me.btnAdvanced.Click, AddressOf Me.btnAdvanced_Click
			Me.gbDatabase.Controls.Add(Me.lblDatabase)
			Me.gbDatabase.Controls.Add(Me.tbDatabase)
			Me.gbDatabase.Location = New Point(8, 136)
			Me.gbDatabase.Name = "gbDatabase"
			Me.gbDatabase.Size = New Size(320, 76)
			Me.gbDatabase.TabIndex = 1
			Me.gbDatabase.TabStop = False
			Me.gbDatabase.Text = "Database"
			Me.lblDatabase.AutoSize = True
			Me.lblDatabase.Location = New Point(12, 20)
			Me.lblDatabase.Name = "lblDatabase"
			Me.lblDatabase.Size = New Size(57, 13)
			Me.lblDatabase.TabIndex = 3
			Me.lblDatabase.Text = "Database:"
			Me.tbPassword.Location = New Point(120, 96)
			Me.tbPassword.Name = "tbPassword"
			Me.tbPassword.Size = New Size(188, 20)
			Me.tbPassword.TabIndex = 2
			Me.tbPassword.UseSystemPasswordChar = True
			Me.tbUserName.Location = New Point(120, 72)
			Me.tbUserName.Name = "tbUserName"
			Me.tbUserName.Size = New Size(188, 20)
			Me.tbUserName.TabIndex = 1
			Me.lblPassword.AutoSize = True
			Me.lblPassword.Location = New Point(12, 100)
			Me.lblPassword.Name = "lblPassword"
			Me.lblPassword.Size = New Size(57, 13)
			Me.lblPassword.TabIndex = 1
			Me.lblPassword.Text = "Password:"
			Me.lblUserName.AutoSize = True
			Me.lblUserName.Location = New Point(12, 76)
			Me.lblUserName.Name = "lblUserName"
			Me.lblUserName.Size = New Size(62, 13)
			Me.lblUserName.TabIndex = 0
			Me.lblUserName.Text = "User name:"
			Me.label1.Location = New Point(8, 260)
			Me.label1.Name = "label1"
			Me.label1.Size = New Size(320, 17)
			Me.label1.TabIndex = 2
			Me.gbServer.Controls.Add(Me.lblServer)
			Me.gbServer.Controls.Add(Me.tbServer)
			Me.gbServer.Controls.Add(Me.tbUserName)
			Me.gbServer.Controls.Add(Me.tbPassword)
			Me.gbServer.Controls.Add(Me.lblUserName)
			Me.gbServer.Controls.Add(Me.lblPassword)
			Me.gbServer.Location = New Point(8, 4)
			Me.gbServer.Name = "gbServer"
			Me.gbServer.Size = New Size(320, 128)
			Me.gbServer.TabIndex = 3
			Me.gbServer.TabStop = False
			Me.gbServer.Text = "Server"
			Me.lblServer.AutoSize = True
			Me.lblServer.Location = New Point(12, 20)
			Me.lblServer.Name = "lblServer"
			Me.lblServer.Size = New Size(72, 13)
			Me.lblServer.TabIndex = 4
			Me.lblServer.Text = "Server name:"
			Me.tbServer.Location = New Point(12, 40)
			Me.tbServer.Name = "tbServer"
			Me.tbServer.Size = New Size(296, 20)
			Me.tbServer.TabIndex = 0
			Me.tbDatabase.Location = New Point(12, 40)
			Me.tbDatabase.Name = "tbDatabase"
			Me.tbDatabase.Size = New Size(296, 20)
			Me.tbDatabase.TabIndex = 0
			MyBase.AutoScaleDimensions = New SizeF(6F, 13F)
			MyBase.AutoScaleMode = AutoScaleMode.None
			MyBase.Controls.Add(Me.gbServer)
			MyBase.Controls.Add(Me.label1)
			MyBase.Controls.Add(Me.gbDatabase)
			MyBase.Controls.Add(Me.btnAdvanced)
			MyBase.Name = "FirebirdConnectionEditor"
			MyBase.Size = New Size(336, 270)
			Me.gbDatabase.ResumeLayout(False)
			Me.gbDatabase.PerformLayout()
			Me.gbServer.ResumeLayout(False)
			Me.gbServer.PerformLayout()
			MyBase.ResumeLayout(False)
			MyBase.PerformLayout()
			
		End Sub

		Private FConnectionString As String
		Private components As IContainer
		Private btnAdvanced As Button
		Private gbDatabase As GroupBox
		Private lblDataSource As Label
		Private label1 As LabelLine
		Private tbDataSource As TextBox
		Private btBrowse As Button
		Private lblDatabase As Label
		Private tbPassword As TextBox
		Private tbUserName As TextBox
		Private lblPassword As Label
		Private lblUserName As Label
		Private gbServer As GroupBox
		Private lblServer As Label
		Private tbServer As TextBox
		Private tbDatabase As TextBox
		
	End Class
End Namespace
