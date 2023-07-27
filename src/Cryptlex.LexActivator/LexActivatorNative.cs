using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Cryptlex
{
    static class LexActivatorNative
    {
        private const string DLL_FILE_NAME = "LexActivator";
        private const string DLL_FILE_NAME_X86 = "LexActivator32";

        public static bool IsWindows()
        {
#if NETFRAMEWORK
            return Environment.OSVersion.Platform == PlatformID.Win32NT;
#else
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
#endif
        }


        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetProductFile(string filePath);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "SetProductFile", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetProductFileA(string filePath);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetProductData(string productData);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "SetProductData", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetProductDataA(string productData);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetProductId(string productId, LexActivator.PermissionFlags flags);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "SetProductId", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetProductIdA(string productId, LexActivator.PermissionFlags flags);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetDataDirectory(string directoryPath);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "SetDataDirectory", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetDataDirectoryA(string directoryPath);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetDebugMode(uint enable);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetCustomDeviceFingerprint(string fingerprint);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "SetCustomDeviceFingerprint", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetCustomDeviceFingerprintA(string fingerprint);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetLicenseKey(string licenseKey);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "SetLicenseKey", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetLicenseKeyA(string licenseKey);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetLicenseUserCredential(string email, string password);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "SetLicenseUserCredential", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetLicenseUserCredentialA(string email, string password);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetLicenseCallback(LexActivator.CallbackType callback);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetActivationLeaseDuration(uint leaseDuration);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetActivationMetadata(string key, string value);
        
        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "SetActivationMetadata", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetActivationMetadataA(string key, string value);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetTrialActivationMetadata(string key, string value);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "SetTrialActivationMetadata", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetTrialActivationMetadataA(string key, string value);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetAppVersion(string appVersion);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "SetAppVersion", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetAppVersionA(string appVersion);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetReleaseVersion(string releaseVersion);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "SetReleaseVersion", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetReleaseVersionA(string releaseVersion);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetReleasePublishedDate(uint releasePublishedDate);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetReleasePlatform(string releasePlatform);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "SetReleasePlatform", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetReleasePlatformA(string releasePlatform);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetReleaseChannel(string releaseChannel);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "SetReleaseChannel", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetReleaseChannelA(string releaseChannel);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetOfflineActivationRequestMeterAttributeUses(string name, uint uses);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "SetOfflineActivationRequestMeterAttributeUses", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetOfflineActivationRequestMeterAttributeUsesA(string name, uint uses);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetNetworkProxy(string proxy);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "SetNetworkProxy", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetNetworkProxyA(string proxy);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetCryptlexHost(string host);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "SetCryptlexHost", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetCryptlexHostA(string host);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetProductMetadata(string key, StringBuilder value, int length);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "GetProductMetadata", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetProductMetadataA(string key, StringBuilder value, int length);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetProductVersionName(StringBuilder name, int length);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "GetProductVersionName", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetProductVersionNameA(StringBuilder name, int length);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetProductVersionDisplayName(StringBuilder displayName, int length);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "GetProductVersionDisplayName", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetProductVersionDisplayNameA(StringBuilder displayName, int length);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetProductVersionFeatureFlag(string name, ref uint enabled, StringBuilder data, int length);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "GetProductVersionFeatureFlag", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetProductVersionFeatureFlagA(string name, ref uint enabled, StringBuilder data, int length);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLicenseMetadata(string key, StringBuilder value, int length);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "GetLicenseMetadata", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLicenseMetadataA(string key, StringBuilder value, int length);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLicenseMeterAttribute(string name, ref uint allowedUses, ref uint totalUses, ref uint grossUses);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "GetLicenseMeterAttribute", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLicenseMeterAttributeA(string name, ref uint allowedUses, ref uint totalUses, ref uint grossUses);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLicenseKey(StringBuilder licenseKey, int length);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "GetLicenseKey", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLicenseKeyA(StringBuilder licenseKey, int length);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLicenseAllowedActivations(ref uint allowedActivations);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLicenseTotalActivations(ref uint totalActivations);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLicenseExpiryDate(ref uint expiryDate);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLicenseMaintenanceExpiryDate(ref uint maintenanceExpiryDate);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLicenseMaxAllowedReleaseVersion(StringBuilder maxAllowedReleaseVersion, int length);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "GetLicenseMaxAllowedReleaseVersion", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLicenseMaxAllowedReleaseVersionA(StringBuilder maxAllowedReleaseVersion, int length);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLicenseUserEmail(StringBuilder email, int length);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "GetLicenseUserEmail", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLicenseUserEmailA(StringBuilder email, int length);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLicenseUserName(StringBuilder name, int length);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "GetLicenseUserName", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLicenseUserNameA(StringBuilder name, int length);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLicenseUserCompany(StringBuilder company, int length);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "GetLicenseUserCompany", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLicenseUserCompanyA(StringBuilder company, int length);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLicenseUserMetadata(string key, StringBuilder value, int length);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "GetLicenseUserMetadata", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLicenseUserMetadataA(string key, StringBuilder value, int length);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLicenseOrganizationName(StringBuilder organizationName, int length);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "GetLicenseOrganizationName", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLicenseOrganizationNameA(StringBuilder organizationName, int length);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetUserLicensesInternal(StringBuilder userLicensesJson, int length);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "GetUserLicensesInternal", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetUserLicensesInternalA(StringBuilder userLicensesJson, int length);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLicenseOrganizationAddressInternal(StringBuilder jsonAddress, int length);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "GetLicenseOrganizationAddressInternal", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLicenseOrganizationAddressInternalA(StringBuilder jsonAddress, int length);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLicenseType(StringBuilder licenseType, int length);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "GetLicenseType", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLicenseTypeA(StringBuilder licenseType, int length);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetActivationMetadata(string key, StringBuilder value, int length);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "GetActivationMetadata", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetActivationMetadataA(string key, StringBuilder value, int length);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetActivationMode(StringBuilder initialMode, int initialModeLength, StringBuilder currentMode, int currentModeLength);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "GetActivationMode", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetActivationModeA(StringBuilder initialMode, int initialModeLength, StringBuilder currentMode, int currentModeLength);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetActivationMeterAttributeUses(string name, ref uint uses);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "GetActivationMeterAttributeUses", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetActivationMeterAttributeUsesA(string name, ref uint uses);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetServerSyncGracePeriodExpiryDate(ref uint expiryDate);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetTrialActivationMetadata(string key, StringBuilder value, int length);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "GetTrialActivationMetadata", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetTrialActivationMetadataA(string key, StringBuilder value, int length);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetTrialExpiryDate(ref uint trialExpiryDate);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetTrialId(StringBuilder trialId, int length);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "GetTrialId", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetTrialIdA(StringBuilder trialId, int length);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLocalTrialExpiryDate(ref uint trialExpiryDate);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLibraryVersion(StringBuilder libraryVersion, int length);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "GetLibraryVersion", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLibraryVersionA(StringBuilder libraryVersion, int length);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int CheckReleaseUpdateInternal(LexActivator.InternalReleaseCallbackType internalReleaseCallback, LexActivator.ReleaseFlags releaseFlags, IntPtr _userData);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "CheckReleaseUpdateInternal", CallingConvention = CallingConvention.Cdecl)]
        public static extern int CheckReleaseUpdateInternalA(LexActivator.InternalReleaseCallbackAType internalReleaseCallbackA, LexActivator.ReleaseFlags releaseFlags, IntPtr _userData);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int CheckForReleaseUpdate(string platform, string version, string channel, LexActivator.CallbackType callback);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "CheckForReleaseUpdate", CallingConvention = CallingConvention.Cdecl)]
        public static extern int CheckForReleaseUpdateA(string platform, string version, string channel, LexActivator.CallbackType callback);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AuthenticateUser(string email, string password);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "AuthenticateUser", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AuthenticateUserA(string email, string password);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ActivateLicense();

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ActivateLicenseOffline(string filePath);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "ActivateLicenseOffline", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ActivateLicenseOfflineA(string filePath);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GenerateOfflineActivationRequest(string filePath);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "GenerateOfflineActivationRequest", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GenerateOfflineActivationRequestA(string filePath);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int DeactivateLicense();

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GenerateOfflineDeactivationRequest(string filePath);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "GenerateOfflineDeactivationRequest", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GenerateOfflineDeactivationRequestA(string filePath);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int IsLicenseGenuine();

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int IsLicenseValid();

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ActivateTrial();

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ActivateTrialOffline(string filePath);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "ActivateTrialOffline", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ActivateTrialOfflineA(string filePath);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GenerateOfflineTrialActivationRequest(string filePath);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "GenerateOfflineTrialActivationRequest", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GenerateOfflineTrialActivationRequestA(string filePath);

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

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "IncrementActivationMeterAttributeUses", CallingConvention = CallingConvention.Cdecl)]
        public static extern int IncrementActivationMeterAttributeUsesA(string name, uint increment);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int DecrementActivationMeterAttributeUses(string name, uint decrement);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "DecrementActivationMeterAttributeUses", CallingConvention = CallingConvention.Cdecl)]
        public static extern int DecrementActivationMeterAttributeUsesA(string name, uint decrement);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ResetActivationMeterAttributeUses(string name);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "ResetActivationMeterAttributeUses", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ResetActivationMeterAttributeUsesA(string name);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Reset();



        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "SetProductFile", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetProductFile_x86(string filePath);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "SetProductData", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetProductData_x86(string productData);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "SetProductId", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetProductId_x86(string productId, LexActivator.PermissionFlags flags);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "SetDataDirectory", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetDataDirectory_x86(string directoryPath);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "SetDebugMode", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetDebugMode_x86(uint enable);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "SetCustomDeviceFingerprint", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetCustomDeviceFingerprint_x86(string fingerprint);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "SetLicenseKey", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetLicenseKey_x86(string licenseKey);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "SetLicenseUserCredential", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetLicenseUserCredential_x86(string email, string password);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "AuthenticateUser", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AuthenticateUser_x86(string email, string password);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "SetLicenseCallback", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetLicenseCallback_x86(LexActivator.CallbackType callback);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "SetActivationLeaseDuration", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetActivationLeaseDuration_x86(uint leaseDuration);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "SetActivationMetadata", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetActivationMetadata_x86(string key, string value);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "SetTrialActivationMetadata", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetTrialActivationMetadata_x86(string key, string value);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "SetAppVersion", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetAppVersion_x86(string appVersion);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "SetReleaseVersion", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetReleaseVersion_x86(string releaseVersion);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "SetReleasePublishedDate", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetReleasePublishedDate_x86(uint releasePublishedDate);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "SetReleasePlatform", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetReleasePlatform_x86(string releasePlatform);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "SetReleaseChannel", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetReleaseChannel_x86(string releaseChannel);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "SetOfflineActivationRequestMeterAttributeUses", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetOfflineActivationRequestMeterAttributeUses_x86(string name, uint uses);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "SetNetworkProxy", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetNetworkProxy_x86(string proxy);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "SetCryptlexHost", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetCryptlexHost_x86(string host);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "GetProductMetadata", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetProductMetadata_x86(string key, StringBuilder value, int length);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "GetProductVersionName", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetProductVersionName_x86(StringBuilder name, int length);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "GetProductVersionDisplayName", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetProductVersionDisplayName_x86(StringBuilder displayName, int length);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "GetProductVersionFeatureFlag", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetProductVersionFeatureFlag_x86(string name, ref uint enabled, StringBuilder data, int length);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "GetLicenseMetadata", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLicenseMetadata_x86(string key, StringBuilder value, int length);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "GetLicenseMeterAttribute", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLicenseMeterAttribute_x86(string name, ref uint allowedUses, ref uint totalUses, ref uint grossUses);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "GetLicenseKey", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLicenseKey_x86(StringBuilder licenseKey, int length);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "GetLicenseAllowedActivations", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLicenseAllowedActivations_x86(ref uint allowedActivations);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "GetLicenseTotalActivations", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLicenseTotalActivations_x86(ref uint totalActivations);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "GetLicenseExpiryDate", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLicenseExpiryDate_x86(ref uint expiryDate);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "GetLicenseMaintenanceExpiryDate", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLicenseMaintenanceExpiryDate_x86(ref uint maintenanceExpiryDate);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "GetLicenseMaxAllowedReleaseVersion", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLicenseMaxAllowedReleaseVersion_x86(StringBuilder maxAllowedReleaseVersion, int length);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "GetLicenseUserEmail", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLicenseUserEmail_x86(StringBuilder email, int length);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "GetLicenseUserName", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLicenseUserName_x86(StringBuilder name, int length);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "GetLicenseUserCompany", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLicenseUserCompany_x86(StringBuilder company, int length);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "GetLicenseUserMetadata", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLicenseUserMetadata_x86(string key, StringBuilder value, int length);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "GetLicenseOrganizationName", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLicenseOrganizationName_x86(StringBuilder organizationName, int length);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "GetLicenseOrganizationAddressInternal", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLicenseOrganizationAddressInternal_x86(StringBuilder userLicensesJson, int length);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "GetUserLicensesInternal", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetUserLicensesInternal_x86(StringBuilder userLicensesJson, int length);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "GetLicenseType", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLicenseType_x86(StringBuilder licenseType, int length);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "GetActivationMetadata", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetActivationMetadata_x86(string key, StringBuilder value, int length);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "GetActivationMode", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetActivationMode_x86(StringBuilder initialMode, int initialModeLength, StringBuilder currentMode, int currentModeLength);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "GetActivationMeterAttributeUses", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetActivationMeterAttributeUses_x86(string name, ref uint uses);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "GetServerSyncGracePeriodExpiryDate", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetServerSyncGracePeriodExpiryDate_x86(ref uint expiryDate);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "GetTrialActivationMetadata", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetTrialActivationMetadata_x86(string key, StringBuilder value, int length);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "GetTrialExpiryDate", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetTrialExpiryDate_x86(ref uint trialExpiryDate);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "GetTrialId", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetTrialId_x86(StringBuilder trialId, int length);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "GetLocalTrialExpiryDate", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLocalTrialExpiryDate_x86(ref uint trialExpiryDate);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "GetLibraryVersion", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLibraryVersion_x86(StringBuilder libraryVersion, int length);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "CheckReleaseUpdateInternal", CallingConvention = CallingConvention.Cdecl)]
        public static extern int CheckReleaseUpdateInternal_x86(LexActivator.InternalReleaseCallbackType internalReleaseCallback, LexActivator.ReleaseFlags releaseFlags, IntPtr _userData);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "CheckForReleaseUpdate", CallingConvention = CallingConvention.Cdecl)]
        public static extern int CheckForReleaseUpdate_x86(string platform, string version, string channel, LexActivator.CallbackType callback);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "ActivateLicense", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ActivateLicense_x86();

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "ActivateLicenseOffline", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ActivateLicenseOffline_x86(string filePath);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "GenerateOfflineActivationRequest", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GenerateOfflineActivationRequest_x86(string filePath);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "DeactivateLicense", CallingConvention = CallingConvention.Cdecl)]
        public static extern int DeactivateLicense_x86();

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "GenerateOfflineDeactivationRequest", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GenerateOfflineDeactivationRequest_x86(string filePath);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "IsLicenseGenuine", CallingConvention = CallingConvention.Cdecl)]
        public static extern int IsLicenseGenuine_x86();

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "IsLicenseValid", CallingConvention = CallingConvention.Cdecl)]
        public static extern int IsLicenseValid_x86();

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "ActivateTrial", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ActivateTrial_x86();

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "ActivateTrialOffline", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ActivateTrialOffline_x86(string filePath);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "GenerateOfflineTrialActivationRequest", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GenerateOfflineTrialActivationRequest_x86(string filePath);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "IsTrialGenuine", CallingConvention = CallingConvention.Cdecl)]
        public static extern int IsTrialGenuine_x86();

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "ActivateLocalTrial", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ActivateLocalTrial_x86(uint trialLength);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "IsLocalTrialGenuine", CallingConvention = CallingConvention.Cdecl)]
        public static extern int IsLocalTrialGenuine_x86();

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "ExtendLocalTrial", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ExtendLocalTrial_x86(uint trialExtensionLength);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "IncrementActivationMeterAttributeUses", CallingConvention = CallingConvention.Cdecl)]
        public static extern int IncrementActivationMeterAttributeUses_x86(string name, uint increment);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "DecrementActivationMeterAttributeUses", CallingConvention = CallingConvention.Cdecl)]
        public static extern int DecrementActivationMeterAttributeUses_x86(string name, uint decrement);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "ResetActivationMeterAttributeUses", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ResetActivationMeterAttributeUses_x86(string name);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "Reset", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Reset_x86();
    }
}
