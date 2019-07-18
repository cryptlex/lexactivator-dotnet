
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Collections.Generic

Namespace Cryptlex
    Public NotInheritable Class LexActivator

        Private Sub New()
        End Sub

        Private Const DLL_FILE_NAME As String = "LexActivator.dll"

        '
        '     In order to use "Any CPU" configuration, rename 64 bit LexActivator.dll to LexActivator64.dll
        '     and uncomment following "LA_ANY_CPU" conditional compilation symbol.
        '        
        '     #Const LA_ANY_CPU = 1
        '

#If LA_ANY_CPU Then
        Private Const DLL_FILE_NAME_X64 As String = "LexActivator64.dll"
#End If

        Public Enum PermissionFlags As UInteger
            LA_USER = 1
            LA_SYSTEM = 2
            LA_IN_MEMORY = 4
        End Enum

        '
        '     FUNCTION: SetProductFile()

        '     PURPOSE: Sets the absolute path of the Product.dat file.

        '     This function must be called on every start of your program
        '     before any other functions are called.

        '     PARAMETERS:
        '     * filePath - absolute path of the product file (Product.dat)

        '     RETURN CODES: LA_OK, LA_E_FILE_PATH, LA_E_PRODUCT_FILE

        '     NOTE: If this function fails to set the path of product file, none of the
        '     other functions will work.
        '
        Public Shared Function SetProductFile(ByVal filePath As String) As Integer
#If LA_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.SetProductFile_x64(filePath), Native.SetProductFile(filePath))
#Else
            Return Native.SetProductFile(filePath)
#End If
        End Function

        '
        '     FUNCTION: SetProductData()

        '     PURPOSE: Embeds the Product.dat file in the application.

        '     It can be used instead of SetProductFile() in case you want
        '     to embed the Product.dat file in your application.

        '     This function must be called on every start of your program
        '     before any other functions are called.

        '     PARAMETERS:
        '     * productData - content of the Product.dat file

        '     RETURN CODES: RETURN CODES: LA_OK, LA_E_PRODUCT_DATA

        '     NOTE: If this function fails to set the product data, none of the
        '     other functions will work.
        '
        Public Shared Function SetProductData(productData As String) As Integer
#If LA_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.SetProductData_x64(productData), Native.SetProductData(productData))
#Else
            Return Native.SetProductData(productData)
#End If
        End Function

        '
        '     FUNCTION: SetProductId()

        '     PURPOSE: Sets the product id of your application.

        '     This function must be called on every start of your program before
        '     any other functions are called, with the exception of SetProductFile()
        '     or SetProductData() function.

        '     PARAMETERS:
        '     * productId - the unique product id of your application as mentioned
        '     on the product page in the dashboard.

        '     * flags - depending upon whether your application requires admin/root
        '     permissions to run or not, this parameter can have one of the following
        '     values: LA_SYSTEM, LA_USER, LA_IN_MEMORY

        '     RETURN CODES: LA_OK, LA_E_WMIC, LA_E_PRODUCT_FILE, LA_E_PRODUCT_DATA, LA_E_PRODUCT_ID,
        '     LA_E_SYSTEM_PERMISSION

        '     NOTE: If this function fails to set the product id, none of the other
        '     functions will work.
        '
        Public Shared Function SetProductId(ByVal productId As String, ByVal flags As PermissionFlags) As Integer
#If LA_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.SetProductId_x64(productId, flags), Native.SetProductId(productId, flags))
#Else
            Return Native.SetProductId(productId, flags)
#End If
        End Function

        '
        '     FUNCTION: SetLicenseKey()

        '     PURPOSE: Sets the license key required to activate the license.

        '     PARAMETERS:
        '     * licenseKey - a valid license key.

        '     RETURN CODES: LA_OK, LA_E_PRODUCT_ID, LA_E_LICENSE_KEY
        '
        Public Shared Function SetLicenseKey(licenseKey As String) As Integer
#If LA_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.SetLicenseKey_x64(licenseKey), Native.SetLicenseKey(licenseKey))
#Else
            Return Native.SetLicenseKey(licenseKey)
#End If
        End Function

        '
        '     FUNCTION: SetLicenseUserCredential()

        '     PURPOSE: Sets the license user email and password for authentication.

        '     This function must be called before ActivateLicense() or IsLicenseGenuine()
        '     function if 'requireAuthentication' property of the license is set to true.

        '     PARAMETERS:
        '     * email - user email address.
        '     * password - user password.

        '     RETURN CODES: LA_OK, LA_E_PRODUCT_ID, LA_E_LICENSE_KEY
        '
        Public Shared Function SetLicenseUserCredential(ByVal email As String, ByVal password As String) As Integer
#If LA_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.SetLicenseUserCredential_x64(email, password), Native.SetLicenseUserCredential(email, password))
#Else
            Return Native.SetLicenseUserCredential(email, password)
#End If
        End Function

        '
        '     FUNCTION: SetLicenseCallback()

        '     PURPOSE: Sets server sync callback Function.

        '     Whenever the server sync occurs In a separate thread, And server returns the response,
        '     license callback Function gets invoked With the following status codes:
        '     LA_OK, LA_EXPIRED, LA_SUSPENDED,
        '     LA_E_REVOKED, LA_E_ACTIVATION_NOT_FOUND, LA_E_MACHINE_FINGERPRINT
        '     LA_E_AUTHENTICATION_FAILED, LA_E_COUNTRY, LA_E_INET, LA_E_SERVER, LA_E_RATE_LIMIT, LA_E_IP

        '     PARAMETERS:
        '     * callback - name of the callback function

        '     Return CODES :  LA_OK, LA_E_PRODUCT_ID, LA_E_LICENSE_KEY
        '        
        Public Shared Function SetLicenseCallback(callback As CallbackType) As Integer
            Dim wrappedCallback = callback
            Dim syncTarget = TryCast(callback.Target, System.Windows.Forms.Control)
            If syncTarget IsNot Nothing Then
                wrappedCallback = Function(v) syncTarget.Invoke(callback, New Object() {v})
            End If
            callbackList.Add(wrappedCallback)
#If LA_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.SetLicenseCallback_x64(wrappedCallback), Native.SetLicenseCallback(wrappedCallback))
#Else
            Return Native.SetLicenseCallback(wrappedCallback)
#End If
        End Function

        '
        '     FUNCTION: SetActivationMetadata()

        '     PURPOSE: Sets the activation metadata.

        '     The  metadata appears along with the activation details of the license
        '     in dashboard.

        '     PARAMETERS:
        '     * key - string of maximum length 256 characters with utf-8 encoding.
        '     * value - string of maximum length 256 characters with utf-8 encoding.

        '     RETURN CODES: LA_OK, LA_E_PRODUCT_ID, LA_E_LICENSE_KEY, LA_E_METADATA_KEY_LENGTH,
        '     LA_E_METADATA_VALUE_LENGTH, LA_E_ACTIVATION_METADATA_LIMIT
        '
        Public Shared Function SetActivationMetadata(ByVal key As String, ByVal value As String) As Integer
#If LA_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.SetActivationMetadata_x64(key, value), Native.SetActivationMetadata(key, value))
#Else
            Return Native.SetActivationMetadata(key, value)
#End If
        End Function

        '
        '     FUNCTION: SetTrialActivationMetadata()

        '     PURPOSE: Sets the trial activation metadata.

        '     The  metadata appears along with the trial activation details of the product
        '     in dashboard.

        '     PARAMETERS:
        '     * key - string of maximum length 256 characters with utf-8 encoding.
        '     * value - string of maximum length 256 characters with utf-8 encoding.

        '     RETURN CODES: LA_OK, LA_E_PRODUCT_ID, LA_E_METADATA_KEY_LENGTH,
        '     LA_E_METADATA_VALUE_LENGTH, LA_E_TRIAL_ACTIVATION_METADATA_LIMIT
        '
        Public Shared Function SetTrialActivationMetadata(ByVal key As String, ByVal value As String) As Integer
