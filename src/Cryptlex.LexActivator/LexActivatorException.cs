﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptlex
{
    public class LexActivatorException : Exception
    {
        public int Code;

        public LexActivatorException(int code) : base(GetErrorMessage(code))
        {
            this.Code = code;
        }

        public static string GetErrorMessage(int code)
        {
            switch (code)
            {
                case LexStatusCodes.LA_E_FILE_PATH:
                    return "Invalid file path.";

                case LexStatusCodes.LA_E_PRODUCT_FILE:
                    return "Invalid or corrupted product file.";

                case LexStatusCodes.LA_E_PRODUCT_DATA:
                    return "Invalid product data.";

                case LexStatusCodes.LA_E_PRODUCT_ID:
                    return "The product id is incorrect.";

                case LexStatusCodes.LA_E_SYSTEM_PERMISSION:
                    return "Insufficient system permissions.";

                case LexStatusCodes.LA_E_FILE_PERMISSION:
                    return "No permission to write to file.";

                case LexStatusCodes.LA_E_WMIC:
                    return "Fingerprint couldn't be generated because Windows Management Instrumentation (WMI) service has been disabled.";

                case LexStatusCodes.LA_E_TIME:
                    return "The difference between the network time and the system time is more than allowed clock offset.";

                case LexStatusCodes.LA_E_INET:
                    return "Failed to connect to the server due to network error.";

                case LexStatusCodes.LA_E_NET_PROXY:
                    return "Invalid network proxy.";

                case LexStatusCodes.LA_E_HOST_URL:
                    return "Invalid Cryptlex host url.";

                case LexStatusCodes.LA_E_BUFFER_SIZE:
                    return "The buffer size was smaller than required.";

                case LexStatusCodes.LA_E_APP_VERSION_LENGTH:
                    return "App version length is more than 256 characters.";

                case LexStatusCodes.LA_E_REVOKED:
                    return "The license has been revoked.";

                case LexStatusCodes.LA_E_LICENSE_KEY:
                    return "Invalid license key.";

                case LexStatusCodes.LA_E_LICENSE_TYPE:
                    return "Invalid license type. Make sure floating license is not being used.";

                case LexStatusCodes.LA_E_OFFLINE_RESPONSE_FILE:
                    return "Invalid offline activation response file.";

                case LexStatusCodes.LA_E_OFFLINE_RESPONSE_FILE_EXPIRED:
                    return "The offline activation response has expired.";

                case LexStatusCodes.LA_E_ACTIVATION_LIMIT:
                    return "The license has reached it's allowed activations limit.";

                case LexStatusCodes.LA_E_ACTIVATION_NOT_FOUND:
                    return "The license activation was deleted on the server.";

                case LexStatusCodes.LA_E_DEACTIVATION_LIMIT:
                    return "The license has reached it's allowed deactivations limit.";

                case LexStatusCodes.LA_E_TRIAL_NOT_ALLOWED:
                    return "Trial not allowed for the product.";

                case LexStatusCodes.LA_E_TRIAL_ACTIVATION_LIMIT:
                    return "Your account has reached it's trial activations limit.";

                case LexStatusCodes.LA_E_MACHINE_FINGERPRINT:
                    return "Machine fingerprint has changed since activation.";

                case LexStatusCodes.LA_E_METADATA_KEY_LENGTH:
                    return "Metadata key length is more than 256 characters.";

                case LexStatusCodes.LA_E_METADATA_VALUE_LENGTH:
                    return "Metadata value length is more than 4096 characters.";

                case LexStatusCodes.LA_E_ACTIVATION_METADATA_LIMIT:
                    return "The license has reached it's metadata fields limit.";

                case LexStatusCodes.LA_E_TRIAL_ACTIVATION_METADATA_LIMIT:
                    return "The trial has reached it's metadata fields limit.";

                case LexStatusCodes.LA_E_METADATA_KEY_NOT_FOUND:
                    return "The metadata key does not exist.";

                case LexStatusCodes.LA_E_TIME_MODIFIED:
                    return "The system time has been tampered (backdated).";

                case LexStatusCodes.LA_E_RELEASE_VERSION_FORMAT:
                    return "Invalid version format.";

                case LexStatusCodes.LA_E_AUTHENTICATION_FAILED:
                    return "Incorrect email or password.";

                case LexStatusCodes.LA_E_METER_ATTRIBUTE_NOT_FOUND:
                    return "The meter attribute does not exist.";

                case LexStatusCodes.LA_E_METER_ATTRIBUTE_USES_LIMIT_REACHED:
                    return "The meter attribute has reached it's usage limit.";

                case LexStatusCodes.LA_E_CUSTOM_FINGERPRINT_LENGTH:
                    return "Custom device fingerprint length is less than 64 characters or more than 256 characters.";

                case LexStatusCodes.LA_E_PRODUCT_VERSION_NOT_LINKED:
                    return "No product version is linked with the license.";

                case LexStatusCodes.LA_E_FEATURE_FLAG_NOT_FOUND:
                    return "The product version feature flag does not exist.";

                case LexStatusCodes.LA_E_RELEASE_VERSION_NOT_ALLOWED:
                    return "The release version is not allowed.";

                case LexStatusCodes.LA_E_RELEASE_PLATFORM_LENGTH:
                    return "Release platform length is more than 256 characters.";

                case LexStatusCodes.LA_E_RELEASE_CHANNEL_LENGTH:
                    return "Release channel length is more than 256 characters.";

                case LexStatusCodes.LA_E_VM:
                    return "Application is being run inside a virtual machine / hypervisor, and activation has been disallowed in the VM.";

                case LexStatusCodes.LA_E_COUNTRY:
                    return "Country is not allowed.";

                case LexStatusCodes.LA_E_IP:
                    return "IP address is not allowed.";

                case LexStatusCodes.LA_E_CONTAINER:
                    return "Application is being run inside a container and activation has been disallowed in the container.";

                case LexStatusCodes.LA_E_RELEASE_VERSION:
                    return "Invalid release version. Make sure the release version uses the following formats: x.x, x.x.x, x.x.x.x (where x is a number).";

                case LexStatusCodes.LA_E_RELEASE_PLATFORM:
                    return "Release platform not set.";

                case LexStatusCodes.LA_E_RELEASE_CHANNEL:
                    return "Release channel not set.";

                case LexStatusCodes.LA_E_USER_NOT_AUTHENTICATED:
                    return "The user is not authenticated.";

                case LexStatusCodes.LA_E_TWO_FACTOR_AUTHENTICATION_CODE_MISSING:
                    return "The two-factor authentication code for the user authentication is missing.";

                case LexStatusCodes.LA_E_TWO_FACTOR_AUTHENTICATION_CODE_INVALID:
                    return "The two-factor authentication code provided by the user is invalid.";

                case LexStatusCodes.LA_E_RATE_LIMIT:
                    return "Rate limit for API has reached, try again later.";

                case LexStatusCodes.LA_E_SERVER:
                    return "Server error.";

                case LexStatusCodes.LA_E_CLIENT:
                    return "Client error.";

                case LexStatusCodes.LA_E_LOGIN_TEMPORARILY_LOCKED:
                    return "The user account has been temporarily locked for 5 mins due to 5 failed attempts.";

                case LexStatusCodes.LA_E_AUTHENTICATION_ID_TOKEN_INVALID:
                    return "Invalid authentication ID token.";

                case LexStatusCodes.LA_E_OIDC_SSO_NOT_ENABLED:
                    return "OIDC SSO is not enabled.";

                case LexStatusCodes.LA_E_USERS_LIMIT_REACHED:
                    return "The allowed users for this account has reached its limit.";

                case LexStatusCodes.LA_E_OS_USER:
                    return "OS user has changed since activation and the license is user-locked.";

                case LexStatusCodes.LA_E_INVALID_PERMISSION_FLAG:
                    return "Invalid permission flag.";
                
                case LexStatusCodes.LA_E_FREE_PLAN_ACTIVATION_LIMIT_REACHED:
                    return "The free plan has reached it's activation limit.";

                default:
                    return "Unknown error!";

            }
        }
    }
}