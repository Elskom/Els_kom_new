Attribute VB_Name = "basMD5"
Option Explicit
'' Build by Mihai Nuta

Private Declare Function CryptAcquireContext Lib "AdvAPI32.dll" Alias _
    "CryptAcquireContextA" (ByRef phProv As Long, ByVal pszContainer As String, _
    ByVal pszProvider As String, ByVal dwProvType As Long, ByVal dwFlags As Long) As Long
Private Declare Function CryptReleaseContext Lib "AdvAPI32.dll" ( _
    ByVal hProv As Long, ByVal dwFlags As Long) As Long
Private Declare Function CryptCreateHash Lib "AdvAPI32.dll" ( _
    ByVal hProv As Long, ByVal Algid As Long, ByVal hKey As Long, _
    ByVal dwFlags As Long, ByRef phHash As Long) As Long
Private Declare Function CryptDestroyHash Lib "AdvAPI32.dll" (ByVal hHash As Long) As Long
Private Declare Function CryptHashData Lib "AdvAPI32.dll" ( _
    ByVal hHash As Long, ByRef pbData As Any, ByVal dwDataLen As Long, _
    ByVal dwFlags As Long) As Long
Private Declare Function CryptGetHashParam Lib "AdvAPI32.dll" ( _
    ByVal hHash As Long, ByVal dwParam As Long, ByRef pbData As Any, _
    ByRef pdwDataLen As Long, ByVal dwFlags As Long) As Long


Private Const MS_DEF_PROV As String = "Microsoft Base Cryptographic Provider v1.0"
Private Const CRYPT_NEWKEYSET As Long = &H8
Private Const PROV_RSA_FULL As Long = &H1
Private Const ALG_CLASS_HASH As Long = &H8000&
Private Const ALG_TYPE_ANY As Long = &H0
Private Const ALG_SID_MD5 As Long = &H3
Private Const CALG_MD5 As Long = (ALG_CLASS_HASH Or ALG_TYPE_ANY Or ALG_SID_MD5)
Private Const HP_HASHVAL As Long = &H2 ' Hash value

Private mlngReadChunkSize As Long

Private mhCryptProv As Long
Private mhCryptHash As Long