#If LA_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.SetTrialActivationMetadata_x64(key, value), Native.SetTrialActivationMetadata(key, value))
#Else
            Return Native.SetTrialActivationMetadata(key, value)
#End If
        End Function

        '
        '     FUNCTION: SetAppVersion()

        '     PURPOSE: Sets the current app version of your application.

        '     The app version appears along with the activation details in dashboard. It
        '     is also used to generate app analytics.

        '     PARAMETERS:
        '     * appVersion - string of maximum length 256 characters with utf-8 encoding.

        '     RETURN CODES: LA_OK, LA_E_PRODUCT_ID, LA_E_APP_VERSION_LENGTH
        '
        Public Shared Function SetAppVersion(ByVal appVersion As String) As Integer
#If LA_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.SetAppVersion_x64(appVersion), Native.SetAppVersion(appVersion))
#Else
            Return Native.SetAppVersion(appVersion)
#End If
        End Function

        '
        '     FUNCTION: SetNetworkProxy()

        '     PURPOSE: Sets the network proxy to be used when contacting Cryptlex servers.

        '     The proxy format should be: [protocol://][username:password@]machine[:port]

        '     Following are some examples of the valid proxy strings:
        '         - http://127.0.0.1:8000/
        '         - http://user:pass@127.0.0.1:8000/
        '         - socks5://127.0.0.1:8000/

        '     PARAMETERS:
        '     * proxy - proxy string having correct proxy format

        '     RETURN CODES: LA_OK, LA_E_PRODUCT_ID, LA_E_NET_PROXY

        '     NOTE: Proxy settings of the computer are automatically detected. So, in most of the
        '     cases you don't need to care whether your user is behind a proxy server or not.
        '
        Public Shared Function SetNetworkProxy(proxy As String) As Integer
#If LA_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.SetNetworkProxy_x64(proxy), Native.SetNetworkProxy(proxy))
#Else
            Return Native.SetNetworkProxy(proxy)
#End If
        End Function

        '
        '     FUNCTION: GetProductMetadata()

        '     PURPOSE: Gets the product metadata as set in the dashboard.

        '     This is available for trial as well as license activations.

        '     PARAMETERS:
        '     * key - key to retrieve the value
        '     * value - pointer to a buffer that receives the value of the string
        '     * length - size of the buffer pointed to by the value parameter

        '     RETURN CODES: LA_OK, LA_E_PRODUCT_ID, LA_E_METADATA_KEY_NOT_FOUND, LA_E_BUFFER_SIZE
        '
        Public Shared Function GetProductMetadata(ByVal key As String, ByVal value As StringBuilder, ByVal length As Integer) As Integer
#If LA_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.GetProductMetadata_x64(key, value, length), Native.GetProductMetadata(key, value, length))
#Else
            Return Native.GetProductMetadata(key, value, length)
#End If
        End Function

        '
        '     FUNCTION: GetLicenseMetadata()

        '     PURPOSE: Gets the license metadata as set in the dashboard.

        '     PARAMETERS:
        '     * key - key to retrieve the value
        '     * value - pointer to a buffer that receives the value of the string
        '     * length - size of the buffer pointed to by the value parameter

        '     RETURN CODES: LA_OK, LA_E_PRODUCT_ID, LA_E_METADATA_KEY_NOT_FOUND, LA_E_BUFFER_SIZE
        '
        Public Shared Function GetLicenseMetadata(ByVal key As String, ByVal value As StringBuilder, ByVal length As Integer) As Integer
#If LA_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.GetLicenseMetadata_x64(key, value, length), Native.GetLicenseMetadata(key, value, length))
#Else
            Return Native.GetLicenseMetadata(key, value, length)
#End If
        End Function

        '
        '     FUNCTION: GetLicenseMeterAttribute()

        '     PURPOSE: Gets the license meter attribute allowed uses and total uses.

        '     PARAMETERS:
        '     * name - name of the meter attribute
        '     * allowedUses - pointer to the integer that receives the value
        '     * totalUses - pointer to the integer that receives the value

        '     RETURN CODES: LA_OK, LA_FAIL, LA_E_PRODUCT_ID, LA_E_METER_ATTRIBUTE_NOT_FOUND
        '
        Public Shared Function GetLicenseMeterAttribute(ByVal name As String, ByRef allowedUses As UInteger, ByRef totalUses As UInteger) As Integer
#If LA_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.GetLicenseMeterAttribute_x64(name, allowedUses, totalUses), Native.GetLicenseMeterAttribute(name, allowedUses, totalUses))
#Else
            Return Native.GetLicenseMeterAttribute(name, allowedUses, totalUses)
#End If
        End Function

        '
        '     FUNCTION: GetLicenseKey()

        '     PURPOSE: Gets the license key used for activation.

        '     PARAMETERS:
        '     * licenseKey - pointer to a buffer that receives the value of the string
        '     * length - size of the buffer pointed to by the licenseKey parameter

        '     RETURN CODES: LA_OK, LA_FAIL, LA_E_PRODUCT_ID, LA_E_BUFFER_SIZE
        '
        Public Shared Function GetLicenseKey(licenseKey As StringBuilder, length As Integer) As Integer
#If LA_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.GetLicenseKey_x64(licenseKey, length), Native.GetLicenseKey(licenseKey, length))
#Else
            Return Native.GetLicenseKey(licenseKey, length)
#End If
        End Function

        '
        '     FUNCTION: GetLicenseExpiryDate()

        '     PURPOSE: Gets the license expiry date timestamp.

        '     PARAMETERS:
        '     * expiryDate - pointer to the integer that receives the value

        '     RETURN CODES: LA_OK, LA_FAIL, LA_E_PRODUCT_ID, LA_E_TIME, LA_E_TIME_MODIFIED
        '
        Public Shared Function GetLicenseExpiryDate(ByRef expiryDate As UInteger) As Integer
#If LA_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.GetLicenseExpiryDate_x64(expiryDate), Native.GetLicenseExpiryDate(expiryDate))
#Else
            Return Native.GetLicenseExpiryDate(expiryDate)
#End If
        End Function

        '
        '     FUNCTION: GetLicenseUserEmail()

        '     PURPOSE: Gets the email associated with the license user.

        '     PARAMETERS:
        '     * licenseKey - pointer to a buffer that receives the value of the string
        '     * length - size of the buffer pointed to by the email parameter

        '     RETURN CODES: LA_OK, LA_FAIL, LA_E_PRODUCT_ID, LA_E_TIME, LA_E_TIME_MODIFIED,
        '     LA_E_BUFFER_SIZE
        '
        Public Shared Function GetLicenseUserEmail(ByVal email As StringBuilder, ByVal length As Integer) As Integer
#If LA_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.GetLicenseUserEmail_x64(email, length), Native.GetLicenseUserEmail(email, length))
#Else
            Return Native.GetLicenseUserEmail(email, length)
#End If
        End Function

        '
        '     FUNCTION: GetLicenseUserName()

        '     PURPOSE: Gets the name associated with the license user.

        '     PARAMETERS:
        '     * name - pointer to a buffer that receives the value of the string
        '     * length - size of the buffer pointed to by the name parameter

        '     RETURN CODES: LA_OK, LA_FAIL, LA_E_PRODUCT_ID, LA_E_TIME, LA_E_TIME_MODIFIED,
        '     LA_E_BUFFER_SIZE
        '
        Public Shared Function GetLicenseUserName(ByVal name As StringBuilder, ByVal length As Integer) As Integer
