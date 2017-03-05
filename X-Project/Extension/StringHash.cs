﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace X_Project.Extension
{
    public static class StringHash
    {//creates appsecret proof 
        public static string GenerateAppSecretProof(this String accessToken)
        {
            //appsecret_proof is encrypted with SHA256 string 
            using (HMACSHA256 algorithm = new HMACSHA256(Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings["Facebook_AppSecret"])))
            {
                byte[] hash = algorithm.ComputeHash(Encoding.ASCII.GetBytes(accessToken));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    builder.Append(hash[i].ToString("x2", CultureInfo.InvariantCulture));
                }
                return builder.ToString();
            }
        }
        public static string GraphAPICall(this string baseGraphApiCall, params object[] args)
        {

            if (!string.IsNullOrEmpty(baseGraphApiCall))
            {
                if (args != null &&
                    args.Count() > 0)
                {
                    string _graphApiCall = string.Empty;
                    if (baseGraphApiCall.Contains("?"))
                        _graphApiCall = string.Format(baseGraphApiCall + "&appsecret_proof={" + (args.Count() - 1) + "}", args);
                    else
                        _graphApiCall = string.Format(baseGraphApiCall + "?appsecret_proof={" + (args.Count() - 1) + "}", args);


                    return string.Format("v2.8/{0}", _graphApiCall);
                }
                else
                    throw new Exception("GraphAPICall requires at least one string parameter that contains the appsecret_proof value.");
            }
            else
                return string.Empty;
        }
    }
}