using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Collections.Generic;

namespace Cryptlex
{
    public static class LexActivator
    {
        private const string DLL_FILE_NAME = "LexActivator.dll";

        /*
            In order to use "Any CPU" configuration, rename 64 bit LexActivator.dll to LexActivator64.dll and add "LA_ANY_CPU"
	        conditional compilation symbol in your project properties.
        */
#if LA_ANY_CPU
        private const string DLL_FILE_NAME_X64 = "LexActivator64.dll";
#endif
        public enum PermissionFlags : uint
        {
            LA_USER = 1,
            LA_SYSTEM = 2,
            LA_IN_MEMORY = 4
        }

        /*
            FUNCTION: SetProductFile()

            PURPOSE: Sets the absolute path of the Product.dat file.

            This function must be called on every start of your program
            before any other functions are called.

            PARAMETERS:
            * filePath - absolute path of the product file (Product.dat)

            RETURN CODES: LA_OK, LA_E_FILE_PATH, LA_E_PRODUCT_FILE

            NOTE: If this function fails to set the path of product file, none of the
            other functions will work.
        */
        public static int SetProductFile(string filePath)
        {
#if LA_ANY_CPU
            return IntPtr.Size == 8 ? Native.SetProductFile_x64(filePath) : Native.SetProductFile(filePath);
#else
            return Native.SetProductFile(filePath);
#endif
        }

        /*
            FUNCTION: SetProductData()

            PURPOSE: Embeds the Product.dat file in the application.

            It can be used instead of SetProductFile() in case you want
            to embed the Product.dat file in your application.

            This function must be called on every start of your program
            before any other functions are called.

            PARAMETERS:
            * productData - content of the Product.dat file

            RETURN CODES: LA_OK, LA_E_PRODUCT_DATA

            NOTE: If this function fails to set the product data, none of the
            other functions will work.
        */
        public static int SetProductData(string productData)
        {
#if LA_ANY_CPU
            return IntPtr.Size == 8 ? Native.SetProductData_x64(productData) : Native.SetProductData(productData);
#else
            return Native.SetProductData(productData);
#endif
        }

        /*
            FUNCTION: SetProductId()

            PURPOSE: Sets the product id of your application.

            This function must be called on every start of your program before
            any other functions are called, with the exception of SetProductFile()
            or SetProductData() function.

            PARAMETERS:
            * productId - the unique product id of your application as mentioned
            on the product page in the dashboard.

            * flags - depending upon whether your application requires admin/root
            permissions to run or not, this parameter can have one of the following
            values: LA_SYSTEM, LA_USER, LA_IN_MEMORY

            RETURN CODES: LA_OK, LA_E_WMIC, LA_E_PRODUCT_FILE, LA_E_PRODUCT_DATA, LA_E_PRODUCT_ID,
            LA_E_SYSTEM_PERMISSION

            NOTE: If this function fails to set the product id, none of the other
            functions will work.
        */
        public static int SetProductId(string productId, PermissionFlags flags)
        {
#if LA_ANY_CPU
            return IntPtr.Size == 8 ? Native.SetProductId_x64(productId, flags) : Native.SetProductId(productId, flags);
#else
            return Native.SetProductId(productId, flags);
#endif
        }

        /*
            FUNCTION: SetLicenseKey()

            PURPOSE: Sets the license key required to activate the license.

            PARAMETERS:
            * licenseKey - a valid license key.

            RETURN CODES: LA_OK, LA_E_PRODUCT_ID, LA_E_LICENSE_KEY
        */
        public static int SetLicenseKey(string licenseKey)
        {
#if LA_ANY_CPU
            return IntPtr.Size == 8 ? Native.SetLicenseKey_x64(licenseKey) : Native.SetLicenseKey(licenseKey);
#else
            return Native.SetLicenseKey(licenseKey);
#endif
        }

        /*
            FUNCTION: SetLicenseUserCredential()

            PURPOSE: Sets the license user email and password for authentication.

            This function must be called before ActivateLicense() or IsLicenseGenuine()
            function if 'requireAuthentication' property of the license is set to true.

            PARAMETERS:
            * email - user email address.
            * password - user password.

            RETURN CODES: LA_OK, LA_E_PRODUCT_ID, LA_E_LICENSE_KEY
        */
        public static int SetLicenseUserCredential(string email, string password)
        {
#if LA_ANY_CPU
            return IntPtr.Size == 8 ? Native.SetLicenseUserCredential_x64(email, password) : Native.SetLicenseUserCredential(email, password);
#else
            return Native.SetLicenseUserCredential(email, password);
#endif
        }

        /*
            FUNCTION: SetLicenseCallback()

            PURPOSE: Sets server sync callback function.

            Whenever the server sync occurs in a separate thread, and server returns the response,
            license callback function gets invoked with the following status codes:
            LA_OK, LA_EXPIRED, LA_SUSPENDED,
            LA_E_REVOKED, LA_E_ACTIVATION_NOT_FOUND, LA_E_MACHINE_FINGERPRINT
            LA_E_AUTHENTICATION_FAILED, LA_E_COUNTRY, LA_E_INET, LA_E_SERVER,
            LA_E_RATE_LIMIT, LA_E_IP

            PARAMETERS:
            * callback - name of the callback function

            RETURN CODES: LA_OK, LA_E_PRODUCT_ID, LA_E_LICENSE_KEY
        */
        public static int SetLicenseCallback(CallbackType callback)
        {
            var wrappedCallback = callback;
            var syncTarget = callback.Target as System.Windows.Forms.Control;
            if (syncTarget != null)
            {
                wrappedCallback = (v) => syncTarget.Invoke(callback, new object[] { v });
            }
            callbackList.Add(wrappedCallback);
#if LA_ANY_CPU
            return IntPtr.Size == 8 ? Native.SetLicenseCallback_x64(wrappedCallback) : Native.SetLicenseCallback(wrappedCallback);
#else
            return Native.SetLicenseCallback(wrappedCallback);
#endif

        }

        /*
            FUNCTION: SetActivationMetadata()

            PURPOSE: Sets the activation metadata.

            The  metadata appears along with the activation details of the license
            in dashboard.

            PARAMETERS:
            * key - string of maximum length 256 characters with utf-8 encoding.
            * value - string of maximum length 256 characters with utf-8 encoding.

            RETURN CODES: LA_OK, LA_E_PRODUCT_ID, LA_E_LICENSE_KEY, LA_E_METADATA_KEY_LENGTH,
            LA_E_METADATA_VALUE_LENGTH, LA_E_ACTIVATION_METADATA_LIMIT
        */
        public static int SetActivationMetadata(string key, string value)
        {
#if LA_ANY_CPU
            return IntPtr.Size == 8 ? Native.SetActivationMetadata_x64(key, value) : Native.SetActivationMetadata(key, value);
#else
            return Native.SetActivationMetadata(key, value);
#endif
        }

        /*
            FUNCTION: SetTrialActivationMetadata()

            PURPOSE: Sets the trial activation metadata.

            The  metadata appears along with the trial activation details of the product
            in dashboard.

            PARAMETERS:
            * key - string of maximum length 256 characters with utf-8 encoding.
            * value - string of maximum length 256 characters with utf-8 encoding.

            RETURN CODES: LA_OK, LA_E_PRODUCT_ID, LA_E_METADATA_KEY_LENGTH,
            LA_E_METADATA_VALUE_LENGTH, LA_E_TRIAL_ACTIVATION_METADATA_LIMIT
        */
        public static int SetTrialActivationMetadata(string key, string value)
        {
#if LA_ANY_CPU
            return IntPtr.Size == 8 ? Native.SetTrialActivationMetadata_x64(key, value) : Native.SetTrialActivationMetadata(key, value);
#else
            return Native.SetTrialActivationMetadata(key, value);
#endif

        }

