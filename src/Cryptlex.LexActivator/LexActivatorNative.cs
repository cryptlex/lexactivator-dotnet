using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Cryptlex
{
    static class LexActivatorNative
    {
        private const string DLL_FILE_NAME_X86 = "LexActivator32";
        private const string DLL_FILE_NAME_X64 = "LexActivator";


        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetProductFile(string filePath);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetProductData(string productData);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetProductId(string productId, LexActivator.PermissionFlags flags);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetLicenseKey(string licenseKey);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetLicenseUserCredential(string email, string password);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetLicenseCallback(LexActivator.CallbackType callback);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetActivationMetadata(string key, string value);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetTrialActivationMetadata(string key, string value);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetAppVersion(string appVersion);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetNetworkProxy(string proxy);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetProductMetadata(string key, StringBuilder value, int length);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLicenseMetadata(string key, StringBuilder value, int length);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLicenseMeterAttribute(string name, ref uint allowedUses, ref uint totalUses);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLicenseKey(StringBuilder licenseKey, int length);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLicenseExpiryDate(ref uint expiryDate);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLicenseUserEmail(StringBuilder email, int length);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLicenseUserName(StringBuilder name, int length);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLicenseUserCompany(StringBuilder company, int length);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLicenseUserMetadata(string key, StringBuilder value, int length);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLicenseType(StringBuilder licenseType, int length);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetActivationMetadata(string key, StringBuilder value, int length);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetActivationMeterAttributeUses(string name, ref uint uses);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetServerSyncGracePeriodExpiryDate(ref uint expiryDate);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetTrialActivationMetadata(string key, StringBuilder value, int length);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetTrialExpiryDate(ref uint trialExpiryDate);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetTrialId(StringBuilder trialId, int length);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLocalTrialExpiryDate(ref uint trialExpiryDate);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int CheckForReleaseUpdate(string platform, string version, string channel, LexActivator.CallbackType callback);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ActivateLicense();

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ActivateLicenseOffline(string filePath);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GenerateOfflineActivationRequest(string filePath);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int DeactivateLicense();

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GenerateOfflineDeactivationRequest(string filePath);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int IsLicenseGenuine();

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int IsLicenseValid();

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ActivateTrial();

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ActivateTrialOffline(string filePath);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GenerateOfflineTrialActivationRequest(string filePath);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int IsTrialGenuine();

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ActivateLocalTrial(uint trialLength);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int IsLocalTrialGenuine();

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ExtendLocalTrial(uint trialExtensionLength);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int IncrementActivationMeterAttributeUses(string name, uint increment);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int DecrementActivationMeterAttributeUses(string name, uint decrement);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ResetActivationMeterAttributeUses(string name);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Reset();



        [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "SetProductFile", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetProductFile_x64(string filePath);

        [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "SetProductData", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetProductData_x64(string productData);

        [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "SetProductId", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetProductId_x64(string productId, LexActivator.PermissionFlags flags);

        [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "SetLicenseKey", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetLicenseKey_x64(string licenseKey);

        [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "SetLicenseUserCredential", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetLicenseUserCredential_x64(string email, string password);

        [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "SetLicenseCallback", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetLicenseCallback_x64(LexActivator.CallbackType callback);

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
        public static extern int CheckForReleaseUpdate_x64(string platform, string version, string channel, LexActivator.CallbackType callback);

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
    }
}
