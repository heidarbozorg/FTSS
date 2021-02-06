﻿using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace FTSS.Logic.Security
{
    public class Common
    {
        /// <summary>
        /// Generate JWT
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="expireDate"></param>
        /// <param name="key"></param>
        /// <param name="issuer"></param>
        /// <returns></returns>
        public static string GenerateJWT(Claim[] claims, string key, string issuer, DateTime expireDate)
        {
            if (claims == null || claims.Count() == 0)
                throw new ArgumentNullException("In GenerateJWT, claims could not be null.");

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("In GenerateJWT, key could not be empty.");

            if (string.IsNullOrEmpty(issuer))
                throw new ArgumentNullException("In GenerateJWT, issuer could not be empty.");

            if (expireDate < DateTime.Now)
                throw new ArgumentException("In GenerateJWT, expireDate could not be before the current date.");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken
            (
                issuer,     //Issure  
                issuer,     //Audience
                claims,
                expires: expireDate,
                signingCredentials: credentials
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

    }
}