using System;
using Cryptlex;

namespace Sample
{
    class Program
    {
        static void Init()
        {
            // LexActivator.SetProductFile ("ABSOLUTE_PATH_OF_PRODUCT.DAT_FILE");
            LexActivator.SetProductData("PASTE_CONTENT_OF_PRODUCT.DAT_FILE");
            LexActivator.SetProductId("PASTE_PRODUCT_ID", LexActivator.PermissionFlags.LA_USER);
            LexActivator.SetAppVersion("PASTE_YOUR_APP_VERION");
        }

        static void Activate()
        {
            LexActivator.SetLicenseKey("PASTE_LICENSE_KEY");
            LexActivator.SetActivationMetadata("key1", "value1");
            int status = LexActivator.ActivateLicense();
            if (LexStatusCodes.LA_OK == status || LexStatusCodes.LA_EXPIRED == status || LexStatusCodes.LA_SUSPENDED == status)
            {
                Console.WriteLine("License activated successfully: ", status);
            }
            else
            {
                Console.WriteLine("License activation failed: ", status);
            }
        }
        static void Main(string[] args)
        {
            try
            {
                Init();
                LexActivator.SetLicenseCallback(LicenseCallback);
                int status = LexActivator.IsLicenseGenuine();
                if (LexStatusCodes.LA_OK == status)
                {
                    Console.WriteLine("License is genuinely activated!");
                    uint expiryDate = LexActivator.GetLicenseExpiryDate();
                    int daysLeft = (int)(expiryDate - DateTimeOffset.Now.ToUnixTimeSeconds()) / 86400;
                    Console.WriteLine("Days left:", daysLeft);

                    // Checking for software release update
                    // LexActivator.CheckForReleaseUpdate("windows", "1.0.0", "stable", SoftwareReleaseUpdateCallback);
                }
                else if (LexStatusCodes.LA_EXPIRED == status)
                {
                    Console.WriteLine("License is genuinely activated but has expired!");
                }
                else if (LexStatusCodes.LA_GRACE_PERIOD_OVER == status)
                {
                    Console.WriteLine("License is genuinely activated but grace period is over!");
                }
                else if (LexStatusCodes.LA_SUSPENDED == status)
                {
                    Console.WriteLine("License is genuinely activated but has been suspended!");
                }
                else
                {
                    int trialStatus;
                    trialStatus = LexActivator.IsTrialGenuine();
                    if (LexStatusCodes.LA_OK == trialStatus)
                    {
                        uint trialExpiryDate = LexActivator.GetTrialExpiryDate();
                        int daysLeft = (int)(trialExpiryDate - DateTimeOffset.Now.ToUnixTimeSeconds()) / 86400;
                        Console.WriteLine("Trial days left: " + daysLeft);
                    }
                    else if (LexStatusCodes.LA_TRIAL_EXPIRED == trialStatus)
                    {
                        Console.WriteLine("Trial has expired!");

                        // Time to buy the product key and activate the app
                        Activate();
                    }
                    else
                    {
                        Console.WriteLine("Either trial has not started or has been tampered!");
                        // Activating the trial
                        trialStatus = LexActivator.ActivateTrial(); // Ideally on a button click inside a dialog
                        if (LexStatusCodes.LA_OK == trialStatus)
                        {
                            uint trialExpiryDate = LexActivator.GetTrialExpiryDate();
                            int daysLeft = (int)(trialExpiryDate - DateTimeOffset.Now.ToUnixTimeSeconds()) / 86400;
                            Console.WriteLine("Trial days left: " + daysLeft);
                        }
                        else
                        {
                            // Trial was tampered or has expired
                            Console.WriteLine("Trial activation failed: " + trialStatus);
                        }
                    }
                }

            }
            catch (LexActivatorException ex)
            {
                Console.WriteLine("Error code: " + ex.Code.ToString() + " Error message: " + ex.Message);
            }
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        // License callback is invoked when LexActivator.IsLicenseGenuine() completes a server sync
        static void LicenseCallback(uint status)
        {
            // NOTE: Don't invoke IsLicenseGenuine(), ActivateLicense() or ActivateTrial() API functions in this callback
            switch (status)
            {
                case LexStatusCodes.LA_SUSPENDED:
                    Console.WriteLine("The license has been suspended.");
                    break;
                default:
                    Console.WriteLine("License status code: " + status.ToString());
                    break;
            }
        }

        // Software release update callback is invoked when CheckForReleaseUpdate() gets a response from the server
        static void SoftwareReleaseUpdateCallback(uint status)
        {
            switch (status)
            {
                case LexStatusCodes.LA_RELEASE_UPDATE_AVAILABLE:
                    Console.WriteLine("An update is available for the app.");
                    break;
                case LexStatusCodes.LA_RELEASE_NO_UPDATE_AVAILABLE:
                    // Current version is already latest.
                    break;
                default:
                    Console.WriteLine("Release status code: " + status.ToString());
                    break;
            }
        }
    }
}