        /*
            FUNCTION: SetAppVersion()

            PURPOSE: Sets the current app version of your application.

            The app version appears along with the activation details in dashboard. It
            is also used to generate app analytics.

            PARAMETERS:
            * appVersion - string of maximum length 256 characters with utf-8 encoding.

            RETURN CODES: LA_OK, LA_E_PRODUCT_ID, LA_E_APP_VERSION_LENGTH
        */
        public static int SetAppVersion(string appVersion)
        {
#if LA_ANY_CPU 
            return IntPtr.Size == 8 ? Native.SetAppVersion_x64(appVersion) : Native.SetAppVersion(appVersion);
#else 
            return Native.SetAppVersion(appVersion);
#endif
        }

        /*
            FUNCTION: SetNetworkProxy()

            PURPOSE: Sets the network proxy to be used when contacting Cryptlex servers.

            The proxy format should be: [protocol://][username:password@]machine[:port]

            Following are some examples of the valid proxy strings:
                - http://127.0.0.1:8000/
                - http://user:pass@127.0.0.1:8000/
                - socks5://127.0.0.1:8000/

            PARAMETERS:
            * proxy - proxy string having correct proxy format

            RETURN CODES: LA_OK, LA_E_PRODUCT_ID, LA_E_NET_PROXY

            NOTE: Proxy settings of the computer are automatically detected. So, in most of the
            cases you don't need to care whether your user is behind a proxy server or not.
        */
        public static int SetNetworkProxy(string proxy)
        {
#if LA_ANY_CPU 
            return IntPtr.Size == 8 ? Native.SetNetworkProxy_x64(proxy) : Native.SetNetworkProxy(proxy);
#else 
            return Native.SetNetworkProxy(proxy);
#endif
        }

        /*
            FUNCTION: GetProductMetadata()

            PURPOSE: Gets the product metadata as set in the dashboard.

            This is available for trial as well as license activations.

            PARAMETERS:
            * key - key to retrieve the value
            * value - pointer to a buffer that receives the value of the string
            * length - size of the buffer pointed to by the value parameter

            RETURN CODES: LA_OK, LA_E_PRODUCT_ID, LA_E_METADATA_KEY_NOT_FOUND, LA_E_BUFFER_SIZE
        */
        public static int GetProductMetadata(string key, StringBuilder value, int length)
        {
#if LA_ANY_CPU
            return IntPtr.Size == 8 ? Native.GetProductMetadata_x64(key, value, length) : Native.GetProductMetadata(key, value, length);
#else 
            return Native.GetProductMetadata(key, value, length);
#endif
        }

        /*
            FUNCTION: GetLicenseMetadata()

            PURPOSE: Gets the license metadata as set in the dashboard.

            PARAMETERS:
            * key - key to retrieve the value
            * value - pointer to a buffer that receives the value of the string
            * length - size of the buffer pointed to by the value parameter

            RETURN CODES: LA_OK, LA_E_PRODUCT_ID, LA_E_METADATA_KEY_NOT_FOUND, LA_E_BUFFER_SIZE
        */
        public static int GetLicenseMetadata(string key, StringBuilder value, int length)
        {
#if LA_ANY_CPU
            return IntPtr.Size == 8 ? Native.GetLicenseMetadata_x64(key, value, length) : Native.GetLicenseMetadata(key, value, length);
#else 
            return Native.GetLicenseMetadata(key, value, length);
#endif
        }

        /*
            FUNCTION: GetLicenseMeterAttribute()

            PURPOSE: Gets the license meter attribute allowed uses and total uses.

            PARAMETERS:
            * name - name of the meter attribute
            * allowedUses - pointer to the integer that receives the value
            * totalUses - pointer to the integer that receives the value

            RETURN CODES: LA_OK, LA_FAIL, LA_E_PRODUCT_ID, LA_E_METER_ATTRIBUTE_NOT_FOUND
        */
        public static int GetLicenseMeterAttribute(string name, ref uint allowedUses, ref uint totalUses)
        {
#if LA_ANY_CPU
            return IntPtr.Size == 8 ? Native.GetLicenseMeterAttribute_x64(name, ref allowedUses, ref totalUses) : Native.GetLicenseMeterAttribute(name, ref allowedUses, ref totalUses);
#else 
            return Native.GetLicenseMeterAttribute(name, ref allowedUses, ref totalUses);
#endif
        }

        /*
            FUNCTION: GetLicenseKey()

            PURPOSE: Gets the license key used for activation.

            PARAMETERS:
            * licenseKey - pointer to a buffer that receives the value of the string
            * length - size of the buffer pointed to by the licenseKey parameter

            RETURN CODES: LA_OK, LA_FAIL, LA_E_PRODUCT_ID, LA_E_BUFFER_SIZE
        */
        public static int GetLicenseKey(StringBuilder licenseKey, int length)
        {
#if LA_ANY_CPU
            return IntPtr.Size == 8 ? Native.GetLicenseKey_x64(licenseKey, length) : Native.GetLicenseKey(licenseKey, length);
#else
            return Native.GetLicenseKey(licenseKey, length);
#endif
        }

        /*
            FUNCTION: GetLicenseExpiryDate()

            PURPOSE: Gets the license expiry date timestamp.

            PARAMETERS:
            * expiryDate - pointer to the integer that receives the value

            RETURN CODES: LA_OK, LA_FAIL, LA_E_PRODUCT_ID, LA_E_TIME, LA_E_TIME_MODIFIED
        */
        public static int GetLicenseExpiryDate(ref uint expiryDate)
        {
#if LA_ANY_CPU 
            return IntPtr.Size == 8 ? Native.GetLicenseExpiryDate_x64(ref expiryDate) : Native.GetLicenseExpiryDate(ref expiryDate);
#else 
            return Native.GetLicenseExpiryDate(ref expiryDate);
#endif
        }

        /*
            FUNCTION: GetLicenseUserEmail()

            PURPOSE: Gets the email associated with the license user.

            PARAMETERS:
            * email - pointer to a buffer that receives the value of the string
            * length - size of the buffer pointed to by the email parameter

            RETURN CODES: LA_OK, LA_FAIL, LA_E_PRODUCT_ID, LA_E_TIME, LA_E_TIME_MODIFIED,
            LA_E_BUFFER_SIZE
        */
        public static int GetLicenseUserEmail(StringBuilder email, int length)
        {
#if LA_ANY_CPU
            return IntPtr.Size == 8 ? Native.GetLicenseUserEmail_x64(email, length) : Native.GetLicenseUserEmail(email, length);
#else
            return Native.GetLicenseUserEmail(email, length);
#endif
        }

        /*
            FUNCTION: GetLicenseUserName()

            PURPOSE: Gets the name associated with the license user.

            PARAMETERS:
            * name - pointer to a buffer that receives the value of the string
            * length - size of the buffer pointed to by the name parameter

            RETURN CODES: LA_OK, LA_FAIL, LA_E_PRODUCT_ID, LA_E_TIME, LA_E_TIME_MODIFIED,
            LA_E_BUFFER_SIZE
        */
        public static int GetLicenseUserName(StringBuilder name, int length)
        {
#if LA_ANY_CPU
            return IntPtr.Size == 8 ? Native.GetLicenseUserName_x64(name, length) : Native.GetLicenseUserName(name, length);
#else
            return Native.GetLicenseUserName(name, length);
#endif
        }

