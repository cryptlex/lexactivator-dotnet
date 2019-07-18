using System;
using System.Text;
using System.Windows.Forms;
using Cryptlex;

namespace Sample
{
    public partial class ActivationForm : Form
    {
        public ActivationForm()
        {
            InitializeComponent();
            int status;
            // status = LexActivator.SetProductFile ("ABSOLUTE_PATH_OF_PRODUCT.DAT_FILE");
            status = LexActivator.SetProductData("PASTE_CONTENT_OF_PRODUCT.DAT_FILE");
            if (status != LexActivator.StatusCodes.LA_OK)
            {
                this.statusLabel.Text = "Error setting product file: " + status.ToString();
                return;
            }
            status = LexActivator.SetProductId("PASTE_PRODUCT_ID", LexActivator.PermissionFlags.LA_USER);
            if (status != LexActivator.StatusCodes.LA_OK)
            {
                this.statusLabel.Text = "Error setting product id: " + status.ToString();
                return;
            }
            // Setting license callback is recommended for floating licenses
            // status = LexActivator.SetLicenseCallback(LicenseCallback);
            // if (status != LexActivator.StatusCodes.LA_OK)
            // {
            // this.statusLabel.Text = "Error setting callback function: " + status.ToString();
            // return;
            // }
            status = LexActivator.IsLicenseGenuine();
            if (status == LexActivator.StatusCodes.LA_OK || status == LexActivator.StatusCodes.LA_EXPIRED || status == LexActivator.StatusCodes.LA_SUSPENDED || status == LexActivator.StatusCodes.LA_GRACE_PERIOD_OVER)
            {
                // uint expiryDate = 0;
                // LexActivator.GetLicenseExpiryDate(ref expiryDate);
                // int daysLeft = (int)(expiryDate - unixTimestamp()) / 86400;
                this.statusLabel.Text = "License genuinely activated! Activation Status: " + status.ToString();
                this.activateBtn.Text = "Deactivate";
                this.activateTrialBtn.Enabled = false;

                // Checking for software release update
                // status = LexActivator.CheckForReleaseUpdate("windows", "1.0.0", "stable", SoftwareReleaseUpdateCallback);
                // if (status != LexActivator.StatusCodes.LA_OK)
                // {
                //     this.statusLabel.Text = "Error checking for software release update: " + status.ToString();
                // }
                return;
            }
            status = LexActivator.IsTrialGenuine();
            if (status == LexActivator.StatusCodes.LA_OK)
            {
                uint trialExpiryDate = 0;
                LexActivator.GetTrialExpiryDate(ref trialExpiryDate);
                int daysLeft = (int)(trialExpiryDate - unixTimestamp()) / 86400;
                this.statusLabel.Text = "Trial period! Days left:" + daysLeft.ToString();
                this.activateTrialBtn.Enabled = false;
            }
            else if (status == LexActivator.StatusCodes.LA_TRIAL_EXPIRED)
            {
                this.statusLabel.Text = "Trial has expired!";
            }
            else
            {
                this.statusLabel.Text = "Trial has not started or has been tampered: " + status.ToString();
            }
        }

        private void activateBtn_Click(object sender, EventArgs e)
        {
            int status;
            if (this.activateBtn.Text == "Deactivate")
            {
                status = LexActivator.DeactivateLicense();
                if (status == LexActivator.StatusCodes.LA_OK)
                {
                    this.statusLabel.Text = "License deactivated successfully";
                    this.activateBtn.Text = "Activate";
                    this.activateTrialBtn.Enabled = true;
                    return;
                }
                this.statusLabel.Text = "Error deactivating license: " + status.ToString();
                return;
            }
            status = LexActivator.SetLicenseKey(productKeyBox.Text);
            if (status != LexActivator.StatusCodes.LA_OK)
            {
                this.statusLabel.Text = "Error setting license key: " + status.ToString();
                return;
            }
            status = LexActivator.SetActivationMetadata("key1", "value1");
            if (status != LexActivator.StatusCodes.LA_OK)
            {
                this.statusLabel.Text = "Error setting activation metadata: " + status.ToString();
                return;
            }
            status = LexActivator.ActivateLicense();
            if (status == LexActivator.StatusCodes.LA_OK || status == LexActivator.StatusCodes.LA_EXPIRED || status == LexActivator.StatusCodes.LA_SUSPENDED)
            {
                this.statusLabel.Text = "Activation Successful :" + status.ToString();
                this.activateBtn.Text = "Deactivate";
                this.activateTrialBtn.Enabled = false;
            }
            else
            {
                this.statusLabel.Text = "Error activating the license: " + status.ToString();
                return;
            }

        }

        private void activateTrialBtn_Click(object sender, EventArgs e)
        {
            int status;
            status = LexActivator.SetTrialActivationMetadata("key2", "value2");
            if (status != LexActivator.StatusCodes.LA_OK)
            {
                this.statusLabel.Text = "Error setting activation metadata: " + status.ToString();
                return;
            }
            status = LexActivator.ActivateTrial();
            if (status != LexActivator.StatusCodes.LA_OK)
            {
                this.statusLabel.Text = "Error activating the trial: " + status.ToString();
                return;
            }
            else
            {
                this.statusLabel.Text = "Trial started Successful";
            }
        }

        // License callback is invoked when LexActivator.IsLicenseGenuine() completes a server sync
        private void LicenseCallback(uint status)
        {
            // NOTE: Don't invoke IsLicenseGenuine(), ActivateLicense() or ActivateTrial() API functions in this callback
            switch (status)
            {
                case LexActivator.StatusCodes.LA_SUSPENDED:
                    this.statusLabel.Text = "The license has been suspended.";
                    break;
                default:
                    this.statusLabel.Text = "License status code: " + status.ToString();
                    break;
            }
        }

        // Software release update callback is invoked when CheckForReleaseUpdate() gets a response from the server
        private void SoftwareReleaseUpdateCallback(uint status)
        {
            switch (status)
            {
                case LexActivator.StatusCodes.LA_RELEASE_UPDATE_AVAILABLE:
                    this.statusLabel.Text = "An update is available for the app.";
                    break;
                case LexActivator.StatusCodes.LA_RELEASE_NO_UPDATE_AVAILABLE:
                    // Current version is already latest.
                    break;
                default:
                    this.statusLabel.Text = "Release status code: " + status.ToString();
                    break;
            }
        }

        private uint unixTimestamp()
        {
            return (uint)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }
    }
}
