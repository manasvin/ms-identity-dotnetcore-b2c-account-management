// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;
using Microsoft.VisualBasic.FileIO;
using System;
using Microsoft.Graph;

namespace b2c_ms_graph
{
    public class UsersModel
    {
        public UserModel[] Users { get; set; }

        public static UsersModel Parse(string JSON)
        {
            return JsonConvert.DeserializeObject(JSON, typeof(UsersModel)) as UsersModel;
        }
        public static UsersModel ParseCSV(string[] CSVRows)
        {
            UsersModel CSVUsers = new UsersModel();
            
            
            if(CSVRows[0].ToString().ToLower()=="email") { //if 'Email' header found

                CSVUsers.Users = new UserModel[CSVRows.Length-1];
                
                for(int i=1;i<CSVRows.Length;i++)
                {

                    string UserEmail =  CSVRows[i].ToString();
                    if(UserEmail.Trim()==String.Empty) continue;
                    
                    CSVUsers.Users[i-1] = new UserModel();
                    CSVUsers.Users[i-1].Identities = new ObjectIdentity[] {
                        new ObjectIdentity() {
                            SignInType = "emailAddress",
                            Issuer = "",
                            IssuerAssignedId = UserEmail
                        }
                    };
                    CSVUsers.Users[i-1].Password = Helpers.PasswordHelper.GenerateNewPassword(10,2,2);  //Guid.NewGuid().ToString();
                    CSVUsers.Users[i-1].DisplayName = UserEmail;
                    CSVUsers.Users[i-1].Mail = UserEmail;
                    
                }
          
            }
            else {
                Console.WriteLine("Header Email not found!");
            }





            return CSVUsers;
        }
    }
}