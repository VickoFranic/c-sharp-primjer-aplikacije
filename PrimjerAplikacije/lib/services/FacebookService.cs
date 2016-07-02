using System;
using System.Collections.Generic;

using Facebook;
using PrimjerAplikacije.lib.models;
using System.Windows.Forms;

namespace PrimjerAplikacije.lib.services
{
    /**
     * This class is responsible for generating login URL,
     * proccessing callback URL after user logs in, and 
     * Facebook API graph calls for fetching user data from Facebook
     */
    public static class FacebookService
    {
        private const string _AppId = "217057782021640";
        private const string _Permissions = "email";
        private const string _FbGraphQuery = "me?fields=name,email,picture.width(200).height(200)";

        public static FacebookOAuthResult _oAuthResult { get; private set; }

        // FacebookClient is class from Facebook library, which is used for 
        // communication with Facebook and needs to be in project references 
        // It can be installed via Nuget Package Manager: https://www.nuget.org/packages/Facebook/

        private static FacebookClient _fbClient = new FacebookClient();

        /**
         * Generates login url with requested permissions for user
         * 
         * @return string
         */
        public static Uri generateLoginUrl()
        {
            var loginParams = new Dictionary<string, object>();

            loginParams["client_id"] = _AppId;
            loginParams["scope"] = _Permissions;
            loginParams["response_type"] = "code token";
            loginParams["redirect_uri"] = "https://www.facebook.com/connect/login_success.html";

            var loginUrl = _fbClient.GetLoginUrl(loginParams);

            return loginUrl;
        }

        /**
         * Check given URL for Facebook oAuth an store oAuth results for user, if recieved
         * 
         * @param Uri url
         * @return void
         */
        public static void getAccessTokenFromCallback(Uri url)
        {
            FacebookOAuthResult oAuthResult;

            if (_fbClient.TryParseOAuthCallbackUrl(url, out oAuthResult))
                _oAuthResult = oAuthResult;
        }

        /**
         * Check if user is authenticated (oAuth results exists)
         * 
         * @return bool
         */
        public static bool userAuthenticated()
        {
            if (_oAuthResult != null)
                return true;

            return false;
        }

        /**
         * Call Facebook API and get basic user info with stored access token
         * 
         * @throws FacebookApiException
         * @throws Exception
         * 
         * @return lib\models\User
         */
        public static User getUserDataFromFacebook()
        {
            User user = new User();

            try
            {
                _fbClient.AccessToken = _oAuthResult.AccessToken;

                // .Get is FacebookClient class method, which sends
                // Facebook Graph API request and returns response as JSON (Documentation: https://developers.facebook.com/docs/graph-api)

                dynamic response = _fbClient.Get(_FbGraphQuery);


                user.id = response.id;
                user.name = response.name;

                if (! String.IsNullOrEmpty(response.email))
                    user.email = response.email;
                else
                    user.email = "No email";

                // Using IDictionary interface for parsing picture object
                var pictureObject = (IDictionary<string, object>)(response["picture"]["data"]);

                user.picture = (string)pictureObject["url"];
            }
            catch (FacebookApiException FbException)
            {
                MessageBox.Show(FbException.Message);
                return null;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                return null;
            }

            return user;
        }
    }
}