#If LA_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.GetLicenseUserName_x64(name, length), Native.GetLicenseUserName(name, length))
#Else
            Return Native.GetLicenseUserName(name, length)
#End If
        End Function

        '
        '     FUNCTION: GetLicenseUserCompany()

        '     PURPOSE: Gets the company associated with the license user.

        '     PARAMETERS:
        '     * company - pointer to a buffer that receives the value of the string
        '     * length - size of the buffer pointed to by the company parameter

        '     RETURN CODES: LA_OK, LA_FAIL, LA_E_PRODUCT_ID, LA_E_TIME, LA_E_TIME_MODIFIED,
        '     LA_E_BUFFER_SIZE
        '
        Public Shared Function GetLicenseUserCompany(ByVal company As StringBuilder, ByVal length As Integer) As Integer
#If LA_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.GetLicenseUserCompany_x64(company, length), Native.GetLicenseUserCompany(company, length))
#Else
            Return Native.GetLicenseUserCompany(company, length)
#End If
        End Function

        '
        '     FUNCTION: GetLicenseUserMetadata()

        '     PURPOSE: Gets the metadata associated with the license user.

        '     PARAMETERS:
        '     * key - key to retrieve the value
        '     * value - pointer to a buffer that receives the value of the string
        '     * length - size of the buffer pointed to by the value parameter

        '     RETURN CODES: LA_OK, LA_FAIL, LA_E_PRODUCT_ID, LA_E_METADATA_KEY_NOT_FOUND, LA_E_BUFFER_SIZE
        '
        Public Shared Function GetLicenseUserMetadata(ByVal key As String, ByVal value As StringBuilder, ByVal length As Integer) As Integer
#If LA_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.GetLicenseUserMetadata_x64(key, value, length), Native.GetLicenseUserMetadata(key, value, length))
#Else
            Return Native.GetLicenseUserMetadata(key, value, length)
#End If
        End Function

        '
        '     FUNCTION: GetLicenseType()

        '     PURPOSE: Gets the license type (node-locked or hosted-floating).

        '     PARAMETERS:
        '     * name - pointer to a buffer that receives the value of the string
        '     * length - size of the buffer pointed to by the licenseType parameter

        '     RETURN CODES: LA_OK, LA_FAIL, LA_E_PRODUCT_ID, LA_E_TIME, LA_E_TIME_MODIFIED,
        '     LA_E_BUFFER_SIZE
        '
        Public Shared Function GetLicenseType(ByVal licenseType As StringBuilder, ByVal length As Integer) As Integer
#If LA_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.GetLicenseType_x64(licenseType, length), Native.GetLicenseType(licenseType, length))
#Else
            Return Native.GetLicenseType(licenseType, length)
#End If
        End Function

        '
        '     FUNCTION: GetActivationMetadata()

        '     PURPOSE: Gets the activation metadata.

        '     PARAMETERS:
        '     * key - key to retrieve the value
        '     * value - pointer to a buffer that receives the value of the string
        '     * length - size of the buffer pointed to by the value parameter

        '     RETURN CODES: LA_OK, LA_FAIL, LA_E_PRODUCT_ID, LA_E_METADATA_KEY_NOT_FOUND, LA_E_BUFFER_SIZE
        '
        Public Shared Function GetActivationMetadata(ByVal key As String, ByVal value As StringBuilder, ByVal length As Integer) As Integer
#If LA_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.GetActivationMetadata_x64(key, value, length), Native.GetActivationMetadata(key, value, length))
#Else
            Return Native.GetActivationMetadata(key, value, length)
#End If
        End Function

        '
        '     FUNCTION: GetActivationMeterAttributeUses()

        '     PURPOSE: Gets the meter attribute uses consumed by the activation.

        '     PARAMETERS:
        '     * name - name of the meter attribute
        '     * uses - pointer to the integer that receives the value

        '     RETURN CODES: LA_OK, LA_FAIL, LA_E_PRODUCT_ID, LA_E_METER_ATTRIBUTE_NOT_FOUND
        '
        Public Shared Function GetActivationMeterAttributeUses(ByVal name As String, ByRef uses As UInteger) As Integer
#If LA_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.GetActivationMeterAttributeUses_x64(name, uses), Native.GetActivationMeterAttributeUses(name, uses))
#Else
            Return Native.GetActivationMeterAttributeUses(name, uses)
#End If
        End Function

        '
        '     FUNCTION: GetServerSyncGracePeriodExpiryDate()

        '     PURPOSE: Gets the server sync grace period expiry date timestamp.

        '     PARAMETERS:
        '     * expiryDate - pointer to the integer that receives the value

        '     RETURN CODES: LA_OK, LA_FAIL, LA_E_PRODUCT_ID, LA_E_TIME, LA_E_TIME_MODIFIED
        '
        Public Shared Function GetServerSyncGracePeriodExpiryDate(ByRef expiryDate As UInteger) As Integer
#If LA_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.GetServerSyncGracePeriodExpiryDate_x64(expiryDate), Native.GetServerSyncGracePeriodExpiryDate(expiryDate))
#Else
            Return Native.GetServerSyncGracePeriodExpiryDate(expiryDate)
#End If
        End Function

        '
        '     FUNCTION: GetTrialActivationMetadata()

        '     PURPOSE: Gets the trial activation metadata.

        '     PARAMETERS:
        '     * key - key to retrieve the value
        '     * value - pointer to a buffer that receives the value of the string
        '     * length - size of the buffer pointed to by the value parameter

        '     RETURN CODES: LA_OK, LA_E_PRODUCT_ID, LA_E_METADATA_KEY_NOT_FOUND, LA_E_BUFFER_SIZE
        '
        Public Shared Function GetTrialActivationMetadata(ByVal key As String, ByVal value As StringBuilder, ByVal length As Integer) As Integer
#If LA_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.GetTrialActivationMetadata_x64(key, value, length), Native.GetTrialActivationMetadata(key, value, length))
#Else
            Return Native.GetTrialActivationMetadata(key, value, length)
#End If
        End Function

        '
        '     FUNCTION: GetTrialExpiryDate()

        '     PURPOSE: Gets the trial expiry date timestamp.

        '     PARAMETERS:
        '     * trialExpiryDate - pointer to the integer that receives the value

        '     RETURN CODES: LA_OK, LA_FAIL, LA_E_PRODUCT_ID, LA_E_TIME, LA_E_TIME_MODIFIED
        '
        Public Shared Function GetTrialExpiryDate(ByRef trialExpiryDate As UInteger) As Integer
#If LA_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.GetTrialExpiryDate_x64(trialExpiryDate), Native.GetTrialExpiryDate(trialExpiryDate))
#Else
            Return Native.GetTrialExpiryDate(trialExpiryDate)
#End If
        End Function

        '
        '     FUNCTION: GetTrialId()

        '     PURPOSE: Gets the trial activation id. Used in case of trial extension.

        '     PARAMETERS:
        '     * trialId - pointer to a buffer that receives the value of the string
        '     * length - size of the buffer pointed to by the value parameter

        '     RETURN CODES: LA_OK, LA_FAIL, LA_E_PRODUCT_ID, LA_E_TIME, LA_E_TIME_MODIFIED,
        '     LA_E_BUFFER_SIZE
        '
        Public Shared Function GetTrialId(ByVal trialId As StringBuilder, ByVal length As Integer) As Integer
#If LA_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.GetTrialId_x64(trialId, length), Native.GetTrialId(trialId, length))
#Else
            Return Native.GetTrialId(trialId, length)
