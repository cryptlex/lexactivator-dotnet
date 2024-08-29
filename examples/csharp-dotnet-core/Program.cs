using System;
using System.Collections.Generic;
using Cryptlex;

namespace Sample
{
    class Program
    {
        static void Init()
        {
            // LexActivator.SetProductFile ("ABSOLUTE_PATH_OF_PRODUCT.DAT_FILE");
            LexActivator.SetProductData("OTRGRkZERUUxQTI3N0MxRjRDOUJENTA5NzFGNDdDOTA=.Bs0xRO7X8Vyvlb1xwwcZrnjDRSkJCQLZRoUbywxpZ6AF9iAl4dJKmsDZv1bxQdPA3RzbwMzf2B6b4NdPCEb4JXYxgVuyFluYXFyrTeleGgXlEko3UyzDLz/nnRgKzhhf+MnFfDzumXjtVfzLqHYKciIQeojarD//Ez6g+mDT15Qmh33mPrRBZKHJJPTB4VetX1R0ioFpvKPLnQPgA9strh5X7HUf0K4u6UEZlI4rY6SIAnnCi5+Eqhs4jWS54sdB0YVYqYBU0ZVY63gTUh1bF69LI1YnaWJqadbRX2DR6JqivSM2YvEd85AM49Ayn8jnrx/ZlmRw6qmOOOdcEdGtnqB3ZNgyqET2yRanzpvMkGmjh09TomOLfxGkrFVJS69bY3IhNwoZ0lsgQfJ/dfDdBdEK44ypVwnwEekUA+RrS1R8FzHrP8x7YU2b4DQ/vnGHmS/yW2VBoWDdQ3bXZTWEckpLfPeepsSwiFrxBnGIAiVwUwRGXibJ5wnvOPwus09Vruo2HGEDC22yh1kH/csk4WC3nohOcKOX1FbDTbYd9E9XECP+JHGthlirHpnvtYMvqlzeVC3tvVIQx1XcebJn0RHHxMrLXkWmazU+LnbR4TwuSKXmzZ/D/Vnb0G+ljdCTrK4XQ1IZ9rLQH3r6w3YNCU2nhshdk8YadbcVnufumJkwAPxAKxr3e/Lx3cozKeisVAkiQApdhIUL/rORj4UddVlC7tCGj84pvJRHhqINgT1G+u6tRJ9d0LQs1iEZ9hIp0l58278tRsKtdt3R3+x9mQRyWgyEP6oYTV09kWqxt3Vo1E/wxez1KiK4WtFj1Fox");
            LexActivator.SetProductId("de5c962f-75b0-417d-a72a-5b474b8cb4bb", LexActivator.PermissionFlags.LA_USER);
            LexActivator.SetReleaseVersion("1.0.0");  // Set this to the release version of your app
        }

        static void Activate()
        {
            LexActivator.SetLicenseKey("064DA0-12BB7F-4EA489-F13C48-742ACE-FFFB2C");
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
        static void DisplayUserLicenses(List<UserLicense> licenses)
{
    foreach (var license in licenses)
    {
        Console.WriteLine($"Key: {license.Key}");
        Console.WriteLine($"Allowed Activations: {license.AllowedActivations}");
        Console.WriteLine($"Allowed Deactivations: {license.AllowedDeactivations}");
        Console.WriteLine($"Type: {license.Type}");

        // Display metadata in the desired format
        Console.Write("Metadata: [");
        for (int i = 0; i < license.Metadata.Count; i++)
        {
            var metadata = license.Metadata[i];
            Console.Write($"{{ key: \"{metadata.Key}\", length : \"{metadata.length}\", value: \"{metadata.Value}\" }}");

            // Add a comma between metadata items, but not after the last item
            if (i < license.Metadata.Count - 1)
            {
                Console.Write(", ");
            }
        }
        Console.WriteLine("]"); // Close the array-like output

        Console.WriteLine(); // Add a blank line between licenses for readability
    }
}

        static void Main(string[] args)
        {
            try
            {
                Init();
                LexActivator.SetLicenseCallback(LicenseCallback);
                int status = LexActivator.AuthenticateUser("muneebkq05+01@gmail.com","qwerty@123");
                if (LexStatusCodes.LA_OK == status)
                {
                    Console.WriteLine("user autheticated successfully");
                    List<UserLicense> userLicenses = LexActivator.GetUserLicenses();
                    DisplayUserLicenses(userLicenses);
                    
                }
                else{
                    Console.WriteLine("user not autheticated successfully");
                }
                List<UserLicense> userlicenses = LexActivator.GetUserLicenses();
                    Console.WriteLine("user autheticated successfully: ",userlicenses);
                status = LexActivator.IsLicenseGenuine();
                if (LexStatusCodes.LA_OK == status)
                {
                    Console.WriteLine("License is genuinely activated!");
                    uint expiryDate = LexActivator.GetLicenseExpiryDate();
                    int daysLeft = (int)(expiryDate - DateTimeOffset.Now.ToUnixTimeSeconds()) / 86400;
                    Console.WriteLine("Days left:" + daysLeft);

                    // Checking for software release update
                    // LexActivator.CheckReleaseUpdate(SoftwareReleaseUpdateCallback, LexActivator.ReleaseFlags.LA_RELEASES_ALL, null);
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
                    Activate();
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

        // Software release update callback is invoked when CheckReleaseUpdate() gets a response from the server
        static void SoftwareReleaseUpdateCallback(uint status, Release release, object userData)
        {
            switch (status)
            {
                case LexStatusCodes.LA_RELEASE_UPDATE_AVAILABLE:
                    Console.WriteLine("A new update is available for the app!");
                    Console.WriteLine("Release Notes: "+ release.Notes);
                    break;
                case LexStatusCodes.LA_RELEASE_UPDATE_AVAILABLE_NOT_ALLOWED:
                    Console.WriteLine("A new update is available for the app but it's not allowed!");
                    Console.WriteLine("Release: "+ release.Notes);
                    break;
                case LexStatusCodes.LA_RELEASE_UPDATE_NOT_AVAILABLE:
                    Console.WriteLine("Current version is already latest!");
                    break;
                default:
                    Console.WriteLine("Error code: " + status.ToString());
                    break;
            }
        }
    }
}