        /*
            FUNCTION: GetLicenseUserCompany()

            PURPOSE: Gets the company associated with the license user.

            PARAMETERS:
            * company - pointer to a buffer that receives the value of the string
            * length - size of the buffer pointed to by the company parameter

            RETURN CODES: LA_OK, LA_FAIL, LA_E_PRODUCT_ID, LA_E_TIME, LA_E_TIME_MODIFIED,
            LA_E_BUFFER_SIZE
        */
        public static int GetLicenseUserCompany(StringBuilder company, int length)
        {
#if LA_ANY_CPU
            return IntPtr.Size == 8 ? Native.GetLicenseUserCompany_x64(company, length) : Native.GetLicenseUserCompany(company, length);
#else
            return Native.GetLicenseUserCompany(company, length);
#endif
        }

        /*
            FUNCTION: GetLicenseUserMetadata()

            PURPOSE: Gets the metadata associated with the license user.

            PARAMETERS:
            * key - key to retrieve the value
            * value - pointer to a buffer that receives the value of the string
            * length - size of the buffer pointed to by the value parameter

            RETURN CODES: LA_OK, LA_FAIL, LA_E_PRODUCT_ID, LA_E_METADATA_KEY_NOT_FOUND, LA_E_BUFFER_SIZE
        */
        public static int GetLicenseUserMetadata(string key, StringBuilder value, int length)
        {
#if LA_ANY_CPU
            return IntPtr.Size == 8 ? Native.GetLicenseUserMetadata_x64(key, value, length) : Native.GetLicenseUserMetadata(key, value, length);
#else 
            return Native.GetLicenseUserMetadata(key, value, length);
#endif
        }

        /*
            FUNCTION: GetLicenseType()

            PURPOSE: Gets the license type (node-locked or hosted-floating).

            PARAMETERS:
            * name - pointer to a buffer that receives the value of the string
            * length - size of the buffer pointed to by the licenseType parameter

            RETURN CODES: LA_OK, LA_FAIL, LA_E_PRODUCT_ID, LA_E_TIME, LA_E_TIME_MODIFIED,
            LA_E_BUFFER_SIZE
        */
        public static int GetLicenseType(StringBuilder licenseType, int length)
        {
#if LA_ANY_CPU
            return IntPtr.Size == 8 ? Native.GetLicenseType_x64(licenseType, length) : Native.GetLicenseType(licenseType, length);
#else
            return Native.GetLicenseType(licenseType, length);
#endif
        }

        /*
            FUNCTION: GetActivationMetadata()

            PURPOSE: Gets the activation metadata.

            PARAMETERS:
            * key - key to retrieve the value
            * value - pointer to a buffer that receives the value of the string
            * length - size of the buffer pointed to by the value parameter

            RETURN CODES: LA_OK, LA_FAIL, LA_E_PRODUCT_ID, LA_E_METADATA_KEY_NOT_FOUND, LA_E_BUFFER_SIZE
        */
        public static int GetActivationMetadata(string key, StringBuilder value, int length)
        {
#if LA_ANY_CPU
            return IntPtr.Size == 8 ? Native.GetActivationMetadata_x64(key, value, length) : Native.GetActivationMetadata(key, value, length);
#else 
            return Native.GetActivationMetadata(key, value, length);
#endif
        }

        /*
            FUNCTION: GetActivationMeterAttributeUses()

            PURPOSE: Gets the meter attribute uses consumed by the activation.

            PARAMETERS:
            * name - name of the meter attribute
            * uses - pointer to the integer that receives the value

            RETURN CODES: LA_OK, LA_FAIL, LA_E_PRODUCT_ID, LA_E_METER_ATTRIBUTE_NOT_FOUND
        */
        public static int GetActivationMeterAttributeUses(string name, ref uint uses)
        {
#if LA_ANY_CPU
            return IntPtr.Size == 8 ? Native.GetActivationMeterAttributeUses_x64(name, ref uses) : Native.GetActivationMeterAttributeUses(name, ref uses);
#else 
            return Native.GetActivationMeterAttributeUses(name, ref uses);
#endif
        }

        /*
            FUNCTION: GetServerSyncGracePeriodExpiryDate()

            PURPOSE: Gets the server sync grace period expiry date timestamp.

            PARAMETERS:
            * expiryDate - pointer to the integer that receives the value

            RETURN CODES: LA_OK, LA_FAIL, LA_E_PRODUCT_ID, LA_E_TIME, LA_E_TIME_MODIFIED
        */
        public static int GetServerSyncGracePeriodExpiryDate(ref uint expiryDate)
        {
#if LA_ANY_CPU 
            return IntPtr.Size == 8 ? Native.GetServerSyncGracePeriodExpiryDate_x64(ref expiryDate) : Native.GetServerSyncGracePeriodExpiryDate(ref expiryDate);
#else 
            return Native.GetServerSyncGracePeriodExpiryDate(ref expiryDate);
#endif
        }

        /*
            FUNCTION: GetTrialActivationMetadata()

            PURPOSE: Gets the trial activation metadata.

            PARAMETERS:
            * key - key to retrieve the value
            * value - pointer to a buffer that receives the value of the string
            * length - size of the buffer pointed to by the value parameter

            RETURN CODES: LA_OK, LA_FAIL, LA_E_PRODUCT_ID, LA_E_METADATA_KEY_NOT_FOUND, LA_E_BUFFER_SIZE
        */
        public static int GetTrialActivationMetadata(string key, StringBuilder value, int length)
        {
#if LA_ANY_CPU
            return IntPtr.Size == 8 ? Native.GetTrialActivationMetadata_x64(key, value, length) : Native.GetTrialActivationMetadata(key, value, length);
#else 
            return Native.GetTrialActivationMetadata(key, value, length);
#endif
        }

        /*
            FUNCTION: GetTrialExpiryDate()

            PURPOSE: Gets the trial expiry date timestamp.

            PARAMETERS:
            * trialExpiryDate - pointer to the integer that receives the value

            RETURN CODES: LA_OK, LA_FAIL, LA_E_PRODUCT_ID, LA_E_TIME, LA_E_TIME_MODIFIED
        */
        public static int GetTrialExpiryDate(ref uint trialExpiryDate)
        {
#if LA_ANY_CPU 
            return IntPtr.Size == 8 ? Native.GetTrialExpiryDate_x64(ref trialExpiryDate) : Native.GetTrialExpiryDate(ref trialExpiryDate);
#else 
            return Native.GetTrialExpiryDate(ref trialExpiryDate);
#endif
        }

        /*
            FUNCTION: GetTrialId()

            PURPOSE: Gets the trial activation id. Used in case of trial extension.

            PARAMETERS:
            * trialId - pointer to a buffer that receives the value of the string
            * length - size of the buffer pointed to by the value parameter

            RETURN CODES: LA_OK, LA_FAIL, LA_E_PRODUCT_ID, LA_E_TIME, LA_E_TIME_MODIFIED,
            LA_E_BUFFER_SIZE
        */
        public static int GetTrialId(StringBuilder trialId, int length)
        {
#if LA_ANY_CPU
            return IntPtr.Size == 8 ? Native.GetTrialId_x64(trialId, length) : Native.GetTrialId(trialId, length);
#else
            return Native.GetTrialId(trialId, length);
#endif
        }