Private Function HashFile(ByRef inFile As String, ByRef outHash() As Byte) As Long
    On Error GoTo ErrTrap
    Const cstrErrSource = "basMD5 - HashFile"
    Dim hCryptProv As Long
    Dim hCryptHash As Long
    Dim FNum As Integer
    Dim FBuf() As Byte
    Dim BufLen As Long
    Dim BytesLeft As Long
    FNum = FreeFile() ' Get free file handle and open file
    Open inFile For Binary As #FNum
    ReDim FBuf(0 To mlngReadChunkSize - 1) As Byte
    ' Attempt to open default cryptographic context
    If (CryptAcquireContext(hCryptProv, vbNullString, _
        MS_DEF_PROV, PROV_RSA_FULL, 0&) = 0) Then
        If (CryptAcquireContext(hCryptProv, vbNullString, MS_DEF_PROV, _
            PROV_RSA_FULL, CRYPT_NEWKEYSET) = 0) Then
            '''Error
            Close #FNum
            Err.Raise 1, cstrErrSource, "Failed to acquire cryptographic context"
            GoTo Exit_Function ' Failed to acquire cryptographic context
        End If
    End If
    ' Create new MD5 hash
    If (CryptCreateHash(hCryptProv, CALG_MD5, 0&, 0&, hCryptHash)) Then
        BytesLeft = LOF(FNum)
        BufLen = mlngReadChunkSize
        Do
            If (BytesLeft < mlngReadChunkSize) Then ' Last chunk
                ReDim FBuf(0 To BytesLeft - 1) As Byte
                BufLen = BytesLeft
            End If
            ' Read chunk from file
            Get #FNum, , FBuf()
            ' Add this data to the hash
            If (CryptHashData(hCryptHash, FBuf(0), BufLen, 0&) = 0) Then
                Err.Raise 2, cstrErrSource, "Can NOT Add this data to the hash"
                ' Done with hash object
                Call CryptDestroyHash(hCryptHash)
                ' Done with provider
                Call CryptReleaseContext(hCryptProv, 0&)
                GoTo Exit_Function ' Failed to acquire cryptographic context
            End If
            ' Decrement read count
            BytesLeft = BytesLeft - BufLen
        Loop While BytesLeft > 0
        BufLen = 0
        ' Get buffer length for hash
        If (CryptGetHashParam(hCryptHash, HP_HASHVAL, ByVal 0&, BufLen, 0&)) Then
            ReDim FBuf(0 To BufLen - 1) As Byte
            If (CryptGetHashParam(hCryptHash, HP_HASHVAL, FBuf(0), BufLen, 0&)) Then
                ' Return final hash buffer
                HashFile = BufLen
                outHash = FBuf
            End If
        End If
        ' Done with hash object
        Call CryptDestroyHash(hCryptHash)
        ' Done with provider
        Call CryptReleaseContext(hCryptProv, 0&)
    Else
        Err.Raise 3, cstrErrSource, "Can NOT Create new MD5 hash"
        ' Done with provider
        Call CryptReleaseContext(hCryptProv, 0&)
    End If
    Close #FNum
Exit_Function:
    Erase FBuf
Exit Function
ErrTrap:
    Err.Raise Err.Number, cstrErrSource, Err.Description
    Err.Clear
    GoTo Exit_Function
End Function


Public Function GetFileMD5HashCode(ByRef inFile As String, _
                            Optional ByVal lngReadChunkSize As Long = 1024) As String
    On Error GoTo ErrTrap
    Const cstrErrSource = "basMD5 - GetFileMD5HashCode"
    Dim Hash() As Byte
    Dim HashLen As Long
    Dim LoopHash As Long
    mlngReadChunkSize = lngReadChunkSize
    ' Perform hash on file
    HashLen = HashFile(inFile, Hash())
    If (HashLen > 0) Then
        ' Allocate return buffer
        GetFileMD5HashCode = String$(HashLen * 2, "0")
        For LoopHash = 0 To HashLen - 1
            If (Hash(LoopHash) < &H10) Then ' Single digit
                Mid$(GetFileMD5HashCode, (LoopHash * 2) + 2, 1) = Hex$(Hash(LoopHash))
            Else                            ' Double digit
                Mid$(GetFileMD5HashCode, (LoopHash * 2) + 1, 2) = Hex$(Hash(LoopHash))
            End If
        Next LoopHash
    End If
Exit_Function:
    Erase Hash
Exit Function
ErrTrap:
    Err.Raise Err.Number, cstrErrSource, Err.Description
    Err.Clear
    GoTo Exit_Function
End Function


Public Function fnArrayMD5(ByRef bytBuffer() As Byte, _
                           ByVal blnFirstCall As Boolean, _
                           ByVal blnLastCall As Boolean) As String
    On Error GoTo ErrTrap
    Dim LoopHash As Long
    Const cstrErrSource = "basMD5 - fnArrayMD5"
    Dim FBuf() As Byte
    Dim lngBufLen As Long
    fnArrayMD5 = vbNullString
    lngBufLen = UBound(bytBuffer) + 1
    FBuf = bytBuffer
    ''ReDim FBuf(0 To UBound(bytBuffer)) As Byte
    If blnFirstCall = True Then
        ' Attempt to open default cryptographic context
        If (CryptAcquireContext(mhCryptProv, vbNullString, _
            MS_DEF_PROV, PROV_RSA_FULL, 0&) = 0) Then
            If (CryptAcquireContext(mhCryptProv, vbNullString, MS_DEF_PROV, _
                PROV_RSA_FULL, CRYPT_NEWKEYSET) = 0) Then
                '''Error
                Err.Raise 1, cstrErrSource, "Failed to acquire cryptographic context"
                GoTo Exit_Function ' Failed to acquire cryptographic context
            End If
        End If
        ' Create new MD5 hash
        If (CryptCreateHash(mhCryptProv, CALG_MD5, 0&, 0&, mhCryptHash)) Then
            ' Add this data to the hash
            If (CryptHashData(mhCryptHash, FBuf(0), lngBufLen, 0&) = 0) Then
                Err.Raise 2, cstrErrSource, "Can NOT Add this data to the hash"
                ' Done with hash object
                Call CryptDestroyHash(mhCryptHash)
                ' Done with provider
                Call CryptReleaseContext(mhCryptProv, 0&)
                GoTo Exit_Function ' Failed to acquire cryptographic context
            End If
        Else
            Err.Raise 3, cstrErrSource, "Can NOT Create new MD5 hash"
            ' Done with provider
            Call CryptReleaseContext(mhCryptProv, 0&)
            GoTo Exit_Function
        End If
    Else ' next buffer
        ' Add this data to the hash
        If (CryptHashData(mhCryptHash, FBuf(0), lngBufLen, 0&) = 0) Then
            Err.Raise 4, cstrErrSource, "Can NOT Add this data to the hash"
            ' Done with hash object
            Call CryptDestroyHash(mhCryptHash)
            ' Done with provider
            Call CryptReleaseContext(mhCryptProv, 0&)
            GoTo Exit_Function ' Failed to acquire cryptographic context
        End If
    End If
    If blnLastCall = True Then
        lngBufLen = 0
        ' Get buffer length for hash
        If (CryptGetHashParam(mhCryptHash, HP_HASHVAL, ByVal 0&, lngBufLen, 0&)) Then
            ReDim FBuf(0 To lngBufLen - 1) As Byte
            If (CryptGetHashParam(mhCryptHash, HP_HASHVAL, FBuf(0), lngBufLen, 0&)) Then
                ' Done with hash object
                Call CryptDestroyHash(mhCryptHash)
                ' Done with provider
                Call CryptReleaseContext(mhCryptProv, 0&)
                ' Return final hash buffer
                If (lngBufLen > 0) Then
                    ' Allocate return buffer
                    fnArrayMD5 = String$(lngBufLen * 2, "0")
                    For LoopHash = 0 To lngBufLen - 1
                        If (FBuf(LoopHash) < &H10) Then ' Single digit
                            Mid$(fnArrayMD5, (LoopHash * 2) + 2, 1) = Hex$(FBuf(LoopHash))
                        Else                            ' Double digit
                            Mid$(fnArrayMD5, (LoopHash * 2) + 1, 2) = Hex$(FBuf(LoopHash))
                        End If
                    Next LoopHash
                Else ' error
                    Err.Raise 5, cstrErrSource, "Zero length for return buffer"
                End If
            End If
        Else 'error
            ' Done with hash object
            Call CryptDestroyHash(mhCryptHash)
            ' Done with provider
            Call CryptReleaseContext(mhCryptProv, 0&)
            Err.Raise 6, cstrErrSource, "Can NOT Get buffer length for hash"
        End If
    End If
Exit_Function:
    Erase FBuf
Exit Function
ErrTrap:
    Err.Raise Err.Number, cstrErrSource, Err.Description
    Err.Clear
    GoTo Exit_Function
End Function