#End If
        End Function

        '
        '     FUNCTION: GetLocalTrialExpiryDate()

        '     PURPOSE: Gets the trial expiry date timestamp.

        '     PARAMETERS:
        '     * trialExpiryDate - pointer to the integer that receives the value

        '     RETURN CODES: LA_OK, LA_FAIL, LA_E_PRODUCT_ID, LA_E_TIME_MODIFIED
        '
        Public Shared Function GetLocalTrialExpiryDate(ByRef trialExpiryDate As UInteger) As Integer
#If LA_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.GetLocalTrialExpiryDate_x64(trialExpiryDate), Native.GetLocalTrialExpiryDate(trialExpiryDate))
#Else
            Return Native.GetLocalTrialExpiryDate(trialExpiryDate)
#End If
        End Function

        '
        '     FUNCTION: CheckForReleaseUpdate()

        '     PURPOSE: Checks whether a new release is available for the product.

        '     This function should only be used if you manage your releases through
        '     Cryptlex release management API.

        '     PARAMETERS:
        '     * platform - release platform e.g. windows, macos, linux
        '     * version - current release version
        '     * channel - release channel e.g. stable
        '     * callback - name of the callback function.

        '     RETURN CODES: LA_OK, LA_E_PRODUCT_ID, LA_E_LICENSE_KEY, LA_E_RELEASE_VERSION_FORMAT
        '
        Public Shared Function CheckForReleaseUpdate(platform As String, version As String, channel As String, callback As CallbackType) As Integer
            Dim wrappedCallback = callback
            Dim syncTarget = TryCast(callback.Target, System.Windows.Forms.Control)
            If syncTarget IsNot Nothing Then
                wrappedCallback = Function(v) syncTarget.Invoke(callback, New Object() {v})
            End If
            callbackList.Add(wrappedCallback)
#If LA_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.CheckForReleaseUpdate_x64(platform, version, channel, wrappedCallback), Native.CheckForReleaseUpdate(platform, version, channel, wrappedCallback))
#Else
            Return Native.CheckForReleaseUpdate(platform, version, channel, wrappedCallback)
#End If

        End Function

        '
        '     FUNCTION: ActivateLicense()

        '     PURPOSE: Activates the license by contacting the Cryptlex servers. It
        '     validates the key and returns with encrypted and digitally signed token
        '     which it stores and uses to activate your application.

        '     This function should be executed at the time of registration, ideally on
        '     a button click.

        '     RETURN CODES: LA_OK, LA_EXPIRED, LA_SUSPENDED, LA_E_REVOKED, LA_FAIL, LA_E_PRODUCT_ID,
        '     LA_E_INET, LA_E_VM, LA_E_TIME, LA_E_ACTIVATION_LIMIT, LA_E_SERVER, LA_E_CLIENT, LA_E_LICENSE_KEY,
        '     LA_E_AUTHENTICATION_FAILED, LA_E_LICENSE_TYPE, LA_E_COUNTRY, LA_E_IP, LA_E_RATE_LIMIT
        '
        Public Shared Function ActivateLicense() As Integer
#If LA_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.ActivateLicense_x64(), Native.ActivateLicense())
#Else
            Return Native.ActivateLicense()
#End If

        End Function

        '
        '     FUNCTION: ActivateLicenseOffline()

        '     PURPOSE: Activates your licenses using the offline activation response file.

        '     PARAMETERS:
        '     * filePath - path of the offline activation response file.

        '     RETURN CODES: LA_OK, LA_EXPIRED, LA_FAIL, LA_E_PRODUCT_ID, LA_E_LICENSE_KEY, LA_E_OFFLINE_RESPONSE_FILE
        '     LA_E_VM, LA_E_TIME, LA_E_FILE_PATH, LA_E_OFFLINE_RESPONSE_FILE_EXPIRED
        '
        Public Shared Function ActivateLicenseOffline(filePath As String) As Integer
#If LA_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.ActivateLicenseOffline_x64(filePath), Native.ActivateLicenseOffline(filePath))
#Else
            Return Native.ActivateLicenseOffline(filePath)
#End If
        End Function

        '
        '     FUNCTION: GenerateOfflineActivationRequest()

        '     PURPOSE: Generates the offline activation request needed for generating
        '     offline activation response in the dashboard.

        '     PARAMETERS:
        '     * filePath - path of the file for the offline request.

        '     RETURN CODES: LA_OK, LA_FAIL, LA_E_PRODUCT_ID, LA_E_LICENSE_KEY, LA_E_FILE_PERMISSION
        '
        Public Shared Function GenerateOfflineActivationRequest(filePath As String) As Integer
#If LA_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.GenerateOfflineActivationRequest_x64(filePath), Native.GenerateOfflineActivationRequest(filePath))
#Else
            Return Native.GenerateOfflineActivationRequest(filePath)
#End If
        End Function

        '
        '     FUNCTION: DeactivateLicense()

        '     PURPOSE: Deactivates the license activation and frees up the corresponding activation
        '     slot by contacting the Cryptlex servers.

        '     This function should be executed at the time of de-registration, ideally on
        '     a button click.

        '     RETURN CODES: LA_OK, LA_E_DEACTIVATION_LIMIT, LA_FAIL, LA_E_PRODUCT_ID, LA_E_TIME
        '     LA_E_LICENSE_KEY, LA_E_INET, LA_E_SERVER, LA_E_RATE_LIMIT, LA_E_TIME_MODIFIED
        '
        Public Shared Function DeactivateLicense() As Integer
#If LA_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.DeactivateLicense_x64(), Native.DeactivateLicense())
#Else
            Return Native.DeactivateLicense()
#End If
        End Function

        '
        '     FUNCTION: GenerateOfflineDeactivationRequest()

        '     PURPOSE: Generates the offline deactivation request needed for deactivation of
        '     the license in the dashboard and deactivates the license locally.

        '     A valid offline deactivation file confirms that the license has been successfully
        '     deactivated on the user's machine.

        '     PARAMETERS:
        '     * filePath - path of the file for the offline request.

        '     RETURN CODES: LA_OK, LA_FAIL, LA_E_PRODUCT_ID, LA_E_LICENSE_KEY, LA_E_FILE_PERMISSION,
        '     LA_E_TIME, LA_E_TIME_MODIFIED
        '
        Public Shared Function GenerateOfflineDeactivationRequest(filePath As String) As Integer
#If LA_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.GenerateOfflineDeactivationRequest_x64(filePath), Native.GenerateOfflineDeactivationRequest(filePath))
#Else
            Return Native.GenerateOfflineDeactivationRequest(filePath)
#End If
        End Function

        '
        '     FUNCTION: IsLicenseGenuine()

        '     PURPOSE: It verifies whether your app is genuinely activated or not. The verification is
        '     done locally by verifying the cryptographic digital signature fetched at the time of
        '     activation.

        '     After verifying locally, it schedules a server check in a separate thread. After the
        '     first server sync it periodically does further syncs at a frequency set for the license.

        '     In case server sync fails due to network error, and it continues to fail for fixed
        '     number of days (grace period), the function returns LA_GRACE_PERIOD_OVER instead of LA_OK.

        '     This function must be called on every start of your program to verify the activation
        '     of your app.

        '     RETURN CODES: LA_OK, LA_EXPIRED, LA_SUSPENDED, LA_GRACE_PERIOD_OVER, LA_FAIL,
        '     LA_E_PRODUCT_ID, LA_E_LICENSE_KEY, LA_E_TIME, LA_E_TIME_MODIFIED

        '     NOTE: If application was activated offline using ActivateLicenseOffline() function, you
        '     may want to set grace period to 0 to ignore grace period.
        '
        Public Shared Function IsLicenseGenuine() As Integer
#If LA_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.IsLicenseGenuine_x64(), Native.IsLicenseGenuine())
#Else
            Return Native.IsLicenseGenuine()
