Imports System.Text.RegularExpressions
Imports Excel = Microsoft.Office.Interop.Excel
Imports FileSystemObject = Scripting.FileSystemObject

Public Class UnionDeArchivos
    Dim cuestionsGroupByCenter(54) As Integer
    Dim cuestionsWithSummation(59) As Integer
    Dim cuestionGroupByCenterSplitTwo(3) As Integer
    Dim excelApp As New Excel.Application
    Dim centrosPenitenciarios
    Dim ruta = "B:\Escritorio\inegi\"
    Public Sub New()
        centrosPenitenciarios = New ArrayList()
    End Sub
    Sub unionArchivos()
        cuestionsGroupByCenter = {3, 4, 5, 6, 7, 8, 9, 10, 11, 15, 22, 25, 26, 27, 34, 35, 36, 38, 39, 45, 46, 47, 48, 49, 64, 65, 66, 67, 72, 73, 74, 92, 95, 100, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 111, 119, 120, 121, 122, 123, 124, 125, 126, 128}
        cuestionsWithSummation = {12, 13, 14, 17, 19, 20, 21, 23, 24, 30, 37, 40, 41, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 68, 69, 70, 71, 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 91, 93, 94, 96, 97, 98, 99, 113, 114, 116, 118, 127}
        excelApp.ScreenUpdating = False
        Dim bookFileBase As Excel.Workbook
        Dim bookTmpCurrent As Excel.Workbook
        Dim sheetFileBase As Excel.Worksheet
        Dim sheetFileCurrent As Excel.Worksheet


        Dim centroPenitenciarioActual As CentroPenitenciario
        Dim primeraAparicion
        Dim rangeAvailableSheet As Excel.Range
        Dim rangeCurrentCuestion As Excel.Range

        Dim columnsRange As Excel.Range
        Dim row As Excel.Range
        Dim currentCell As Excel.Range
        Dim numberCuestion As Integer
        Dim startCuestion
        Dim endCuestion
        Dim typeCuestion As Integer
        Dim arrayToString() As String

        Dim regex As New Regex("^\d+$")
        Dim valueCellFileBase As Integer
        Dim valueCellFileCurrent As Integer
        Dim valueCell As Integer

        'setCentrosPenitenciarios()

        'Esta parte del codigo crea una copia del primer archivo para tener un unico archivo
        'que concentre la información de todos los centros penitenciarios
        centroPenitenciarioActual = CType(centrosPenitenciarios(0), CentroPenitenciario)
        bookFileBase = excelApp.Workbooks.Open(centroPenitenciarioActual.NombreArchivoP)
        bookFileBase.SaveCopyAs(Filename:=ruta & "New1.xlsx")
        bookFileBase.Close()

        bookFileBase = excelApp.Workbooks.Open(ruta & "New1.xlsx")
        sheetFileBase = bookFileBase.Worksheets(4)

        startCuestion = 0
        endCuestion = 0
        typeCuestion = 0

        For i = 1 To (centrosPenitenciarios.Count - 1)

            centroPenitenciarioActual = CType(centrosPenitenciarios(i), CentroPenitenciario)
            bookTmpCurrent = excelApp.Workbooks.Open(centroPenitenciarioActual.NombreArchivoP)
            sheetFileCurrent = bookTmpCurrent.Worksheets(4)
            rangeAvailableSheet = sheetFileCurrent.Range("A1:AD4975")

            'Codigo para copiar la respuesta de la pregunta no. 2
            arrayToString = centroPenitenciarioActual.DireccionCeldaP.Split("$")
            sheetFileCurrent.Range(centroPenitenciarioActual.DireccionCeldaP & ":AD" & arrayToString(2)).Copy(Destination:=sheetFileBase.Range(centroPenitenciarioActual.DireccionCeldaP & ":AD" & arrayToString(2)))

            For numberCuestion = 3 To 128
                Console.WriteLine("Numero de pregunta " & numberCuestion)
                'Busca la direccion inicio y la direccion final de la pregunta actual
                startCuestion = rangeAvailableSheet.Find(What:=numberCuestion & ".-", LookIn:=Excel.XlFindLookIn.xlFormulas, LookAt:=Excel.XlLookAt.xlPart, SearchOrder:=Excel.XlSearchOrder.xlByColumns).Address
                If numberCuestion = 128 Then
                    endCuestion = "$AD$4975"
                Else
                    endCuestion = rangeAvailableSheet.Find(What:=(numberCuestion + 1) & ".-", LookIn:=Excel.XlFindLookIn.xlFormulas, LookAt:=Excel.XlLookAt.xlPart, SearchOrder:=Excel.XlSearchOrder.xlByColumns).Address
                    arrayToString = endCuestion.Split("$")
                    endCuestion = "AD" & (arrayToString(2) - 1)
                End If

                rangeCurrentCuestion = rangeAvailableSheet.Range(startCuestion & ":" & endCuestion)
                typeCuestion = typeOfCuestion(numberCuestion)
                If typeCuestion = 1 Then
                    'Busca la celda donde se encuentre la direccion del centro penitenciarioo dentro de alguna formula dentro del Range de la pregunta actual
                    primeraAparicion = rangeCurrentCuestion.Find(What:=centroPenitenciarioActual.DireccionCeldaP.Replace("$", ""), LookIn:=Excel.XlFindLookIn.xlFormulas, LookAt:=Excel.XlLookAt.xlPart, SearchOrder:=Excel.XlSearchOrder.xlByRows)

                    If Not primeraAparicion Is Nothing And (primeraAparicion.Offset(, -1).Value Like (centroPenitenciarioActual.NoCentroP & ".")) Then
                        arrayToString = primeraAparicion.Address.Split("$")
                        sheetFileCurrent.Range(primeraAparicion.Address & ":AD" & arrayToString(2)).Copy(Destination:=sheetFileBase.Range(primeraAparicion.Address & ":AD" & arrayToString(2)))
                    End If

                    'Busca dentro del Range de la pregunta actual si existe una seguna parte de la tabla, en dado caso que la tabla este divida en dos partes
                    'El criterio a buscar sera la cadena "(2/2)"
                    primeraAparicion = rangeCurrentCuestion.Find(What:="(2/2)", LookIn:=Excel.XlFindLookIn.xlValues, LookAt:=Excel.XlLookAt.xlWhole, SearchOrder:=Excel.XlSearchOrder.xlByColumns)
                    If Not primeraAparicion Is Nothing Then
                        arrayToString = primeraAparicion.Address.Split("$")
                        rangeCurrentCuestion = sheetFileCurrent.Range("A" & arrayToString(2) & ":" & endCuestion)

                        primeraAparicion = rangeCurrentCuestion.Find(What:=centroPenitenciarioActual.NoCentroP & ".", LookIn:=Excel.XlFindLookIn.xlValues, LookAt:=Excel.XlLookAt.xlWhole, SearchOrder:=Excel.XlSearchOrder.xlByRows)
                        If Not primeraAparicion Is Nothing Then
                            arrayToString = primeraAparicion.Address.Split("$")
                            sheetFileCurrent.Range(primeraAparicion.Address & ":AD" & arrayToString(2)).Copy(Destination:=sheetFileBase.Range(primeraAparicion.Address & ":AD" & arrayToString(2)))
                        End If
                    End If
                ElseIf typeCuestion = 2 Then
                    For Each row In rangeCurrentCuestion.Rows
                        columnsRange = row.Columns
                        For Each currentCell In columnsRange.Cells
                            If String.IsNullOrEmpty(currentCell.Value) = False Then
                                If regex.IsMatch(currentCell.Value) Or currentCell.Value Like "NA" Then
                                    valueCellFileBase = IIf(sheetFileBase.Range(currentCell.Address).Value Like "NA", 0, sheetFileBase.Range(currentCell.Address).Value)
                                    valueCellFileCurrent = IIf(sheetFileCurrent.Range(currentCell.Address).Value Like "NA", 0, sheetFileCurrent.Range(currentCell.Address).Value)
                                    valueCell = valueCellFileBase + valueCellFileCurrent
                                    sheetFileBase.Range(currentCell.Address).Value = valueCell
                                End If
                            End If
                        Next
                    Next
                End If
            Next

            'Esto es para copiar y pegar las respuestas de las preguntas que estan agrupadas por centro penitenciario
            'primeraAparicion = rangeAvailableSheet.Find(What:=centroPenitenciarioActual.DireccionCeldaP.Replace("$", ""), LookIn:=Excel.XlFindLookIn.xlFormulas, LookAt:=Excel.XlLookAt.xlPart, SearchOrder:=Excel.XlSearchOrder.xlByRows)
            'siguienteAparicion = primeraAparicion
            'Console.WriteLine("Primera aparicion " & primeraAparicion.Address)
            'If Not primeraAparicion Is Nothing Then
            '    Do
            '        If siguienteAparicion.Offset(, -1).Value Like (centroPenitenciarioActual.NoCentroP & ".") Then
            '            arrayToString = siguienteAparicion.Address.Split("$")
            '            rangeAvailableSheet.Range(siguienteAparicion.Address & ":AD" & arrayToString(2)).Copy(Destination:=sheetFileBase.Range(siguienteAparicion.Address & ":AD" & arrayToString(2)))
            '        End If
            '        siguienteAparicion = rangeAvailableSheet.FindNext(siguienteAparicion)
            '        'Console.WriteLine("Siguiente aparicion " & siguienteAparicion.Address)
            '    Loop While Not siguienteAparicion Is Nothing And primeraAparicion.Address <> siguienteAparicion.Address
            'End If

            bookTmpCurrent.Close()
        Next
        bookFileBase.Save()
        bookFileBase.Close()
        excelApp.Quit()

    End Sub
    Function typeOfCuestion(numberCuestion As Integer) As Integer
        Dim item As Integer
        Dim typeCuestion As Integer
        typeCuestion = 0
        For Each item In cuestionsGroupByCenter
            If item = numberCuestion Then
                typeOfCuestion = 1
                Exit Function
            End If
        Next
        For Each item In cuestionsWithSummation
            If item = numberCuestion Then
                typeOfCuestion = 2
                Exit Function
            End If
        Next
        Return typeCuestion
    End Function
    'Esta función es para copiar las respuestas de cada pregunta de un archivo al archivo concentrador
    Function copyRowsToNewWorkBook(workBookBase As Excel.Workbook, newWorkBook As Excel.Workbook)
        Dim workSheetBase As Excel.Worksheet
        Dim rowsFile As Excel.Range
        Dim valueCell
        Dim numCuestion As Integer
        Dim startCuestion As Integer
        Dim endCuestion As Integer
        Dim typeCuestion As Integer
        workSheetBase = workBookBase.Worksheets(4)
        rowsFile = workSheetBase.Rows
        startCuestion = 0
        endCuestion = 0
        typeCuestion = 0
        For i = 0 To rowsFile.Count
            If Len(rowsFile(i).Cells(1, 1).Value) > 0 Then
                valueCell = rowsFile(i).Cells(1, 1).Value
                numCuestion = Strings.Left(valueCell, (Len(valueCell) - 2))
                typeCuestion = 0 'TypeOfCuestion(numCuestion)
                If typeCuestion = 1 Then
                    valueCell = rowsFile(i).Cells(1, 3).Value
                    Do While (Left(valueCell, (Len(valueCell) - 1)))
                        i = i + 1
                        valueCell = rowsFile(i).Cells(1, 3).Value
                    Loop
                ElseIf typeCuestion = 2 Then

                End If
            End If
        Next
    End Function

    'Función para determinar que número de centro se trata en el libro que se pasa por parametro
    Function getCenterOfWorkBook(nameFile As String) As CentroPenitenciario
        Dim workbook As Excel.Workbook
        Dim areaRange As Excel.Range
        Dim currentRow As Excel.Range
        Dim currentSheet As Excel.Worksheet
        Dim centroPenitenciario As CentroPenitenciario
        Dim noCentro As Integer
        Dim nombreCentro As String
        Dim direccionCelda As String
        Dim cellCurrent As Excel.Range

        workbook = excelApp.Workbooks.Open(nameFile)
        centroPenitenciario = Nothing

        currentSheet = workbook.Worksheets(4)
        areaRange = currentSheet.Range("C40:M64")
        For Each currentRow In areaRange.Rows
            cellCurrent = currentRow.Cells(1, areaRange.Columns.Count)
            If Len(cellCurrent.Value) > 0 Then

                noCentro = Left(currentRow.Cells(1, 1).Value, (Len(currentRow.Cells(1, 1).Value) - 1))
                nombreCentro = currentRow.Cells(1, 2).Value
                direccionCelda = currentRow.Cells(1, 2).Address()
                centroPenitenciario = New CentroPenitenciario(noCentro, nombreCentro, direccionCelda)
                centroPenitenciario.NombreArchivoP = nameFile
                getCenterOfWorkBook = centroPenitenciario
                workbook.Close()
                Exit Function
            End If
        Next
        workbook.Close()
        Return centroPenitenciario
    End Function

    'Función que obtiene los datos basicos de cada centro penitenciario y los almacena
    'en un ArrayList definido de manera global
    Private Function setCentrosPenitenciarios()
        Dim bookList As Excel.Workbook
        Dim mergeObj As Object
        Dim dirObj As Scripting.Folder
        Dim filesObj As Scripting.Files
        Dim everyObj As Scripting.File
        Dim i As Integer
        Dim nameFileActual As String
        Dim centroP As CentroPenitenciario

        mergeObj = CreateObject("Scripting.FileSystemObject")
        dirObj = mergeObj.Getfolder("B:\Escritorio\inegi")
        filesObj = dirObj.Files
        i = 0
        For Each everyObj In filesObj
            'bookList = excelApp.Workbooks.Open(ruta & everyObj.Name)
            'nameFileActual = ruta & everyObj.Name 'bookList.FullName
            centroP = getCenterOfWorkBook(ruta & everyObj.Name)
            'centroP.NombreArchivoP = nameFileActual
            centrosPenitenciarios.Add(centroP)
            'bookList.Close()
        Next
    End Function
    Function addCentro(nuevoCentro As CentroPenitenciario)
        centrosPenitenciarios.Add(nuevoCentro)
    End Function

End Class
