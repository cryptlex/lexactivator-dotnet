using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Cryptlex
{
    public static class LexActivator
    {
        public enum PermissionFlags : uint
        {
            LA_USER = 1,
            LA_SYSTEM = 2,
            LA_IN_MEMORY = 4
        }

        public enum ReleaseFlags : uint
        {
            LA_RELEASES_ALL = 1,
            LA_RELEASES_ALLOWED = 2
        }

        private const int MetadataBufferSize = 4096;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void CallbackType(uint status);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void InternalReleaseCallbackAType(uint status, string releaseJson, IntPtr _userData);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void InternalReleaseCallbackType(uint status, IntPtr releaseJson, IntPtr _userData);

        public delegate void ReleaseUpdateCallbackType(uint status, Release release, object userData);

        /* To prevent garbage collection of delegate, need to keep a reference */
        static readonly List<CallbackType> callbackList = new List<CallbackType>();

        static readonly List<InternalReleaseCallbackAType> internalReleaseCallbackAList = new List<InternalReleaseCallbackAType>();

        static readonly List<InternalReleaseCallbackType> internalReleaseCallbackList = new List<InternalReleaseCallbackType>();

        /// <summary>
        /// Sets the absolute path of the Product.dat file.
        /// 
        /// This function must be called on every start of your program
        /// before any other functions are called.
        /// </summary>
        /// <param name="filePath">absolute path of the product file (Product.dat)</param>
        public static void SetProductFile(string filePath)
        {
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.SetProductFile_x86(filePath) : LexActivatorNative.SetProductFile(filePath);
            }
            else
            {
                status = LexActivatorNative.SetProductFileA(filePath);
            }
            if (LexStatusCodes.LA_OK != status)
            {
                throw new LexActivatorException(status);
            }
        }

        /// <summary>
        /// Embeds the Product.dat file in the application.
        /// 
        /// It can be used instead of SetProductFile() in case you want
        /// to embed the Product.dat file in your application.
        /// 
        /// This function must be called on every start of your program
        /// before any other functions are called.
        /// </summary>
        /// <param name="productData">content of the Product.dat file</param>
        public static void SetProductData(string productData)
        {
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.SetProductData_x86(productData) : LexActivatorNative.SetProductData(productData);
            }
            else
            {
                status = LexActivatorNative.SetProductDataA(productData);
            }
            if (LexStatusCodes.LA_OK != status)
            {
                throw new LexActivatorException(status);
            }
        }

        /// <summary>
        /// Sets the product id of your application.
        /// 
        /// This function must be called on every start of your program before
        /// any other functions are called, with the exception of SetProductFile()
        /// or SetProductData() function.
        /// </summary>
        /// <param name="productId">the unique product id of your application as mentioned on the product page in the dashboard</param>
        /// <param name="flags">depending upon whether your application requires admin/root permissions to run or not, this parameter can have one of the following values: LA_SYSTEM, LA_USER, LA_IN_MEMORY</param>
        public static void SetProductId(string productId, PermissionFlags flags)
        {
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.SetProductId_x86(productId, flags) : LexActivatorNative.SetProductId(productId, flags);
            }
            else
            {
                status = LexActivatorNative.SetProductIdA(productId, flags);
            }
            if (LexStatusCodes.LA_OK != status)
            {
                throw new LexActivatorException(status);
            }
        }

        /// <summary>
        /// In case you want to change the default directory used by LexActivator to
        /// store the activation data on Linux and macOS, this function can be used to
        /// set a different directory.
        
        /// If you decide to use this function, then it must be called on every start of
        /// your program before calling SetProductFile() or SetProductData() function.
        
        /// Please ensure that the directory exists and your app has read and write
        /// permissions in the directory.
        /// </summary>
        /// <param name="directoryPath">absolute path of the directory.</param>
        public static void SetDataDirectory(string directoryPath)
        {
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.SetDataDirectory_x86(directoryPath) : LexActivatorNative.SetDataDirectory(directoryPath);
            }
            else
            {
                status = LexActivatorNative.SetDataDirectoryA(directoryPath);
            }
            if (LexStatusCodes.LA_OK != status)
            {
                throw new LexActivatorException(status);
            }
        }

        /// <summary>
        /// Enables network logs.
        /// This function should be used for network testing only in case of network errors.
        /// By default logging is disabled.
        /// </summary>
        /// <param name="enable">0 or 1 to disable or enable logging.</param>
        public static void SetDebugMode(uint enable)
        {
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.SetDebugMode_x86(enable) : LexActivatorNative.SetDebugMode(enable);
            }
            else
            {
                status = LexActivatorNative.SetDebugMode(enable);
            }
            if (LexStatusCodes.LA_OK != status)
            {
                throw new LexActivatorException(status);
            }
        }

        /// <summary>
        /// In case you don't want to use the LexActivator's advanced
        /// device fingerprinting algorithm, this function can be used to set a custom
        /// device fingerprint.

        /// If you decide to use your own custom device fingerprint then this function must be
        /// called on every start of your program immediately after calling SetProductFile()
        /// or SetProductData() function.

        /// The license fingerprint matching strategy is ignored if this function is used.
        /// </summary>
        /// <param name="fingerprint">string of minimum length 64 characters and maximum length 256 characters</param>
        public static void SetCustomDeviceFingerprint(string fingerprint)
        {
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.SetCustomDeviceFingerprint_x86(fingerprint) : LexActivatorNative.SetCustomDeviceFingerprint(fingerprint);
            }
            else
            {
                status = LexActivatorNative.SetCustomDeviceFingerprintA(fingerprint);
            }
            if (LexStatusCodes.LA_OK != status)
            {
                throw new LexActivatorException(status);
            }
        }

        /// <summary>
        /// Sets the license key required to activate the license.
        /// </summary>
        /// <param name="licenseKey">a valid license key</param>
        public static void SetLicenseKey(string licenseKey)
        {
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.SetLicenseKey_x86(licenseKey) : LexActivatorNative.SetLicenseKey(licenseKey);
            }
            else
            {
                status = LexActivatorNative.SetLicenseKeyA(licenseKey);
            }
            if (LexStatusCodes.LA_OK != status)
            {
                throw new LexActivatorException(status);
            }
        }

        /// <summary>
        /// Sets the license user email and password for authentication.
        /// 
        /// This function must be called before ActivateLicense() or IsLicenseGenuine()
        /// function if 'requireAuthentication' property of the license is set to true.
        /// </summary>
        /// <param name="email">user email address</param>
        /// <param name="password">user password</param>
        public static void SetLicenseUserCredential(string email, string password)
        {
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.SetLicenseUserCredential_x86(email, password) : LexActivatorNative.SetLicenseUserCredential(email, password);
            }
            else
            {
                status = LexActivatorNative.SetLicenseUserCredentialA(email, password);
            }
            if (LexStatusCodes.LA_OK != status)
            {
                throw new LexActivatorException(status);
            }
        }

        /// <summary>
        /// Sets server sync callback function.
        /// 
        /// Whenever the server sync occurs in a separate thread, and server returns the response,
        /// license callback function gets invoked with the following status codes:
        /// LA_OK, LA_EXPIRED, LA_SUSPENDED, LA_E_REVOKED, LA_E_ACTIVATION_NOT_FOUND,
        /// LA_E_MACHINE_FINGERPRINT, LA_E_AUTHENTICATION_FAILED, LA_E_COUNTRY, LA_E_INET,
        /// LA_E_SERVER, LA_E_RATE_LIMIT, LA_E_IP
        /// </summary>
        /// <param name="callback"></param>
        public static void SetLicenseCallback(CallbackType callback)
        {
            var wrappedCallback = callback;
#if NETFRAMEWORK
            var syncTarget = callback.Target as System.Windows.Forms.Control;
            if (syncTarget != null)
            {
                wrappedCallback = (v) => syncTarget.Invoke(callback, new object[] { v });
            }
#endif
            callbackList.Add(wrappedCallback);
            int status;
            if (LexActivatorNative.IsWindows())
            {
                
                status = IntPtr.Size == 4 ? LexActivatorNative.SetLicenseCallback_x86(wrappedCallback) : LexActivatorNative.SetLicenseCallback(wrappedCallback);
            }
            else
            {
                status =  LexActivatorNative.SetLicenseCallback(wrappedCallback);
            }
            if (LexStatusCodes.LA_OK != status)
            {
                throw new LexActivatorException(status);
            }
        }

        /// <summary>
        /// Sets the lease duration for the activation.
        ///
        /// The activation lease duration is honoured when the allow client
        /// lease duration property is enabled.
        /// </summary>
        /// <param name="leaseDuration"></param>
        public static void SetActivationLeaseDuration(uint leaseDuration)
        {
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.SetActivationLeaseDuration_x86(leaseDuration) : LexActivatorNative.SetActivationLeaseDuration(leaseDuration);
            }
            else
            {
                status = LexActivatorNative.SetActivationLeaseDuration(leaseDuration);
            }
            if (LexStatusCodes.LA_OK != status)
            {
                throw new LexActivatorException(status);
            }
        }

        /// <summary>
        /// Sets the activation metadata.
        /// 
        /// The  metadata appears along with the activation details of the license
        /// in dashboard.
        /// </summary>
        /// <param name="key">string of maximum length 256 characters with utf-8 encoding</param>
        /// <param name="value">string of maximum length 256 characters with utf-8 encoding</param>
        public static void SetActivationMetadata(string key, string value)
        {
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.SetActivationMetadata_x86(key, value) : LexActivatorNative.SetActivationMetadata(key, value);
            }
            else
            {
                status = LexActivatorNative.SetActivationMetadataA(key, value);
            }
            if (LexStatusCodes.LA_OK != status)
            {
                throw new LexActivatorException(status);
            }
        }

        /// <summary>
        /// Sets the trial activation metadata.
        /// 
        /// The  metadata appears along with the trial activation details of the product
        /// in dashboard.
        /// </summary>
        /// <param name="key">string of maximum length 256 characters with utf-8 encoding</param>
        /// <param name="value">string of maximum length 256 characters with utf-8 encoding</param>
        public static void SetTrialActivationMetadata(string key, string value)
        {
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.SetTrialActivationMetadata_x86(key, value) : LexActivatorNative.SetTrialActivationMetadata(key, value);
            }
            else
            {
                status = LexActivatorNative.SetTrialActivationMetadataA(key, value);
            }
            if (LexStatusCodes.LA_OK != status)
            {
                throw new LexActivatorException(status);
            }
        }

        /// <summary>
        /// Sets the current app version of your application.
        /// 
        /// The app version appears along with the activation details in dashboard. It
        /// is also used to generate app analytics.
        /// </summary>
        /// <param name="appVersion"></param>
        public static void SetAppVersion(string appVersion)
        {
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.SetAppVersion_x86(appVersion) : LexActivatorNative.SetAppVersion(appVersion);
            }
            else
            {
                status = LexActivatorNative.SetAppVersionA(appVersion);
            }
            if (LexStatusCodes.LA_OK != status)
            {
                throw new LexActivatorException(status);
            }
        }

        /// <summary>
        /// Sets the current release version of your application.
        /// 
        /// The release version appears along with the activation details in dashboard.
        /// </summary>
        /// <param name="releaseVersion"> string in following allowed formats: x.x, x.x.x, x.x.x.x </param>
        public static void SetReleaseVersion(string releaseVersion)
        {
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.SetReleaseVersion_x86(releaseVersion) : LexActivatorNative.SetReleaseVersion(releaseVersion);
            }
            else
            {
                status = LexActivatorNative.SetReleaseVersionA(releaseVersion);
            }
            if (LexStatusCodes.LA_OK != status)
            {
                throw new LexActivatorException(status);
            }
        }

        /// <summary>
        /// Sets the release published date of your application.
        ///
        /// </summary>
        /// <param name="releasePublishedDate"> unix timestamp of release published date. </param>
        public static void SetReleasePublishedDate(uint releasePublishedDate)
        {
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.SetReleasePublishedDate_x86(releasePublishedDate) : LexActivatorNative.SetReleasePublishedDate(releasePublishedDate);
            }
            else
            {
                status = LexActivatorNative.SetReleasePublishedDate(releasePublishedDate);
            }
            if (LexStatusCodes.LA_OK != status)
            {
                throw new LexActivatorException(status);
            }
        
        }

        /// <summary>
        /// Sets the release platform e.g. windows, macos, linux
        /// 
        /// The release platform appears along with the activation details in dashboard..
        /// </summary>
        /// <param name="releasePlatform"> release platform e.g. windows, macos, linux
        public static void SetReleasePlatform(string releasePlatform)
        {
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.SetReleasePlatform_x86(releasePlatform) : LexActivatorNative.SetReleasePlatform(releasePlatform);
            }
            else
            {
                status = LexActivatorNative.SetReleasePlatformA(releasePlatform);
            }
            if (LexStatusCodes.LA_OK != status)
            {
                throw new LexActivatorException(status);
            }
        }

        /// <summary>
        /// Sets the release channel e.g. stable, beta
        /// 
        /// The release channel appears along with the activation details in dashboard..
        /// </summary>
        /// <param name="releaseChannel"> release channel e.g. stable
        public static void SetReleaseChannel(string releaseChannel)
        {
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.SetReleaseChannel_x86(releaseChannel) : LexActivatorNative.SetReleaseChannel(releaseChannel);
            }
            else
            {
                status = LexActivatorNative.SetReleaseChannelA(releaseChannel);
            }
            if (LexStatusCodes.LA_OK != status)
            {
                throw new LexActivatorException(status);
            }
        }

        /// <summary>
        /// Sets the meter attribute uses for the offline activation request.
        /// 
        /// This function should only be called before GenerateOfflineActivationRequest()
        /// function to set the meter attributes in case of offline activation.
        /// </summary>
        /// <param name="name">name of the meter attribute</param>
        /// <param name="uses">the uses value</param>
        public static void SetOfflineActivationRequestMeterAttributeUses(string name, uint uses)
        {
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.SetOfflineActivationRequestMeterAttributeUses_x86(name, uses) : LexActivatorNative.SetOfflineActivationRequestMeterAttributeUses(name, uses);
            }
            else
            {
                status = LexActivatorNative.SetOfflineActivationRequestMeterAttributeUsesA(name, uses);
            }
            if (LexStatusCodes.LA_OK != status)
            {
                throw new LexActivatorException(status);
            }
        }

        /// <summary>
        /// Sets the network proxy to be used when contacting Cryptlex servers.
        /// 
        /// The proxy format should be: [protocol://][username:password@]machine[:port]
        /// 
        /// NOTE: Proxy settings of the computer are automatically detected. So, in most of the
        /// cases you don't need to care whether your user is behind a proxy server or not.
        /// </summary>
        /// <param name="proxy">proxy string having correct proxy format</param>
        public static void SetNetworkProxy(string proxy)
        {
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.SetNetworkProxy_x86(proxy) : LexActivatorNative.SetNetworkProxy(proxy);
            }
            else
            {
                status = LexActivatorNative.SetNetworkProxyA(proxy);
            }
            if (LexStatusCodes.LA_OK != status)
            {
                throw new LexActivatorException(status);
            }
        }

        /// <summary>
        /// In case you are running Cryptlex on-premise, you can set the host for your on-premise server.
        /// 
        /// </summary>
        /// <param name="host">the address of the Cryptlex on-premise server</param>
        public static void SetCryptlexHost(string host)
        {
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.SetCryptlexHost_x86(host) : LexActivatorNative.SetCryptlexHost(host);
            }
            else
            {
                status = LexActivatorNative.SetCryptlexHostA(host);
            }
            if (LexStatusCodes.LA_OK != status)
            {
                throw new LexActivatorException(status);
            }
        }

        /// <summary>
        /// Gets the product metadata as set in the dashboard.
        /// 
        /// This is available for trial as well as license activations.
        /// </summary>
        /// <param name="key">metadata key to retrieve the value</param>
        /// <returns>Returns the value of metadata for the key.</returns>
        public static string GetProductMetadata(string key)
        {
            var builder = new StringBuilder(MetadataBufferSize);
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.GetProductMetadata_x86(key, builder, builder.Capacity) : LexActivatorNative.GetProductMetadata(key, builder, builder.Capacity);
            }
            else
            {
                status = LexActivatorNative.GetProductMetadataA(key, builder, builder.Capacity);
            }
            if (LexStatusCodes.LA_OK == status)
            {
                return builder.ToString();
            }
            throw new LexActivatorException(status);
        }

        /// <summary>
        /// Gets the product version name.
        /// </summary>
        /// <returns>Returns the value of the product version name.</returns>
        public static string GetProductVersionName()
        {
            var builder = new StringBuilder(MetadataBufferSize);
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.GetProductVersionName_x86(builder, builder.Capacity) : LexActivatorNative.GetProductVersionName(builder, builder.Capacity);
            }
            else
            {
                status = LexActivatorNative.GetProductVersionNameA(builder, builder.Capacity);
            }
            if (LexStatusCodes.LA_OK == status)
            {
                return builder.ToString();
            }
            throw new LexActivatorException(status);
        }

        /// <summary>
        /// Gets the product version display name.
        /// </summary>
        /// <returns>Returns the value of the product version display name.</returns>
        public static string GetProductVersionDisplayName()
        {
            var builder = new StringBuilder(MetadataBufferSize);
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.GetProductVersionDisplayName_x86(builder, builder.Capacity) : LexActivatorNative.GetProductVersionDisplayName(builder, builder.Capacity);
            }
            else
            {
                status = LexActivatorNative.GetProductVersionDisplayNameA(builder, builder.Capacity);
            }
            if (LexStatusCodes.LA_OK == status)
            {
                return builder.ToString();
            }
            throw new LexActivatorException(status);
        }

        /// <summary>
        /// Gets the product version feature flag.
        /// </summary>
        /// <param name="name">name of the product version feature flag</param>
        /// <returns>Returns the product version feature flag.</returns>
        public static ProductVersionFeatureFlag GetProductVersionFeatureFlag(string name)
        {
            uint enabled = 0;
            var builder = new StringBuilder(MetadataBufferSize);
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.GetProductVersionFeatureFlag_x86(name, ref enabled, builder, builder.Capacity) : LexActivatorNative.GetProductVersionFeatureFlag(name, ref enabled, builder, builder.Capacity);
            }
            else
            {
                status = LexActivatorNative.GetProductVersionFeatureFlagA(name, ref enabled, builder, builder.Capacity);
            }
            if (LexStatusCodes.LA_OK == status)
            {
                return new ProductVersionFeatureFlag(name, enabled > 0, builder.ToString());
            }
            throw new LexActivatorException(status);
        }

        /// <summary>
        /// Gets the license metadata of the license.
        /// </summary>
        /// <param name="key">metadata key to retrieve the value</param>
        /// <returns>Returns the value of metadata for the key.</returns>
        public static string GetLicenseMetadata(string key)
        {
            var builder = new StringBuilder(MetadataBufferSize);
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.GetLicenseMetadata_x86(key, builder, builder.Capacity) : LexActivatorNative.GetLicenseMetadata(key, builder, builder.Capacity);
            }
            else
            {
                status = LexActivatorNative.GetLicenseMetadataA(key, builder, builder.Capacity);
            }
            if (LexStatusCodes.LA_OK == status)
            {
                return builder.ToString();
            }
            throw new LexActivatorException(status);
        }

        /// <summary>
        /// Gets the license meter attribute allowed, total and gross uses.
        /// </summary>
        /// <param name="name">name of the meter attribute</param>
        /// <returns>Returns the values of meter attribute allowed, total and gross uses.</returns>
        public static LicenseMeterAttribute GetLicenseMeterAttribute(string name)
        {
            uint allowedUses = 0, totalUses = 0, grossUses = 0;
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.GetLicenseMeterAttribute_x86(name, ref allowedUses, ref totalUses, ref grossUses) : LexActivatorNative.GetLicenseMeterAttribute(name, ref allowedUses, ref totalUses, ref grossUses);
            }
            else
            {
                status = LexActivatorNative.GetLicenseMeterAttributeA(name, ref allowedUses, ref totalUses, ref grossUses);
            }
            if (LexStatusCodes.LA_OK == status)
            {
                return new LicenseMeterAttribute(name, allowedUses, totalUses, grossUses);
            }
            throw new LexActivatorException(status);
        }

        /// <summary>
        /// Gets the license key used for activation.
        /// </summary>
        /// <returns>Returns the license key.</returns>
        public static string GetLicenseKey()
        {
            var builder = new StringBuilder(512);
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.GetLicenseKey_x86(builder, builder.Capacity) : LexActivatorNative.GetLicenseKey(builder, builder.Capacity);
            }
            else
            {
                status = LexActivatorNative.GetLicenseKeyA(builder, builder.Capacity);
            }
            if (LexStatusCodes.LA_OK == status)
            {
                return builder.ToString();
            }
            throw new LexActivatorException(status);
        }

        /// <summary>
        /// Gets the allowed activations of the license.
        /// </summary>
        /// <returns>Returns the allowed activations.</returns>
        public static uint GetLicenseAllowedActivations()
        {
            uint allowedActivations = 0;
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.GetLicenseAllowedActivations_x86(ref allowedActivations) : LexActivatorNative.GetLicenseAllowedActivations(ref allowedActivations);
            }
            else
            {
                status =  LexActivatorNative.GetLicenseAllowedActivations(ref allowedActivations);
            }
            switch (status)
            {
                case LexStatusCodes.LA_OK:
                    return allowedActivations;
                case LexStatusCodes.LA_FAIL:
                    return 0;
                default:
                    throw new LexActivatorException(status);
            }
        }

        /// <summary>
        /// Gets the total activations of the license.
        /// </summary>
        /// <returns>Returns the total activations.</returns>
        public static uint GetLicenseTotalActivations()
        {
            uint totalActivations = 0;
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.GetLicenseTotalActivations_x86(ref totalActivations) : LexActivatorNative.GetLicenseTotalActivations(ref totalActivations);
            }
            else
            {
                status =  LexActivatorNative.GetLicenseTotalActivations(ref totalActivations);
            }
            switch (status)
            {
                case LexStatusCodes.LA_OK:
                    return totalActivations;
                case LexStatusCodes.LA_FAIL:
                    return 0;
                default:
                    throw new LexActivatorException(status);
            }
        }

        /// <summary>
        /// Gets the license creation date timestamp.
        /// </summary>
        /// <returns>Returns the timestamp.</returns>
        public static uint GetLicenseCreationDate()
        {
            uint creationDate = 0;
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.GetLicenseCreationDate_x86(ref creationDate) : LexActivatorNative.GetLicenseCreationDate(ref creationDate);
            }
            else
            {
                status =  LexActivatorNative.GetLicenseCreationDate(ref creationDate);
            }
            switch (status)
            {
                case LexStatusCodes.LA_OK:
                    return creationDate;
                case LexStatusCodes.LA_FAIL:
                    return 0;
                default:
                    throw new LexActivatorException(status);
            }
        }

        /// <summary>
        /// Gets the activation creation date timestamp.
        /// </summary>
        /// <returns>Returns the timestamp.</returns>
        public static uint GetLicenseActivationDate()
        {
            uint activationDate = 0;
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.GetLicenseActivationDate_x86(ref activationDate) : LexActivatorNative.GetLicenseActivationDate(ref activationDate);
            }
            else
            {
                status =  LexActivatorNative.GetLicenseActivationDate(ref activationDate);
            }
            switch (status)
            {
                case LexStatusCodes.LA_OK:
                    return activationDate;
                case LexStatusCodes.LA_FAIL:
                    return 0;
                default:
                    throw new LexActivatorException(status);
            }
        }


        /// <summary>
        /// Gets the license expiry date timestamp.
        /// </summary>
        /// <returns>Returns the timestamp.</returns>
        public static uint GetLicenseExpiryDate()
        {
            uint expiryDate = 0;
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.GetLicenseExpiryDate_x86(ref expiryDate) : LexActivatorNative.GetLicenseExpiryDate(ref expiryDate);
            }
            else
            {
                status =  LexActivatorNative.GetLicenseExpiryDate(ref expiryDate);
            }
            switch (status)
            {
                case LexStatusCodes.LA_OK:
                    return expiryDate;
                case LexStatusCodes.LA_FAIL:
                    return 0;
                default:
                    throw new LexActivatorException(status);
            }
        }

        /// <summary>
        /// Gets the license maintenance expiry date timestamp.
        /// </summary>
        /// <returns>Returns the timestamp.</returns>
        public static uint GetLicenseMaintenanceExpiryDate()
        {
            uint maintenanceExpiryDate = 0;
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.GetLicenseMaintenanceExpiryDate_x86(ref maintenanceExpiryDate) : LexActivatorNative.GetLicenseMaintenanceExpiryDate(ref maintenanceExpiryDate);
            }
            else
            {
                status =  LexActivatorNative.GetLicenseMaintenanceExpiryDate(ref maintenanceExpiryDate);
            }
            switch (status)
            {
                case LexStatusCodes.LA_OK:
                    return maintenanceExpiryDate;
                case LexStatusCodes.LA_FAIL:
                    return 0;
                default:
                    throw new LexActivatorException(status);
            }
        }

        /// <summary>
        /// Gets the maximum allowed release version of the license.
        /// </summary>
        /// <returns>Returns the max allowed release version.</returns>
        public static string GetLicenseMaxAllowedReleaseVersion()
        {
            var builder = new StringBuilder(512);
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.GetLicenseMaxAllowedReleaseVersion_x86(builder, builder.Capacity) : LexActivatorNative.GetLicenseMaxAllowedReleaseVersion(builder, builder.Capacity);
            }
            else
            {
                status = LexActivatorNative.GetLicenseMaxAllowedReleaseVersionA(builder, builder.Capacity);
            }
            if (LexStatusCodes.LA_OK == status)
            {
                return builder.ToString();
            }
            throw new LexActivatorException(status);
        }

        /// <summary>
        /// Gets the email associated with the license user.
        /// </summary>
        /// <returns>Returns the license user email.</returns>
        public static string GetLicenseUserEmail()
        {
            var builder = new StringBuilder(512);
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.GetLicenseUserEmail_x86(builder, builder.Capacity) : LexActivatorNative.GetLicenseUserEmail(builder, builder.Capacity);
            }
            else
            {
                status = LexActivatorNative.GetLicenseUserEmailA(builder, builder.Capacity);
            }
            if (LexStatusCodes.LA_OK == status)
            {
                return builder.ToString();
            }
            throw new LexActivatorException(status);
        }

        /// <summary>
        /// Gets the name associated with the license user.
        /// </summary>
        /// <returns>Returns the license user name.</returns>
        public static string GetLicenseUserName()
        {
            var builder = new StringBuilder(512);
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.GetLicenseUserName_x86(builder, builder.Capacity) : LexActivatorNative.GetLicenseUserName(builder, builder.Capacity);
            }
            else
            {
                status = LexActivatorNative.GetLicenseUserNameA(builder, builder.Capacity);
            }
            if (LexStatusCodes.LA_OK == status)
            {
                return builder.ToString();
            }
            throw new LexActivatorException(status);
        }

        /// <summary>
        /// Gets the company associated with the license user.
        /// </summary>
        /// <returns>Returns the license user company.</returns>
        public static string GetLicenseUserCompany()
        {
            var builder = new StringBuilder(512);
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.GetLicenseUserCompany_x86(builder, builder.Capacity) : LexActivatorNative.GetLicenseUserCompany(builder, builder.Capacity);
            }
            else
            {
                status = LexActivatorNative.GetLicenseUserCompanyA(builder, builder.Capacity);
            }
            if (LexStatusCodes.LA_OK == status)
            {
                return builder.ToString();
            }
            throw new LexActivatorException(status);
        }

        /// <summary>
        /// Gets the metadata associated with the license user.
        /// </summary>
        /// <param name="key">key to retrieve the value</param>
        /// <returns>Returns the value of metadata for the key.</returns>
        public static string GetLicenseUserMetadata(string key)
        {
            var builder = new StringBuilder(MetadataBufferSize);
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.GetLicenseUserMetadata_x86(key, builder, builder.Capacity) : LexActivatorNative.GetLicenseUserMetadata(key, builder, builder.Capacity);
            }
            else
            {
                status = LexActivatorNative.GetLicenseUserMetadataA(key, builder, builder.Capacity);
            }
            if (LexStatusCodes.LA_OK == status)
            {
                return builder.ToString();
            }
            throw new LexActivatorException(status);
        }

        /// <summary>
        /// Gets the name associated with the license organization.
        /// </summary>
        /// <returns>Returns the license organization name.</returns>
        public static string GetLicenseOrganizationName()
        {
            var builder = new StringBuilder(512);
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.GetLicenseOrganizationName_x86(builder, builder.Capacity) : LexActivatorNative.GetLicenseOrganizationName(builder, builder.Capacity);
            }
            else
            {
                status = LexActivatorNative.GetLicenseOrganizationNameA(builder, builder.Capacity);
            }
            if (LexStatusCodes.LA_OK == status)
            {
                return builder.ToString();
            }
            throw new LexActivatorException(status);
        }

        /// <summary>
        /// Gets the license organization address.
        /// </summary>
        /// <returns>Returns the license organization address.</returns>
        public static OrganizationAddress GetLicenseOrganizationAddress()
        {
            var builder = new StringBuilder(512);
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.GetLicenseOrganizationAddressInternal_x86(builder, builder.Capacity) : LexActivatorNative.GetLicenseOrganizationAddressInternal(builder, builder.Capacity);
            }
            else
            {
                status = LexActivatorNative.GetLicenseOrganizationAddressInternalA(builder, builder.Capacity);
            }
            if (LexStatusCodes.LA_OK == status)
            {
                string jsonAddress = builder.ToString();
                if (jsonAddress.Length > 0)
                {
                    OrganizationAddress organizationAddress = null;
                    organizationAddress = JsonConvert.DeserializeObject<OrganizationAddress>(jsonAddress);
                    return organizationAddress; 
                }
                return null;
            }
            throw new LexActivatorException(status);
        }

        /// <summary>
        /// Gets the user licenses for the product.
        ///
        /// This function sends a network request to Cryptlex servers to get the licenses.
        ///
        /// Make sure AuthenticateUser() function is called before calling this function.
        /// </summary>
        /// <returns>Returns the list of user licenses.</returns>
        public static List<UserLicense> GetUserLicenses()
        {
            var builder = new StringBuilder(4096);
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.GetUserLicensesInternal_x86(builder, builder.Capacity) : LexActivatorNative.GetUserLicensesInternal(builder, builder.Capacity);
            }
            else
            {
                status = LexActivatorNative.GetUserLicensesInternalA(builder, builder.Capacity);
            }
            if (LexStatusCodes.LA_OK == status)
            {
                string userLicensesJson = builder.ToString();
                List<UserLicense> userLicenses = new List<UserLicense>();
                if (userLicensesJson != "") 
                {
                    userLicenses = JsonConvert.DeserializeObject<List<UserLicense>>(userLicensesJson);
                    return userLicenses;
                }
                return userLicenses;
            }
            throw new LexActivatorException(status);
        }

        /// <summary>
        /// Gets the license type (node-locked or hosted-floating).
        /// </summary>
        /// <returns>Returns the license type.</returns>
        public static string GetLicenseType()
        {
            var builder = new StringBuilder(512);
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.GetLicenseType_x86(builder, builder.Capacity) : LexActivatorNative.GetLicenseType(builder, builder.Capacity);
            }
            else
            {
                status = LexActivatorNative.GetLicenseTypeA(builder, builder.Capacity);
            }
            if (LexStatusCodes.LA_OK == status)
            {
                return builder.ToString();
            }
            throw new LexActivatorException(status);
        }

        /// <summary>
        /// Gets the activation metadata.
        /// </summary>
        /// <param name="key">key to retrieve the value</param>
        /// <returns>Returns the value of metadata for the key.</returns>
        public static string GetActivationMetadata(string key)
        {
            var builder = new StringBuilder(MetadataBufferSize);
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.GetActivationMetadata_x86(key, builder, builder.Capacity) : LexActivatorNative.GetActivationMetadata(key, builder, builder.Capacity);
            }
            else
            {
                status = LexActivatorNative.GetActivationMetadataA(key, builder, builder.Capacity);
            }
            if (LexStatusCodes.LA_OK == status)
            {
                return builder.ToString();
            }
            throw new LexActivatorException(status);
        }

        /// <summary>
        /// Gets the initial and current mode of activation (online or offline).
        /// </summary>
        /// <returns>Returns the activation mode.</returns>
        public static ActivationMode GetActivationMode()
        {
            var initialModeBuilder = new StringBuilder(MetadataBufferSize);
            var currentModeBuilder = new StringBuilder(MetadataBufferSize);

            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.GetActivationMode_x86(initialModeBuilder, initialModeBuilder.Capacity, currentModeBuilder, currentModeBuilder.Capacity) : LexActivatorNative.GetActivationMode(initialModeBuilder, initialModeBuilder.Capacity, currentModeBuilder, currentModeBuilder.Capacity);
            }
            else
            {
                status = LexActivatorNative.GetActivationModeA(initialModeBuilder, initialModeBuilder.Capacity, currentModeBuilder, currentModeBuilder.Capacity);
            }
            if (LexStatusCodes.LA_OK == status)
            {
                 return new ActivationMode(initialModeBuilder.ToString(), currentModeBuilder.ToString());
            }
            throw new LexActivatorException(status);
        }

        /// <summary>
        /// Gets the meter attribute uses consumed by the activation.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Returns the value of meter attribute uses by the activation.</returns>
        public static uint GetActivationMeterAttributeUses(string name)
        {
            uint uses = 0;
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.GetActivationMeterAttributeUses_x86(name, ref uses) : LexActivatorNative.GetActivationMeterAttributeUses(name, ref uses);
            }
            else
            {
                status = LexActivatorNative.GetActivationMeterAttributeUsesA(name, ref uses);
            }
            if (LexStatusCodes.LA_OK == status)
            {
                return uses;
            }
            throw new LexActivatorException(status);
        }

        /// <summary>
        /// Gets the server sync grace period expiry date timestamp.
        /// </summary>
        /// <returns>Returns server sync grace period expiry date timestamp.</returns>
        public static uint GetServerSyncGracePeriodExpiryDate()
        {
            uint expiryDate = 0;
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.GetServerSyncGracePeriodExpiryDate_x86(ref expiryDate) : LexActivatorNative.GetServerSyncGracePeriodExpiryDate(ref expiryDate);
            }
            else
            {
                status =  LexActivatorNative.GetServerSyncGracePeriodExpiryDate(ref expiryDate);
            }
            switch (status)
            {
                case LexStatusCodes.LA_OK:
                    return expiryDate;
                case LexStatusCodes.LA_FAIL:
                    return 0;
                default:
                    throw new LexActivatorException(status);
            }
        }

        /// <summary>
        /// Gets the trial activation metadata.
        /// </summary>
        /// <param name="key">key to retrieve the value</param>
        /// <returns>Returns the value of metadata for the key.</returns>
        public static string GetTrialActivationMetadata(string key)
        {
            var builder = new StringBuilder(4096);
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.GetTrialActivationMetadata_x86(key, builder, builder.Capacity) : LexActivatorNative.GetTrialActivationMetadata(key, builder, builder.Capacity);
            }
            else
            {
                status = LexActivatorNative.GetTrialActivationMetadataA(key, builder, builder.Capacity);
            }
            if (LexStatusCodes.LA_OK == status)
            {
                return builder.ToString();
            }
            throw new LexActivatorException(status);
        }

        /// <summary>
        /// Gets the trial expiry date timestamp.
        /// </summary>
        /// <returns>Returns trial expiry date timestamp.</returns>
        public static uint GetTrialExpiryDate()
        {
            uint trialExpiryDate = 0;
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.GetTrialExpiryDate_x86(ref trialExpiryDate) : LexActivatorNative.GetTrialExpiryDate(ref trialExpiryDate);
            }
            else
            {
                status =  LexActivatorNative.GetTrialExpiryDate(ref trialExpiryDate);
            }
            switch (status)
            {
                case LexStatusCodes.LA_OK:
                    return trialExpiryDate;
                case LexStatusCodes.LA_FAIL:
                    return 0;
                default:
                    throw new LexActivatorException(status);
            }
        }

        /// <summary>
        /// Gets the trial activation id. Used in case of trial extension.
        /// </summary>
        /// <returns>Returns the trial id.</returns>
        public static string GetTrialId()
        {
            var builder = new StringBuilder(512);
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.GetTrialId_x86(builder, builder.Capacity) : LexActivatorNative.GetTrialId(builder, builder.Capacity);
            }
            else
            {
                status = LexActivatorNative.GetTrialIdA(builder, builder.Capacity);
            }
            if (LexStatusCodes.LA_OK == status)
            {
                return builder.ToString();
            }
            throw new LexActivatorException(status);
        }

        /// <summary>
        /// Gets the trial expiry date timestamp.
        /// </summary>
        /// <returns>Returns trial expiry date timestamp.</returns>
        public static uint GetLocalTrialExpiryDate()
        {
            uint trialExpiryDate = 0;
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.GetLocalTrialExpiryDate_x86(ref trialExpiryDate) : LexActivatorNative.GetLocalTrialExpiryDate(ref trialExpiryDate);
            }
            else
            {
                status =  LexActivatorNative.GetLocalTrialExpiryDate(ref trialExpiryDate);
            }
            switch (status)
            {
                case LexStatusCodes.LA_OK:
                    return trialExpiryDate;
                case LexStatusCodes.LA_FAIL:
                    return 0;
                default:
                    throw new LexActivatorException(status);
            }
        }

        /// <summary>
        /// Gets the version of this library.
        /// </summary>
        /// <returns>Returns the version of this library.</returns>
        public static string GetLibraryVersion()
        {
            var builder = new StringBuilder(512);
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.GetLibraryVersion_x86(builder, builder.Capacity) : LexActivatorNative.GetLibraryVersion(builder, builder.Capacity);
            }
            else
            {
                status = LexActivatorNative.GetLibraryVersionA(builder, builder.Capacity);
            }
            if (LexStatusCodes.LA_OK == status)
            {
                return builder.ToString();
            }
            throw new LexActivatorException(status);
        }

        /// <summary>
        /// Checks whether a new release is available for the product.
        /// 
        /// This function should only be used if you manage your releases through
        /// Cryptlex release management API.
        ///
        /// When this function is called the release update callback function gets invoked 
        /// which passes the following parameters:
        ///
        /// status - determines if any update is available or not. It also determines whether 
        /// an update is allowed or not. Expected values are LA_RELEASE_UPDATE_AVAILABLE,
        /// LA_RELEASE_UPDATE_NOT_AVAILABLE, LA_RELEASE_UPDATE_AVAILABLE_NOT_ALLOWED.
        ///
        /// release - object of the latest available release, depending on the 
        /// flag LA_RELEASES_ALLOWED or LA_RELEASES_ALL passed to the CheckReleaseUpdate().
        /// 
        /// userData - data that is passed to the callback function when it is registered
	    /// using the CheckReleaseUpdate function. This parameter is optional and can be null if no user data
	    /// is passed to the CheckReleaseUpdate function.
        /// </summary>
        /// <param name="releaseUpdateCallback">name of the callback function</param>
        /// <param name="releaseFlags">if an update only related to the allowed release is required, then use LA_RELEASES_ALLOWED. Otherwise, if an update for all the releases is required, then use LA_RELEASES_ALL.</param>
        /// <param name="userData">data that can be passed to the callback function. This parameter has to be null if no user data needs to be passed to the callback.</param>

        public static void CheckReleaseUpdate(ReleaseUpdateCallbackType releaseUpdateCallback, ReleaseFlags releaseFlags, object userData)
        {
            InternalReleaseCallbackType internalReleaseCallback = (releaseStatus, releaseJson, _userData) =>
            {
                string releaseJsonString = Marshal.PtrToStringUni(releaseJson);
                Release release = null;
                release = JsonConvert.DeserializeObject<Release>(releaseJsonString);
                var wrappedCallback = releaseUpdateCallback;
#if NETFRAMEWORK
            var syncTarget = releaseUpdateCallback.Target as System.Windows.Forms.Control;
            if (syncTarget != null)
            {
                wrappedCallback = (u, v, w) => syncTarget.Invoke(releaseUpdateCallback, new object[] {u, v, w});
            }
#endif
                wrappedCallback(releaseStatus, release, userData);
            };
            internalReleaseCallbackList.Add(internalReleaseCallback);
            InternalReleaseCallbackAType internalReleaseCallbackA = (releaseStatus, releaseJson, _userData) =>
            {
                Release release = null;
                release = JsonConvert.DeserializeObject<Release>(releaseJson);
                releaseUpdateCallback(releaseStatus, release, userData);
            };
            internalReleaseCallbackAList.Add(internalReleaseCallbackA);
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.CheckReleaseUpdateInternal_x86(internalReleaseCallback, releaseFlags, IntPtr.Zero) : LexActivatorNative.CheckReleaseUpdateInternal(internalReleaseCallback, releaseFlags, IntPtr.Zero);
            }
            else
            {
                status = LexActivatorNative.CheckReleaseUpdateInternalA(internalReleaseCallbackA, releaseFlags, IntPtr.Zero);
            }
            if (LexStatusCodes.LA_OK != status)
            {
                throw new LexActivatorException(status);
            }
        }
        
        
        /// <summary>
        /// Checks whether a new release is available for the product.
        /// 
        /// This function should only be used if you manage your releases through
        /// Cryptlex release management API.
        /// </summary>
        /// <param name="platform">release platform e.g. windows, macos, linux</param>
        /// <param name="version">current release version</param>
        /// <param name="channel">release channel e.g. stable</param>
        /// <param name="callback">name of the callback function</param>
        public static void CheckForReleaseUpdate(string platform, string version, string channel, CallbackType callback)
        {
            var wrappedCallback = callback;
#if NETFRAMEWORK
            var syncTarget = callback.Target as System.Windows.Forms.Control;
            if (syncTarget != null)
            {
                wrappedCallback = (v) => syncTarget.Invoke(callback, new object[] { v });
            }
#endif
            callbackList.Add(wrappedCallback);
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.CheckForReleaseUpdate_x86(platform, version, channel, wrappedCallback) : LexActivatorNative.CheckForReleaseUpdate(platform, version, channel, wrappedCallback);
            }
            else
            {
                status = LexActivatorNative.CheckForReleaseUpdateA(platform, version, channel, wrappedCallback);
            }
            if (LexStatusCodes.LA_OK != status)
            {
                throw new LexActivatorException(status);
            }
        }

        /// <summary>
        /// It sends the request to the Cryptlex servers to authenticate the user.
        /// </summary>
        /// <param name="email">user email address</param>
        /// <param name="password">user password</param> 
        /// <returns>LA_OK</returns>
        public static int AuthenticateUser(string email, string password)
        {
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.AuthenticateUser_x86(email, password) : LexActivatorNative.AuthenticateUser(email, password);
            }
            else
            {
                status = LexActivatorNative.AuthenticateUserA(email, password);
            }
            if (LexStatusCodes.LA_OK == status)
            {
                return LexStatusCodes.LA_OK;
            }
            else
            {
                throw new LexActivatorException(status);
            }
        }
        /// <summary>
        /// Activates the license by contacting the Cryptlex servers. It
        /// validates the key and returns with encrypted and digitally signed token
        /// which it stores and uses to activate your application.
        /// 
        /// This function should be executed at the time of registration, ideally on
        /// a button click.
        /// </summary>
        /// <returns>LA_OK, LA_EXPIRED, LA_SUSPENDED, LA_FAIL</returns>
        public static int ActivateLicense()
        {
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.ActivateLicense_x86() : LexActivatorNative.ActivateLicense();
            }
            else
            {
                status =  LexActivatorNative.ActivateLicense();
            }
            switch (status)
            {
                case LexStatusCodes.LA_OK:
                    return LexStatusCodes.LA_OK;
                case LexStatusCodes.LA_EXPIRED:
                    return LexStatusCodes.LA_EXPIRED;
                case LexStatusCodes.LA_SUSPENDED:
                    return LexStatusCodes.LA_SUSPENDED;
                case LexStatusCodes.LA_FAIL:
                    return LexStatusCodes.LA_FAIL;
                default:
                    throw new LexActivatorException(status);
            }
        }

        /// <summary>
        /// Activates your licenses using the offline activation response file.
        /// </summary>
        /// <param name="filePath">path of the offline activation response file</param>
        /// <returns>LA_OK, LA_EXPIRED, LA_SUSPENDED, LA_FAIL</returns>
        public static int ActivateLicenseOffline(string filePath)
        {
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.ActivateLicenseOffline_x86(filePath) : LexActivatorNative.ActivateLicenseOffline(filePath);
            }
            else
            {
                status = LexActivatorNative.ActivateLicenseOfflineA(filePath);
            }
            switch (status)
            {
                case LexStatusCodes.LA_OK:
                    return LexStatusCodes.LA_OK;
                case LexStatusCodes.LA_EXPIRED:
                    return LexStatusCodes.LA_EXPIRED;
                case LexStatusCodes.LA_SUSPENDED:
                    return LexStatusCodes.LA_SUSPENDED;
                case LexStatusCodes.LA_FAIL:
                    return LexStatusCodes.LA_FAIL;
                default:
                    throw new LexActivatorException(status);
            }
        }

        /// <summary>
        /// Generates the offline activation request needed for generating
        /// offline activation response in the dashboard.
        /// </summary>
        /// <param name="filePath">path of the file for the offline request</param>
        public static void GenerateOfflineActivationRequest(string filePath)
        {
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.GenerateOfflineActivationRequest_x86(filePath) : LexActivatorNative.GenerateOfflineActivationRequest(filePath);
            }
            else
            {
                status = LexActivatorNative.GenerateOfflineActivationRequestA(filePath);
            }
            if (LexStatusCodes.LA_OK != status)
            {
                throw new LexActivatorException(status);
            }
        }

        /// <summary>
        /// Deactivates the license activation and frees up the corresponding activation
        /// slot by contacting the Cryptlex servers.
        /// 
        /// This function should be executed at the time of de-registration, ideally on
        /// a button click.
        /// </summary>
        /// <returns>LA_OK, LA_FAIL</returns>
        public static int DeactivateLicense()
        {
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.DeactivateLicense_x86() : LexActivatorNative.DeactivateLicense();
            }
            else
            {
                status =  LexActivatorNative.DeactivateLicense();
            }
            switch (status)
            {
                case LexStatusCodes.LA_OK:
                    return LexStatusCodes.LA_OK;
                case LexStatusCodes.LA_FAIL:
                    return LexStatusCodes.LA_FAIL;
                default:
                    throw new LexActivatorException(status);
            }
        }

        /// <summary>
        /// Generates the offline deactivation request needed for deactivation of
        /// the license in the dashboard and deactivates the license locally.
        /// 
        /// A valid offline deactivation file confirms that the license has been successfully
        /// deactivated on the user's machine.
        /// </summary>
        /// <param name="filePath">path of the file for the offline request</param>
        /// <returns>LA_OK, LA_FAIL</returns>
        public static int GenerateOfflineDeactivationRequest(string filePath)
        {
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.GenerateOfflineDeactivationRequest_x86(filePath) : LexActivatorNative.GenerateOfflineDeactivationRequest(filePath);
            }
            else
            {
                status = LexActivatorNative.GenerateOfflineDeactivationRequestA(filePath);
            }
            switch (status)
            {
                case LexStatusCodes.LA_OK:
                    return LexStatusCodes.LA_OK;
                case LexStatusCodes.LA_FAIL:
                    return LexStatusCodes.LA_FAIL;
                default:
                    throw new LexActivatorException(status);
            }
        }

        /// <summary>
        /// It verifies whether your app is genuinely activated or not. The verification is
        /// done locally by verifying the cryptographic digital signature fetched at the time of activation.
        /// 
        /// After verifying locally, it schedules a server check in a separate thread. After the
        /// first server sync it periodically does further syncs at a frequency set for the license.
        /// 
        /// In case server sync fails due to network error, and it continues to fail for fixed
        /// number of days (grace period), the function returns LA_GRACE_PERIOD_OVER instead of LA_OK.
        /// 
        /// This function must be called on every start of your program to verify the activation
        /// of your app.
        /// </summary>
        /// <returns>LA_OK, LA_EXPIRED, LA_SUSPENDED, LA_GRACE_PERIOD_OVER, LA_FAIL</returns>
        public static int IsLicenseGenuine()
        {
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.IsLicenseGenuine_x86() : LexActivatorNative.IsLicenseGenuine();
            }
            else
            {
                status =  LexActivatorNative.IsLicenseGenuine();
            }
            switch (status)
            {
                case LexStatusCodes.LA_OK:
                    return LexStatusCodes.LA_OK;
                case LexStatusCodes.LA_EXPIRED:
                    return LexStatusCodes.LA_EXPIRED;
                case LexStatusCodes.LA_SUSPENDED:
                    return LexStatusCodes.LA_SUSPENDED;
                case LexStatusCodes.LA_GRACE_PERIOD_OVER:
                    return LexStatusCodes.LA_GRACE_PERIOD_OVER;
                case LexStatusCodes.LA_FAIL:
                    return LexStatusCodes.LA_FAIL;
                default:
                    throw new LexActivatorException(status);
            }
        }

        /// <summary>
        /// It verifies whether your app is genuinely activated or not. The verification is
        /// done locally by verifying the cryptographic digital signature fetched at the time of activation.
        /// 
        /// This is just an auxiliary function which you may use in some specific cases, when you
        /// want to skip the server sync.
        /// 
        /// NOTE: You may want to set grace period to 0 to ignore grace period.
        /// </summary>
        /// <returns>LA_OK, LA_EXPIRED, LA_SUSPENDED, LA_GRACE_PERIOD_OVER, LA_FAIL</returns>
        public static int IsLicenseValid()
        {
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.IsLicenseValid_x86() : LexActivatorNative.IsLicenseValid();
            }
            else
            {
                status =  LexActivatorNative.IsLicenseValid();
            }
            switch (status)
            {
                case LexStatusCodes.LA_OK:
                    return LexStatusCodes.LA_OK;
                case LexStatusCodes.LA_EXPIRED:
                    return LexStatusCodes.LA_EXPIRED;
                case LexStatusCodes.LA_SUSPENDED:
                    return LexStatusCodes.LA_SUSPENDED;
                case LexStatusCodes.LA_GRACE_PERIOD_OVER:
                    return LexStatusCodes.LA_GRACE_PERIOD_OVER;
                case LexStatusCodes.LA_FAIL:
                    return LexStatusCodes.LA_FAIL;
                default:
                    throw new LexActivatorException(status);
            }
        }

        /// <summary>
        /// Starts the verified trial in your application by contacting the
        /// Cryptlex servers.
        /// 
        /// This function should be executed when your application starts first time on
        /// the user's computer, ideally on a button click.
        /// </summary>
        /// <returns>LA_OK, LA_TRIAL_EXPIRED</returns>
        public static int ActivateTrial()
        {
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.ActivateTrial_x86() : LexActivatorNative.ActivateTrial();
            }
            else
            {
                status =  LexActivatorNative.ActivateTrial();
            }
            switch (status)
            {
                case LexStatusCodes.LA_OK:
                    return LexStatusCodes.LA_OK;
                case LexStatusCodes.LA_TRIAL_EXPIRED:
                    return LexStatusCodes.LA_TRIAL_EXPIRED;
                case LexStatusCodes.LA_FAIL:
                    return LexStatusCodes.LA_FAIL;
                default:
                    throw new LexActivatorException(status);
            }
        }

        /// <summary>
        /// Activates your trial using the offline activation response file.
        /// </summary>
        /// <param name="filePath">path of the offline activation response file</param>
        /// <returns>LA_OK, LA_TRIAL_EXPIRED, LA_FAIL</returns>
        public static int ActivateTrialOffline(string filePath)
        {
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.ActivateTrialOffline_x86(filePath) : LexActivatorNative.ActivateTrialOffline(filePath);
            }
            else
            {
                status = LexActivatorNative.ActivateTrialOfflineA(filePath);
            }
            switch (status)
            {
                case LexStatusCodes.LA_OK:
                    return LexStatusCodes.LA_OK;
                case LexStatusCodes.LA_TRIAL_EXPIRED:
                    return LexStatusCodes.LA_TRIAL_EXPIRED;
                case LexStatusCodes.LA_FAIL:
                    return LexStatusCodes.LA_FAIL;
                default:
                    throw new LexActivatorException(status);
            }
        }

        /// <summary>
        /// Generates the offline trial activation request needed for generating
        /// offline trial activation response in the dashboard.
        /// </summary>
        /// <param name="filePath">path of the file for the offline request</param>
        public static void GenerateOfflineTrialActivationRequest(string filePath)
        {
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.GenerateOfflineTrialActivationRequest_x86(filePath) : LexActivatorNative.GenerateOfflineTrialActivationRequest(filePath);
            }
            else
            {
                status = LexActivatorNative.GenerateOfflineTrialActivationRequestA(filePath);
            }
            if (LexStatusCodes.LA_OK != status)
            {
                throw new LexActivatorException(status);
            }
        }

        /// <summary>
        /// It verifies whether trial has started and is genuine or not. The
        /// verification is done locally by verifying the cryptographic digital signature
        /// fetched at the time of trial activation.
        /// 
        /// This function must be called on every start of your program during the trial period.
        /// </summary>
        /// <returns>LA_OK, LA_TRIAL_EXPIRED, LA_FAIL</returns>
        public static int IsTrialGenuine()
        {
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.IsTrialGenuine_x86() : LexActivatorNative.IsTrialGenuine();
            }
            else
            {
                status =  LexActivatorNative.IsTrialGenuine();
            }
            switch (status)
            {
                case LexStatusCodes.LA_OK:
                    return LexStatusCodes.LA_OK;
                case LexStatusCodes.LA_TRIAL_EXPIRED:
                    return LexStatusCodes.LA_TRIAL_EXPIRED;
                case LexStatusCodes.LA_FAIL:
                    return LexStatusCodes.LA_FAIL;
                default:
                    throw new LexActivatorException(status);
            }
        }

        /// <summary>
        /// Starts the local (unverified) trial.
        /// 
        /// This function should be executed when your application starts first time on
        /// the user's computer.
        /// </summary>
        /// <param name="trialLength">trial length in days</param>
        /// <returns>LA_OK, LA_LOCAL_TRIAL_EXPIRED, LA_FAIL</returns>
        public static int ActivateLocalTrial(uint trialLength)
        {
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.ActivateLocalTrial_x86(trialLength) : LexActivatorNative.ActivateLocalTrial(trialLength);
            }
            else
            {
                status =  LexActivatorNative.ActivateLocalTrial(trialLength);
            }
            switch (status)
            {
                case LexStatusCodes.LA_OK:
                    return LexStatusCodes.LA_OK;
                case LexStatusCodes.LA_LOCAL_TRIAL_EXPIRED:
                    return LexStatusCodes.LA_LOCAL_TRIAL_EXPIRED;
                case LexStatusCodes.LA_FAIL:
                    return LexStatusCodes.LA_FAIL;
                default:
                    throw new LexActivatorException(status);
            }
        }

        /// <summary>
        /// It verifies whether trial has started and is genuine or not. The
        /// verification is done locally.
        /// 
        /// This function must be called on every start of your program during the trial period.
        /// 
        /// NOTE: The function is only meant for local (unverified) trials.
        /// </summary>
        /// <returns>LA_OK, LA_LOCAL_TRIAL_EXPIRED, LA_FAIL</returns>
        public static int IsLocalTrialGenuine()
        {
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.IsLocalTrialGenuine_x86() : LexActivatorNative.IsLocalTrialGenuine();
            }
            else
            {
                status =  LexActivatorNative.IsLocalTrialGenuine();
            }
            switch (status)
            {
                case LexStatusCodes.LA_OK:
                    return LexStatusCodes.LA_OK;
                case LexStatusCodes.LA_LOCAL_TRIAL_EXPIRED:
                    return LexStatusCodes.LA_LOCAL_TRIAL_EXPIRED;
                case LexStatusCodes.LA_FAIL:
                    return LexStatusCodes.LA_FAIL;
                default:
                    throw new LexActivatorException(status);
            }
        }

        /// <summary>
        /// Extends the local trial.
        /// 
        /// NOTE: The function is only meant for local (unverified) trials.
        /// </summary>
        /// <param name="trialExtensionLength">number of days to extend the trial</param>
        /// <returns>LA_OK, LA_FAIL</returns>
        public static int ExtendLocalTrial(uint trialExtensionLength)
        {
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.ExtendLocalTrial_x86(trialExtensionLength) : LexActivatorNative.ExtendLocalTrial(trialExtensionLength);
            }
            else
            {
                status =  LexActivatorNative.ExtendLocalTrial(trialExtensionLength);
            }
            switch (status)
            {
                case LexStatusCodes.LA_OK:
                    return LexStatusCodes.LA_OK;
                case LexStatusCodes.LA_FAIL:
                    return LexStatusCodes.LA_FAIL;
                default:
                    throw new LexActivatorException(status);
            }
        }

        /// <summary>
        /// Increments the meter attribute uses of the activation.
        /// </summary>
        /// <param name="name">name of the meter attribute</param>
        /// <param name="increment">the increment value</param>
        public static void IncrementActivationMeterAttributeUses(string name, uint increment)
        {
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.IncrementActivationMeterAttributeUses_x86(name, increment) : LexActivatorNative.IncrementActivationMeterAttributeUses(name, increment);
            }
            else
            {
                status = LexActivatorNative.IncrementActivationMeterAttributeUsesA(name, increment);
            }
            if (LexStatusCodes.LA_OK != status)
            {
                throw new LexActivatorException(status);
            }
        }

        /// <summary>
        /// Decrements the meter attribute uses of the activation.
        /// </summary>
        /// <param name="name">name of the meter attribute</param>
        /// <param name="decrement">the decrement value</param>
        public static void DecrementActivationMeterAttributeUses(string name, uint decrement)
        {
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.DecrementActivationMeterAttributeUses_x86(name, decrement) : LexActivatorNative.DecrementActivationMeterAttributeUses(name, decrement);
            }
            else
            {
                status = LexActivatorNative.DecrementActivationMeterAttributeUsesA(name, decrement);
            }
            if (LexStatusCodes.LA_OK != status)
            {
                throw new LexActivatorException(status);
            }
        }

        /// <summary>
        /// Resets the meter attribute uses consumed by the activation.
        /// </summary>
        /// <param name="name">name of the meter attribute</param>
        public static void ResetActivationMeterAttributeUses(string name)
        {
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.ResetActivationMeterAttributeUses_x86(name) : LexActivatorNative.ResetActivationMeterAttributeUses(name);
            }
            else
            {
                status = LexActivatorNative.ResetActivationMeterAttributeUsesA(name);
            }
            if (LexStatusCodes.LA_OK != status)
            {
                throw new LexActivatorException(status);
            }
        }

        /// <summary>
        /// Resets the activation and trial data stored in the machine.
        /// 
        /// This function is meant for developer testing only.
        /// 
        /// NOTE: The function does not reset local (unverified) trial data.
        /// </summary>
        public static void Reset()
        {
            int status;
            if (LexActivatorNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexActivatorNative.Reset_x86() : LexActivatorNative.Reset();
            }
            else
            {
                status =  LexActivatorNative.Reset();
            }
            if (LexStatusCodes.LA_OK != status)
            {
                throw new LexActivatorException(status);
            }
        }
    }
}