#End If
        End Function

        '
        '     FUNCTION: IsLicenseValid()

        '     PURPOSE: It verifies whether your app is genuinely activated or not. The verification is
        '     done locally by verifying the cryptographic digital signature fetched at the time of
        '     activation.

        '     This is just an auxiliary function which you may use in some specific cases, when you
        '     want to skip the server sync.

        '     RETURN CODES: LA_OK, LA_EXPIRED, LA_SUSPENDED, LA_GRACE_PERIOD_OVER, LA_FAIL,
        '     LA_E_PRODUCT_ID, LA_E_LICENSE_KEY, LA_E_TIME, LA_E_TIME_MODIFIED

        '     NOTE: You may want to set grace period to 0 to ignore grace period.
        '
        Public Shared Function IsLicenseValid() As Integer
#If LA_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.IsLicenseValid_x64(), Native.IsLicenseValid())
#Else
            Return Native.IsLicenseValid()
#End If
        End Function

        '
        '     FUNCTION: ActivateTrial()

        '     PURPOSE: Starts the verified trial in your application by contacting the
        '     Cryptlex servers.

        '     This function should be executed when your application starts first time on
        '     the user's computer, ideally on a button click.

        '     RETURN CODES: LA_OK, LA_TRIAL_EXPIRED, LA_FAIL, LA_E_PRODUCT_ID, LA_E_INET,
        '     LA_E_VM, LA_E_TIME, LA_E_SERVER, LA_E_CLIENT, LA_E_COUNTRY, LA_E_IP, LA_E_RATE_LIMIT
        '
        Public Shared Function ActivateTrial() As Integer
#If LA_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.ActivateTrial_x64(), Native.ActivateTrial())
#Else
            Return Native.ActivateTrial()
#End If
        End Function

        '
        '     FUNCTION: ActivateTrialOffline()

        '     PURPOSE: Activates your trial using the offline activation response file.

        '     PARAMETERS:
        '     * filePath - path of the offline activation response file.

        '     RETURN CODES: LA_OK, LA_TRIAL_EXPIRED, LA_FAIL, LA_E_PRODUCT_ID, LA_E_OFFLINE_RESPONSE_FILE
        '     LA_E_VM, LA_E_TIME, LA_E_FILE_PATH, LA_E_OFFLINE_RESPONSE_FILE_EXPIRED
        '
        Public Shared Function ActivateTrialOffline(filePath As String) As Integer
#If LA_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.ActivateTrialOffline_x64(filePath), Native.ActivateTrialOffline(filePath))
#Else
            Return Native.ActivateTrialOffline(filePath)
#End If
        End Function

        '
        '     FUNCTION: GenerateOfflineTrialActivationRequest()

        '     PURPOSE: Generates the offline trial activation request needed for generating
        '     offline trial activation response in the dashboard.

        '     PARAMETERS:
        '     * filePath - path of the file for the offline request.

        '     RETURN CODES: LA_OK, LA_FAIL, LA_E_PRODUCT_ID, LA_E_FILE_PERMISSION
        '
        Public Shared Function GenerateOfflineTrialActivationRequest(filePath As String) As Integer
#If LA_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.GenerateOfflineTrialActivationRequest_x64(filePath), Native.GenerateOfflineTrialActivationRequest(filePath))
#Else
            Return Native.GenerateOfflineTrialActivationRequest(filePath)
#End If
        End Function

        '
        '     FUNCTION: IsTrialGenuine()

        '     PURPOSE: It verifies whether trial has started and is genuine or not. The
        '     verification is done locally by verifying the cryptographic digital signature
        '     fetched at the time of trial activation.

        '     This function must be called on every start of your program during the trial period.

        '     RETURN CODES: LA_OK, LA_TRIAL_EXPIRED, LA_FAIL, LA_E_TIME, LA_E_PRODUCT_ID,
        '     LA_E_TIME_MODIFIED
        '
        Public Shared Function IsTrialGenuine() As Integer
#If LA_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.IsTrialGenuine_x64(), Native.IsTrialGenuine())
#Else
            Return Native.IsTrialGenuine()
#End If
        End Function

        '
        '     FUNCTION: ActivateLocalTrial()

        '     PURPOSE: Starts the local(unverified) trial.

        '     This function should be executed when your application starts first time on
        '     the user's computer.

        '     PARAMETERS:
        '     * trialLength - trial length in days

        '     RETURN CODES: LA_OK, LA_LOCAL_TRIAL_EXPIRED, LA_FAIL, LA_E_PRODUCT_ID, LA_E_TIME_MODIFIED

        '     NOTE: The function is only meant for local(unverified) trials.
        '
        Public Shared Function ActivateLocalTrial(trialLength As UInteger) As Integer
#If LA_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.ActivateLocalTrial_x64(trialLength), Native.ActivateLocalTrial(trialLength))
#Else
            Return Native.ActivateLocalTrial(trialLength)
#End If
        End Function

        '
        '     FUNCTION: IsLocalTrialGenuine()

        '     PURPOSE: It verifies whether trial has started and is genuine or not. The
        '     verification is done locally.

        '     This function must be called on every start of your program during the trial period.

        '     RETURN CODES: LA_OK, LA_LOCAL_TRIAL_EXPIRED, LA_FAIL, LA_E_PRODUCT_ID, LA_E_TIME_MODIFIED

        '     NOTE: The function is only meant for local(unverified) trials.
        '
        Public Shared Function IsLocalTrialGenuine() As Integer
#If LA_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.IsLocalTrialGenuine_x64(), Native.IsLocalTrialGenuine())
#Else
            Return Native.IsLocalTrialGenuine()
#End If
        End Function

        '
        '     FUNCTION: ExtendLocalTrial()

        '     PURPOSE: Extends the local trial.

        '     PARAMETERS:
        '     * trialExtensionLength - number of days to extend the trial

        '     RETURN CODES: LA_OK, LA_FAIL, LA_E_PRODUCT_ID, LA_E_TIME_MODIFIED

        '     NOTE: The function is only meant for local(unverified) trials.
        '
        Public Shared Function ExtendLocalTrial(trialExtensionLength As UInteger) As Integer
#If LA_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.ExtendLocalTrial_x64(trialExtensionLength), Native.ExtendLocalTrial(trialExtensionLength))
#Else
            Return Native.ExtendLocalTrial(trialExtensionLength)
#End If
        End Function

        '
        '     FUNCTION: IncrementActivationMeterAttributeUses()

        '     PURPOSE: Increments the meter attribute uses of the activation.

        '     PARAMETERS:
        '     * name - name of the meter attribute
        '     * increment - the increment value

        '     RETURN CODES: LA_OK, LA_FAIL, LA_E_PRODUCT_ID, LA_E_METER_ATTRIBUTE_NOT_FOUND,
        '     LA_E_INET, LA_E_TIME, LA_E_SERVER, LA_E_CLIENT, LA_E_METER_ATTRIBUTE_USES_LIMIT_REACHED,
        '     LA_E_AUTHENTICATION_FAILED, LA_E_COUNTRY, LA_E_IP, LA_E_RATE_LIMIT, LA_E_LICENSE_KEY

        '
        Public Shared Function IncrementActivationMeterAttributeUses(ByVal name As String, increment As UInteger) As Integer
#If LA_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.IncrementActivationMeterAttributeUses_x64(name, increment), Native.IncrementActivationMeterAttributeUses(name, increment))
#Else
            Return Native.IncrementActivationMeterAttributeUses(name, increment)
