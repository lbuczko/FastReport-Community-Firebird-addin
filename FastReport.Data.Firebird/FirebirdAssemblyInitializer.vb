Imports System
Imports FastReport.Utils

Namespace FastReport.Data
	Public Class FirebirdAssemblyInitializer
		Inherits AssemblyInitializerBase
		Public Sub New()
			RegisteredObjects.AddConnection(GetType(FirebirdDataConnection))
		End Sub
	End Class
End Namespace