        /*
            FUNCTION: GetLocalTrialExpiryDate()

            PURPOSE: Gets the trial expiry date timestamp.

            PARAMETERS:
            * trialExpiryDate - pointer to the integer that receives the value

            RETURN CODES: LA_OK, LA_FAIL, LA_E_PRODUCT_ID, LA_E_TIME_MODIFIED
        */
        public static int GetLocalTrialExpiryDate(ref uint trialExpiryDate)
        {
#if LA_ANY_CPU 
            return IntPtr.Size == 8 ? Native.GetLocalTrialExpiryDate_x64(ref trialExpiryDate) : Native.GetLocalTrialExpiryDate(ref trialExpiryDate);
#else 
            return Native.GetLocalTrialExpiryDate(ref trialExpiryDate);
#endif
        }

        /*
            FUNCTION: CheckForReleaseUpdate()

            PURPOSE: Checks whether a new release is available for the product.

            This function should only be used if you manage your releases through
            Cryptlex release management API.

            PARAMETERS:
            * platform - release platform e.g. windows, macos, linux
            * version - current release version
            * channel - release channel e.g. stable
            * callback - name of the callback function.

            RETURN CODES: LA_OK, LA_E_PRODUCT_ID, LA_E_LICENSE_KEY, LA_E_RELEASE_VERSION_FORMAT
        */
        public static int CheckForReleaseUpdate(string platform, string version, string channel, CallbackType callback)
        {
            var wrappedCallback = callback;
            var syncTarget = callback.Target as System.Windows.Forms.Control;
            if (syncTarget != null)
            {
                wrappedCallback = (v) => syncTarget.Invoke(callback, new object[] { v });
            }
            callbackList.Add(wrappedCallback);
#if LA_ANY_CPU
            return IntPtr.Size == 8 ? Native.CheckForReleaseUpdate_x64(platform, version, channel, wrappedCallback) : Native.CheckForReleaseUpdate(platform, version, channel, wrappedCallback);
#else 
            return Native.CheckForReleaseUpdate(platform, version, channel, wrappedCallback);
#endif
        }

        /*
            FUNCTION: ActivateLicense()

            PURPOSE: Activates the license by contacting the Cryptlex servers. It
            validates the key and returns with encrypted and digitally signed token
            which it stores and uses to activate your application.

            This function should be executed at the time of registration, ideally on
            a button click.

            RETURN CODES: LA_OK, LA_EXPIRED, LA_SUSPENDED, LA_E_REVOKED, LA_FAIL, LA_E_PRODUCT_ID,
            LA_E_INET, LA_E_VM, LA_E_TIME, LA_E_ACTIVATION_LIMIT, LA_E_SERVER, LA_E_CLIENT,
            LA_E_AUTHENTICATION_FAILED, LA_E_LICENSE_TYPE, LA_E_COUNTRY, LA_E_IP, LA_E_RATE_LIMIT, LA_E_LICENSE_KEY
        */
        public static int ActivateLicense()
        {
#if LA_ANY_CPU
            return IntPtr.Size == 8 ? Native.ActivateLicense_x64() : Native.ActivateLicense();
#else
            return Native.ActivateLicense();
#endif

        }

        /*
            FUNCTION: ActivateLicenseOffline()

            PURPOSE: Activates your licenses using the offline activation response file.

            PARAMETERS:
            * filePath - path of the offline activation response file.

            RETURN CODES: LA_OK, LA_EXPIRED, LA_FAIL, LA_E_PRODUCT_ID, LA_E_LICENSE_KEY, LA_E_OFFLINE_RESPONSE_FILE
            LA_E_VM, LA_E_TIME, LA_E_FILE_PATH, LA_E_OFFLINE_RESPONSE_FILE_EXPIRED
        */
        public static int ActivateLicenseOffline(string filePath)
        {
#if LA_ANY_CPU
            return IntPtr.Size == 8 ? Native.ActivateLicenseOffline_x64(filePath) : Native.ActivateLicenseOffline(filePath);
#else 
            return Native.ActivateLicenseOffline(filePath);
#endif
        }

        /*
            FUNCTION: GenerateOfflineActivationRequest()

            PURPOSE: Generates the offline activation request needed for generating
            offline activation response in the dashboard.

            PARAMETERS:
            * filePath - path of the file for the offline request.

            RETURN CODES: LA_OK, LA_FAIL, LA_E_PRODUCT_ID, LA_E_LICENSE_KEY, LA_E_FILE_PERMISSION
        */
        public static int GenerateOfflineActivationRequest(string filePath)
        {
#if LA_ANY_CPU
            return IntPtr.Size == 8 ? Native.GenerateOfflineActivationRequest_x64(filePath) : Native.GenerateOfflineActivationRequest(filePath);
#else 
            return Native.GenerateOfflineActivationRequest(filePath);
#endif
        }

        /*
            FUNCTION: DeactivateLicense()

            PURPOSE: Deactivates the license activation and frees up the corresponding activation
            slot by contacting the Cryptlex servers.

            This function should be executed at the time of de-registration, ideally on
            a button click.

            RETURN CODES: LA_OK, LA_E_DEACTIVATION_LIMIT, LA_FAIL, LA_E_PRODUCT_ID, LA_E_TIME
            LA_E_LICENSE_KEY, LA_E_INET, LA_E_SERVER, LA_E_RATE_LIMIT, LA_E_TIME_MODIFIED
        */
        public static int DeactivateLicense()
        {
#if LA_ANY_CPU
            return IntPtr.Size == 8 ? Native.DeactivateLicense_x64() : Native.DeactivateLicense();
#else
            return Native.DeactivateLicense();
#endif
        }

        /*
            FUNCTION: GenerateOfflineDeactivationRequest()

            PURPOSE: Generates the offline deactivation request needed for deactivation of
            the license in the dashboard and deactivates the license locally.

            A valid offline deactivation file confirms that the license has been successfully
            deactivated on the user's machine.

            PARAMETERS:
            * filePath - path of the file for the offline request.

            RETURN CODES: LA_OK, LA_FAIL, LA_E_PRODUCT_ID, LA_E_LICENSE_KEY, LA_E_FILE_PERMISSION,
            LA_E_TIME, LA_E_TIME_MODIFIED
        */
        public static int GenerateOfflineDeactivationRequest(string filePath)
        {
#if LA_ANY_CPU
            return IntPtr.Size == 8 ? Native.GenerateOfflineDeactivationRequest_x64(filePath) : Native.GenerateOfflineDeactivationRequest(filePath);
#else 
            return Native.GenerateOfflineDeactivationRequest(filePath);
#endif
        }

        /*
            FUNCTION: IsLicenseGenuine()

            PURPOSE: It verifies whether your app is genuinely activated or not. The verification is
            done locally by verifying the cryptographic digital signature fetched at the time of
            activation.

            After verifying locally, it schedules a server check in a separate thread. After the
            first server sync it periodically does further syncs at a frequency set for the license.

            In case server sync fails due to network error, and it continues to fail for fixed
            number of days (grace period), the function returns LA_GRACE_PERIOD_OVER instead of LA_OK.

            This function must be called on every start of your program to verify the activation
            of your app.

            RETURN CODES: LA_OK, LA_EXPIRED, LA_SUSPENDED, LA_GRACE_PERIOD_OVER, LA_FAIL,
            LA_E_PRODUCT_ID, LA_E_LICENSE_KEY, LA_E_TIME, LA_E_TIME_MODIFIED

            NOTE: If application was activated offline using ActivateLicenseOffline() function, you
            may want to set grace period to 0 to ignore grace period.
        */
        public static int IsLicenseGenuine()
        {
#if LA_ANY_CPU
            return IntPtr.Size == 8 ? Native.IsLicenseGenuine_x64() : Native.IsLicenseGenuine();
#else 
            return Native.IsLicenseGenuine();
#endif
        }