#End If
        End Function

        '
        '     FUNCTION: DecrementActivationMeterAttributeUses()

        '     PURPOSE: Decrements the meter attribute uses of the activation.

        '     PARAMETERS:
        '     * name - name of the meter attribute
        '     * decrement - the decrement value

        '     RETURN CODES: LA_OK, LA_FAIL, LA_E_PRODUCT_ID, LA_E_METER_ATTRIBUTE_NOT_FOUND,
        '     LA_E_INET, LA_E_TIME, LA_E_SERVER, LA_E_CLIENT, LA_E_RATE_LIMIT, LA_E_LICENSE_KEY,
        '     LA_E_AUTHENTICATION_FAILED, LA_E_COUNTRY, LA_E_IP, LA_E_ACTIVATION_NOT_FOUND

        '
        Public Shared Function DecrementActivationMeterAttributeUses(ByVal name As String, decrement As UInteger) As Integer
#If LA_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.DecrementActivationMeterAttributeUses_x64(name, decrement), Native.DecrementActivationMeterAttributeUses(name, decrement))
#Else
            Return Native.DecrementActivationMeterAttributeUses(name, decrement)
#End If
        End Function

        '
        '     FUNCTION: ResetActivationMeterAttributeUses()

        '     PURPOSE: Resets the meter attribute uses of the activation.

        '     PARAMETERS:
        '     * name - name of the meter attribute

        '     RETURN CODES: LA_OK, LA_FAIL, LA_E_PRODUCT_ID, LA_E_METER_ATTRIBUTE_NOT_FOUND,
        '     LA_E_INET, LA_E_TIME, LA_E_SERVER, LA_E_CLIENT, LA_E_RATE_LIMIT, LA_E_LICENSE_KEY,
        '     LA_E_AUTHENTICATION_FAILED, LA_E_COUNTRY, LA_E_IP, LA_E_ACTIVATION_NOT_FOUND

        '
        Public Shared Function ResetActivationMeterAttributeUses(ByVal name As String) As Integer
#If LA_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.ResetActivationMeterAttributeUses_x64(name), Native.ResetActivationMeterAttributeUses(name))
#Else
            Return Native.ResetActivationMeterAttributeUses(name)
#End If
        End Function

        '
        '     FUNCTION: Reset()

        '     PURPOSE: Resets the activation and trial data stored in the machine.

        '     This function is meant for developer testing only.

        '     RETURN CODES: LA_OK, LA_E_PRODUCT_ID

        '     NOTE: The function does not reset local(unverified) trial data.
        '
        Public Shared Function Reset() As Integer
#If LA_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.Reset_x64(), Native.Reset())
#Else
            Return Native.Reset()