        /*
            FUNCTION: IsLicenseValid()

            PURPOSE: It verifies whether your app is genuinely activated or not. The verification is
            done locally by verifying the cryptographic digital signature fetched at the time of
            activation.

            This is just an auxiliary function which you may use in some specific cases, when you
            want to skip the server sync.

            RETURN CODES: LA_OK, LA_EXPIRED, LA_SUSPENDED, LA_GRACE_PERIOD_OVER, LA_FAIL,
            LA_E_PRODUCT_ID, LA_E_LICENSE_KEY, LA_E_TIME, LA_E_TIME_MODIFIED

            NOTE: You may want to set grace period to 0 to ignore grace period.
        */
        public static int IsLicenseValid()
        {
#if LA_ANY_CPU
            return IntPtr.Size == 8 ? Native.IsLicenseValid_x64() : Native.IsLicenseValid();
#else 
            return Native.IsLicenseValid();
#endif
        }

        /*
             FUNCTION: ActivateTrial()

             PURPOSE: Starts the verified trial in your application by contacting the
             Cryptlex servers.

             This function should be executed when your application starts first time on
             the user's computer, ideally on a button click.

             RETURN CODES: LA_OK, LA_TRIAL_EXPIRED, LA_FAIL, LA_E_PRODUCT_ID, LA_E_INET,
             LA_E_VM, LA_E_TIME, LA_E_SERVER, LA_E_CLIENT, LA_E_COUNTRY, LA_E_IP, LA_E_RATE_LIMIT
         */
        public static int ActivateTrial()
        {
#if LA_ANY_CPU 
            return IntPtr.Size == 8 ? Native.ActivateTrial_x64() : Native.ActivateTrial();
#else 
            return Native.ActivateTrial();
#endif
        }

        /*
            FUNCTION: ActivateTrialOffline()

            PURPOSE: Activates your trial using the offline activation response file.

            PARAMETERS:
            * filePath - path of the offline activation response file.

            RETURN CODES: LA_OK, LA_TRIAL_EXPIRED, LA_FAIL, LA_E_PRODUCT_ID, LA_E_OFFLINE_RESPONSE_FILE
            LA_E_VM, LA_E_TIME, LA_E_FILE_PATH, LA_E_OFFLINE_RESPONSE_FILE_EXPIRED
        */
        public static int ActivateTrialOffline(string filePath)
        {
#if LA_ANY_CPU
            return IntPtr.Size == 8 ? Native.ActivateTrialOffline_x64(filePath) : Native.ActivateTrialOffline(filePath);
#else 
            return Native.ActivateTrialOffline(filePath);
#endif
        }

        /*
            FUNCTION: GenerateOfflineTrialActivationRequest()

            PURPOSE: Generates the offline trial activation request needed for generating
            offline trial activation response in the dashboard.

            PARAMETERS:
            * filePath - path of the file for the offline request.

            RETURN CODES: LA_OK, LA_FAIL, LA_E_PRODUCT_ID, LA_E_FILE_PERMISSION
        */
        public static int GenerateOfflineTrialActivationRequest(string filePath)
        {
#if LA_ANY_CPU
            return IntPtr.Size == 8 ? Native.GenerateOfflineTrialActivationRequest_x64(filePath) : Native.GenerateOfflineTrialActivationRequest(filePath);
#else 
            return Native.GenerateOfflineTrialActivationRequest(filePath);
#endif
        }

        /*
            FUNCTION: IsTrialGenuine()

            PURPOSE: It verifies whether trial has started and is genuine or not. The
            verification is done locally by verifying the cryptographic digital signature
            fetched at the time of trial activation.

            This function must be called on every start of your program during the trial period.

            RETURN CODES: LA_OK, LA_TRIAL_EXPIRED, LA_FAIL, LA_E_TIME, LA_E_PRODUCT_ID,
            LA_E_TIME_MODIFIED
        */
        public static int IsTrialGenuine()
        {
#if LA_ANY_CPU 
            return IntPtr.Size == 8 ? Native.IsTrialGenuine_x64() : Native.IsTrialGenuine();
#else 
            return Native.IsTrialGenuine();
#endif
        }

        /*
            FUNCTION: ActivateLocalTrial()

            PURPOSE: Starts the local(unverified) trial.

            This function should be executed when your application starts first time on
            the user's computer.

            PARAMETERS:
            * trialLength - trial length in days

            RETURN CODES: LA_OK, LA_LOCAL_TRIAL_EXPIRED, LA_FAIL, LA_E_PRODUCT_ID, LA_E_TIME_MODIFIED

            NOTE: The function is only meant for local(unverified) trials.
        */
        public static int ActivateLocalTrial(uint trialLength)
        {
#if LA_ANY_CPU 
            return IntPtr.Size == 8 ? Native.ActivateLocalTrial_x64(trialLength) : Native.ActivateLocalTrial(trialLength);
#else 
            return Native.ActivateLocalTrial(trialLength);
#endif
        }

        /*
            FUNCTION: IsLocalTrialGenuine()

            PURPOSE: It verifies whether trial has started and is genuine or not. The
            verification is done locally.

            This function must be called on every start of your program during the trial period.

            RETURN CODES: LA_OK, LA_LOCAL_TRIAL_EXPIRED, LA_FAIL, LA_E_PRODUCT_ID, LA_E_TIME_MODIFIED

            NOTE: The function is only meant for local(unverified) trials.
        */
        public static int IsLocalTrialGenuine()
        {
#if LA_ANY_CPU 
            return IntPtr.Size == 8 ? Native.IsLocalTrialGenuine_x64() : Native.IsLocalTrialGenuine();
#else 
            return Native.IsLocalTrialGenuine();
#endif
        }

        /*
            FUNCTION: ExtendLocalTrial()

            PURPOSE: Extends the local trial.

            PARAMETERS:
            * trialExtensionLength - number of days to extend the trial

            RETURN CODES: LA_OK, LA_FAIL, LA_E_PRODUCT_ID, LA_E_TIME_MODIFIED

            NOTE: The function is only meant for local(unverified) trials.
        */
        public static int ExtendLocalTrial(uint trialExtensionLength)
        {
#if LA_ANY_CPU 
            return IntPtr.Size == 8 ? Native.ExtendLocalTrial_x64(trialExtensionLength) : Native.ExtendLocalTrial(trialExtensionLength);
#else 
            return Native.ExtendLocalTrial(trialExtensionLength);
#endif
        }

        /*
            FUNCTION: IncrementActivationMeterAttributeUses()

            PURPOSE: Increments the meter attribute uses of the activation.

            PARAMETERS:
            * name - name of the meter attribute
            * increment - the increment value

            RETURN CODES: LA_OK, LA_FAIL, LA_E_PRODUCT_ID, LA_E_METER_ATTRIBUTE_NOT_FOUND,
            LA_E_INET, LA_E_TIME, LA_E_SERVER, LA_E_CLIENT, LA_E_METER_ATTRIBUTE_USES_LIMIT_REACHED,
            LA_E_AUTHENTICATION_FAILED, LA_E_COUNTRY, LA_E_IP, LA_E_RATE_LIMIT, LA_E_LICENSE_KEY

        */
        public static int IncrementActivationMeterAttributeUses(string name, uint increment)
        {
#if LA_ANY_CPU 
            return IntPtr.Size == 8 ? Native.IncrementActivationMeterAttributeUses_x64(name, increment) : Native.IncrementActivationMeterAttributeUses(name, increment);
#else 
            return Native.IncrementActivationMeterAttributeUses(name, increment);
#endif
        }

        /*
            FUNCTION: DecrementActivationMeterAttributeUses()

            PURPOSE: Decrements the meter attribute uses of the activation.

            PARAMETERS:
            * name - name of the meter attribute
            * decrement - the decrement value

            RETURN CODES: LA_OK, LA_FAIL, LA_E_PRODUCT_ID, LA_E_METER_ATTRIBUTE_NOT_FOUND,
            LA_E_INET, LA_E_TIME, LA_E_SERVER, LA_E_CLIENT, LA_E_RATE_LIMIT, LA_E_LICENSE_KEY,
            LA_E_AUTHENTICATION_FAILED, LA_E_COUNTRY, LA_E_IP, LA_E_ACTIVATION_NOT_FOUND

            NOTE: If the decrement is more than the current uses, it resets the uses to 0.
        */
        public static int DecrementActivationMeterAttributeUses(string name, uint decrement)
        {
#if LA_ANY_CPU 
            return IntPtr.Size == 8 ? Native.DecrementActivationMeterAttributeUses_x64(name, decrement) : Native.DecrementActivationMeterAttributeUses(name, decrement);
#else 
            return Native.DecrementActivationMeterAttributeUses(name, decrement);
#endif
        }

        /*
            FUNCTION: ResetActivationMeterAttributeUses()

            PURPOSE: Resets the meter attribute uses consumed by the activation.

            PARAMETERS:
            * name - name of the meter attribute

            RETURN CODES: LA_OK, LA_FAIL, LA_E_PRODUCT_ID, LA_E_METER_ATTRIBUTE_NOT_FOUND,
            LA_E_INET, LA_E_TIME, LA_E_SERVER, LA_E_CLIENT, LA_E_RATE_LIMIT, LA_E_LICENSE_KEY,
            LA_E_AUTHENTICATION_FAILED, LA_E_COUNTRY, LA_E_IP, LA_E_ACTIVATION_NOT_FOUND
        */
        public static int ResetActivationMeterAttributeUses(string name)
        {
#if LA_ANY_CPU 
            return IntPtr.Size == 8 ? Native.ResetActivationMeterAttributeUses_x64(name) : Native.ResetActivationMeterAttributeUses(name);
#else 
            return Native.ResetActivationMeterAttributeUses(name);
#endif
        }

        /*
            FUNCTION: Reset()

            PURPOSE: Resets the activation and trial data stored in the machine.

            This function is meant for developer testing only.

            RETURN CODES: LA_OK, LA_E_PRODUCT_ID

            NOTE: The function does not reset local(unverified) trial data.
        */
        public static int Reset()
        {
#if LA_ANY_CPU 
            return IntPtr.Size == 8 ? Native.Reset_x64() : Native.Reset();
#else 
            return Native.Reset();
#endif
        }

        public static class StatusCodes
        {
            /*
                CODE: LA_OK

                MESSAGE: Success code.
            */
            public const int LA_OK = 0;

            /*
                CODE: LA_FAIL

                MESSAGE: Failure code.
            */
            public const int LA_FAIL = 1;

            /*
                CODE: LA_EXPIRED

                MESSAGE: The license has expired or system time has been tampered
                with. Ensure your date and time settings are correct.
            */
            public const int LA_EXPIRED = 20;

            /*
                CODE: LA_SUSPENDED

                MESSAGE: The license has been suspended.
            */
            public const int LA_SUSPENDED = 21;

            /*
                CODE: LA_GRACE_PERIOD_OVER

                MESSAGE: The grace period for server sync is over.
            */
            public const int LA_GRACE_PERIOD_OVER = 22;

            /*
                CODE: LA_TRIAL_EXPIRED

                MESSAGE: The trial has expired or system time has been tampered
                with. Ensure your date and time settings are correct.
            */
            public const int LA_TRIAL_EXPIRED = 25;

            /*
                CODE: LA_LOCAL_TRIAL_EXPIRED

                MESSAGE: The local trial has expired or system time has been tampered
                with. Ensure your date and time settings are correct.
            */
            public const int LA_LOCAL_TRIAL_EXPIRED = 26;

            /*
                CODE: LA_RELEASE_UPDATE_AVAILABLE

                MESSAGE: A new update is available for the product. This means a new release has
                been published for the product.
            */
            public const int LA_RELEASE_UPDATE_AVAILABLE = 30;

            /*
                CODE: LA_RELEASE_NO_UPDATE_AVAILABLE

                MESSAGE: No new update is available for the product. The current version is latest.
            */
            public const int LA_RELEASE_NO_UPDATE_AVAILABLE = 31;

            /*
                CODE: LA_E_FILE_PATH

                MESSAGE: Invalid file path.
            */
            public const int LA_E_FILE_PATH = 40;

            /*
                CODE: LA_E_PRODUCT_FILE

                MESSAGE: Invalid or corrupted product file.
            */
            public const int LA_E_PRODUCT_FILE = 41;

            /*
                CODE: LA_E_PRODUCT_DATA

                MESSAGE: Invalid product data.
            */
            public const int LA_E_PRODUCT_DATA = 42;

            /*
                CODE: LA_E_PRODUCT_ID

                MESSAGE: The product id is incorrect.
            */
            public const int LA_E_PRODUCT_ID = 43;

            /*
                CODE: LA_E_SYSTEM_PERMISSION

                MESSAGE: Insufficent system permissions. Occurs when LA_SYSTEM flag is used
                but application is not run with admin privileges.
            */
            public const int LA_E_SYSTEM_PERMISSION = 44;

            /*
                CODE: LA_E_FILE_PERMISSION

                MESSAGE: No permission to write to file.
            */
            public const int LA_E_FILE_PERMISSION = 45;

            /*
                CODE: LA_E_WMIC

                MESSAGE: Fingerprint couldn't be generated because Windows Management
                Instrumentation (WMI) service has been disabled. This error is specific
                to Windows only.
            */
            public const int LA_E_WMIC = 46;

            /*
                CODE: LA_E_TIME

                MESSAGE: The difference between the network time and the system time is
                more than allowed clock offset.
            */
            public const int LA_E_TIME = 47;

            /*
                CODE: LA_E_INET

                MESSAGE: Failed to connect to the server due to network error.
            */
            public const int LA_E_INET = 48;

            /*
                CODE: LA_E_NET_PROXY

                MESSAGE: Invalid network proxy.
            */
            public const int LA_E_NET_PROXY = 49;

            /*
                CODE: LA_E_HOST_URL

                MESSAGE: Invalid Cryptlex host url.
            */
            public const int LA_E_HOST_URL = 50;

            /*
                CODE: LA_E_BUFFER_SIZE

                MESSAGE: The buffer size was smaller than required.
            */
            public const int LA_E_BUFFER_SIZE = 51;

            /*
                CODE: LA_E_APP_VERSION_LENGTH

                MESSAGE: App version length is more than 256 characters.
            */
            public const int LA_E_APP_VERSION_LENGTH = 52;

            /*
                CODE: LA_E_REVOKED

                MESSAGE: The license has been revoked.
            */
            public const int LA_E_REVOKED = 53;

            /*
                CODE: LA_E_LICENSE_KEY

                MESSAGE: Invalid license key.
            */
            public const int LA_E_LICENSE_KEY = 54;

            /*
                CODE: LA_E_LICENSE_TYPE

                MESSAGE: Invalid license type. Make sure floating license
                is not being used.
            */
            public const int LA_E_LICENSE_TYPE = 55;