#End If
        End Function

        Public Enum StatusCodes As UInteger

            '
            '    CODE: LA_OK

            '    MESSAGE: Success code.
            '
            LA_OK = 0

            '
            '    CODE: LA_FAIL

            '    MESSAGE: Failure code.
            '
            LA_FAIL = 1

            '
            '    CODE: LA_EXPIRED

            '    MESSAGE: The license has expired or system time has been tampered
            '    with. Ensure your date and time settings are correct.
            '
            LA_EXPIRED = 20

            '
            '    CODE: LA_SUSPENDED

            '    MESSAGE: The license has been suspended.
            '
            LA_SUSPENDED = 21

            '
            '    CODE: LA_GRACE_PERIOD_OVER

            '    MESSAGE: The grace period for server sync is over.
            '
            LA_GRACE_PERIOD_OVER = 22

            '
            '    CODE: LA_TRIAL_EXPIRED

            '    MESSAGE: The trial has expired or system time has been tampered
            '    with. Ensure your date and time settings are correct.
            '
            LA_TRIAL_EXPIRED = 25

            '
            '    CODE: LA_LOCAL_TRIAL_EXPIRED

            '    MESSAGE: The local trial has expired or system time has been tampered
            '    with. Ensure your date and time settings are correct.
            '
            LA_LOCAL_TRIAL_EXPIRED = 26

            '
            '    CODE: LA_RELEASE_UPDATE_AVAILABLE

            '    MESSAGE: A new update is available for the product. This means a new release has
            '    been published for the product.
            '
            LA_RELEASE_UPDATE_AVAILABLE = 30

            '
            '    CODE: LA_RELEASE_NO_UPDATE_AVAILABLE

            '    MESSAGE: No new update is available for the product. The current version is latest.
            '
            LA_RELEASE_NO_UPDATE_AVAILABLE = 31

            '
            '    CODE: LA_E_FILE_PATH

            '    MESSAGE: Invalid file path.
            '
            LA_E_FILE_PATH = 40

            '
            '    CODE: LA_E_PRODUCT_FILE

            '    MESSAGE: Invalid or corrupted product file.
            '
            LA_E_PRODUCT_FILE = 41

            '
            '    CODE: LA_E_PRODUCT_DATA

            '    MESSAGE: Invalid product data.
            '
            LA_E_PRODUCT_DATA = 42

            '
            '    CODE: LA_E_PRODUCT_ID

            '    MESSAGE: The product id is incorrect.
            '
            LA_E_PRODUCT_ID = 43

            '
            '    CODE: LA_E_SYSTEM_PERMISSION

            '    MESSAGE: Insufficent system permissions. Occurs when LA_SYSTEM flag is used
            '    but application is not run with admin privileges.
            '
            LA_E_SYSTEM_PERMISSION = 44

            '
            '    CODE: LA_E_FILE_PERMISSION

            '    MESSAGE: No permission to write to file.
            '
            LA_E_FILE_PERMISSION = 45

            '
            '    CODE: LA_E_WMIC

            '    MESSAGE: Fingerprint couldn't be generated because Windows Management
            '    Instrumentation (WMI) service has been disabled. This error is specific
            '    to Windows only.
            '
            LA_E_WMIC = 46

            '
            '    CODE: LA_E_TIME

            '    MESSAGE: The difference between the network time and the system time is
            '    more than allowed clock offset.
            '
            LA_E_TIME = 47

            '
            '    CODE: LA_E_INET

            '    MESSAGE: Failed to connect to the server due to network error.
            '
            LA_E_INET = 48

            '
            '    CODE: LA_E_NET_PROXY

            '    MESSAGE: Invalid network proxy.
            '
            LA_E_NET_PROXY = 49

            '
            '    CODE: LA_E_HOST_URL

            '    MESSAGE: Invalid Cryptlex host url.
            '
            LA_E_HOST_URL = 50

            '
            '    CODE: LA_E_BUFFER_SIZE

            '    MESSAGE: The buffer size was smaller than required.
            '
            LA_E_BUFFER_SIZE = 51

            '
            '    CODE: LA_E_APP_VERSION_LENGTH

            '    MESSAGE: App version length is more than 256 characters.
            '
            LA_E_APP_VERSION_LENGTH = 52

            '
            '    CODE: LA_E_REVOKED

            '    MESSAGE: The license has been revoked.
            '
            LA_E_REVOKED = 53

            '
            '    CODE: LA_E_LICENSE_KEY

            '    MESSAGE: Invalid license key.
            '
            LA_E_LICENSE_KEY = 54

            '
            '    CODE: LA_E_LICENSE_TYPE

            '    MESSAGE: Invalid license type. Make sure floating license
            '    is not being used.
            '
            LA_E_LICENSE_TYPE = 55

            '
            '    CODE: LA_E_OFFLINE_RESPONSE_FILE

            '    MESSAGE: Invalid offline activation response file.
            '
            LA_E_OFFLINE_RESPONSE_FILE = 56

            '
            '    CODE: LA_E_OFFLINE_RESPONSE_FILE_EXPIRED

            '    MESSAGE: The offline activation response has expired.
            '
            LA_E_OFFLINE_RESPONSE_FILE_EXPIRED = 57

            '
            '    CODE: LA_E_ACTIVATION_LIMIT

            '    MESSAGE: The license has reached it's allowed activations limit.
            '
            LA_E_ACTIVATION_LIMIT = 58

            '
            '    CODE: LA_E_ACTIVATION_NOT_FOUND

            '    MESSAGE: The license activation was deleted on the server.
            '
            LA_E_ACTIVATION_NOT_FOUND = 59

            '
            '    CODE: LA_E_DEACTIVATION_LIMIT

            '    MESSAGE: The license has reached it's allowed deactivations limit.
            '
            LA_E_DEACTIVATION_LIMIT = 60

            '
            '    CODE: LA_E_TRIAL_NOT_ALLOWED

            '    MESSAGE: Trial not allowed for the product.
            '
            LA_E_TRIAL_NOT_ALLOWED = 61

            '
            '    CODE: LA_E_TRIAL_ACTIVATION_LIMIT

            '    MESSAGE: Your account has reached it's trial activations limit.
            '
            LA_E_TRIAL_ACTIVATION_LIMIT = 62

            '
            '    CODE: LA_E_MACHINE_FINGERPRINT

            '    MESSAGE: Machine fingerprint has changed since activation.
            '
            LA_E_MACHINE_FINGERPRINT = 63

            '
            '    CODE: LA_E_METADATA_KEY_LENGTH

            '    MESSAGE: Metadata key length is more than 256 characters.
            '
            LA_E_METADATA_KEY_LENGTH = 64

            '
            '    CODE: LA_E_METADATA_VALUE_LENGTH

            '    MESSAGE: Metadata value length is more than 256 characters.
            '
            LA_E_METADATA_VALUE_LENGTH = 65

            '
            '    CODE: LA_E_ACTIVATION_METADATA_LIMIT

            '    MESSAGE: The license has reached it's metadata fields limit.
            '
            LA_E_ACTIVATION_METADATA_LIMIT = 66

            '
            '    CODE: LA_E_TRIAL_ACTIVATION_METADATA_LIMIT

            '    MESSAGE: The trial has reached it's metadata fields limit.
            '
            LA_E_TRIAL_ACTIVATION_METADATA_LIMIT = 67

            '
            '    CODE: LA_E_METADATA_KEY_NOT_FOUND

            '    MESSAGE: The metadata key does not exist.
            '
            LA_E_METADATA_KEY_NOT_FOUND = 68

            '
            '    CODE: LA_E_TIME_MODIFIED

            '    MESSAGE: The system time has been tampered (backdated).
            '
            LA_E_TIME_MODIFIED = 69

            '
            '    CODE: LA_E_RELEASE_VERSION_FORMAT

            '    MESSAGE: Invalid version format.
            '
            LA_E_RELEASE_VERSION_FORMAT = 70

            '
            '    CODE: LA_E_AUTHENTICATION_FAILED

            '    MESSAGE: Incorrect email or password.
            '
            LA_E_AUTHENTICATION_FAILED = 71

            '
            '    CODE: LA_E_METER_ATTRIBUTE_NOT_FOUND

            '    MESSAGE: The meter attribute does not exist.
            '
            LA_E_METER_ATTRIBUTE_NOT_FOUND = 72

            '
            '    CODE: LA_E_METER_ATTRIBUTE_USES_LIMIT_REACHED

            '    MESSAGE: The meter attribute has reached it's usage limit.
            '
            LA_E_METER_ATTRIBUTE_USES_LIMIT_REACHED = 73

            '
            '    CODE: LA_E_VM

            '    MESSAGE: Application is being run inside a virtual machine / hypervisor
            '    and activation has been disallowed in the VM.
            '
            LA_E_VM = 80

            '
            '    CODE: LA_E_COUNTRY

            '    MESSAGE: Country is not allowed.
            '
            LA_E_COUNTRY = 81

            '
            '    CODE: LA_E_IP

            '    MESSAGE: IP address is not allowed.
            '
            LA_E_IP = 82

            '
            '    CODE: LA_E_RATE_LIMIT

            '    MESSAGE: Rate limit for API has reached try again later.
            '
            LA_E_RATE_LIMIT = 90

            '
            '    CODE: LA_E_SERVER

            '    MESSAGE: Server error.
            '
            LA_E_SERVER = 91

            '
            '    CODE: LA_E_CLIENT

            '    MESSAGE: Client error.
            '
            LA_E_CLIENT = 92
        End Enum

        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Sub CallbackType(status As UInteger)

        ' To prevent garbage collection of delegate, need to keep a reference 
        Shared ReadOnly callbackList As List(Of CallbackType) = New List(Of CallbackType)()

        Private NotInheritable Class Native

            Private Sub New()
            End Sub

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function SetProductFile(ByVal filePath As String) As Integer
            End Function

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function SetProductData(ByVal productData As String) As Integer
            End Function

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function SetProductId(ByVal productId As String, ByVal flags As PermissionFlags) As Integer
            End Function

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function SetLicenseKey(ByVal licenseKey As String) As Integer
            End Function

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function SetLicenseUserCredential(ByVal email As String, ByVal password As String) As Integer
            End Function

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function SetLicenseCallback(callback As CallbackType) As Integer
            End Function

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function SetActivationMetadata(ByVal key As String, ByVal value As String) As Integer
            End Function

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function SetTrialActivationMetadata(ByVal key As String, ByVal value As String) As Integer
            End Function

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function SetAppVersion(ByVal appVersion As String) As Integer
            End Function

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function SetNetworkProxy(ByVal proxy As String) As Integer
            End Function

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function GetProductMetadata(ByVal key As String, ByVal value As StringBuilder, ByVal length As Integer) As Integer
            End Function

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function GetLicenseMetadata(ByVal key As String, ByVal value As StringBuilder, ByVal length As Integer) As Integer
            End Function

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function GetLicenseMeterAttribute(ByVal name As String, ByRef allowedUses As UInteger, ByRef totalUses As UInteger) As Integer
            End Function

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function GetLicenseKey(ByVal licenseKey As StringBuilder, ByVal length As Integer) As Integer
            End Function

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function GetLicenseExpiryDate(ByRef expiryDate As UInteger) As Integer
            End Function

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function GetLicenseUserEmail(ByVal email As StringBuilder, ByVal length As Integer) As Integer
            End Function

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function GetLicenseUserName(ByVal name As StringBuilder, ByVal length As Integer) As Integer
            End Function

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function GetLicenseUserCompany(ByVal company As StringBuilder, ByVal length As Integer) As Integer
            End Function

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function GetLicenseUserMetadata(ByVal key As String, ByVal value As StringBuilder, ByVal length As Integer) As Integer
            End Function

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function GetLicenseType(ByVal licenseType As StringBuilder, ByVal length As Integer) As Integer
            End Function

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function GetActivationMetadata(ByVal key As String, ByVal value As StringBuilder, ByVal length As Integer) As Integer
            End Function

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function GetActivationMeterAttributeUses(ByVal name As String, ByRef uses As UInteger) As Integer
            End Function

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function GetServerSyncGracePeriodExpiryDate(ByRef expiryDate As UInteger) As Integer
            End Function

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function GetTrialActivationMetadata(ByVal key As String, ByVal value As StringBuilder, ByVal length As Integer) As Integer
            End Function

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function GetTrialExpiryDate(ByRef trialExpiryDate As UInteger) As Integer
            End Function

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function GetTrialId(ByVal trialId As StringBuilder, ByVal length As Integer) As Integer
            End Function

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function GetLocalTrialExpiryDate(ByRef trialExpiryDate As UInteger) As Integer
            End Function

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function CheckForReleaseUpdate(ByVal platform As String, ByVal version As String, ByVal channel As String, callback As CallbackType) As Integer
            End Function

             <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function ActivateLicense() As Integer
            End Function

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function ActivateLicenseOffline(ByVal filePath As String) As Integer
            End Function

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function GenerateOfflineActivationRequest(ByVal filePath As String) As Integer
            End Function

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function DeactivateLicense() As Integer
            End Function

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function GenerateOfflineDeactivationRequest(ByVal filePath As String) As Integer
            End Function

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function IsLicenseGenuine() As Integer
            End Function

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function IsLicenseValid() As Integer
            End Function

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function ActivateTrial() As Integer
            End Function

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function ActivateTrialOffline(ByVal filePath As String) As Integer
            End Function

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function GenerateOfflineTrialActivationRequest(ByVal filePath As String) As Integer
            End Function

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function IsTrialGenuine() As Integer
            End Function

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function ActivateLocalTrial(ByVal trialLength As UInteger) As Integer
            End Function

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function IsLocalTrialGenuine() As Integer
            End Function

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function ExtendLocalTrial(ByVal trialExtensionLength As UInteger) As Integer
            End Function

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function IncrementActivationMeterAttributeUses(ByVal name As String, increment As UInteger) As Integer
            End Function

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function DecrementActivationMeterAttributeUses(ByVal name As String, decrement As UInteger) As Integer
            End Function

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function ResetActivationMeterAttributeUses(ByVal name As String) As Integer
            End Function

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function Reset() As Integer
            End Function


#If LA_ANY_CPU Then
            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="SetProductFile", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function SetProductFile_x64(ByVal filePath As String) As Integer
            End Function

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="SetProductData", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function SetProductData_x64(ByVal productData As String) As Integer
            End Function

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="SetProductId", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function SetProductId_x64(ByVal productId As String, ByVal flags As PermissionFlags) As Integer
            End Function

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="SetLicenseKey", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function SetLicenseKey_x64(ByVal licenseKey As String) As Integer
            End Function

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="SetLicenseUserCredential", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function SetLicenseUserCredential_x64(ByVal email As String, ByVal password As String) As Integer
            End Function

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="SetLicenseCallback", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function SetLicenseCallback_x64(callback As CallbackType) As Integer
            End Function

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="SetActivationMetadata", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function SetActivationMetadata_x64(ByVal key As String, ByVal value As String) As Integer
            End Function

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="SetTrialActivationMetadata", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function SetTrialActivationMetadata_x64(ByVal key As String, ByVal value As String) As Integer
            End Function

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="SetAppVersion", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function SetAppVersion_x64(ByVal appVersion As String) As Integer
            End Function

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="SetNetworkProxy", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function SetNetworkProxy_x64(ByVal proxy As String) As Integer
            End Function

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="GetProductMetadata", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function GetProductMetadata_x64(ByVal key As String, ByVal value As StringBuilder, ByVal length As Integer) As Integer
            End Function

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="GetLicenseMetadata", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function GetLicenseMetadata_x64(ByVal key As String, ByVal value As StringBuilder, ByVal length As Integer) As Integer
            End Function

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="GetLicenseMeterAttribute", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function GetLicenseMeterAttribute_x64(ByVal name As String, ByRef allowedUses As UInteger, ByRef totalUses As UInteger) As Integer
            End Function

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="GetLicenseKey", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function GetLicenseKey_x64(ByVal licenseKey As StringBuilder, ByVal length As Integer) As Integer
            End Function

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="GetLicenseExpiryDate", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function GetLicenseExpiryDate_x64(ByRef expiryDate As UInteger) As Integer
            End Function

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="GetLicenseUserEmail", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function GetLicenseUserEmail_x64(ByVal email As StringBuilder, ByVal length As Integer) As Integer
            End Function

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="GetLicenseUserName", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function GetLicenseUserName_x64(ByVal name As StringBuilder, ByVal length As Integer) As Integer
            End Function

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="GetLicenseUserCompany", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function GetLicenseUserCompany_x64(ByVal company As StringBuilder, ByVal length As Integer) As Integer
            End Function

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="GetLicenseUserMetadata", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function GetLicenseUserMetadata_x64(ByVal key As String, ByVal value As StringBuilder, ByVal length As Integer) As Integer
            End Function

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="GetLicenseType", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function GetLicenseType_x64(ByVal licenseType As StringBuilder, ByVal length As Integer) As Integer
            End Function

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="GetActivationMetadata", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function GetActivationMetadata_x64(ByVal key As String, ByVal value As StringBuilder, ByVal length As Integer) As Integer
            End Function

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="GetActivationMeterAttributeUses", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function GetActivationMeterAttributeUses_x64(ByVal name As String, ByRef uses As UInteger) As Integer
            End Function

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="GetServerSyncGracePeriodExpiryDate", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function GetServerSyncGracePeriodExpiryDate_x64(ByRef expiryDate As UInteger) As Integer
            End Function

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="GetTrialActivationMetadata", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function GetTrialActivationMetadata_x64(ByVal key As String, ByVal value As StringBuilder, ByVal length As Integer) As Integer
            End Function

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="GetTrialExpiryDate", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function GetTrialExpiryDate_x64(ByRef trialExpiryDate As UInteger) As Integer
            End Function

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="GetTrialId", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function GetTrialId_x64(ByVal trialId As StringBuilder, ByVal length As Integer) As Integer
            End Function

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="GetLocalTrialExpiryDate", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function GetLocalTrialExpiryDate_x64(ByRef trialExpiryDate As UInteger) As Integer
            End Function

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="CheckForReleaseUpdate", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function CheckForReleaseUpdate_x64(ByVal platform As String, ByVal version As String, ByVal channel As String, callback As CallbackType) As Integer
            End Function

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="ActivateLicense", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function ActivateLicense_x64() As Integer
            End Function

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="ActivateLicenseOffline", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function ActivateLicenseOffline_x64(ByVal filePath As String) As Integer
            End Function

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="GenerateOfflineActivationRequest", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function GenerateOfflineActivationRequest_x64(ByVal filePath As String) As Integer
            End Function

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="DeactivateLicense", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function DeactivateLicense_x64() As Integer
            End Function

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="GenerateOfflineDeactivationRequest", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function GenerateOfflineDeactivationRequest_x64(ByVal filePath As String) As Integer
            End Function

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="IsLicenseGenuine", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function IsLicenseGenuine_x64() As Integer
            End Function

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="IsLicenseValid", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function IsLicenseValid_x64() As Integer
            End Function

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="ActivateTrial", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function ActivateTrial_x64() As Integer
            End Function

             <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="ActivateTrialOffline", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function ActivateTrialOffline_x64(ByVal filePath As String) As Integer
            End Function

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="GenerateOfflineTrialActivationRequest", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function GenerateOfflineTrialActivationRequest_x64(ByVal filePath As String) As Integer
            End Function

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="IsTrialGenuine", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function IsTrialGenuine_x64() As Integer
            End Function

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="ActivateLocalTrial", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function ActivateLocalTrial_x64(ByVal trialLength As UInteger) As Integer
            End Function

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="IsLocalTrialGenuine", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function IsLocalTrialGenuine_x64() As Integer
            End Function

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="ExtendLocalTrial", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function ExtendLocalTrial_x64(ByVal trialExtensionLength As UInteger) As Integer
            End Function

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="IncrementActivationMeterAttributeUses", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function IncrementActivationMeterAttributeUses_x64(ByVal name As String, increment As UInteger) As Integer
            End Function

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="DecrementActivationMeterAttributeUses", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function DecrementActivationMeterAttributeUses_x64(ByVal name As String, decrement As UInteger) As Integer
            End Function

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="ResetActivationMeterAttributeUses", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function ResetActivationMeterAttributeUses_x64(ByVal name As String) As Integer
            End Function

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="Reset", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function Reset_x64() As Integer
            End Function

#End If
        End Class
    End Class
End Namespace