            /*
                CODE: LA_E_OFFLINE_RESPONSE_FILE

                MESSAGE: Invalid offline activation response file.
            */
            public const int LA_E_OFFLINE_RESPONSE_FILE = 56;

            /*
                CODE: LA_E_OFFLINE_RESPONSE_FILE_EXPIRED

                MESSAGE: The offline activation response has expired.
            */
            public const int LA_E_OFFLINE_RESPONSE_FILE_EXPIRED = 57;

            /*
                CODE: LA_E_ACTIVATION_LIMIT

                MESSAGE: The license has reached it's allowed activations limit.
            */
            public const int LA_E_ACTIVATION_LIMIT = 58;

            /*
                CODE: LA_E_ACTIVATION_NOT_FOUND

                MESSAGE: The license activation was deleted on the server.
            */
            public const int LA_E_ACTIVATION_NOT_FOUND = 59;

            /*
                CODE: LA_E_DEACTIVATION_LIMIT

                MESSAGE: The license has reached it's allowed deactivations limit.
            */
            public const int LA_E_DEACTIVATION_LIMIT = 60;

            /*
                CODE: LA_E_TRIAL_NOT_ALLOWED

                MESSAGE: Trial not allowed for the product.
            */
            public const int LA_E_TRIAL_NOT_ALLOWED = 61;

            /*
                CODE: LA_E_TRIAL_ACTIVATION_LIMIT

                MESSAGE: Your account has reached it's trial activations limit.
            */
            public const int LA_E_TRIAL_ACTIVATION_LIMIT = 62;

            /*
                CODE: LA_E_MACHINE_FINGERPRINT

                MESSAGE: Machine fingerprint has changed since activation.
            */
            public const int LA_E_MACHINE_FINGERPRINT = 63;

            /*
                CODE: LA_E_METADATA_KEY_LENGTH

                MESSAGE: Metadata key length is more than 256 characters.
            */
            public const int LA_E_METADATA_KEY_LENGTH = 64;

            /*
                CODE: LA_E_METADATA_VALUE_LENGTH

                MESSAGE: Metadata value length is more than 256 characters.
            */
            public const int LA_E_METADATA_VALUE_LENGTH = 65;

            /*
                CODE: LA_E_ACTIVATION_METADATA_LIMIT

                MESSAGE: The license has reached it's metadata fields limit.
            */
            public const int LA_E_ACTIVATION_METADATA_LIMIT = 66;

            /*
                CODE: LA_E_TRIAL_ACTIVATION_METADATA_LIMIT

                MESSAGE: The trial has reached it's metadata fields limit.
            */
            public const int LA_E_TRIAL_ACTIVATION_METADATA_LIMIT = 67;

            /*
                CODE: LA_E_METADATA_KEY_NOT_FOUND

                MESSAGE: The metadata key does not exist.
            */
            public const int LA_E_METADATA_KEY_NOT_FOUND = 68;

            /*
                CODE: LA_E_TIME_MODIFIED

                MESSAGE: The system time has been tampered (backdated).
            */
            public const int LA_E_TIME_MODIFIED = 69;

            /*
                CODE: LA_E_RELEASE_VERSION_FORMAT

                MESSAGE: Invalid version format.
            */
            public const int LA_E_RELEASE_VERSION_FORMAT = 70;

            /*
                CODE: LA_E_AUTHENTICATION_FAILED

                MESSAGE: Incorrect email or password.
            */
            public const int LA_E_AUTHENTICATION_FAILED = 71;

            /*
                CODE: LA_E_METER_ATTRIBUTE_NOT_FOUND

                MESSAGE: The meter attribute does not exist.
            */
            public const int LA_E_METER_ATTRIBUTE_NOT_FOUND = 72;

            /*
                CODE: LA_E_METER_ATTRIBUTE_USES_LIMIT_REACHED

                MESSAGE: The meter attribute has reached it's usage limit.
            */
            public const int LA_E_METER_ATTRIBUTE_USES_LIMIT_REACHED = 73;

            /*
                CODE: LA_E_VM

                MESSAGE: Application is being run inside a virtual machine / hypervisor,
                and activation has been disallowed in the VM.
            */
            public const int LA_E_VM = 80;

            /*
                CODE: LA_E_COUNTRY

                MESSAGE: Country is not allowed.
            */
            public const int LA_E_COUNTRY = 81;

            /*
                CODE: LA_E_IP

                MESSAGE: IP address is not allowed.
            */
            public const int LA_E_IP = 82;

            /*
                CODE: LA_E_RATE_LIMIT

                MESSAGE: Rate limit for API has reached, try again later.
            */
            public const int LA_E_RATE_LIMIT = 90;

            /*
                CODE: LA_E_SERVER

                MESSAGE: Server error.
            */
            public const int LA_E_SERVER = 91;

            /*
                CODE: LA_E_CLIENT

                MESSAGE: Client error.
            */
            public const int LA_E_CLIENT = 92;
        };

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void CallbackType(uint status);

        /* To prevent garbage collection of delegate, need to keep a reference */
        static readonly List<CallbackType> callbackList = new List<CallbackType>();
        
        static class Native
        {
            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int SetProductFile(string filePath);

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int SetProductData(string productData);

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int SetProductId(string productId, PermissionFlags flags);

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int SetLicenseKey(string licenseKey);

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int SetLicenseUserCredential(string email, string password);

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int SetLicenseCallback(CallbackType callback);

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int SetActivationMetadata(string key, string value);

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int SetTrialActivationMetadata(string key, string value);

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int SetAppVersion(string appVersion);

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int SetNetworkProxy(string proxy);

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int GetProductMetadata(string key, StringBuilder value, int length);

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int GetLicenseMetadata(string key, StringBuilder value, int length);

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int GetLicenseMeterAttribute(string name, ref uint allowedUses, ref uint totalUses);

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int GetLicenseKey(StringBuilder licenseKey, int length);

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int GetLicenseExpiryDate(ref uint expiryDate);

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int GetLicenseUserEmail(StringBuilder email, int length);

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int GetLicenseUserName(StringBuilder name, int length);

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int GetLicenseUserCompany(StringBuilder company, int length);

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int GetLicenseUserMetadata(string key, StringBuilder value, int length);

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int GetLicenseType(StringBuilder licenseType, int length);

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int GetActivationMetadata(string key, StringBuilder value, int length);

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int GetActivationMeterAttributeUses(string name, ref uint uses);

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int GetServerSyncGracePeriodExpiryDate(ref uint expiryDate);

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int GetTrialActivationMetadata(string key, StringBuilder value, int length);

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int GetTrialExpiryDate(ref uint trialExpiryDate);

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int GetTrialId(StringBuilder trialId, int length);

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int GetLocalTrialExpiryDate(ref uint trialExpiryDate);

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int CheckForReleaseUpdate(string platform, string version, string channel, CallbackType callback);

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int ActivateLicense();

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int ActivateLicenseOffline(string filePath);

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int GenerateOfflineActivationRequest(string filePath);

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int DeactivateLicense();

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int GenerateOfflineDeactivationRequest(string filePath);

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int IsLicenseGenuine();

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int IsLicenseValid();

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int ActivateTrial();

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int ActivateTrialOffline(string filePath);

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int GenerateOfflineTrialActivationRequest(string filePath);

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int IsTrialGenuine();

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int ActivateLocalTrial(uint trialLength);

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int IsLocalTrialGenuine();

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int ExtendLocalTrial(uint trialExtensionLength);

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int IncrementActivationMeterAttributeUses(string name, uint increment);

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int DecrementActivationMeterAttributeUses(string name, uint decrement);

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int ResetActivationMeterAttributeUses(string name);

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int Reset();

#if LA_ANY_CPU

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "SetProductFile", CallingConvention = CallingConvention.Cdecl)]
            public static extern int SetProductFile_x64(string filePath);

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "SetProductData", CallingConvention = CallingConvention.Cdecl)]
            public static extern int SetProductData_x64(string productData);

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "SetProductId", CallingConvention = CallingConvention.Cdecl)]
            public static extern int SetProductId_x64(string productId, PermissionFlags flags);

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "SetLicenseKey", CallingConvention = CallingConvention.Cdecl)]
            public static extern int SetLicenseKey_x64(string licenseKey);

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "SetLicenseUserCredential", CallingConvention = CallingConvention.Cdecl)]
            public static extern int SetLicenseUserCredential_x64(string email, string password);

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "SetLicenseCallback", CallingConvention = CallingConvention.Cdecl)]
            public static extern int SetLicenseCallback_x64(CallbackType callback);

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "SetActivationMetadata", CallingConvention = CallingConvention.Cdecl)]
            public static extern int SetActivationMetadata_x64(string key, string value);

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "SetTrialActivationMetadata", CallingConvention = CallingConvention.Cdecl)]
            public static extern int SetTrialActivationMetadata_x64(string key, string value);

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "SetAppVersion", CallingConvention = CallingConvention.Cdecl)]
            public static extern int SetAppVersion_x64(string appVersion);

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "SetNetworkProxy", CallingConvention = CallingConvention.Cdecl)]
            public static extern int SetNetworkProxy_x64(string proxy);

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "GetProductMetadata", CallingConvention = CallingConvention.Cdecl)]
            public static extern int GetProductMetadata_x64(string key, StringBuilder value, int length);

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "GetLicenseMetadata", CallingConvention = CallingConvention.Cdecl)]
            public static extern int GetLicenseMetadata_x64(string key, StringBuilder value, int length);

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "GetLicenseMeterAttribute", CallingConvention = CallingConvention.Cdecl)]
            public static extern int GetLicenseMeterAttribute_x64(string name, ref uint allowedUses, ref uint totalUses);

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "GetLicenseKey", CallingConvention = CallingConvention.Cdecl)]
            public static extern int GetLicenseKey_x64(StringBuilder licenseKey, int length);

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "GetLicenseExpiryDate", CallingConvention = CallingConvention.Cdecl)]
            public static extern int GetLicenseExpiryDate_x64(ref uint expiryDate);

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "GetLicenseUserEmail", CallingConvention = CallingConvention.Cdecl)]
            public static extern int GetLicenseUserEmail_x64(StringBuilder email, int length);

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "GetLicenseUserName", CallingConvention = CallingConvention.Cdecl)]
            public static extern int GetLicenseUserName_x64(StringBuilder name, int length);

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "GetLicenseUserCompany", CallingConvention = CallingConvention.Cdecl)]
            public static extern int GetLicenseUserCompany_x64(StringBuilder company, int length);

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "GetLicenseUserMetadata", CallingConvention = CallingConvention.Cdecl)]
            public static extern int GetLicenseUserMetadata_x64(string key, StringBuilder value, int length);

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "GetLicenseType", CallingConvention = CallingConvention.Cdecl)]
            public static extern int GetLicenseType_x64(StringBuilder licenseType, int length);

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "GetActivationMetadata", CallingConvention = CallingConvention.Cdecl)]
            public static extern int GetActivationMetadata_x64(string key, StringBuilder value, int length);

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "GetActivationMeterAttributeUses", CallingConvention = CallingConvention.Cdecl)]
            public static extern int GetActivationMeterAttributeUses_x64(string name, ref uint uses);

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "GetServerSyncGracePeriodExpiryDate", CallingConvention = CallingConvention.Cdecl)]
            public static extern int GetServerSyncGracePeriodExpiryDate_x64(ref uint expiryDate);

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "GetTrialActivationMetadata", CallingConvention = CallingConvention.Cdecl)]
            public static extern int GetTrialActivationMetadata_x64(string key, StringBuilder value, int length);

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "GetTrialExpiryDate", CallingConvention = CallingConvention.Cdecl)]
            public static extern int GetTrialExpiryDate_x64(ref uint trialExpiryDate);

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "GetTrialId", CallingConvention = CallingConvention.Cdecl)]
            public static extern int GetTrialId_x64(StringBuilder trialId, int length);

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "GetLocalTrialExpiryDate", CallingConvention = CallingConvention.Cdecl)]
            public static extern int GetLocalTrialExpiryDate_x64(ref uint trialExpiryDate);

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "CheckForReleaseUpdate", CallingConvention = CallingConvention.Cdecl)]
            public static extern int CheckForReleaseUpdate_x64(string platform, string version, string channel, CallbackType callback);

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "ActivateLicense", CallingConvention = CallingConvention.Cdecl)]
            public static extern int ActivateLicense_x64();

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "ActivateLicenseOffline", CallingConvention = CallingConvention.Cdecl)]
            public static extern int ActivateLicenseOffline_x64(string filePath);

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "GenerateOfflineActivationRequest", CallingConvention = CallingConvention.Cdecl)]
            public static extern int GenerateOfflineActivationRequest_x64(string filePath);

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "DeactivateLicense", CallingConvention = CallingConvention.Cdecl)]
            public static extern int DeactivateLicense_x64();

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "GenerateOfflineDeactivationRequest", CallingConvention = CallingConvention.Cdecl)]
            public static extern int GenerateOfflineDeactivationRequest_x64(string filePath);

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "IsLicenseGenuine", CallingConvention = CallingConvention.Cdecl)]
            public static extern int IsLicenseGenuine_x64();

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "IsLicenseValid", CallingConvention = CallingConvention.Cdecl)]
            public static extern int IsLicenseValid_x64();

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "ActivateTrial", CallingConvention = CallingConvention.Cdecl)]
            public static extern int ActivateTrial_x64();

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "ActivateTrialOffline", CallingConvention = CallingConvention.Cdecl)]
            public static extern int ActivateTrialOffline_x64(string filePath);

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "GenerateOfflineTrialActivationRequest", CallingConvention = CallingConvention.Cdecl)]
            public static extern int GenerateOfflineTrialActivationRequest_x64(string filePath);

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "IsTrialGenuine", CallingConvention = CallingConvention.Cdecl)]
            public static extern int IsTrialGenuine_x64();

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "ActivateLocalTrial", CallingConvention = CallingConvention.Cdecl)]
            public static extern int ActivateLocalTrial_x64(uint trialLength);

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "IsLocalTrialGenuine", CallingConvention = CallingConvention.Cdecl)]
            public static extern int IsLocalTrialGenuine_x64();

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "ExtendLocalTrial", CallingConvention = CallingConvention.Cdecl)]
            public static extern int ExtendLocalTrial_x64(uint trialExtensionLength);

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "IncrementActivationMeterAttributeUses", CallingConvention = CallingConvention.Cdecl)]
            public static extern int IncrementActivationMeterAttributeUses_x64(string name, uint increment);

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "DecrementActivationMeterAttributeUses", CallingConvention = CallingConvention.Cdecl)]
            public static extern int DecrementActivationMeterAttributeUses_x64(string name, uint decrement);

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "ResetActivationMeterAttributeUses", CallingConvention = CallingConvention.Cdecl)]
            public static extern int ResetActivationMeterAttributeUses_x64(string name);

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "Reset", CallingConvention = CallingConvention.Cdecl)]
            public static extern int Reset_x64();
            
#endif
        }
    }
}
